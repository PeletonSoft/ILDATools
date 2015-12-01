using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TestModel.Outline;
using TestTools.DialogAttribute;
using TestTools.Geometry;
using TestTools.PointAttribute;

namespace TestModel.Source
{
    [BottomLeft(-256 * 32, -256 * 32)]
    [BottomRight(256 * 128, -256 * 32)]
    [TopLeft(-256 * 32, 256 * 64)]
    [TopRight(256 * 128, 256 * 64)]
    [FileFormat("PLT")]
    [OpenDialogOptions(Filter = "HPLT Files (*.plt)|*.plt", DefaultExt = "plt")]
    public class PltFrameFactory : CustomFactory, IFactory<IFrameable>
    {
        public IFrameable Create(string fileName)
        {

            var rx = new Regex(
                @"(?<cmd>PU)(?<x>[-]?\d+)[,](?<y>[-]?\d+)[;]"
                + "|" + @"(?<cmd>PD)(?<x>[-]?\d+)[,](?<y>[-]?\d+)[;]"
                + "|" + @"(?<cmd>AA)(?<x0>[-]?\d+)[,](?<y0>[-]?\d+)[,](?<v>[-]?\d+)[;]",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var text = File.ReadAllText(fileName);
            var matches = rx.Matches(text);
            var name = Path.GetFileName(fileName);

            var start = new Point(0, 0);
            var tracePointList = new List<TracePoint>();

            foreach (var match in matches.OfType<Match>())
            {
                IEnumerable<TracePoint> elementTracePoints = null;
                switch (match.Groups["cmd"].Value)
                {
                    case "AA":
                        elementTracePoints = TakeAA(start, match);
                        break;
                    case "PU":
                        elementTracePoints = TakePU(match);
                        break;
                    case "PD":
                        elementTracePoints = TakePD(match);
                        break;
                }

                if (elementTracePoints == null)
                {
                    continue;
                }

                var elementTracePointList = elementTracePoints.ToList();
                if (elementTracePointList.Any())
                {
                    tracePointList.AddRange(elementTracePointList);
                    start = elementTracePointList.Last().Point;

                }
            }
            var tracePoints = tracePointList
                .Take(256 * 256 - 1)
                .ToArray();

            return new Frame(tracePoints, Transformation, name);
        }

        public static IEnumerable<TracePoint> TakePD(Match match)
        {
            var x = Convert.ToDouble(match.Groups["x"].Value);
            var y = Convert.ToDouble(match.Groups["y"].Value);
            var tracePoint = new TracePoint(new Point(x, y), true);
            return new[] {tracePoint};
        }

        public static IEnumerable<TracePoint> TakePU(Match match)
        {
            var x = Convert.ToDouble(match.Groups["x"].Value);
            var y = Convert.ToDouble(match.Groups["y"].Value);
            var tracePoint = new TracePoint(new Point(x, y), false);
            return new[] { tracePoint };
        }

        public static IEnumerable<TracePoint> TakeAA(Point start, Match match)
        {
            var x0 = Convert.ToInt32(match.Groups["x0"].Value);
            var y0 = Convert.ToInt32(match.Groups["y0"].Value);
            var v = Convert.ToInt32(match.Groups["v"].Value);

            var x = start.X;
            var y = start.Y;

            if (Math.Abs(v)%360 == 0)
            {
                return null;
            }

            var d = Math.Abs(v)%360/5;
            
            if (d*5 < Math.Abs(v)%360)
            {
                d++;
            }

            var tracePoints = new List<TracePoint>();
            for (var i = 0; i < d; i++)
            {
                var s = (i == d - 1 ? Math.Abs(v)%360 - 5*(d - 1) : 5)*Math.Sign(v);
                var nx = x0 + (x - x0)*Math.Cos(s*Math.PI/180) - (y - y0)*Math.Sin(s*Math.PI/180);
                var ny = y0 + (x - x0)*Math.Sin(s*Math.PI/180) + (y - y0)*Math.Cos(s*Math.PI/180);
                x = nx;
                y = ny;
                var tracePoint = new TracePoint(new Point(x, y), true);
                tracePoints.Add(tracePoint);
            }
            return tracePoints;
        }

    }
}

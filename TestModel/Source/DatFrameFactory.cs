using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TestModel.Outline;
using TestModel.Transformation;
using TestTools.DialogAttribute;
using TestTools.Geometry;
using TestTools.PointAttribute;

namespace TestModel.Source
{
    [BottomLeft(0, 0)]
    [BottomRight(30000,0)]
    [TopLeft(0, 30000)]
    [TopRight(30000, 30000)]
    [FileFormat("DAT")]
    [OpenDialogOptions(Filter = "DAT Files (*.dat)|*.dat", DefaultExt = "dat")]
    public class DatFrameFactory : CustomFactory, IFactory<IFrameable>
    {
        public IFrameable Create(string fileName)
        {

            var text = File.ReadAllText(fileName);

            var rx =
                new Regex(
                    @"(?<trace>[-]?[012])\s(?<sx>[-]?\d+)\s(?<sy>[-]?\d+)\s(?<fx>[-]?\d+)\s(?<fy>[-]?\d+)\s(?<ix>[-]?\d+)\s(?<iy>[-]?\d+)\s[01]\s\d+",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var matches = rx.Matches(text);

            var tracePoints = matches.OfType<Match>()
                .Aggregate(
                    new List<TracePoint>(),
                    (list, m) =>
                    {
                        switch (m.Groups["trace"].Value)
                        {
                            case "0":
                                list.AddRange(
                                    new[]
                                    {
                                        new TracePoint(
                                            new Point
                                            {
                                                X = Convert.ToDouble(m.Groups["sx"].Value),
                                                Y = Convert.ToDouble(m.Groups["sy"].Value)
                                            }, false),
                                        new TracePoint(
                                            new Point
                                            {
                                                X = Convert.ToDouble(m.Groups["fx"].Value),
                                                Y = Convert.ToDouble(m.Groups["fy"].Value)
                                            }, true)
                                    });
                                break;
                            case "1":
                                
                                break;
                        }
                        return list;
                    }
                )
                .Take(256*256 - 1)
                .ToArray();
            var name = Path.GetFileName(fileName);
            return new Frame(tracePoints, Transformation, name);
        }

    }
}

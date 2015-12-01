using System.Collections.Generic;
using System.Linq;
using TestModel.Transformation;
using TestTools.Geometry;

namespace TestModel.Outline
{
    public static class FrameableHelper
    {
        public static IEnumerable<Point> GetPoints(this IFrameable frameable)
        {
            return frameable.TracePoints
                .Select(tracePoint => tracePoint.Point)
                .Select(point => frameable.Transformation.ReverseTransform(point));
        }

        public static IEnumerable<Line> GetLines(this IFrameable frameable)
        {
            var lines = new List<Line>();

            var tracePoints = frameable.TracePoints;
            for (var i = 1; i < tracePoints.Length; i++)
            {
                var start = tracePoints[i - 1];
                var finish = tracePoints[i];

                if (!finish.Trace)
                {
                    continue;
                }


                var line = new Line(start.Point, finish.Point);
                lines.Add(line);

            }

            return lines
                .Select(line => frameable.Transformation.ReverseTransform(line)); ;
        }
    }
}

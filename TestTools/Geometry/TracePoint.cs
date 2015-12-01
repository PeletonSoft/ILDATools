namespace TestTools.Geometry
{
    public class TracePoint
    {
        public Point Point { get; set; }
        public bool Trace { get; set; }

        public TracePoint()
        {
        }

        public TracePoint(Point point, bool trace)
        {
            Point = point;
            Trace = trace;
        }
    }
}

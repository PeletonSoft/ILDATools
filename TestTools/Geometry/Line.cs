namespace TestTools.Geometry
{
    public class Line
    {
        public Point Start { get; set; }
        public Point Finish { get; set; }

        public Line()
        {
        }

        public Line(Point start, Point finish)
        {
            Start = start;
            Finish = finish;
        }
    }
}

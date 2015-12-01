using TestTools.Geometry;

namespace TestViewModel.Geomentry
{
    public class LineViewModel : IDrawViewModel
    {
        public Line Line { get; private set; }

        public LineViewModel(Line line)
        {
            Line = line;
        }


        public double X
        {
            get { return Line.Start.X; }
        }

        public double Y
        {
            get { return Line.Start.Y; }
        }

        public PointViewModel Offset
        {
            get
            {
                var offset = new Point()
                {
                    X = Line.Finish.X - Line.Start.X,
                    Y = Line.Finish.Y - Line.Start.Y
                };
                return new PointViewModel(offset);
            }
        }
    }
}

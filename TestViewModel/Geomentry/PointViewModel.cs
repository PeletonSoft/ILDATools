using TestTools.Geometry;

namespace TestViewModel.Geomentry
{
    public class PointViewModel : IDrawViewModel
    {
        private readonly Point _point;
        public PointViewModel(Point point)
        {
            _point = point;
        }

        public double X
        {
            get { return _point.X; }
        }

        public double Y
        {
            get { return _point.Y; }
        }
    }
}

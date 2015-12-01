using TestModel.Transformation;
using TestTools.Geometry;

namespace TestModel.Outline
{
    public class NullFramable : IFrameable
    {
        public TracePoint[] TracePoints { get; private set; }
        public ITransformation Transformation { get; private set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public NullFramable()
        {
            TracePoints = new TracePoint[0];
            Transformation = new TranslateTransformation(new Point(0,0));
            Name = "Null";
        }
    }
}

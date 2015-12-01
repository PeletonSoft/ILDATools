using System.Linq;
using System.Xml.Linq;
using TestModel.Outline;
using TestTools.DialogAttribute;
using TestTools.Geometry;
using TestTools.PointAttribute;

namespace TestModel.Source
{
    [BottomLeft(0, 0)]
    [BottomRight(1, 0)]
    [TopLeft(0, 1)]
    [TopRight(1, 1)]
    [FileFormat("XML")]
    [OpenDialogOptions(Filter = "XML Files (*.xml)|*.xml", DefaultExt = "xml")]
    public class XmlFrameFactory : CustomFactory, IFactory<IFrameable>
    {
        public IFrameable Create(string fileName)
        {
            var xml = XDocument.Load(fileName).Root;
            var name = (string) xml.Element("Name");
            var tracePoints = xml.Element("Points").Elements("Point")
                .Select(x =>
                    new TracePoint
                    {
                        Point = new Point
                        {
                            X = (double)x.Attribute("X"),
                            Y = (double)x.Attribute("Y")
                        },
                        Trace = (bool)x.Attribute("Trace")
                    })
                .Take(10000)
                .ToArray();
            return new Frame(tracePoints,Transformation,name);
        }

    }
}

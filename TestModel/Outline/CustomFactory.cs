using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TestModel.Transformation;
using TestTools.DialogAttribute;
using TestTools.Geometry;
using TestTools.PointAttribute;

namespace TestModel.Outline
{
    public class CustomFactory
    {
        private ITransformation _transformation;

        public ITransformation Transformation
        {
            get
            {
                if (_transformation == null)
                {
                    Point[] points;
                    try
                    {
                        points = GetQuadrangleFromXml();
                    }
                    catch (Exception)
                    {
                        points = GetQuadrangleByAttribute();
                    }

                    var factoryTransformation = new StandardTransformationFactory();
                    _transformation = factoryTransformation.CreateFromFourPoints(points);
                }
                return _transformation;
            }
        }

        private Point[] GetQuadrangleByAttribute()
        {
            var attrs = Attribute.GetCustomAttributes(GetType());
            var pointAttributeTypes = new[]
            {
                typeof (BottomLeftAttribute),
                typeof (BottomRightAttribute),
                typeof (TopLeftAttribute),
                typeof (TopRightAttribute),
            };

            var points = pointAttributeTypes
                .Select(type => attrs.Single(attr => attr.GetType() == type))
                .Select(attr => (PointAttribute) attr)
                .Select(attr => attr.Point)
                .ToArray();
            return points;
        }

        private Point[] GetQuadrangleFromXml()
        {

            var attrs = Attribute.GetCustomAttributes(GetType());
            var fileFormat = attrs
                .OfType<FileFormatAttribute>()
                .FirstOrDefault()
                .FileFormat;

            var path = Directory.GetCurrentDirectory();
            var config = XDocument.Load(Path.Combine(path, "quadrangle.xml"));
            var elements = config.Root.Elements()
                .Single(el => (string) el.Attribute("FileFormat") == fileFormat)
                .Elements();

            var pointNames = new[]
            {
                "BottomLeft", "BottomRight",
                "TopLeft", "TopRight"
            };

            var points = pointNames
                .Select(name => elements.Single(el => el.Name == name))
                .Select(el =>
                    new Point()
                    {
                        X = (double) el.Attribute("X"),
                        Y = (double) el.Attribute("Y")
                    })
                .ToArray();
            return points;
        }
    }
}

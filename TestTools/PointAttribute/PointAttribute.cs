using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTools.Geometry;

namespace TestTools.PointAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PointAttribute : Attribute
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point Point
        {
            get
            {
                return new Point(X,Y);
            }
        }

        public PointAttribute()
        {
        }

        public PointAttribute(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}

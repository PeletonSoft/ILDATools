using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTools.PointAttribute
{
    public class BottomLeftAttribute : PointAttribute
    {
        public BottomLeftAttribute(double x, double y)
            : base(x, y)
        {
        }

        public BottomLeftAttribute()
        {
        }
    }
}

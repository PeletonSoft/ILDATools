using System.Collections.Generic;
using System.Linq;
using TestModel.Outline;
using TestModel.Transformation;
using TestTools.Geometry;

namespace TestModel.Source.Ild
{
    public class IldFrame : IFrameable
    {
        public IldHeader Header { get; private set; }
        public IEnumerable<IldRecord> Records { get; private set; }
        public IldFrame(IldHeader header, IEnumerable<IldRecord> records, ITransformation transformation)
        {
            Header = header;
            Records = records;

            Transformation = transformation; 

            TracePoints = Records
                .Select(rec => rec.ToTracePoint())
                .ToArray();
            Name = Header.FrameName + " #" + Header.FrameNumber;
        }

        public TracePoint[] TracePoints { get; private set; }
        public ITransformation Transformation { get; private set; }
        public string Name { get; private set; }
    }
}

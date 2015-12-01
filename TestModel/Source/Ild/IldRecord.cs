using TestModel.Tools;
using TestTools.Geometry;

namespace TestModel.Source.Ild
{
    public class IldRecord
    {
        private readonly BytesConverter _converter;
        public IldRecordRaw Raw { get; set; }
        public int X { 
            get { return _converter.GetInt16(0); } 
            set { _converter.SetInt16(0, value); } 
        }
        public int Y {
            get { return _converter.GetInt16(2); }
            set { _converter.SetInt16(2, value); }
        }
        public int Z {
            get { return _converter.GetInt16(4); }
            set { _converter.SetInt16(4, value); }
        }
        public int Status {
            get { return _converter.GetUInt16(6); }
            set { _converter.SetUInt16(6, value); }
        }

        public bool IsMove
        {
            get { return _converter.GetBitInUInt16(Status, 64*256); }
            set { Status = _converter.SetBitInUInt16(Status, 64*256, value ? 64*256 : 0); }
        }

        public bool IsLast
        {
            get { return _converter.GetBitInUInt16(Status, 128*256); }
            set { Status = _converter.SetBitInUInt16(Status, 128*256, value ? 128*256 : 0); }
        }

        public Point ToPoint()
        {
            var w = -((double)16)/Z;
            return new Point(w*X, w*Y);
        }

        public TracePoint ToTracePoint()
        {
            return new TracePoint(ToPoint(), !IsMove);
        }
        public IldRecord(IldRecordRaw raw)
        {
            Raw = raw;
            _converter = new BytesConverter(raw.Data);
        }
    }
}

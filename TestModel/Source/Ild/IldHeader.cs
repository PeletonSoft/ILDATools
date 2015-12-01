using TestModel.Tools;

namespace TestModel.Source.Ild
{
    public class IldHeader
    {
        private readonly BytesConverter _converter;
        public IldHeaderRaw Raw { get; set; }
        public string FileFormat
        {
            get { return _converter.GetString(0, 4); }
            set { _converter.SetString(0, 4,value); }
        }

        public byte FormatCode
        {
            get { return _converter.GetByte(7); }
            set { _converter.SetByte(7, value); }
        }

        public string FrameName {
            get { return _converter.GetString(8, 8); }
            set { _converter.SetString(8, 8, value); }
        }

        public string CompanyName {
            get { return _converter.GetString(16, 8); }
            set { _converter.SetString(16, 8, value); }
        }

        public int TotalPoints {
            get { return _converter.GetUInt16(24); }
            set { _converter.SetUInt16(24,value); }
        }

        public int FrameNumber {
            get { return _converter.GetUInt16(26); }
            set { _converter.SetUInt16(26, value); }
        }

        public int TotalFrames {
            get { return _converter.GetUInt16(28); }
            set { _converter.SetUInt16(28, value); }
        }

        public byte ScannerHead {
            get { return _converter.GetByte(30); }
            set { _converter.SetByte(30, value); }
        }

        public byte Future {
            get { return _converter.GetByte(31); }
            set { _converter.SetByte(31,value); }
        }

        public IldHeader(IldHeaderRaw raw)
        {
            Raw = raw;
            _converter= new BytesConverter(raw.Data);
        }

    }
}

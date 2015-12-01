using System.Runtime.InteropServices;

namespace TestModel.Source.Ild
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IldRecordRaw
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Data;

        public void Init()
        {
            Data = new byte[8];
        }
    }
}

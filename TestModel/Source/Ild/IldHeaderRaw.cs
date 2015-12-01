using System.Runtime.InteropServices;

namespace TestModel.Source.Ild
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IldHeaderRaw
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Data;

        public void Init()
        {
            Data = new byte[32];
        }
    }
}

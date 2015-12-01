using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace TestModel.Tools
{
    public class BytesConverter
    {
        public byte[] Raw { get; private set; }

        public BytesConverter(byte[] raw)
        {
            Raw = raw;
        }

        public char[] GetChars(int start, int length)
        {
            return Raw
                .Where((ch, i) => i >= start && i < start + length)
                .Select(ch => (char)ch)
                .ToArray();
        }

        public char[] Trancate(char[] chars)
        {
            var zeros = chars
                .Select((ch, i) => new {Char = ch, Index = i})
                .Where(chi => chi.Char == (char) 0)
                .ToList();
            if (zeros.Any())
            {
                var min = zeros
                    .Select(chi => chi.Index)
                    .Min();
                return chars
                    .Where((ch, i) => i < min)
                    .ToArray();
            }
            else
            {
                return chars;
            }
        }

        public string GetString(int start, int length)
        {
            return new String(Trancate(GetChars(start, length)));
        }

        public int GetUInt16(int start)
        {
            return 256 * Raw[start] + Raw[start + 1];
        }

        public int GetInt16(int start)
        {
            var ii = 256 * Raw[start] + Raw[start + 1];
            return ii >= 256 * 128 ? ii - 256 * 256 : ii;
        }

        public byte GetByte(int start)
        {
            return Raw[start];
        }

        public static T ByteToType<T>(BinaryReader reader)
        {
            var size = Marshal.SizeOf(typeof(T));
            var bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return structure;
        }

        public void SetByte(int start, byte value)
        {
            Raw[start] = value;
        }

        public void SetUInt16(int start, int value)
        {
            var corrected = value;
            if (corrected > 256*256 - 1)
            {
                corrected = 256*256 - 1;
            }
            if (corrected < 0)
            {
                corrected = 0;
            }

            Raw[start] = (byte) (corrected/256);
            Raw[start + 1] = (byte) (corrected%256);
        }

        public void SetInt16(int start, int value)
        {
            var corrected = value;
            if (corrected > 256*128 - 1)
            {
                corrected = 256*128 - 1;
            }
            if (corrected < -256*128)
            {
                corrected = -256*128;
            }
            
            if (corrected < 0)
            {
                corrected = corrected + 256*256;
            }

            Raw[start] = (byte) (corrected/256);
            Raw[start + 1] = (byte) (corrected%256);
            if (Raw[start] == 8*16)
            {
                Console.WriteLine("here");
            }
        }

        public void SetString(int start, int length, string value)
        {
            for (var i = 0; i < length; i++)
            {
                Raw[start + i] = 0;
            }

            if (value == null)
            {
                return;
            }

            var chars = value.ToCharArray();
            for (var i = 0; i < length && i < chars.Length; i++)
            {
                Raw[start + i] = (byte) chars[i];
            }
        }

        public bool GetBitInUInt16(int data, int mask)
        {
            return (data & mask) == mask;
        }

        public int SetBitInUInt16(int data, int mask, int value)
        {
            return data - (data & mask) + (value & mask);
        }
    }
}

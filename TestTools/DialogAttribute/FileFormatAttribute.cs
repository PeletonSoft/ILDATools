using System;

namespace TestTools.DialogAttribute
{
    public class FileFormatAttribute : Attribute
    {
        public string FileFormat { get; set; }

        public FileFormatAttribute()
        {
        }

        public FileFormatAttribute(string fileFormat)
        {
            FileFormat = fileFormat;
        }
    }
}

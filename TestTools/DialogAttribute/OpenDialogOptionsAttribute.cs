using System;

namespace TestTools.DialogAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OpenDialogOptionsAttribute : Attribute
    {
        public string Filter { get; set; }
        public string DefaultExt { get; set; }
    }
}

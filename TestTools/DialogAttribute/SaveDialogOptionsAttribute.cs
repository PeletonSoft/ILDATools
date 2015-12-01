using System;

namespace TestTools.DialogAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SaveDialogOptionsAttribute : Attribute
    {
        public string Filter { get; set; }
        public string DefaultExt { get; set; }
    }
}

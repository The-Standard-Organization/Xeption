using System;

namespace Xeption
{
    public class Xeption : Exception
    {
        public Xeption() : base()
        { }

        public Xeption(string message) : base(message)
        { }

        public Xeption(string message, Exception innerException) 
            : base(message, innerException)
        { }

        public void UpsertDataList(string key, object value)
        {
            throw new NotImplementedException();
        }
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Apache License 2.0 (Apache)
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace System
{

    using System;
    [Serializable()]
    public class InvalidOperationException : SystemException
    {
        public InvalidOperationException()
            : base()
        {
        }

        public InvalidOperationException(String message)
            : base(message)
        {
        }

        public InvalidOperationException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}



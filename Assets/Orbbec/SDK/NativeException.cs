using System;

namespace Orbbec
{
    public class NativeException : Exception
    {
        public NativeException(Error error) : base(error.GetMessage())
        {

        }
    }
}
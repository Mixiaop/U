using System;

namespace U.Dapper
{
    public class UDapperException : UException
    {
        public UDapperException(string message)
            : base(message)
        {

        }


        public UDapperException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}

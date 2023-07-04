using System;

namespace Razor.Api.Services.V1.CustomException
{
    public class UserTokenNotFoundException : Exception
    {
        public UserTokenNotFoundException()
        {
        }

        public UserTokenNotFoundException(string message) : base(message)
        {
        }

        public UserTokenNotFoundException(string message, Exception innerException) : base(message, innerException)
        { 
        }
    }
}

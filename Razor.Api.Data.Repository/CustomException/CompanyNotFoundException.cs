using System;

namespace Razor.Api.DataAccess.CustomException
{
    public class CompanyNotFoundException : Exception
    {
        public CompanyNotFoundException()
        {
        }

        public CompanyNotFoundException(string message) : base(message)
        {

        }
  
        public CompanyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}

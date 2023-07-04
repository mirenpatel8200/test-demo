using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.CustomException
{
    //TODO: m2-rev1: yet no need for a new assembly as they are thrown on the service layer. Just move to the Servies
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

using System;
using System.Collections.Generic;


namespace Razor.Api.Web.Filters.Model
{
    public class ApiExceptionDetails
    {
        public string Message { get; set; }
        public string Details { get; set; }

        public ApiExceptionDetails() {}

        public ApiExceptionDetails(string message, Exception exception)
        {
            Message = message;
            if (exception != null)
            {
                var sb = new List<string>();
                while (exception != null)
                {
                    sb.Add(exception.Message);
                    exception = exception.InnerException;
                }

                Details = string.Join(" in ", sb);
            }
        }
    }
}

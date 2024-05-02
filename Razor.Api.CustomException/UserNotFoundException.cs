using System;

namespace Razor.Api.CustomException
{
    //TODO: m2-rev1: yet no need for a new assembly as they are thrown on the service layer. Just move to the Servies
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        { 
        }
    }

    //TODO: m2-rev1: also I think it is good to use a ModelValidationException: ValidationException having EntityType property to indicate which part of the composite model is invalid
}

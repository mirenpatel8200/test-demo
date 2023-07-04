using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Razor.Api.Context;
using Razor.Api.Context.Models;
using Razor.Api.Data.Repository;
using Razor.Api.Data.Repository.User;

namespace Razor.Api.Validation.ValidationRule.ExecContext
{
    public class ExecContextValidationRule : AbstractValidator<ContextData>, IExecContextValidationRule
    {

        //IUserRepository Repository { get; }
        public ExecContextValidationRule()
        {
            //this.Repository = repository;

            RuleFor(x => x.CompanyId).NotEmpty();

            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x).Must(IsUserExist)
                .WithMessage("User is not exist");

            //To DO: Implement Other validation rules

        }


        private bool IsUserExist(ContextData Data)
        {
            //TO DO : Need to discuss with Olag to Correct approach to validate model agaist DB
            //return Repository.isValidUser();
            throw new NotImplementedException();
        }
    }
}

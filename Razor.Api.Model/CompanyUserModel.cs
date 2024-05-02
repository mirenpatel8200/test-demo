using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.Model.V1
{
    public class CompanyUserModel : CompanyModel
    {
        public long UserId { get; set; }

        public string Token { get; set; }
    }
}

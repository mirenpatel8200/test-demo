using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.Data.Models
{
    public class Substitute
    {
        public int SubItemMasterId { get; set; }
        public int Is1Way { get; set; }
        public int ItemMasterGroupId { get; set; }
    }
}

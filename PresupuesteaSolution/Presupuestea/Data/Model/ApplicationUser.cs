using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presupuestea.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string PostalCode { get; set; }

    }
}

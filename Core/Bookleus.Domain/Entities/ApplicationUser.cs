using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookleus.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = default!;

        [PersonalData]
        public string LastName { get; set; } = default!;
    }
}

using Bookleus.Application.Common.Interfaces.Services;
using Bookleus.Domain.Entities;
using Bookleus.Identity.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bookleus.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            return _userManager.GetUserId(principal);
        }

        public bool ValidateUserExists(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if(user is null || _userManager.IsLockedOutAsync(user).Result)
            {
                return false;
            }

            return true;
        }
    }
}

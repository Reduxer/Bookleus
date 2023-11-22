using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Bookleus.Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Gets the id of the authenticated user.
        /// </summary>
        /// <returns>User id in string</returns>
        string? GetUserId(ClaimsPrincipal principal);

        /// <summary>
        /// Checks if user exists and active by given id
        /// </summary>
        /// <returns>bool</returns>
        bool ValidateUserExists(string id);
    }
}

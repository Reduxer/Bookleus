using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookleus.Application.Common.Interfaces.Services;
using System.Security.Claims;

namespace Bookleus.Web.Test.Fixtures
{
    public class UserServiceFixture
    {
        public readonly IUserService UserService;

        public UserServiceFixture()
        {
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(s => s.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(Guid.NewGuid().ToString());

            UserService = userServiceMock.Object;
        }
    }
}

using System.Security.Claims;
using Webshop.Model;

namespace Webshop.Api.Tests
{
    internal class MockAuthService : IAuthService
    {
        private readonly List<ApplicationUser> _users = [];
        public MockAuthService()
        {
            _users =
            [
                new ApplicationUser()
                {
                    Email = "email@example.com",
                    IsActive = true,
                    Name = "Test",
                    Password = "password",
                    Token = "Not.A.RealToken"
                }
            ];
        }

        public ApplicationUser FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserFromClaims(IEnumerable<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Register(ApplicationUser user)
        {
            if (_users.Where(x => x.Email == user.Email).Any()) { return new(); }

            _users.Add(user);
            return user;
        }

        public bool TryLogin(string email, string password, out ApplicationUser user)
        {
            var result = _users.Find(x => x.Email == email && x.Password == password);
            if (result != null)
            {
                user = result;
                return true;
            }
            else 
            {
                user = new();
                return false; 
            }
        }
    }
}

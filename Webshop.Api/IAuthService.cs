using System.Security.Claims;
using Webshop.Model;

namespace Webshop.Api;

public interface IAuthService
{
    public bool TryLogin(string email, string password, out ApplicationUser user);
    public ApplicationUser Register(ApplicationUser user);

    public ApplicationUser GetUserFromClaims(IEnumerable<Claim> claims);
    public ApplicationUser FindByEmail(string email);
}

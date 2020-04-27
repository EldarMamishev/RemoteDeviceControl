using Microsoft.AspNetCore.Identity;

namespace WebApi.Helpers.Token
{
    public interface ITokenProvider
    {
        string MakeToken(IdentityUser<long> user, TokenConfiguration configuration);
    }
}
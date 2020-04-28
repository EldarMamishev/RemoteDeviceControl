using Microsoft.AspNetCore.Identity;

namespace WebApi.Helpers.Token
{
    public interface ITokenProvider
    {
        string MakeToken(IdentityUser<int> user, TokenConfiguration configuration);
    }
}
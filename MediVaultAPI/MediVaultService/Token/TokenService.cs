using System.Security.Claims;
using MediVault.Common.Authentication;
using MediVault.Utils.Token;
using MediVaultDal.Account;

namespace MediVaultService.Token;

public class TokenService(UserSessionDal userSessionDal, UserDal userDal) : ITokenService
{
    public async Task<ClaimsPrincipal?> GenerateTokenAsync(string accessToken)
    {
        var session = await userSessionDal.GetSessionByAccessToken(accessToken);
        if (session == null) return null;

        var user = await userDal.GetUserByIdAsync(session.UserId);

        if (user == null) return null;

        var principal = TokenUtils.CreatePrincipal(new TokenUser { UserId = user.Id, UserName = user.UserName });

        return principal;
    }
}
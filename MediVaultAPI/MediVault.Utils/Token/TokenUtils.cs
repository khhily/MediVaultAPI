using System.Security.Claims;

namespace MediVault.Utils.Token;

public static class TokenUtils
{
    public static ClaimsPrincipal CreatePrincipal(TokenUser user)
    {
        var principal = new ClaimsPrincipal();
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, $"{user.UserId}"),
            new Claim(ClaimTypes.Name, user.UserName)
        };
        var identity = new ClaimsIdentity(claims);
        principal.AddIdentity(identity);

        return principal;
    }

    public static TokenUser? GetTokenUser(ClaimsPrincipal principal)
    {
        var id = principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(id, out var userId)) return null;

        var userName = principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        return new TokenUser()
        {
            UserId = userId,
            UserName = userName ?? ""
        };
    }
}
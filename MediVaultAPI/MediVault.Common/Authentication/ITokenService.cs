using System.Security.Claims;

namespace MediVault.Common.Authentication;

public interface ITokenService
{
    Task<ClaimsPrincipal?> GenerateTokenAsync(string accessToken);
}
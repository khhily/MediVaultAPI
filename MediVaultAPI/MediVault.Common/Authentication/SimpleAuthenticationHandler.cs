using System.Text.Encodings.Web;
using MediVault.Common.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MediVault.Common.Authentication;

public class SimpleAuthenticationHandler(
    IOptionsMonitor<SimpleAuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock,
    ITokenService tokenService)
    : AuthenticationHandler<SimpleAuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    public const string SchemeName = "SimpleAuthentication";
    // protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SimpleAuthorizationRequirement requirement)
    // {
    //     if (!contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorization))
    //     {
    //         context.Fail(new AuthorizationFailureReason(this, "No authorization header found"));
    //         return;
    //     }
    //     
    //     var principal = await tokenService.GenerateTokenAsync(authorization.ToString());
    //
    //     if (principal == null)
    //     {
    //         context.Fail(new AuthorizationFailureReason(this, "Session expired"));
    //         return;
    //     }
    //
    //     // context. = new ClaimsPrincipal(principal);
    //     context.Succeed(requirement);
    // }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authorization))
        {
            return AuthenticateResult.Fail(new AppException(AppErrors.NoAuthorizationHeaderFound));
        }

        var principal = await tokenService.GenerateTokenAsync(authorization.ToString());

        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (principal == null)
        {
            return AuthenticateResult.Fail(new AppException(AppErrors.SessionExpired));
        }
        
        return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
    }
}
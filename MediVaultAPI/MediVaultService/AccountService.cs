using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using MediVault.Common.Exceptions;
using MediVault.Utils.Crypto;
using MediVaultDal.Account;
using MediVaultData.Dto.Account;
using MediVaultData.Entity;
using MediVaultData.Enum;
using Microsoft.Extensions.Options;

namespace MediVaultService;

public class AccountService(UserDal userDal, UserSessionDal userSessionDal, IOptions<JwtOptions> options)
{
    // 从配置中获取JwtOptions
    private JwtOptions Options => options.Value;

    // 用户登录
    public async Task<UserSessionLite> LoginAsync(UserForLogin userForLoginUser)
    {
        // 检查用户名是否为空
        if (string.IsNullOrWhiteSpace(userForLoginUser.UserName))
            throw new AppException(AppErrors.EnterUserName);

        // 检查密码是否为空
        if (string.IsNullOrWhiteSpace(userForLoginUser.Password))
            throw new AppException(AppErrors.EnterPassword);

        // 根据用户名获取用户信息
        var user = await userDal.GetUserByNameAsync(userForLoginUser.UserName);

        // 如果用户不存在，抛出异常
        if (user == null)
            throw new AppException(AppErrors.UserNameOrPasswordInvalid);

        // 验证密码是否正确
        if (!CryptoHelper.VerifyPassword(userForLoginUser.Password, user.Password))
            throw new AppException(AppErrors.UserNameOrPasswordInvalid);

        // 生成新的会话
        return await GenerateNewSession(user.Id);
    }

    // 用户注册
    public async Task<UserSessionLite> Register(UserForRegistry register)
    {
        // 检查用户名是否为空
        if (string.IsNullOrWhiteSpace(register.UserName))
            throw new AppException(AppErrors.EnterUserName);

        // 检查密码是否为空
        if (string.IsNullOrWhiteSpace(register.Password))
            throw new AppException(AppErrors.EnterPassword);

        // 根据用户名获取用户信息
        var existUser = await userDal.GetUserByNameAsync(register.UserName.Trim());
        // 如果用户已存在，抛出异常
        if (existUser != null)
            throw new AppException(AppErrors.UserHasAlreadyBeenRegistered);

        // 创建新用户
        var user = new User()
        {
            UserName = register.UserName,
            Password = CryptoHelper.HashPassword(register.Password.Trim()),
        };

        // 插入新用户
        await userDal.InsertAsync(user);

        // 生成新的会话
        return await GenerateNewSession(user.Id);
    }

    // 生成新的会话
    private async Task<UserSessionLite> GenerateNewSession(int userId)
    {
        // 根据用户ID获取用户信息
        var user = await userDal.GetUserByIdAsync(userId);
        // 生成新的刷新令牌
        var refreshToken = GenerateRefreshToken();
        // 生成新的访问令牌
        var accessToken = GenerateAccessToken(userId);

        // 创建新的用户会话
        var userSession = new UserSession
        {
            Id = Guid.NewGuid().ToString("N"),
            UserId = userId,
            RefreshToken = refreshToken,
            AccessToken = accessToken,
            TokenType = UserSessionTokenType.OAuth2,
            CreateTime = DateTime.UtcNow,
            ExpireTime = DateTime.UtcNow.AddMinutes(30),
        };

        // 插入新的用户会话
        await userSessionDal.InsertAsync(userSession);

        // 返回新的用户会话信息
        return new UserSessionLite
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = new LoginUser
            {
                UserName = user!.UserName,
                UserId = user.Id
            },
        };
    }

    // 生成访问令牌
    private string GenerateAccessToken(int userId)
    {
        // 创建JwtSecurityTokenHandler对象
        var tokenHandler = new JwtSecurityTokenHandler();
        // 获取密钥
        var key = Encoding.ASCII.GetBytes(Options.SecretKey);
        // 创建SecurityTokenDescriptor对象
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // 设置主题
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, $"{userId}")
            ]),
            // 设置令牌有效期
            Expires = DateTime.UtcNow.AddMinutes(30), // 令牌有效期为30分钟
            // 设置签发者
            Issuer = Options.Issuer,
            // 设置接收者
            Audience = Options.Audience,
            // 设置签名凭证
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        // 创建JwtSecurityToken对象
        var token = tokenHandler.CreateToken(tokenDescriptor);
        // 返回令牌字符串
        return tokenHandler.WriteToken(token);
    }

    // 生成刷新令牌
    private string GenerateRefreshToken()
    {
        // 返回Guid字符串
        return Guid.NewGuid().ToString();
    }

    // 刷新访问令牌
    public async Task<UserSessionLite> RefreshAccessToken(string refreshToken)
    {
        // 根据刷新令牌获取用户会话
        var oldSession = await userSessionDal.GetSessionByRefreshToken(refreshToken);

        // 如果用户会话不存在，抛出异常
        if (oldSession == null) throw new AppException(AppErrors.RefreshTokenExpired){ Status = (int)HttpStatusCode.Unauthorized };

        // 获取用户ID
        var userId = oldSession.UserId;

        // 删除旧的会话
        await userSessionDal.RemoveAsync(oldSession);

        // 生成新的会话
        return await GenerateNewSession(userId);
    }
}
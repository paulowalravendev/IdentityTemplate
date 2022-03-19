using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityTemplate.Api.Extensions;
using IdentityTemplate.Api.Features;
using IdentityTemplate.Api.Features.UserRegister;
using IdentityTemplate.Api.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames=Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace IdentityTemplate.Api.Services;

public class AuthService : IAuthService
{
    private readonly AppSettings _appSettings;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthService(IOptions<AppSettings> appSettings, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _appSettings = appSettings.Value;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public async Task<UserTokenViewModel?> Register(UserRegisterInputModel inputModel)
    {
        var user = CreateIdentityUser(inputModel);
        var result = await _userManager.CreateAsync(user, inputModel.Password);
        if (result.Succeeded == false)
            throw new CreateUserWithIdentityErrorsException(result.Errors.Select(e => e.Description));
        await _signInManager.SignInAsync(user, false);
        return await GenerateTokenJwt(user);
    }

    private IdentityUser CreateIdentityUser(UserRegisterInputModel inputModel)
    {
        return new IdentityUser
        {
            UserName = inputModel.Email,
            Email = inputModel.Email,
            EmailConfirmed = true
        };
    }

    private async Task<UserTokenViewModel> GenerateTokenJwt(IdentityUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(),
                             ClaimValueTypes.Integer64));

        foreach (var userRole in userRoles)
            claims.Add(new Claim("role", userRole));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.ValidIn,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpireInHours),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token)!;

        return new UserTokenViewModel
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpireInHours).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new UserClaim {Type = c.Type, Value = c.Value})
            }
        };
    }
}
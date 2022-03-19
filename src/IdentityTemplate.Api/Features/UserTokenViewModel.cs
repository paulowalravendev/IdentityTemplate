namespace IdentityTemplate.Api.Features;

public class UserTokenViewModel
{
    public string AccessToken { get; set; } = null!;
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; } = null!;
}

public class UserToken
{
    public string Id { get; set; } = null!;
    public long UserId { get; set; }
    public string Email { get; set; } = null!;
    public IEnumerable<UserClaim> Claims { get; set; } = null!;
}

public class UserClaim
{
    public string Value { get; set; } = null!;
    public string Type { get; set; } = null!;
}
namespace IdentityTemplate.Api.Entities;

public abstract class Entity
{
    public long? Id;
}

public abstract class AuditableEntity : Entity
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}

public class User : AuditableEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
}
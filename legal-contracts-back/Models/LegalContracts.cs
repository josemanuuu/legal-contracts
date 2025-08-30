using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class LegalContract
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Author { get; set; } = null!;
    public string EntityName { get; set; } = null!;
    public string? Description { get; set; }

    private DateTime _createdAt;
    public DateTime CreatedAt
    {
        get => _createdAt;
        set => _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc); // Specify as UTC
    }

    private DateTime? _updatedAt;
    public DateTime? UpdatedAt
    {
        get => _updatedAt;
        set => _updatedAt = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null; // Specify as UTC
    }
}

// DTO for creating a new LegalContract, ignoring the Id field
public class LegalContractCreateDto
{
    public string Author { get; set; } = null!;
    public string EntityName { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
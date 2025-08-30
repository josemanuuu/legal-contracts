using Microsoft.EntityFrameworkCore;

public class LegalContractsDbContext(DbContextOptions<LegalContractsDbContext> options) : DbContext(options)
{
    public DbSet<LegalContract> Contracts { get; set; } = null!;
}
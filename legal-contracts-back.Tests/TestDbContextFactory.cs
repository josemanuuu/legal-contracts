using Microsoft.EntityFrameworkCore;

// Factory class to create instances of LegalContractsDbContext for testing.
// It uses an in-memory database with a unique name each time to ensure isolation between tests.
// The database is pre-seeded with a sample LegalContract, so the returned context is ready for use in unit tests.
public static class TestDbContextFactory
{
    public static LegalContractsDbContext Create()
    {
        var options = new DbContextOptionsBuilder<LegalContractsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new LegalContractsDbContext(options);

        // Seed a sample contract for testing
        context.Contracts.Add(new LegalContract 
        { 
            Id = 1, 
            Author = "Jose", 
            EntityName = "Entity 1", 
            Description = "Desc 1", 
            CreatedAt = DateTime.UtcNow.AddDays(-10) 
        });
        context.SaveChanges();

        return context;
    }
}

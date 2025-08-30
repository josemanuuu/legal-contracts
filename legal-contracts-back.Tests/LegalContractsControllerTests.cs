using Microsoft.AspNetCore.Mvc;

// Unit tests for the LegalContractsController.
// These tests use an in-memory database context created by TestDbContextFactory.
// Each test verifies a different controller action, including retrieval, creation, update, and deletion of contracts.
// The in-memory database ensures isolation between tests and allows predictable, repeatable results.
public class LegalContractsControllerTests
{
    [Fact]
    public async Task GetAllContracts_ReturnsAll()
    {
        using var context = TestDbContextFactory.Create();
        var controller = new LegalContractsController(context);

        var result = await controller.GetAllContracts(null, null);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var contracts = Assert.IsAssignableFrom<IEnumerable<LegalContract>>(okResult.Value);
        Assert.NotEmpty(contracts);
    }

    [Fact]
    public async Task GetContractById_ReturnsContract()
    {
        using var context = TestDbContextFactory.Create();
        var controller = new LegalContractsController(context);

        var result = await controller.GetContractById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var contract = Assert.IsType<LegalContract>(okResult.Value);
        Assert.Equal("Jose", contract.Author);
        Assert.Equal("Entity 1", contract.EntityName);
    }

    [Fact]
    public async Task CreateContract_AddsContract()
    {
        using var context = TestDbContextFactory.Create();
        var controller = new LegalContractsController(context);

        var newContract = new LegalContractCreateDto
        {
            Author = "Nuevo",
            EntityName = "Entidad Nueva",
            Description = "Nueva Desc",
            CreatedAt = DateTime.UtcNow
        };

        var result = await controller.CreateContract(newContract);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var contract = Assert.IsType<LegalContract>(createdResult.Value);
        Assert.Equal("Nuevo", contract.Author);
        Assert.True(contract.Id > 0);
    }

    [Fact]
    public async Task UpdateContract_UpdatesContract()
    {
        using var context = TestDbContextFactory.Create();
        var controller = new LegalContractsController(context);

        var contract = context.Contracts.First();
        contract.Author = "Modificado";

        var result = await controller.UpdateContract(contract.Id, contract);

        Assert.IsType<NoContentResult>(result);
        Assert.Equal("Modificado", context.Contracts.Find(contract.Id)!.Author);
    }

    [Fact]
    public async Task DeleteContract_RemovesContract()
    {
        using var context = TestDbContextFactory.Create();
        var controller = new LegalContractsController(context);

        var result = await controller.DeleteContract(1);

        Assert.IsType<NoContentResult>(result);
        Assert.Null(context.Contracts.Find(1));
    }

    [Fact]
    public async Task GetLatestContracts_ReturnsContracts()
    {
        using var context = TestDbContextFactory.Create();
        var controller = new LegalContractsController(context);

        var result = await controller.GetLatestContracts(null);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var contracts = Assert.IsAssignableFrom<IEnumerable<LegalContract>>(okResult.Value);
        Assert.NotNull(contracts);
    }
}

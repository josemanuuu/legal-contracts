using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class LegalContractsController(LegalContractsDbContext context) : ControllerBase
{
    private readonly LegalContractsDbContext _context = context;

    /// <summary>
    /// Get all legal contract results.
    /// </summary>
    /// <remarks>
    /// Data can be filtered using <c>before</c> and <c>after</c> for creation dates.
    /// </remarks>
    /// <param name="before">Return contracts created before the given date (UTC).</param>
    /// <param name="after">Return contracts created after the given date (UTC).</param>
    /// <returns>Legal contract list.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LegalContract>>> GetAllContracts(
        [FromQuery] DateTime? before,
        [FromQuery] DateTime? after)
    {
        IQueryable<LegalContract> query = _context.Contracts;

        // Query is created dynamically
        if (before.HasValue)
        {
            DateTime beforeUtc = DateTime.SpecifyKind(before.Value, DateTimeKind.Utc);
            query = query.Where(c => c.CreatedAt < beforeUtc);
        }

        if (after.HasValue)
        {
            DateTime afterUtc = DateTime.SpecifyKind(after.Value, DateTimeKind.Utc);
            query = query.Where(c => c.CreatedAt > afterUtc);
        }

        // Sorting data
        query = query
            .OrderByDescending(c => c.UpdatedAt.HasValue) // First those with UpdatedAt
            .ThenByDescending(c => c.UpdatedAt); // Then by recent updates

        List<LegalContract>? contracts = await query.ToListAsync();
        return Ok(contracts);
    }

    /// <summary>
    /// Get a legal contract by its ID.
    /// </summary>
    /// <param name="id">ID of the given contract</param>
    /// <returns>Returns the contract specify.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LegalContract>> GetContractById(int id)
    {
        LegalContract? contract = await _context.Contracts.FindAsync(id);
        if (contract == null)
            return NotFound();
        return Ok(contract);
    }

    /// <summary>
    /// Create a new legal contract.
    /// </summary>
    /// <param name="contract">Details of the contract to be created.</param>
    /// <returns>The created contract.</returns>
    [HttpPost]
    public async Task<ActionResult<LegalContract>> CreateContract(LegalContractCreateDto contract)
    {

        LegalContract newContract = new()
        {
            Author = contract.Author,
            EntityName = contract.EntityName,
            Description = contract.Description,
            CreatedAt = DateTime.SpecifyKind(contract.CreatedAt, DateTimeKind.Utc),
            UpdatedAt = contract.UpdatedAt.HasValue ? DateTime.SpecifyKind(contract.UpdatedAt.Value, DateTimeKind.Utc) : null
        };

        _context.Contracts.Add(newContract);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetContractById), new { id = newContract.Id }, newContract);
    }

    /// <summary>
    /// Update an existing legal contract.
    /// </summary>
    /// <param name="id">ID of the contract to modify.</param>
    /// <param name="contract">New values.</param>
    /// <returns>Returns the updated contract.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContract(int id, LegalContract contract)
    {
        if (id != contract.Id)
            return BadRequest();

        contract.UpdatedAt = DateTime.UtcNow;
        _context.Entry(contract).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Contracts.Any(e => e.Id == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a legal contract by its ID.
    /// </summary>
    /// <param name="id">ID of the contract to delete.</param>
    /// <returns>Nothing.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContract(int id)
    {
        LegalContract? contract = await _context.Contracts.FindAsync(id);
        if (contract == null)
            return NotFound();

        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Get the latest edited contracts, sorted by UpdatedAt descending.
    /// </summary>
    /// <param name="maxItems">Number of contracts to return.</param>
    /// <returns>A list of contracts.</returns>
    [HttpGet("latest")]
    public async Task<ActionResult<IEnumerable<LegalContract>>> GetLatestContracts([FromQuery] int? maxItems)
    {
        int itemsToFetch = maxItems.HasValue && maxItems.Value > 0 ? maxItems.Value : 10;  // 10 items by default

        List<LegalContract>? contracts = await _context.Contracts
            .Where(c => c.UpdatedAt != null)
            .OrderByDescending(c => c.UpdatedAt)
            .ThenByDescending(c => c.CreatedAt)
            .Take(itemsToFetch)
            .ToListAsync();

        return Ok(contracts);
    }
}

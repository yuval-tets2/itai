using Dotnet.APIs;
using Dotnet.APIs.Common;
using Dotnet.APIs.Dtos;
using Dotnet.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class GuestsControllerBase : ControllerBase
{
    protected readonly IGuestsService _service;

    public GuestsControllerBase(IGuestsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Guest
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Guest>> CreateGuest(GuestCreateInput input)
    {
        var guest = await _service.CreateGuest(input);

        return CreatedAtAction(nameof(Guest), new { id = guest.Id }, guest);
    }

    /// <summary>
    /// Delete one Guest
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteGuest([FromRoute()] GuestWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteGuest(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Guests
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Guest>>> Guests([FromQuery()] GuestFindManyArgs filter)
    {
        return Ok(await _service.Guests(filter));
    }

    /// <summary>
    /// Meta data about Guest records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> GuestsMeta([FromQuery()] GuestFindManyArgs filter)
    {
        return Ok(await _service.GuestsMeta(filter));
    }

    /// <summary>
    /// Get one Guest
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Guest>> Guest([FromRoute()] GuestWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Guest(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Guest
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateGuest(
        [FromRoute()] GuestWhereUniqueInput uniqueId,
        [FromQuery()] GuestUpdateInput guestUpdateDto
    )
    {
        try
        {
            await _service.UpdateGuest(uniqueId, guestUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}

using Dotnet.APIs;
using Dotnet.APIs.Common;
using Dotnet.APIs.Dtos;
using Dotnet.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class HotelsControllerBase : ControllerBase
{
    protected readonly IHotelsService _service;

    public HotelsControllerBase(IHotelsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Hotel
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Hotel>> CreateHotel(HotelCreateInput input)
    {
        var hotel = await _service.CreateHotel(input);

        return CreatedAtAction(nameof(Hotel), new { id = hotel.Id }, hotel);
    }

    /// <summary>
    /// Delete one Hotel
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteHotel([FromRoute()] HotelWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteHotel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Hotels
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Hotel>>> Hotels([FromQuery()] HotelFindManyArgs filter)
    {
        return Ok(await _service.Hotels(filter));
    }

    /// <summary>
    /// Meta data about Hotel records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> HotelsMeta([FromQuery()] HotelFindManyArgs filter)
    {
        return Ok(await _service.HotelsMeta(filter));
    }

    /// <summary>
    /// Get one Hotel
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Hotel>> Hotel([FromRoute()] HotelWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Hotel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Hotel
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateHotel(
        [FromRoute()] HotelWhereUniqueInput uniqueId,
        [FromQuery()] HotelUpdateInput hotelUpdateDto
    )
    {
        try
        {
            await _service.UpdateHotel(uniqueId, hotelUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}

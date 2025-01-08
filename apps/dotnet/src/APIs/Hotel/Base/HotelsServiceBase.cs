using Dotnet.APIs;
using Dotnet.APIs.Common;
using Dotnet.APIs.Dtos;
using Dotnet.APIs.Errors;
using Dotnet.APIs.Extensions;
using Dotnet.Infrastructure;
using Dotnet.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.APIs;

public abstract class HotelsServiceBase : IHotelsService
{
    protected readonly DotnetDbContext _context;

    public HotelsServiceBase(DotnetDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Hotel
    /// </summary>
    public async Task<Hotel> CreateHotel(HotelCreateInput createDto)
    {
        var hotel = new HotelDbModel
        {
            Address = createDto.Address,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            NumberOfRooms = createDto.NumberOfRooms,
            Rating = createDto.Rating,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            hotel.Id = createDto.Id;
        }

        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<HotelDbModel>(hotel.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Hotel
    /// </summary>
    public async Task DeleteHotel(HotelWhereUniqueInput uniqueId)
    {
        var hotel = await _context.Hotels.FindAsync(uniqueId.Id);
        if (hotel == null)
        {
            throw new NotFoundException();
        }

        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Hotels
    /// </summary>
    public async Task<List<Hotel>> Hotels(HotelFindManyArgs findManyArgs)
    {
        var hotels = await _context
            .Hotels.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return hotels.ConvertAll(hotel => hotel.ToDto());
    }

    /// <summary>
    /// Meta data about Hotel records
    /// </summary>
    public async Task<MetadataDto> HotelsMeta(HotelFindManyArgs findManyArgs)
    {
        var count = await _context.Hotels.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Hotel
    /// </summary>
    public async Task<Hotel> Hotel(HotelWhereUniqueInput uniqueId)
    {
        var hotels = await this.Hotels(
            new HotelFindManyArgs { Where = new HotelWhereInput { Id = uniqueId.Id } }
        );
        var hotel = hotels.FirstOrDefault();
        if (hotel == null)
        {
            throw new NotFoundException();
        }

        return hotel;
    }

    /// <summary>
    /// Update one Hotel
    /// </summary>
    public async Task UpdateHotel(HotelWhereUniqueInput uniqueId, HotelUpdateInput updateDto)
    {
        var hotel = updateDto.ToModel(uniqueId);

        _context.Entry(hotel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Hotels.Any(e => e.Id == hotel.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}

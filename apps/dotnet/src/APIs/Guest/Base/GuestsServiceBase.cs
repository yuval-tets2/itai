using Dotnet.APIs;
using Dotnet.APIs.Common;
using Dotnet.APIs.Dtos;
using Dotnet.APIs.Errors;
using Dotnet.APIs.Extensions;
using Dotnet.Infrastructure;
using Dotnet.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.APIs;

public abstract class GuestsServiceBase : IGuestsService
{
    protected readonly DotnetDbContext _context;

    public GuestsServiceBase(DotnetDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Guest
    /// </summary>
    public async Task<Guest> CreateGuest(GuestCreateInput createDto)
    {
        var guest = new GuestDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            PhoneNumber = createDto.PhoneNumber,
            RoomNumber = createDto.RoomNumber,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            guest.Id = createDto.Id;
        }

        _context.Guests.Add(guest);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<GuestDbModel>(guest.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Guest
    /// </summary>
    public async Task DeleteGuest(GuestWhereUniqueInput uniqueId)
    {
        var guest = await _context.Guests.FindAsync(uniqueId.Id);
        if (guest == null)
        {
            throw new NotFoundException();
        }

        _context.Guests.Remove(guest);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Guests
    /// </summary>
    public async Task<List<Guest>> Guests(GuestFindManyArgs findManyArgs)
    {
        var guests = await _context
            .Guests.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return guests.ConvertAll(guest => guest.ToDto());
    }

    /// <summary>
    /// Meta data about Guest records
    /// </summary>
    public async Task<MetadataDto> GuestsMeta(GuestFindManyArgs findManyArgs)
    {
        var count = await _context.Guests.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Guest
    /// </summary>
    public async Task<Guest> Guest(GuestWhereUniqueInput uniqueId)
    {
        var guests = await this.Guests(
            new GuestFindManyArgs { Where = new GuestWhereInput { Id = uniqueId.Id } }
        );
        var guest = guests.FirstOrDefault();
        if (guest == null)
        {
            throw new NotFoundException();
        }

        return guest;
    }

    /// <summary>
    /// Update one Guest
    /// </summary>
    public async Task UpdateGuest(GuestWhereUniqueInput uniqueId, GuestUpdateInput updateDto)
    {
        var guest = updateDto.ToModel(uniqueId);

        _context.Entry(guest).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Guests.Any(e => e.Id == guest.Id))
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

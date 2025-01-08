using Dotnet.APIs.Dtos;
using Dotnet.Infrastructure.Models;

namespace Dotnet.APIs.Extensions;

public static class GuestsExtensions
{
    public static Guest ToDto(this GuestDbModel model)
    {
        return new Guest
        {
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            FirstName = model.FirstName,
            Id = model.Id,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            RoomNumber = model.RoomNumber,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static GuestDbModel ToModel(
        this GuestUpdateInput updateDto,
        GuestWhereUniqueInput uniqueId
    )
    {
        var guest = new GuestDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            PhoneNumber = updateDto.PhoneNumber,
            RoomNumber = updateDto.RoomNumber
        };

        if (updateDto.CreatedAt != null)
        {
            guest.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            guest.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return guest;
    }
}

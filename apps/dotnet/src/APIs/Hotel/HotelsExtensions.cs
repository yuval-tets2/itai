using Dotnet.APIs.Dtos;
using Dotnet.Infrastructure.Models;

namespace Dotnet.APIs.Extensions;

public static class HotelsExtensions
{
    public static Hotel ToDto(this HotelDbModel model)
    {
        return new Hotel
        {
            Address = model.Address,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            NumberOfRooms = model.NumberOfRooms,
            Rating = model.Rating,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static HotelDbModel ToModel(
        this HotelUpdateInput updateDto,
        HotelWhereUniqueInput uniqueId
    )
    {
        var hotel = new HotelDbModel
        {
            Id = uniqueId.Id,
            Address = updateDto.Address,
            Name = updateDto.Name,
            NumberOfRooms = updateDto.NumberOfRooms,
            Rating = updateDto.Rating
        };

        if (updateDto.CreatedAt != null)
        {
            hotel.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            hotel.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return hotel;
    }
}

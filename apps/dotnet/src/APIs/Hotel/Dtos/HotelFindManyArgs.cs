using Dotnet.APIs.Common;
using Dotnet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class HotelFindManyArgs : FindManyInput<Hotel, HotelWhereInput> { }

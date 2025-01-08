using Dotnet.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Infrastructure;

public class DotnetDbContext : DbContext
{
    public DotnetDbContext(DbContextOptions<DotnetDbContext> options)
        : base(options) { }

    public DbSet<HotelDbModel> Hotels { get; set; }

    public DbSet<GuestDbModel> Guests { get; set; }
}

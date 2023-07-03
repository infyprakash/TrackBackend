using System;
using Microsoft.EntityFrameworkCore;

namespace TrackBackend.Models;

public class TrackContext:DbContext
{
    public TrackContext(DbContextOptions<TrackContext> options)
        :base(options)
    {
    }
    public DbSet<OrderSheet> orderSheets { get; set; } = null!;
    public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
    public DbSet<OrderManufacturer> orderManufacturers { get; set; } = null!;
    public DbSet<OrderPriority> orderPriorities { get; set; } = null!;
    public DbSet<OrderStatus> orderStatuses { get; set; } = null!;
    public DbSet<OrderStage> orderStages { get; set; } = null!;

}


using System;
using System.ComponentModel.DataAnnotations;

namespace TrackBackend.Models;

public class OrderManufacturer
{
    public int Id { get; set; }
    public int? OrderSheetId { get; set; }
    public int? ManufacturerId { get; set; }



    public OrderSheet? OrderSheet { get; set; }
    public Manufacturer? Manufacturer { get; set; }

    public Boolean? IsActive { get; set; } = true;
    [DataType(DataType.DateTime)]
    public DateTime? CreatedAt { get; set; }

    public OrderManufacturer()
    {
        CreatedAt = DateTime.UtcNow;
    }

}

public class OrderManufacturerCreate
{
    public int? OrderSheetId { get; set; }
    public int? ManufacturerId { get; set; }
}


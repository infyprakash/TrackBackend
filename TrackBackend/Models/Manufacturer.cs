using System;
using System.ComponentModel.DataAnnotations;

namespace TrackBackend.Models;


public class Manufacturer
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? CompanyName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    public Boolean? IsActive { get; set; } = true;
    [DataType(DataType.DateTime)]
    public DateTime? CreatedAt { get; set; }

    public Manufacturer()
    {
        CreatedAt = DateTime.UtcNow;
    }

}

public class ManufacturerCreate
{
    public string? UserName { get; set; }
    public string? CompanyName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

}
public class ManufacturerUpdate
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? CompanyName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

}


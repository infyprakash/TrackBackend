using System;
using System.ComponentModel.DataAnnotations;

namespace TrackBackend.Models;

public class OrderPriority
{
    public int Id { get; set; }
    public int? ManufacturerId { get; set; }
    public String? PriorityName { get; set; }
    public Double? DaysRequiredForProcessing { get; set; }
    public Double? LinesPerDay { get; set; }
    public Double? DaysRequiredForTrimming { get; set; }
    public Double? DaysRequiredForWashing { get; set; }
    public Double? DaysRequiredForFinalTriming { get; set; }
    public Double? DaysRequiredForFinishing { get; set; }
    public Double? DaysRequiredForPackaging { get; set; }
    public Double? DaysRequiredForShipping { get; set; }

    public Manufacturer? Manufacturer { get; set; }

    public Boolean? IsActive { get; set; } = true;
    [DataType(DataType.DateTime)]
    public DateTime? CreatedAt { get; set; }

    public OrderPriority()
    {
        CreatedAt = DateTime.UtcNow;
    }
}

public class OrderPriorityCreate
{
    public int? ManufacturerId { get; set; }
    public String? PriorityName { get; set; }
    public Double? DaysRequiredForProcessing { get; set; }
    public Double? LinesPerDay { get; set; }
    public Double? DaysRequiredForTrimming { get; set; }
    public Double? DaysRequiredForWashing { get; set; }
    public Double? DaysRequiredForFinalTriming { get; set; }
    public Double? DaysRequiredForFinishing { get; set; }
    public Double? DaysRequiredForPackaging { get; set; }
    public Double? DaysRequiredForShipping { get; set; }

}

public class OrderPriorityUpdate
{
    public int? ManufacturerId { get; set; }
    public String? PriorityName { get; set; }
    public Double? DaysRequiredForProcessing { get; set; }
    public Double? LinesPerDay { get; set; }
    public Double? DaysRequiredForTrimming { get; set; }
    public Double? DaysRequiredForWashing { get; set; }
    public Double? DaysRequiredForFinalTriming { get; set; }
    public Double? DaysRequiredForFinishing { get; set; }
    public Double? DaysRequiredForPackaging { get; set; }
    public Double? DaysRequiredForShipping { get; set; }

}


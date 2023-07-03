using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackBackend.Models;


public enum Stage { Processing, Weaving, Trimming, Washing, Finalised };
public enum LengthUnit { meter, centimeter, feet }

public class OrderStage
{
    public int Id { get; set; }
    public int? OrderSheetId { get; set; }
    public Stage stage { get; set; }
    public double? LengthCompleted { get; set; } = 0;
    public LengthUnit? LengthUnit { get; set; }
    public String? FilePath { get; set; }
    public Boolean? IsCompleted { get; set; } = false;

    public OrderSheet? OrderSheet { get; set; }

    public Boolean? IsActive { get; set; } = true;
    [DataType(DataType.DateTime)]
    public DateTime? CreatedAt { get; set; }

    public OrderStage()
    {
        CreatedAt = DateTime.UtcNow;
    }

}

public class OrderStageCreate
{
    public Stage stage { get; set; }
    public int? OrderSheetId { get; set; }
    public double? LengthCompleted { get; set; } = 0;
    public LengthUnit? LengthUnit { get; set; }
    public Boolean? IsCompleted { get; set; } = false;

    [NotMapped]
    public IFormFile? Image { get; set; }

}

public class OrderStageUpdate
{
    public int Id { get; set; }
    public Stage stage { get; set; }
    public int? OrderSheetId { get; set; }
    public double? LengthCompleted { get; set; } = 0;
    public LengthUnit? LengthUnit { get; set; }
    public Boolean? IsCompleted { get; set; } = false;

    [NotMapped]
    public IFormFile? Image { get; set; }

}

using System;
using System.ComponentModel.DataAnnotations;

namespace TrackBackend.Models;

public class OrderStatus
{
    public int Id { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int? OrderSheetId { get; set; }
    public int? OrderPriorityId { get; set; }

    public Boolean? IsActive { get; set; } = true;
    [DataType(DataType.DateTime)]
    public DateTime? CreatedAt { get; set; }

    public OrderSheet? OrderSheet { get; set; }
    public OrderPriority? OrderPriority { get; set; }

    public OrderStatus()
    {
        CreatedAt = DateTime.UtcNow;
    }

}

public class OrderStatusCreate
{
    public int? OrderSheetId { get; set; }
    public int? OrderPriorityId { get; set; }

}

public class OrderStatusUpdate
{
    public int? OrderSheetId { get; set; }
    public int? OrderPriorityId { get; set; }

}


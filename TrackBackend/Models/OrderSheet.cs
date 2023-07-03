using System;
using System.ComponentModel.DataAnnotations;

namespace TrackBackend.Models;

public class OrderSheet {
    public int Id { get; set; }
    public string? OrderId { get; set; }
    public string? DesignName { get; set; }
    public string? ImageUrl { get; set; }
    public string? PdfUrl { get; set; }
    public string? CtfUrl { get; set; }
    public string? PhysicalWidth { get; set; }
    public string? PhysicalHeight { get; set; }
    public string? Unit { get; set; }
    public string? PixelWidth { get; set; }
    public string? PixelHeight { get; set; }

    public Boolean? IsActive { get; set; } = true;
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    private static int orderCounter = 1;

    public OrderSheet()
    {
        OrderId = GenerateOrderId();
    }

    private static string GenerateOrderId()
    {
        string orderNumber = orderCounter.ToString();
        string orderId = orderNumber.PadLeft(5, '0');
        orderCounter++;
        return orderId;
    }

}

public class OrderSheetCreate {
    public string? OrderId { get; set; }
    public string? DesignName { get; set; }
    public string? ImageUrl { get; set; }
    public string? PdfUrl { get; set; }
    public string? CtfUrl { get; set; }
    public string? PhysicalWidth { get; set; }
    public string? PhysicalHeight { get; set; }
    public string? Unit { get; set; }
    public string? PixelWidth { get; set; }
    public string? PixelHeight { get; set; }
}

public class OrderSheetUpdate
{
    public int Id { get; set; }
    public string? OrderId { get; set; }
    public string? DesignName { get; set; }
    public string? ImageUrl { get; set; }
    public string? PdfUrl { get; set; }
    public string? CtfUrl { get; set; }
    public string? PhysicalWidth { get; set; }
    public string? PhysicalHeight { get; set; }
    public string? Unit { get; set; }
    public string? PixelWidth { get; set; }
    public string? PixelHeight { get; set; }
}



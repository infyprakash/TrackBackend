using System;
using AutoMapper;
using TrackBackend.Models;
namespace TrackBackend.Controllers;


public class MappingTrack : Profile
{
    public MappingTrack()
    {
        CreateMap<ManufacturerCreate,Manufacturer>();
        CreateMap<ManufacturerUpdate, Manufacturer>();
        CreateMap<OrderManufacturerCreate, OrderManufacturer>();
        CreateMap<OrderPriorityCreate, OrderPriority>();
        CreateMap<OrderPriorityUpdate, OrderPriority>();
        CreateMap<OrderStatusCreate, OrderStatus>();
        CreateMap<OrderStatusUpdate, OrderStatus>();
        CreateMap<OrderStageCreate, OrderStage>();
    }
}


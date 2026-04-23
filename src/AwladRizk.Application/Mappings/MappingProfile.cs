using AutoMapper;
using AwladRizk.Domain.Entities;
using AwladRizk.Application.DTOs;

namespace AwladRizk.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product → ProductDto
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.CategorySlug, o => o.MapFrom(s => s.Category.Slug))
            .ForMember(d => d.CategoryNameAr, o => o.MapFrom(s => s.Category.NameAr))
            .ForMember(d => d.CategoryNameEn, o => o.MapFrom(s => s.Category.NameEn));

        // Category → CategoryDto
        CreateMap<Category, CategoryDto>()
            .ForMember(d => d.ProductCount, o => o.MapFrom(s => s.Products.Count));

        // Order → OrderDto
        CreateMap<Order, OrderDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.ItemCount, o => o.MapFrom(s => s.Items.Count));

        // Order → OrderDetailDto
        CreateMap<Order, OrderDetailDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.ItemCount, o => o.MapFrom(s => s.Items.Count))
            .ForMember(d => d.DeliveryAddress, o => o.MapFrom(s =>
                $"{s.DeliveryStreet}, {s.DeliveryArea}, {s.DeliveryCity}, {s.DeliveryGovernorate}"));

        // OrderItem → OrderItemDto
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductNameSnapshot));

        // Payment → PaymentDto
        CreateMap<Payment, PaymentDto>()
            .ForMember(d => d.Method, o => o.MapFrom(s => s.Method.ToString()))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        // Offer → OfferDto
        CreateMap<Offer, OfferDto>();
    }
}

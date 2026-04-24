using MediatR;
using AwladRizk.Application.DTOs;

namespace AwladRizk.Application.Features.Admin.Commands;

public record CreateProductCommand(
    string NameAr,
    string NameEn,
    decimal Price,
    decimal? OldPrice,
    string ImageUrl,
    bool IsOnSale,
    int StockQty,
    int CategoryId) : IRequest<ProductDto>;

public record UpdateProductCommand(
    int Id,
    string NameAr,
    string NameEn,
    decimal Price,
    decimal? OldPrice,
    string ImageUrl,
    bool IsOnSale,
    int StockQty,
    int CategoryId) : IRequest<ProductDto?>;

public record DeleteProductCommand(int Id) : IRequest<bool>;

public record CreateOfferCommand(
    string TitleAr,
    string TitleEn,
    string DescriptionAr,
    string DescriptionEn,
    int DiscountPercent,
    string BadgeText,
    string Icon,
    DateTime ValidUntil,
    bool IsActive) : IRequest<OfferDto>;

public record UpdateOfferCommand(
    int Id,
    string TitleAr,
    string TitleEn,
    string DescriptionAr,
    string DescriptionEn,
    int DiscountPercent,
    string BadgeText,
    string Icon,
    DateTime ValidUntil,
    bool IsActive) : IRequest<OfferDto?>;

public record DeleteOfferCommand(int Id) : IRequest<bool>;

public record CreateTickerMessageCommand(string TextAr, string TextEn, int SortOrder, bool IsActive) : IRequest<HomeTickerMessageDto>;
public record UpdateTickerMessageCommand(int Id, string TextAr, string TextEn, int SortOrder, bool IsActive) : IRequest<HomeTickerMessageDto?>;
public record DeleteTickerMessageCommand(int Id) : IRequest<bool>;

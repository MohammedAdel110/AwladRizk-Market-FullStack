using MediatR;
using AwladRizk.Application.DTOs;

namespace AwladRizk.Application.Features.Products.Queries;

// ── Get All Products (with filtering, search, pagination) ──
public record GetAllProductsQuery(
    string? CategorySlug = null,
    string? Search = null,
    bool? OnSaleOnly = null,
    int Page = 1,
    int PageSize = 12
) : IRequest<PagedResult<ProductDto>>;

// ── Get Single Product ──
public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

// ── Get Featured (on sale) Products ──
public record GetFeaturedProductsQuery(int Count = 6) : IRequest<List<ProductDto>>;

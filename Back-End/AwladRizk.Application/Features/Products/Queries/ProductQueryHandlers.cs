using AutoMapper;
using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Application.Features.Products.Queries;

public sealed class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
{
    public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize <= 0 ? 12 : request.PageSize;

        var (items, totalCount) = await productRepository.GetPagedAsync(
            request.CategorySlug,
            request.Search,
            request.OnSaleOnly,
            page,
            pageSize,
            cancellationToken);

        return new PagedResult<ProductDto>
        {
            Items = mapper.Map<List<ProductDto>>(items),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
}

public sealed class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetWithCategoryByIdAsync(request.Id, cancellationToken);
        return product is null ? null : mapper.Map<ProductDto>(product);
    }
}

public sealed class GetFeaturedProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetFeaturedProductsQuery, List<ProductDto>>
{
    public async Task<List<ProductDto>> Handle(GetFeaturedProductsQuery request, CancellationToken cancellationToken)
    {
        var count = request.Count <= 0 ? 6 : request.Count;
        var products = await productRepository.GetFeaturedAsync(count, cancellationToken);
        return mapper.Map<List<ProductDto>>(products);
    }
}

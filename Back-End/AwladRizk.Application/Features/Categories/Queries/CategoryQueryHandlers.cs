using AutoMapper;
using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Application.Features.Categories.Queries;

public sealed class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllWithProductsAsync(cancellationToken);
        return mapper.Map<List<CategoryDto>>(categories);
    }
}

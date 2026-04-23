using MediatR;
using AwladRizk.Application.DTOs;

namespace AwladRizk.Application.Features.Categories.Queries;

public record GetAllCategoriesQuery : IRequest<List<CategoryDto>>;

using MediatR;
using AwladRizk.Application.DTOs;

namespace AwladRizk.Application.Features.Offers.Queries;

public record GetActiveOffersQuery : IRequest<List<OfferDto>>;

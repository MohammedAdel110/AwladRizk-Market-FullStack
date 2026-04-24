using AutoMapper;
using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Application.Features.Offers.Queries;

public sealed class GetActiveOffersQueryHandler(IOfferRepository offerRepository, IMapper mapper)
    : IRequestHandler<GetActiveOffersQuery, List<OfferDto>>
{
    public async Task<List<OfferDto>> Handle(GetActiveOffersQuery request, CancellationToken cancellationToken)
    {
        var offers = await offerRepository.GetActiveAsync(cancellationToken);
        return mapper.Map<List<OfferDto>>(offers);
    }
}

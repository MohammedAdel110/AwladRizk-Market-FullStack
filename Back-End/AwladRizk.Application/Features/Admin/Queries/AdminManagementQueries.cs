using MediatR;
using AwladRizk.Application.DTOs;

namespace AwladRizk.Application.Features.Admin.Queries;

public record GetAllTickerMessagesQuery : IRequest<List<HomeTickerMessageDto>>;

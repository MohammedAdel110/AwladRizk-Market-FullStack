using MediatR;

namespace AwladRizk.Application.Features.Newsletter.Commands;

public record SubscribeNewsletterCommand(string Email) : IRequest<bool>;

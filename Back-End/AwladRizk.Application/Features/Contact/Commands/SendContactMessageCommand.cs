using MediatR;

namespace AwladRizk.Application.Features.Contact.Commands;

public record SendContactMessageCommand(
    string Name,
    string Email,
    string? Phone,
    string Message
) : IRequest<bool>;

using MediatR;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Application.Features.Contact.Commands;

public sealed class SendContactMessageHandler(
    IRepository<ContactMessage> contactRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SendContactMessageCommand, bool>
{
    public async Task<bool> Handle(SendContactMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new ContactMessage
        {
            Name = request.Name.Trim(),
            Email = request.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim(),
            Message = request.Message.Trim(),
            IsRead = false
        };

        await contactRepository.AddAsync(message, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

using MediatR;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Application.Features.Newsletter.Commands;

public sealed class SubscribeNewsletterHandler(
    INewsletterSubscriberRepository newsletterRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SubscribeNewsletterCommand, bool>
{
    public async Task<bool> Handle(SubscribeNewsletterCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var existing = await newsletterRepository.GetByEmailAsync(email, cancellationToken);

        if (existing is not null)
        {
            if (!existing.IsActive)
            {
                existing.IsActive = true;
                existing.SubscribedAt = DateTime.UtcNow;
                newsletterRepository.Update(existing);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        var subscriber = new NewsletterSubscriber
        {
            Email = email,
            IsActive = true,
            SubscribedAt = DateTime.UtcNow
        };

        await newsletterRepository.AddAsync(subscriber, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

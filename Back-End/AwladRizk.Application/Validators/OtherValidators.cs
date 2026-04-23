using FluentValidation;
using AwladRizk.Application.Features.Newsletter.Commands;
using AwladRizk.Application.Features.Contact.Commands;
using AwladRizk.Application.Features.Auth.Commands;

namespace AwladRizk.Application.Validators;

public class SubscribeNewsletterValidator : AbstractValidator<SubscribeNewsletterCommand>
{
    public SubscribeNewsletterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}

public class SendContactMessageValidator : AbstractValidator<SendContactMessageCommand>
{
    public SendContactMessageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required.")
            .MaximumLength(2000);
    }
}

public class AdminLoginValidator : AbstractValidator<AdminLoginCommand>
{
    public AdminLoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6);
    }
}

using FluentValidation;
using AwladRizk.Application.Features.Orders.Commands;

namespace AwladRizk.Application.Validators;

public class PlaceOrderValidator : AbstractValidator<PlaceOrderCommand>
{
    public PlaceOrderValidator()
    {
        RuleFor(x => x.SessionId)
            .NotEmpty().WithMessage("Session ID is required.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Delivery address is required.")
            .MaximumLength(200);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100);

        RuleFor(x => x.Governorate)
            .NotEmpty().WithMessage("Governorate is required.")
            .MaximumLength(100);

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^01[0-9]{9}$").WithMessage("Invalid Egyptian phone number format.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid payment method.");

        // Wallet phone required when payment method is Wallet
        RuleFor(x => x.WalletPhone)
            .NotEmpty()
            .When(x => x.PaymentMethod == Domain.Enums.PaymentMethod.Wallet)
            .WithMessage("Wallet phone number is required.");
    }
}

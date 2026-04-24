using FluentValidation;
using AwladRizk.Application.Features.Payments.Commands;

namespace AwladRizk.Application.Validators;

public class ProcessPaymentValidator : AbstractValidator<ProcessPaymentCommand>
{
    public ProcessPaymentValidator()
    {
        RuleFor(x => x.OrderNumber)
            .NotEmpty().WithMessage("Order number is required.");

        RuleFor(x => x.Method)
            .IsInEnum().WithMessage("Invalid payment method.");
        
        RuleFor(x => x.Method)
            .Must(m => m != Domain.Enums.PaymentMethod.Cod)
            .WithMessage("COD does not require payment processing.");

        RuleFor(x => x.WalletPhone)
            .NotEmpty()
            .When(x => x.Method == Domain.Enums.PaymentMethod.Wallet)
            .WithMessage("Wallet phone is required for wallet payments.");
    }
}

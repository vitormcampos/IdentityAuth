using FluentValidation;
using IdentityAuth.ViewModels.Usuarios;

namespace IdentityAuth.Validators;

public class EditarUsuarioValidator : AbstractValidator<EditarUsuarioViewModel>
{
    public EditarUsuarioValidator()
    {
        RuleFor(u => u.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("O Campo {PropertyName} é obrigátorio")
            .MaximumLength(50).WithMessage("O Campo {PropertyName} não pode ser maior que 50 caracteres");

        RuleFor(u => u.Telefone)
            .NotEmpty().WithMessage("O Campo {PropertyName} é obrigátorio")
            .MaximumLength(11).WithMessage("O Campo {PropertyName} não pode ser maior que 11 números")
            .Must(x => x.ToList().All(c => char.IsDigit(c))).WithMessage("{PropertyName} deve conter somente números");
    }
}
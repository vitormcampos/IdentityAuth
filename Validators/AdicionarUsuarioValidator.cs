using FluentValidation;
using IdentityAuth.ViewModels.Usuarios;

namespace IdentityAuth.Validators;

public class AdicionarUsuarioValidator : AbstractValidator<AdicionarUsuarioViewModel>
{
    public AdicionarUsuarioValidator()
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

        RuleFor(u => u.Senha)
            .NotEmpty().WithMessage("O Campo {PropertyName} é obrigátorio")
            .MinimumLength(5).WithMessage("{PropertyName} deve conter pelo menos 5 números");

        RuleFor(u => u.ConfirmacaoSenha)
            .NotEmpty().WithMessage("O Campo {PropertyName} é obrigátorio")
            .Equal(u => u.ConfirmacaoSenha).WithMessage("Senhas não conferem");
    }
}
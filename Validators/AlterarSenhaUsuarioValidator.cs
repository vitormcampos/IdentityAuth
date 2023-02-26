using FluentValidation;
using IdentityAuth.ViewModels.Login;

namespace IdentityAuth.Validators;

public class AlterarSenhaUsuarioValidator : AbstractValidator<AlterarSenhaViewModel>
{
    public AlterarSenhaUsuarioValidator()
    {
        RuleFor(u => u.SenhaAtual)
            .NotEmpty().WithMessage("O Campo {PropertyName} é obrigátorio");
        
        RuleFor(u => u.NovaSenha)
            .NotEmpty().WithMessage("O Campo {PropertyName} é obrigátorio")
            .MinimumLength(6).WithMessage("{PropertyName} deve conter pelo menos 6 números");

        RuleFor(u => u.NovaSenha)
            .NotEmpty().WithMessage("O Campo {PropertyName} é obrigátorio")
            .Equal(u => u.NovaSenha).WithMessage("Senhas não conferem");
    }
}
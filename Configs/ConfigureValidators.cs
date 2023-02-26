using FluentValidation;
using IdentityAuth.Validators;
using IdentityAuth.ViewModels.Login;
using IdentityAuth.ViewModels.Usuarios;

namespace IdentityAuth.Configs;

public static class ConfigureValidators
{
    public static void ApplyValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AdicionarUsuarioViewModel>, AdicionarUsuarioValidator>();
        services.AddScoped<IValidator<EditarUsuarioViewModel>, EditarUsuarioValidator>();
        services.AddScoped<IValidator<AlterarSenhaViewModel>, AlterarSenhaUsuarioValidator>();
    }
}
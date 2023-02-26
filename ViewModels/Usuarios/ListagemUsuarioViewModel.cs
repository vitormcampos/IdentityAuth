using System.ComponentModel;

namespace IdentityAuth.ViewModels.Usuarios;

public class ListagemUsuarioViewModel
{
    public string? Id { get; set; }

    [DisplayName("Usuário")] public string UserName { get; set; } = string.Empty;

    [DisplayName("E-mail")] public string Email { get; set; } = string.Empty;

    [DisplayName("Telefone")] public string Telefone { get; set; } = string.Empty;
}
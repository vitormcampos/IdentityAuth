using System.ComponentModel;

namespace IdentityAuth.ViewModels.Login;

public class AlterarSenhaViewModel
{
    [DisplayName("Senha Atual")]
    public string SenhaAtual { get; set; } = string.Empty;
    
    [DisplayName("Nova Senha")]
    public string NovaSenha { get; set; } = string.Empty;
    
    [DisplayName("Confirme a nova senha")]
    public string ConfirmarNovaSenha { get; set; } = string.Empty;
    
}
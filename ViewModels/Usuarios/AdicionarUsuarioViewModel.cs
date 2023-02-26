using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityAuth.ViewModels.Usuarios;

public class AdicionarUsuarioViewModel : BaseFormUsuarioViewModel
{
    [DisplayName("Senha")]
    public string Senha { get; set; } = string.Empty;
    
    [DisplayName("Confirme a senha")]
    public string ConfirmacaoSenha { get; set; } = string.Empty;
}
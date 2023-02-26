using System.ComponentModel;

namespace IdentityAuth.ViewModels.Login;

public class LoginViewModel
{
    [DisplayName("Username")]
    public string UserName { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    [DisplayName("Lembrar")]
    public bool PersistirLogin { get; set; }
}
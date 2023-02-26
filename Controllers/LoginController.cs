using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityAuth.ViewModels.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuth.Controllers;

public class LoginController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IValidator<AlterarSenhaViewModel> _alterarSenhaValidator;

    public LoginController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IValidator<AlterarSenhaViewModel> alterarSenhaValidator
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _alterarSenhaValidator = alterarSenhaValidator;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel login, string? returnUrl)
    {
        if (!ModelState.IsValid) return View(nameof(Index), login);

        var signInResult = await _signInManager.PasswordSignInAsync(
            login.UserName,
            login.Senha,
            login.PersistirLogin,
            false);
        if (signInResult.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect($"/{returnUrl}");
            return Redirect("/usuarios");
        }

        return View(nameof(Index), login);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        if (User.Identity.IsAuthenticated)
        {
            await _signInManager.SignOutAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public IActionResult AlterarSenha()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel alterarSenhaViewModel)
    {
        var validatorResult = _alterarSenhaValidator.Validate(alterarSenhaViewModel);
        if(!validatorResult.IsValid) validatorResult.AddToModelState(ModelState);
        if (!ModelState.IsValid) return View(alterarSenhaViewModel);
        
        var usuario = await _userManager.GetUserAsync(User);

        var identityResult = await _userManager.ChangePasswordAsync(
            usuario,
            alterarSenhaViewModel.SenhaAtual,
            alterarSenhaViewModel.NovaSenha
        );

        if (!identityResult.Succeeded) return View(alterarSenhaViewModel);

        await _signInManager.RefreshSignInAsync(usuario);
        return Redirect("/usuarios");
    }
}
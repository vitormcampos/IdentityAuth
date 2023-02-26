using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityAuth.ViewModels.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityAuth.Controllers;

public class UsuariosController : Controller
{
    private readonly IValidator<AdicionarUsuarioViewModel> _adicionarUsuarioValidator;
    private readonly IValidator<EditarUsuarioViewModel> _editarUsuarioValidator;
    private readonly UserManager<IdentityUser> _userManager;

    public UsuariosController(IValidator<AdicionarUsuarioViewModel> adicionarUsuarioValidator,
        UserManager<IdentityUser> userManager, IValidator<EditarUsuarioViewModel> editarUsuarioValidator)
    {
        _adicionarUsuarioValidator = adicionarUsuarioValidator;
        _userManager = userManager;
        _editarUsuarioValidator = editarUsuarioValidator;
    }

    public ActionResult Index()
    {
        var usuarios = _userManager.Users
            .Select(u => new ListagemUsuarioViewModel
            {
                Id = u.Id,
                Telefone = u.PhoneNumber,
                UserName = u.UserName,
                Email = u.Email
            });
        return View(usuarios);
    }

    public ActionResult Adicionar()
    {
        return View();
    }

    public async Task<ActionResult> Editar(string id)
    {
        var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario is null) return NotFound();

        var usuarioViewModel = new EditarUsuarioViewModel
        {
            Telefone = usuario.PhoneNumber,
            Email = usuario.Email,
            UserName = usuario.UserName,
        };

        return View(usuarioViewModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Editar(string id, EditarUsuarioViewModel usuarioViewModel)
    {
        var validationResult = _editarUsuarioValidator.Validate(usuarioViewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, String.Empty);
            return View(usuarioViewModel);
        }
        
        var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario is null) return NotFound();

        usuario.UserName = usuarioViewModel.UserName;
        usuario.Email = usuarioViewModel.Email;
        usuario.PhoneNumber = usuarioViewModel.Telefone;

        var identityResult = await _userManager.UpdateAsync(usuario);
        
        if (identityResult.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(usuarioViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Adicionar(AdicionarUsuarioViewModel usuario)
    {
        var validationResult = _adicionarUsuarioValidator.Validate(usuario);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, String.Empty);
            return View(usuario);
        }

        var identityUser = new IdentityUser
        {
            Email = usuario.Email,
            UserName = usuario.UserName,
            PhoneNumber = usuario.Telefone
        };

        var identityResult = await _userManager.CreateAsync(identityUser, usuario.Senha);
        if (identityResult.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    public async Task<IActionResult> Excluir(string id)
    {
        var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is null) return NotFound();

        await _userManager.DeleteAsync(usuario);

        return RedirectToAction(nameof(Index));
    }
}
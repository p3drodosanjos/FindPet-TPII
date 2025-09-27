// Em Controllers/UsuariosController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    // O .NET injeta o serviço automaticamente aqui
    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Usuario novoUsuario)
    {
        // Validação básica (pode ser melhorada com FluentValidation, por exemplo)
        if (novoUsuario == null || string.IsNullOrEmpty(novoUsuario.Email))
        {
            return BadRequest("Dados de usuário inválidos.");
        }

        // Remove o Id, pois o MongoDB irá gerar um novo
        novoUsuario.Id = null;

        await _usuarioService.CreateAsync(novoUsuario);

        // Retorna um status 201 Created com o usuário recém-criado
        return CreatedAtAction(nameof(Post), new { id = novoUsuario.Id }, novoUsuario);
    }
}
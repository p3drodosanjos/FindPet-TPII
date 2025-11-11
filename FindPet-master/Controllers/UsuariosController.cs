// Em Controllers/UsuariosController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Usuario novoUsuario)
    {
        // ... seu método Post (criar usuário) que já existe ...
        // [código omitido para breveidade]
        if (novoUsuario == null || string.IsNullOrEmpty(novoUsuario.Email))
        {
            return BadRequest("Dados de usuário inválidos.");
        }
        novoUsuario.Id = null;
        await _usuarioService.CreateAsync(novoUsuario);
        return CreatedAtAction(nameof(Post), new { id = novoUsuario.Id }, novoUsuario);
    }

    // ==================================================
    // == ADICIONE ESTE NOVO MÉTODO AQUI ==
    // ==================================================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Usuario usuarioComEndereco)
    {
        // 1. Busca o usuário que foi criado na Tela 1
        var usuarioExistente = await _usuarioService.GetAsync(id);

        if (usuarioExistente is null)
        {
            return NotFound("Usuário não encontrado para adicionar endereço.");
        }

        // 2. Atualiza SOMENTE os campos de endereço
        usuarioExistente.CEP = usuarioComEndereco.CEP;
        usuarioExistente.Rua = usuarioComEndereco.Rua;
        usuarioExistente.Bairro = usuarioComEndereco.Bairro;
        usuarioExistente.Cidade = usuarioComEndereco.Cidade;
        usuarioExistente.Numero = usuarioComEndereco.Numero;
        usuarioExistente.Complemento = usuarioComEndereco.Complemento;
        usuarioExistente.Estado = usuarioComEndereco.Estado;
        usuarioExistente.Pais = usuarioComEndereco.Pais;

        // 3. Salva o usuário completo de volta no banco
        await _usuarioService.UpdateAsync(id, usuarioExistente);

        // Retorna "Sem Conteúdo", que é o padrão para PUT/UPDATE bem-sucedido
        return NoContent(); 
    }
}
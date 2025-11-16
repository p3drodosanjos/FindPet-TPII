using Microsoft.AspNetCore.Mvc;
using FindPet.Models;      // Para TelaCadastro2 e TelaCadastroModel
using FindPet.Interfaces;  // Para IUsuarioBuilder e ICepService
using FindPet.Services;    // Para UsuarioService
using Newtonsoft.Json;     // Para deserializar o TempData
using System.Threading.Tasks;

namespace FindPet.Controllers // <<< CORRIGIDO: Deve ser "FindPet" (F maiúsculo)
{
    public class TelaCadastro2Controller : Controller
    {
        private readonly ICepService _cepService;

        // Construtor para Injeção de Dependência
        public TelaCadastro2Controller(ICepService cepService)
        {
            _cepService = cepService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TelaCadastro2Model model) // <<< CORRIGIDO: Nome da classe (T maiúsculo)
        {
            // Aqui você pode processar os dados do formulário, salvar no banco, etc.

            // Redireciona para a TelaLogin após o submit
            return RedirectToAction("Index", "TelaLogin");
        }

        [HttpPost]
        public IActionResult Index(TelaCadastro2Model model) // <<< CORRIGIDO: Nome da classe (T maiúsculo)
        {
            if (ModelState.IsValid)
            {
                // Aqui você pode processar os dados do formulário da TelaCadastro2
                return RedirectToAction("Success"); // Pode redirecionar para uma página de sucesso
            }

            return View();
        }

        // Endpoint da API de CEP (do passo anterior)
        [HttpGet("api/buscar-cep/{cep}")]
        public async Task<IActionResult> BuscarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                return BadRequest("CEP não pode ser nulo.");
            }

            var endereco = await _cepService.BuscarPorCep(cep);

            if (endereco == null)
            {
                return NotFound("CEP não encontrado.");
            }

            return Ok(endereco);
        }
    }
}

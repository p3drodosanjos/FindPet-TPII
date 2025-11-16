using Microsoft.AspNetCore.Mvc;
using FindPet.Models; // <<< CORRIGIDO (de 'findPet' para 'FindPet')
using Newtonsoft.Json;

namespace FindPet.Controllers // <<< CORRIGIDO (de 'findPet' para 'FindPet')
{
    public class TelaCadastroController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TelaCadastroModel model) // <<< CORRIGIDO (de 'telaCadastroModel' para 'TelaCadastroModel')
        {
            // Aqui você pode processar os dados do formulário, salvar no banco, etc.

            // Redireciona para a TelaCadastro2 após o submit
            return RedirectToAction("Index", "TelaCadastro2");
        }

    }
}
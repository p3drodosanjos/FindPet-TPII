using Microsoft.AspNetCore.Mvc;
using FindPet.Models; // <<< CORRIGIDO (de 'findPet' para 'FindPet')

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

        [HttpPost]
        public IActionResult Index(TelaCadastroModel model) // <<< CORRIGIDO (de 'telaCadastroModel' para 'TelaCadastroModel')
        {
            if (ModelState.IsValid)
            {
                // Aqui você pode processar os dados do formulário da TelaCadastro
                return RedirectToAction("Index", "TelaCadastro2"); // Redireciona para a próxima etapa
            }

            return View();
        }
    }
}
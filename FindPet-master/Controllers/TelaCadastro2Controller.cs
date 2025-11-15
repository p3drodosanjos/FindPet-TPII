using Microsoft.AspNetCore.Mvc;
using findPet.Models;

namespace findPet.Controllers
{
    public class TelaCadastro2Controller : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(telaCadastro2Model model)
        {
        // Aqui voc� pode processar os dados do formul�rio, salvar no banco, etc.
        
        // Redireciona para a TelaCadastro2 ap�s o submit
        return RedirectToAction("Index", "TelaLogin");
        }

        [HttpPost]
        public IActionResult Index(telaCadastro2Model model)
        {
            if (ModelState.IsValid)
            {
                // Aqui voc� pode processar os dados do formul�rio da TelaCadastro2
                return RedirectToAction("Success"); // Pode redirecionar para uma p�gina de sucesso
            }

            return View();
        }
    }
} //Teste
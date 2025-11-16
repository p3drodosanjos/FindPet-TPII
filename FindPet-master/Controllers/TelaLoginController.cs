using Microsoft.AspNetCore.Mvc;
using findPet.Models;

namespace findPet.Controllers
{
    public class TelaLoginController : Controller
    {
        [HttpPost]
        public IActionResult Login(TelaLoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Valida��o das credenciais do usu�rio
                bool usuarioValido = ValidarUsuario(model.Email, model.Senha);

                if (usuarioValido)
                {
                    // Redireciona para o dashboard ou p�gina inicial ap�s o login bem-sucedido
                    return RedirectToAction("Dashboard", "Home");
                }

                // Mensagem de erro para credenciais inv�lidas
                ModelState.AddModelError(string.Empty, "Email ou senha inv�lidos.");
            }

            // Retorna � tela de login com mensagens de erro
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Retorna a view de login
            return View();
        }

        private bool ValidarUsuario(string email, string senha)
        {
            // Aqui voc� implementa a l�gica de valida��o (banco de dados ou servi�o externo)
            // Exemplo: Login v�lido com email e senha fixos para simula��o
            return email == "teste@exemplo.com" && senha == "senha123";
        }
    }
}

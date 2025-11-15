using Microsoft.AspNetCore.Mvc;
using findPet.Services;
using findPet.Models;
using Newtonsoft.Json;

namespace findPet.Controllers
{
    public class TelaPublicacaoController : Controller
    {
        [HttpPost]
        public IActionResult Create(TelaPublicacaoModel model)
        {
            var publicacaoManager = PublicacaoManager.Instance;

            if (model != null)
            {
                if (model.Imagem != null && model.Imagem.Length > 0)
                {
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Imagem");
                    Directory.CreateDirectory(uploadFolder);

                    var fileNameId = Guid.NewGuid().ToString() + "_" + model.Imagem.FileName;
                    var filePath = Path.Combine(uploadFolder, fileNameId);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Imagem.CopyTo(fileStream);
                    }

                    model.ImageFileName = fileNameId;
                }

                // Cria o modelo de feed a partir do modelo de publicação
                var novaPublicacao = new TelaFeedModel
                {
                    Nome = model.Nome,
                    Raca = model.Raca,
                    Cor = model.Cor,
                    Porte = model.Porte,
                    LocalDesaparecimento = model.LocalDesaparecimento,
                    DataDesaparecimento = model.DataDesaparecimento,
                    Chip = model.Chip,
                    Legenda = model.Legenda,
                    ImageFileName = model.ImageFileName
                };

                // Adiciona a publicação e notifica os Observers (TelaFeedController)
                publicacaoManager.AdicionarPublicacao(novaPublicacao);

                return RedirectToAction("Index", "TelaFeed");
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
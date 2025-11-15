using Microsoft.AspNetCore.Mvc;
using findPet.Interfaces;
using findPet.Services;
using findPet.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace findPet.Controllers
{
    public class TelaFeedController : Controller, IObserver
    {
        private readonly PublicacaoManager _publicacaoManager;

        public TelaFeedController()
        {
            _publicacaoManager = PublicacaoManager.Instance;
            _publicacaoManager.Attach(this); // O Controller se registra como Observer
        }

        [HttpGet]
        public IActionResult Index()
        {
            // O feed agora obtém as publicações do PublicacaoManager
            var publicacoesOrdenadas = _publicacaoManager.ObterPublicacoesOrdenadas();
            
            return View(publicacoesOrdenadas);
        }

        [HttpPost]
        public IActionResult Curtir(int publicacaoId)
        {
            _publicacaoManager.IncrementarCurtidas(publicacaoId);
            var publicacao = _publicacaoManager.ObterPublicacaoPorId(publicacaoId);

            return Json(new { success = true, curtidas = publicacao?.Curtidas ?? 0 });
        }

        [HttpPost]
        public IActionResult Comentar(int publicacaoId, string textoComentario, string nomeUsuario = "Usuário Anônimo")
        {
            var novoComentario = new Comentario
            {
                NomeUsuario = nomeUsuario,
                Texto = textoComentario
            };

            _publicacaoManager.AdicionarComentario(publicacaoId, novoComentario);
            var publicacao = _publicacaoManager.ObterPublicacaoPorId(publicacaoId);

            return Json(new { 
                success = true, 
                totalComentarios = publicacao?.Comentarios.Count ?? 0,
                comentario = publicacao?.Comentarios.LastOrDefault()
            });
        }

        [HttpPost]
        public IActionResult Compartilhar(int publicacaoId)
        {
            _publicacaoManager.IncrementarCompartilhamentos(publicacaoId);
            var publicacao = _publicacaoManager.ObterPublicacaoPorId(publicacaoId);

            return Json(new { success = true, compartilhamentos = publicacao?.Compartilhamentos ?? 0 });
        }

        [HttpGet]
        public IActionResult ObterComentarios(int publicacaoId)
        {
            var publicacao = _publicacaoManager.ObterPublicacaoPorId(publicacaoId);
            if (publicacao != null)
            {
                return Json(new { 
                    success = true, 
                    comentarios = publicacao.Comentarios.Select(c => new {
                        id = c.Id,
                        nomeUsuario = c.NomeUsuario,
                        texto = c.Texto,
                        dataComentario = c.DataComentario.ToString("dd/MM/yyyy HH:mm")
                    })
                });
            }

            return Json(new { success = false });
        }

        // Implementação do método Update da interface IObserver
        public void Update(TelaFeedModel publicacao)
        {
            // Este método é chamado quando uma nova publicação é feita.
            // Como o feed já obtém a lista completa do PublicacaoManager no Index(),
            // a notificação aqui serve apenas para manter o Controller ciente da mudança,
            // mas não requer uma ação imediata de atualização de estado interno,
            // pois o estado é centralizado no PublicacaoManager.
            // Em um cenário real com SignalR, este seria o ponto para enviar a notificação em tempo real.
        }
    }
}


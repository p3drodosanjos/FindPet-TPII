using findPet.Interfaces;
using findPet.Models;
using System.Collections.Generic;
using System.Linq;

namespace findPet.Services
{
    public class PublicacaoManager : ISubject
    {
        private static PublicacaoManager _instance;
        private readonly List<IObserver> _observers = new List<IObserver>();
        private readonly List<TelaFeedModel> _publicacoes = new List<TelaFeedModel>();
        private int _proximoId = 1;

        private PublicacaoManager() { }

        public static PublicacaoManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PublicacaoManager();
                }
                return _instance;
            }
        }

        public void Attach(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(TelaFeedModel publicacao)
        {
            foreach (var observer in _observers)
            {
                observer.Update(publicacao);
            }
        }

        public void AdicionarPublicacao(TelaFeedModel publicacao)
        {
            publicacao.Id = _proximoId++;
            publicacao.DataPublicacao = System.DateTime.Now;
            _publicacoes.Insert(0, publicacao);
            Notify(publicacao);
        }

        public List<TelaFeedModel> ObterPublicacoesOrdenadas()
        {
            return _publicacoes.OrderByDescending(p => p.DataPublicacao).ToList();
        }

        // Métodos para Curtir, Comentar e Compartilhar
        public TelaFeedModel ObterPublicacaoPorId(int id)
        {
            return _publicacoes.FirstOrDefault(p => p.Id == id);
        }

        public void IncrementarCurtidas(int publicacaoId)
        {
            var publicacao = ObterPublicacaoPorId(publicacaoId);
            if (publicacao != null)
            {
                publicacao.Curtidas++;
            }
        }

        public void AdicionarComentario(int publicacaoId, Comentario comentario)
        {
            var publicacao = ObterPublicacaoPorId(publicacaoId);
            if (publicacao != null)
            {
                comentario.Id = publicacao.Comentarios.Count + 1;
                comentario.DataComentario = System.DateTime.Now;
                publicacao.Comentarios.Add(comentario);
            }
        }

        public void IncrementarCompartilhamentos(int publicacaoId)
        {
            var publicacao = ObterPublicacaoPorId(publicacaoId);
            if (publicacao != null)
            {
                publicacao.Compartilhamentos++;
            }
        }
    }
}

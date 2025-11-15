using findPet.Models;

namespace findPet.Interfaces
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(TelaFeedModel publicacao);
    }
}

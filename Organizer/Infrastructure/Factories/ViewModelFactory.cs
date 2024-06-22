using DryIoc;
using ReactiveUI;

namespace Organizer.Infrastructure.Factories
{
    public class ViewModelFactory
    {
        private readonly IContainer _container;

        public ViewModelFactory(IContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : ReactiveObject
        {
            return _container.Resolve<T>();
        }
    }
}

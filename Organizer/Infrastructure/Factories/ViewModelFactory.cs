using DryIoc;
using Organizer.ViewModels;
using System;

namespace Organizer.Infrastructure.Factories
{
    public class ViewModelFactory
    {
        private readonly IContainer _container;

        public ViewModelFactory(IContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : ViewModelBase
        {
            try
            {
                var instance = _container.Resolve<T>();
                return instance;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }

}

using Autofac;
using System.Windows.Controls;

namespace PlatformaEducationalaPentruScoala
{
    public class ViewFactory
    {
        IComponentContext _componentContext;

        public ViewFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public T Create<T>() where T : UserControl
        {
            return _componentContext.Resolve<T>();
        }
    }
}
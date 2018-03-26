using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Diagnostics;

namespace AlperAslanApps.Core.SimpleInjector.Services
{
    [DebuggerStepThrough]
    public class SimpleInjectorAsyncScopeStarter : IScopeStarter
    {
        private readonly Container _container;

        public SimpleInjectorAsyncScopeStarter(Container container)
        {
            _container = container;
        }

        public IDisposable BeginScope()
        {
            return AsyncScopedLifestyle.BeginScope(_container);
        }
    }
}

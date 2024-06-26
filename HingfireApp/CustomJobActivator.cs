using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;

public class CustomJobActivator : JobActivator
{
    private readonly IServiceProvider _serviceProvider;

    public CustomJobActivator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override object ActivateJob(Type jobType)
    {
        return _serviceProvider.GetService(jobType);
    }

    public override JobActivatorScope BeginScope(JobActivatorContext context)
    {
        return new CustomJobActivatorScope(_serviceProvider.CreateScope());
    }

    private class CustomJobActivatorScope : JobActivatorScope
    {
        private readonly IServiceScope _scope;

        public CustomJobActivatorScope(IServiceScope scope)
        {
            _scope = scope;
        }

        public override object Resolve(Type type)
        {
            return _scope.ServiceProvider.GetService(type);
        }

        public override void DisposeScope()
        {
            _scope.Dispose();
        }
    }
}

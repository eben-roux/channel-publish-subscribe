using System;
using System.Data.Common;
using System.Data.SqlClient;
using Shuttle.Core.ServiceHost;
using Shuttle.Core.StructureMap;
using Shuttle.Esb;
using StructureMap;
using Shuttle.Core.Container;
using Shuttle.Esb.Sql.Subscription;
using Shuttle.PublishSubscribe.Messages;

namespace Shuttle.PublishSubscribe.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost.Run<Host>();
        }
    }

    internal class Host : IServiceHost
    {
        private IServiceBus _bus;

        public void Start()
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);

            var registryActual = new Registry();
            var registry = new StructureMapComponentRegistry(registryActual);

            ServiceBus.Register(registry);

            var resolver = new StructureMapComponentResolver(new Container(registryActual));

            resolver.Resolve<ISubscriptionManager>().Subscribe<MemberRegisteredEvent>();

            _bus = ServiceBus.Create(resolver).Start();
        }

        public void Stop()
        {
            _bus.Dispose();
        }
    }
}

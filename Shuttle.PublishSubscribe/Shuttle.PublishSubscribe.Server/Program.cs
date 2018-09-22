using System;
using System.Data.Common;
using System.Data.SqlClient;
using Shuttle.Core.ServiceHost;
using Shuttle.Core.StructureMap;
using Shuttle.Esb;
using StructureMap;

namespace Shuttle.PublishSubscribe.Server
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

            _bus = ServiceBus.Create(new StructureMapComponentResolver(new Container(registryActual))).Start();
        }

        public void Stop()
        {
            _bus.Dispose();
        }
    }
}

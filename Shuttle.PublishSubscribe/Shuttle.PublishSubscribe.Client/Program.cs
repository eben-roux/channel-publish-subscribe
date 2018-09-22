using System;
using Shuttle.Core.StructureMap;
using Shuttle.Esb;
using Shuttle.PublishSubscribe.Messages;
using StructureMap;

namespace Shuttle.PublishSubscribe.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var registryActual = new Registry();
            var registry = new StructureMapComponentRegistry(registryActual);

            ServiceBus.Register(registry);

            using (var bus = ServiceBus.Create(new StructureMapComponentResolver(new Container(registryActual)))
                .Start())
            {
                string name;

                while (!string.IsNullOrEmpty(name = Console.ReadLine()))
                {
                    bus.Send(new RegisterMemberCommand
                    {
                        Name = name
                    });
                }
            }
        }
    }
}

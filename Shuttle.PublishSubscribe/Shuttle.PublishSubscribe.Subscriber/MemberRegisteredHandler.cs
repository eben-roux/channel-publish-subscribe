using System;
using Shuttle.Esb;
using Shuttle.PublishSubscribe.Messages;

namespace Shuttle.PublishSubscribe.Subscriber
{
    public class MemberRegisteredHandler : IMessageHandler<MemberRegisteredEvent>
    {
        public void ProcessMessage(IHandlerContext<MemberRegisteredEvent> context)
        {
            Console.WriteLine($"[event received] : name = '{context.Message.Name}'");
        }
    }
}
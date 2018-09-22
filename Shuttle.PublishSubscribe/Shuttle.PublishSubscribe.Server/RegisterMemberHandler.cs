using System;
using Shuttle.Esb;
using Shuttle.PublishSubscribe.Messages;

namespace Shuttle.PublishSubscribe.Server
{
    public class RegisterMemberHandler : IMessageHandler<RegisterMemberCommand>
    {
        public void ProcessMessage(IHandlerContext<RegisterMemberCommand> context)
        {
            Console.WriteLine($"[registered] : name = '{context.Message.Name}'");

            context.Publish(new MemberRegisteredEvent
            {
                Name = context.Message.Name
            });
        }
    }
}
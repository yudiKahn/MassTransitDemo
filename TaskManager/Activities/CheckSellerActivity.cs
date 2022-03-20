using Automatonymous;
using GreenPipes;
using Common.Commands;

namespace TaskManager.Activities
{
    public class CheckSellerActivity : Activity<DemoProcess, ItemSelectedCmd>
    {
        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<DemoProcess, ItemSelectedCmd> context, Behavior<DemoProcess, ItemSelectedCmd> next)
        {
            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<DemoProcess, ItemSelectedCmd, TException> context, Behavior<DemoProcess, ItemSelectedCmd> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope(nameof(CheckSellerActivity));
        }
    }
}

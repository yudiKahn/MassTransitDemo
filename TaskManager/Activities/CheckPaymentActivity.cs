using Automatonymous;
using GreenPipes;
using Common.Commands;

namespace TaskManager.Activities
{
    public class CheckPaymentActivity : Activity<DemoProcess, SellerApprovedCmd>
    {
        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<DemoProcess, SellerApprovedCmd> context, Behavior<DemoProcess, SellerApprovedCmd> next)
        {
            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<DemoProcess, SellerApprovedCmd, TException> context, Behavior<DemoProcess, SellerApprovedCmd> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope(nameof(CheckPaymentActivity));
        }
    }
}

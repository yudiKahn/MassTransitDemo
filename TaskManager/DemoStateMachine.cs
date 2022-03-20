using System;
using Automatonymous;
using Common.Commands;
using TaskManager.Activities;

namespace TaskManager
{
    public class DemoStateMachine : MassTransitStateMachine<DemoProcess>
    {
        public State CheckSellerState { get; }
        public State CheckPaymentState { get; }
        public State PurchaseCompleteState { get; }

        public Event<ItemSelectedCmd> ItemSelectedCmd { get; }
        public Event<SellerApprovedCmd> SellerApprovedCmd { get; }
        public Event<PaymentApprovedCmd> PaymentApprovedCmd { get; }

        public DemoStateMachine()
        {
            #region Event correlation

            Event(() => ItemSelectedCmd, x => x.CorrelateById(msg => msg.Message.CorrelationId));

            #endregion

            #region State flow
            InstanceState(x => x.CurrentState);

            Initially(
                 When(ItemSelectedCmd)
                 .Activity(x => x.OfType<CheckSellerActivity>())
                 .TransitionTo(CheckSellerState)
            );

            During(CheckSellerState,
                Ignore(ItemSelectedCmd),
                When(SellerApprovedCmd)
                .Activity(x => x.OfType<CheckPaymentActivity>())
                .TransitionTo(CheckPaymentState)
            );

            During(CheckPaymentState,
                Ignore(ItemSelectedCmd),
                Ignore(SellerApprovedCmd),
                When(PaymentApprovedCmd)
                .TransitionTo(PurchaseCompleteState)
            );

            During(PurchaseCompleteState,
                Ignore(ItemSelectedCmd),
                Ignore(SellerApprovedCmd),
                Ignore(PaymentApprovedCmd)
            );
            #endregion
        }
    }
}

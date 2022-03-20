using MassTransit.Transports;

namespace Client.Services
{
    public class PurchaseService
    {
        private readonly IBusInstance _BusInstance;
        public PurchaseService()
        {

        }

        public async Task<bool> Purchase(int id)
        {
            return await Task.FromResult(true);
        }
    }
}

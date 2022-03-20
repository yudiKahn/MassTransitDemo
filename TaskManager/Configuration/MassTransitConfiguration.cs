namespace TaskManager.Configuration
{
    public class MassTransitConfiguration
    {
        public string SagaQueueName { get; set; }
        public string ErrorQueueSuffix { get; set; }
    }
}

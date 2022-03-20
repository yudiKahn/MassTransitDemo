using Client.Configuration;
using Client.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seller.Models;

namespace Client;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = ConfigureAppConfiguration();
        var serviceProvider = ConfigureServices(configuration);
        
        Console.WriteLine("Select item id:");

        Repository.Print(Console.WriteLine);

        int id = Convert.ToInt32(Console.ReadLine());

        var ps = serviceProvider.GetService<PurchaseService>();
    }

    private static IConfiguration ConfigureAppConfiguration() =>
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

    private static IServiceProvider ConfigureServices(IConfiguration configuration)
    {
        var rmqConfigModel = configuration.GetSection(nameof(RabbitMqConfiguration)).Get<RabbitMqConfiguration>();

        var serviceProvider = new ServiceCollection();

        serviceProvider.AddSingleton<PurchaseService>();

        serviceProvider.AddMassTransit(mtConfig =>
        {
            mtConfig.UsingRabbitMq((ctx, rmqConfig) =>
            {
                rmqConfig.Host(rmqConfigModel.Host);
            });
        });

        return serviceProvider.BuildServiceProvider();
    }
}
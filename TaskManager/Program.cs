
using MassTransit;
using TaskManager.Configuration;

namespace TaskManager;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureAppConfiguration(builder.Configuration);

        // Add services to the container.
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        //app.UseStaticFiles();

        //app.UseRouting();

        app.UseAuthorization();

        //app.MapRazorPages();

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var massTransitConfiguration = configuration.GetSection(nameof(MassTransitConfiguration)).Get<MassTransitConfiguration>();
        var mongoDbConfiguration = configuration.GetSection(nameof(MongoDbConfiguration)).Get<MongoDbConfiguration>();
        var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqConfiguration)).Get<RabbitMqConfiguration>();

        services.AddMassTransit(cnf =>
        {
            cnf.AddSagaStateMachine<DemoStateMachine, DemoProcess>(typeof(DemoSagaDefinition))
                .Endpoint(e => e.Name = massTransitConfiguration.SagaQueueName)
                .MongoDbRepository(mongoDbConfiguration.ConnectionString, r =>
                {
                    r.DatabaseName = mongoDbConfiguration.DbName;
                    r.CollectionName = mongoDbConfiguration.CollectionName;
                });

            cnf.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(rabbitMqConfiguration.Host), hst =>
                {
                    hst.Username(rabbitMqConfiguration.Username);
                    hst.Password(rabbitMqConfiguration.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddMassTransitHostedService();
    }

    public static void ConfigureAppConfiguration(IConfigurationBuilder configuration)
    {
        configuration.AddJsonFile("Configuration/appsettings.json");
        configuration.AddEnvironmentVariables();
    }
}

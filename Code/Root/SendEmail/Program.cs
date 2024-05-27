using EmailSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public partial class Program
{
    public static async Task Main(string[] _)
    {
        var host = Host
            .CreateDefaultBuilder()
            .ConfigureServices((context, services) => ConfigureServices(context.Configuration, services))
            .Build();

        var emailer = host.Services.GetRequiredService<IEmailer>();
        emailer.SendEmail();
    }

    private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        var v = configuration.GetSection(nameof(EmailerOptions));

        services
            .AddTransient<IEmailer, Emailer>()
            .Configure<EmailerOptions>(options => configuration.GetSection(nameof(EmailerOptions)));
    }
}

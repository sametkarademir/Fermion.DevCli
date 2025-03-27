using DevCLI.App.Commands.Nuget;
using DevCLI.App.Commands.Password;
using DevCLI.App.Core.Services;
using DevCLI.App.Core.Services.Nuget;
using DevCLI.App.Core.Services.Password;
using DevCLI.App.Core.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace DevCLI.App.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDevCliServices(this IServiceCollection services)
    {
        //Utils
        services.AddSingleton<IRandomProvider, RandomProvider>();
        
        //Services
        services.AddSingleton<IPasswordGeneratorService, PasswordGeneratorService>();
        services.AddSingleton<INugetService, NugetService>();
        
        //Commands
        services.AddSingleton<GenerateCommand>();
        services.AddSingleton<LengthCommand>();
        services.AddSingleton<PasswordCommand>();
        
        services.AddSingleton<NugetCommand>();
        services.AddSingleton<SetTokenCommand>();
        services.AddSingleton<PushCommand>();

        return services;
    }
}
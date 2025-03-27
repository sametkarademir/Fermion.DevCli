using System.CommandLine;
using Fermion.DevCli.Command.Password.Commands;
using Fermion.DevCli.Command.Password.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fermion.DevCli.Command.Password.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDevCliPasswordCommandServices(this IServiceCollection services)
    {
        //Services
        services.AddSingleton<IRandomProvider, RandomProvider>();
        services.AddSingleton<IPasswordGeneratorService, PasswordGeneratorService>();
        
        //Commands
        services.AddSingleton<GenerateCommand>();
        services.AddSingleton<LengthCommand>();
        services.AddSingleton<PasswordCommand>();

        return services;
    }
    
    public static RootCommand AddPasswordCommand(this RootCommand rootCommand, IServiceProvider serviceProvider)
    {
        var passwordCommand = serviceProvider.GetRequiredService<PasswordCommand>();
        rootCommand.AddCommand(passwordCommand.Configure());
        return rootCommand;
    }
}
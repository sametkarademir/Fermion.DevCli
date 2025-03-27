using System.CommandLine;
using Fermion.DevCli.Command.Password.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Fermion.DevCli.ConsoleApp;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            var services = new ServiceCollection()
                .AddDevCliPasswordCommandServices()
                .BuildServiceProvider();

            var rootCommand = new RootCommand("DevCLI - Geliştirici araçları için komut satırı uygulaması");
            rootCommand.AddPasswordCommand(services);
            return await rootCommand.InvokeAsync(args);
        }
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync(e.Message);
            return 1;
        }
    }
}
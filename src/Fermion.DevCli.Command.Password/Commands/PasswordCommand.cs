using Fermion.DevCli.Core.Commands;

namespace Fermion.DevCli.Command.Password.Commands;

public class PasswordCommand : BaseCommand
{
    private readonly GenerateCommand _generateCommand;
    private readonly LengthCommand _lengthCommand;
    
    public PasswordCommand(GenerateCommand generateCommand, LengthCommand lengthCommand)
    {
        _generateCommand = generateCommand ?? throw new ArgumentNullException(nameof(generateCommand));
        _lengthCommand = lengthCommand ?? throw new ArgumentNullException(nameof(lengthCommand));
    }

    public override string Name => "password";
    public override string Description => "Parola işlemleri için komutlar";

    public override System.CommandLine.Command Configure()
    {
        var command = new System.CommandLine.Command(Name, Description);
        
        command.AddCommand(_generateCommand.Configure());
        command.AddCommand(_lengthCommand.Configure());
            
        return command;
    }
}
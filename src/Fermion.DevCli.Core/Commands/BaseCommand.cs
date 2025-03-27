using System.CommandLine;

namespace Fermion.DevCli.Core.Commands;

public abstract class BaseCommand
{
    public abstract string Name { get; }
    public abstract string Description { get; }

    public abstract Command Configure();
}
using Fermion.DevCli.Command.Password.Commands;

namespace Fermion.DevCli.Command.Password.Services;

public interface IPasswordGeneratorService
{
    string GeneratePassword(PasswordOptions options);
}
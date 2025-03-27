using DevCLI.App.Commands.Password;

namespace DevCLI.App.Core.Services.Password;

public interface IPasswordGeneratorService
{
    string GeneratePassword(PasswordOptions options);
}
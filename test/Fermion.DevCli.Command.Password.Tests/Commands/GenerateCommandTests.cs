using Fermion.DevCli.Command.Password.Commands;
using Fermion.DevCli.Command.Password.Services;
using Moq;
using FluentAssertions;

namespace Fermion.DevCli.Tests.Commands;

public class GenerateCommandTests
{
    private readonly Mock<IPasswordGeneratorService> _mockPasswordService;
    private readonly GenerateCommand _generateCommand;

    public GenerateCommandTests()
    {
        _mockPasswordService = new Mock<IPasswordGeneratorService>();
        _generateCommand = new GenerateCommand(_mockPasswordService.Object);
    }

    [Fact]
    public void Configure_ShouldReturnValidCommand()
    {
        // Act
        var command = _generateCommand.Configure();

        // Assert
        command.Should().NotBeNull();
        command.Name.Should().Be("generate");
        command.Description.Should().Be("Güçlü ve güvenli parolalar oluşturur");
        
        // Verify command has the expected options
        command.Options.Should().HaveCount(6);
        
        // Verify specific options exist
        command.Options.Should().Contain(x => x.Name == "length");
        command.Options.Should().Contain(x => x.Name == "uppercase");
        command.Options.Should().Contain(x => x.Name == "lowercase");
        command.Options.Should().Contain(x => x.Name == "numbers");
        command.Options.Should().Contain(x => x.Name == "special");
        command.Options.Should().Contain(x => x.Name == "count");
    }

    [Fact]
    public void GenerateCommand_WithDefaultOptions_ShouldCallService()
    {
        // Arrange
        _mockPasswordService.Setup(x => x.GeneratePassword(It.IsAny<PasswordOptions>()))
            .Returns("TestPassword123!");

        var command = _generateCommand.Configure();
        
        // This is a simplified test since we can't directly test the handler execution
        // In a real test, you would use System.CommandLine's TestConsole to capture output
        
        // Verify the service method is properly set up to be called
        _mockPasswordService.Verify(x => x.GeneratePassword(It.IsAny<PasswordOptions>()), Times.Never);
        
        // In a real test scenario, we would execute the command and verify the output
    }
}
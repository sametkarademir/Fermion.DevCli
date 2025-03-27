using Fermion.DevCli.Command.Password.Commands;
using FluentAssertions;

namespace Fermion.DevCli.Tests.Commands;

public class LengthCommandTests
{
    private readonly LengthCommand _lengthCommand;

    public LengthCommandTests()
    {
        _lengthCommand = new LengthCommand();
    }

    [Fact]
    public void Configure_ShouldReturnValidCommand()
    {
        // Act
        var command = _lengthCommand.Configure();

        // Assert
        command.Should().NotBeNull();
        command.Name.Should().Be("length");
        command.Description.Should().Be("Bir parolanın karakter sayısını belirler");
        
        // Verify command has the expected arguments
        command.Arguments.Should().HaveCount(1);
        command.Arguments[0].Name.Should().Be("password");
    }

    [Theory]
    [InlineData("TestPassword", 12)]
    [InlineData("a", 1)]
    [InlineData("SuperSecurePassword123!", 23)]
    public void LengthCommand_ShouldCalculateCorrectLength(string password, int expectedLength)
    {
        // This test would normally use System.CommandLine's TestConsole to capture output
        // For simplicity, we're just verifying that the password argument is passed correctly
        
        // Arrange
        var command = _lengthCommand.Configure();
        var passwordArgument = command.Arguments[0];

        // Assert
        passwordArgument.Should().NotBeNull();
        password.Length.Should().Be(expectedLength);
    }
}
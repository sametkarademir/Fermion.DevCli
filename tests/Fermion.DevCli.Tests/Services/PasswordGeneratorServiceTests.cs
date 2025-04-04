using Fermion.DevCli.Command.Password.Commands;
using Fermion.DevCli.Command.Password.Services;
using Moq;
using Xunit;
using FluentAssertions;

namespace Fermion.DevCli.Tests.Services;

public class PasswordGeneratorServiceTests
{
    private readonly Mock<IRandomProvider> _mockRandomProvider;
    private readonly PasswordGeneratorService _service;

    public PasswordGeneratorServiceTests()
    {
        _mockRandomProvider = new Mock<IRandomProvider>();
        _service = new PasswordGeneratorService(_mockRandomProvider.Object);
    }

    [Fact]
    public void GeneratePassword_ShouldReturnPasswordWithSpecifiedLength()
    {
        // Arrange
        var options = new PasswordOptions
        {
            Length = 16,
            IncludeUppercase = true,
            IncludeLowercase = true,
            IncludeNumbers = true,
            IncludeSpecialCharacters = true
        };

        // Mock random provider to always return first character from each set
        _mockRandomProvider.Setup(x => x.GetRandomInt(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(0);

        // Act
        var password = _service.GeneratePassword(options);

        // Assert
        password.Should().NotBeNullOrEmpty();
        password.Length.Should().Be(16);
    }

    [Fact]
    public void GeneratePassword_WithNoCharacterSets_ShouldThrowArgumentException()
    {
        // Arrange
        var options = new PasswordOptions
        {
            Length = 8,
            IncludeUppercase = false,
            IncludeLowercase = false,
            IncludeNumbers = false,
            IncludeSpecialCharacters = false
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.GeneratePassword(options));
    }

    [Fact]
    public void GeneratePassword_WithZeroLength_ShouldThrowArgumentException()
    {
        // Arrange
        var options = new PasswordOptions
        {
            Length = 0,
            IncludeUppercase = true,
            IncludeLowercase = true
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.GeneratePassword(options));
    }

    [Fact]
    public void GeneratePassword_WithOnlyUppercase_ShouldContainOnlyUppercaseChars()
    {
        // Arrange
        var options = new PasswordOptions
        {
            Length = 10,
            IncludeUppercase = true,
            IncludeLowercase = false,
            IncludeNumbers = false,
            IncludeSpecialCharacters = false
        };

        // Setup random provider to return predictable sequence
        _mockRandomProvider.Setup(x => x.GetRandomInt(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(0);

        // Act
        var password = _service.GeneratePassword(options);

        // Assert
        password.Should().NotBeNullOrEmpty();
        password.Length.Should().Be(10);
        password.Should().MatchRegex("^[A-Z]+$");
    }
}
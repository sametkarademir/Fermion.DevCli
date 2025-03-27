using System.CommandLine;
using Fermion.DevCli.Command.Password.Services;
using Fermion.DevCli.Core.Commands;

namespace Fermion.DevCli.Command.Password.Commands;

public class GenerateCommand(IPasswordGeneratorService passwordService) : BaseCommand
{
        private readonly IPasswordGeneratorService _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));

        public override string Name => "generate";
        public override string Description => "Güçlü ve güvenli parolalar oluşturur";

        public override System.CommandLine.Command Configure()
        {
            var command = new System.CommandLine.Command(Name, Description);
            
            var lengthOption = new Option<int>(
                aliases: new[] { "--length", "-l" },
                description: "Parola uzunluğu",
                getDefaultValue: () => 16);

            var uppercaseOption = new Option<bool>(
                aliases: new[] { "--uppercase", "-u" },
                description: "Büyük harf içersin mi",
                getDefaultValue: () => true);

            var lowercaseOption = new Option<bool>(
                aliases: new[] { "--lowercase", "-w" },
                description: "Küçük harf içersin mi",
                getDefaultValue: () => true);

            var numbersOption = new Option<bool>(
                aliases: new[] { "--numbers", "-n" },
                description: "Sayı içersin mi",
                getDefaultValue: () => true);

            var specialCharsOption = new Option<bool>(
                aliases: new[] { "--special", "-s" },
                description: "Özel karakterler içersin mi",
                getDefaultValue: () => true);

            var countOption = new Option<int>(
                aliases: new[] { "--count", "-c" },
                description: "Oluşturulacak parola sayısı",
                getDefaultValue: () => 1);
            
            command.AddOption(lengthOption);
            command.AddOption(uppercaseOption);
            command.AddOption(lowercaseOption);
            command.AddOption(numbersOption);
            command.AddOption(specialCharsOption);
            command.AddOption(countOption);

            command.SetHandler((length, uppercase, lowercase, numbers, special, count) =>
            {
                var options = new PasswordOptions
                {
                    Length = length,
                    IncludeUppercase = uppercase,
                    IncludeLowercase = lowercase,
                    IncludeNumbers = numbers,
                    IncludeSpecialCharacters = special
                };

                for (int i = 0; i < count; i++)
                {
                    var password = _passwordService.GeneratePassword(options);
                    Console.WriteLine(password);
                }
            }, lengthOption, uppercaseOption, lowercaseOption, numbersOption, specialCharsOption, countOption);

            return command;
        }
    }
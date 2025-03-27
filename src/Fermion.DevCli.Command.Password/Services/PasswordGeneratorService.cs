using System.Text;
using DevCLI.App.Commands.Password;
using DevCLI.App.Core.Utils;

namespace DevCLI.App.Core.Services.Password;

public class PasswordGeneratorService(IRandomProvider randomProvider) : IPasswordGeneratorService
{
    private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
    private const string NumberChars = "0123456789";
    private const string SpecialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?/";

    public string GeneratePassword(PasswordOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        if (options.Length <= 0)
            throw new ArgumentException("Parola uzunluğu pozitif bir sayı olmalıdır.", nameof(options.Length));
        
        if (!options.IncludeUppercase && !options.IncludeLowercase &&
            !options.IncludeNumbers && !options.IncludeSpecialCharacters)
        {
            throw new ArgumentException("En az bir karakter seti seçilmelidir.");
        }

        var charSets = new List<string>();
        if (options.IncludeUppercase) charSets.Add(UppercaseChars);
        if (options.IncludeLowercase) charSets.Add(LowercaseChars);
        if (options.IncludeNumbers) charSets.Add(NumberChars);
        if (options.IncludeSpecialCharacters) charSets.Add(SpecialChars);
        
        string allChars = string.Join("", charSets);
        
        var password = new StringBuilder();
        foreach (var charSet in charSets)
        {
            password.Append(charSet[randomProvider.GetRandomInt(0, charSet.Length)]);
        }
        
        for (int i = password.Length; i < options.Length; i++)
        {
            password.Append(allChars[randomProvider.GetRandomInt(0, allChars.Length)]);
        }
        
        return new string(password.ToString().OrderBy(c => randomProvider.GetRandomInt(0, options.Length)).ToArray());
    }
}
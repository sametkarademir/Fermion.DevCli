using System.CommandLine;
using Fermion.DevCli.Core.Commands;

namespace Fermion.DevCli.Command.Password.Commands;

public class LengthCommand : BaseCommand
{
    public override string Name => "length";
    public override string Description => "Bir parolanın karakter sayısını belirler";

    public override System.CommandLine.Command Configure()
    {
        var command = new System.CommandLine.Command(Name, Description);
        
        var passwordArgument = new Argument<string>(
            name: "password",
            description: "Değerlendirilecek parola (özel karakterler için tırnak işaretleri kullanın)")
        {
            Arity = ArgumentArity.ExactlyOne
        };
        
        passwordArgument.AddValidator(result =>
        {
            var token = result.Tokens.FirstOrDefault();
            
            if (token == null)
            {
                result.ErrorMessage = "Parola girmelisiniz";
            }
            else if (token.Value.Contains(" ") && !(token.Value.StartsWith("\"") && token.Value.EndsWith("\"")))
            {
                result.ErrorMessage = "Boşluk içeren parolalar tırnak işaretleri içinde girilmelidir. Örnek: \"my password\"";
            }
        });
        
        command.AddArgument(passwordArgument);
        
        command.SetHandler((password) =>
        {
            Console.WriteLine($"Parola Uzunluğu: {password.Length} karakter");
                
            if (password.Length < 8)
            {
                Console.WriteLine("Güvenlik Seviyesi: Zayıf - Parolanız çok kısa!");
            }
            else if (password.Length < 12)
            {
                Console.WriteLine("Güvenlik Seviyesi: Orta - Daha uzun bir parola tercih edebilirsiniz.");
            }
            else if (password.Length < 16)
            {
                Console.WriteLine("Güvenlik Seviyesi: İyi - Güvenli bir parola uzunluğu.");
            }
            else
            {
                Console.WriteLine("Güvenlik Seviyesi: Mükemmel - Çok güvenli bir parola uzunluğu!");
            }

        }, passwordArgument);
            
        return command;
    }
}
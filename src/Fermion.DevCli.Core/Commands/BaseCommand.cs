using System.CommandLine;

namespace DevCLI.App.Commands;

public abstract class BaseCommand
{
    /// <summary>
    /// Komutun adını döndürür
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Komutun açıklamasını döndürür
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// Komutu yapılandırır ve System.CommandLine Command nesnesini döndürür
    /// </summary>
    public abstract Command Configure();
}
using System.Security.Cryptography;

namespace DevCLI.App.Core.Utils;

public interface IRandomProvider
{
    int GetRandomInt(int minInclusive, int maxExclusive);
    byte[] GetRandomBytes(int length);
}

public class RandomProvider : IRandomProvider, IDisposable
{
    private readonly RandomNumberGenerator _rng;

    public RandomProvider()
    {
        _rng = RandomNumberGenerator.Create();
    }

    public int GetRandomInt(int minInclusive, int maxExclusive)
    {
        if (minInclusive >= maxExclusive)
            throw new ArgumentOutOfRangeException(nameof(minInclusive), "Min değeri max değerinden küçük olmalıdır.");
        
        var buffer = new byte[4];
        _rng.GetBytes(buffer);
        
        var randomInt = BitConverter.ToInt32(buffer, 0);
        var result = Math.Abs(randomInt == int.MinValue ? randomInt + 1 : randomInt);
            
        return minInclusive + result % (maxExclusive - minInclusive);
    }

    public byte[] GetRandomBytes(int length)
    {
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Uzunluk pozitif bir değer olmalıdır.");

        var buffer = new byte[length];
        _rng.GetBytes(buffer);
        return buffer;
    }

    public void Dispose()
    {
        _rng.Dispose();
    }
}
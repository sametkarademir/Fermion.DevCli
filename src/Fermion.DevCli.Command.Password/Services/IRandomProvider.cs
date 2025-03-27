namespace Fermion.DevCli.Command.Password.Services;

public interface IRandomProvider
{
    int GetRandomInt(int minInclusive, int maxExclusive);
    byte[] GetRandomBytes(int length);
}
namespace AlperAslanApps.Core
{
    public interface ITokenProvider<TSource>
    {
        byte[] Generate(TSource source, string purpose);
        bool Validate(byte[] data, TSource source, string purpose);
    }
}

namespace Workshop.Services.Data.HashProvider
{
    public interface IHashProvider
    {
        string Hash(string first, string second);
    }
}

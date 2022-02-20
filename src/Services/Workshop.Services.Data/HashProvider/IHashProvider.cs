namespace Workshop.Services.Data.HashProvider
{
    public interface IHashProvider
    {
        public string Hash(string first, string second);
    }
}

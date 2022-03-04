namespace Workshop.Services.Data.HashProvider
{
    public interface IHashProvider
    {
        string HashOfGivenStrings(string first, string second);

        public string HashOfGivenByteArray(byte[] input);
    }
}

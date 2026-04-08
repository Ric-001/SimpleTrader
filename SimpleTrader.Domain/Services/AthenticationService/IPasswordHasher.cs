namespace SimpleTrader.Domain.Services.AthenticationService
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}

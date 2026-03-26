using Application.Interfaces;

namespace Infrastructure.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string hashedPassword, string providePasssword)
            => BCrypt.Net.BCrypt.Verify(providePasssword, hashedPassword);
    }
}

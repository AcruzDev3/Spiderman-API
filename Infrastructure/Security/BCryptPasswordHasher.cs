using Application.Interfaces;

namespace Infrastructure.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password) {
            if (String.IsNullOrEmpty(password)) 
                throw new ArgumentNullException("La contraseña es nula o vacia");

             return BCrypt.Net.BCrypt.HashPassword(password);
        }
        
        public bool Verify(string providePasssword, string hashedPassword) {
            if (String.IsNullOrEmpty(hashedPassword) || String.IsNullOrEmpty(providePasssword))
                return false;

            return BCrypt.Net.BCrypt.Verify(providePasssword, hashedPassword);
        }
    }
}

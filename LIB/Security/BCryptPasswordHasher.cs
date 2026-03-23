using LIB.Interfaces;
using LIB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string hashedPassword, string providePasssword)
            => BCrypt.Net.BCrypt.Verify(providePasssword, hashedPassword);
    }
}

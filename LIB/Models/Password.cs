using LIB.Interfaces;

namespace LIB.Models
{
    public class Password
    {
        public string Value { get; }

        private Password(string value) {
            Value = value;
        }

        public static Password Create(string rawPasswordm, IPasswordHasher hasher) {
            string hashed = hasher.Hash(rawPasswordm);
            return new Password(hashed);
        }
    }
}

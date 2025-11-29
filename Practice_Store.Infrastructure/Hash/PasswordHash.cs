using System.Security.Cryptography;
using System.Text;

namespace Practice_Store.Common
{
    public class PasswordHash
    {
        public static string HashPassword(string Password, out string Salt)
        {
            Salt = GenerateSalt();
            var SaltedPassword = Password + Salt;
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(SaltedPassword);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public static bool VerifyPassword(string EnteredPassword, string StoredHash, string StoredSalt)
        {
            var SaltedPassword = EnteredPassword + StoredSalt;
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(SaltedPassword);
                var hash = sha256.ComputeHash(bytes);
                var enteredHash = Convert.ToBase64String(hash);
                return enteredHash == StoredHash;
            }
        }
        private static string GenerateSalt(int Size = 32)
        {
            var SaltBytes = new byte[Size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(SaltBytes);
            }
            return Convert.ToBase64String(SaltBytes);
        }
    }
}

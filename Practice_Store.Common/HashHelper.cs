using System.Security.Cryptography;
using System.Text;

namespace Practice_Store.Common
{
    public class HashHelper
    {
        public static string Hash(string Word)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(Word);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}

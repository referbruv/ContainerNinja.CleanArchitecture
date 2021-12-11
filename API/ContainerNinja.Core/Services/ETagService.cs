using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Text.Json;

namespace ContainerNinja.Core.Services
{
    public static class ETagService
    {
        private const string salt = "mysaltissostringthatitbreaksyourwilltobreakit";

        public static string ComputeWithHashFunction(object value)
        {
            var serialized = JsonSerializer.Serialize(value);
            var valueBytes = KeyDerivation.Pbkdf2(
                             password: serialized,
                             salt: Encoding.UTF8.GetBytes(salt),
                             prf: KeyDerivationPrf.HMACSHA512,
                             iterationCount: 10000,
                             numBytesRequested: 256 / 8);
            return Convert.ToBase64String(valueBytes);
        }
    }
}

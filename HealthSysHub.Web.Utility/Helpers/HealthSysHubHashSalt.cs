﻿using System.Security.Cryptography;

namespace HealthSysHub.Web.Utility.Helpers
{
    public class HealthSysHubHashSalt
    {
        public string Hash { get; set; }
        public string Salt { get; set; }

        public static HealthSysHubHashSalt GenerateSaltedHash(string password)
        {
            var saltBytes = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            HealthSysHubHashSalt hashSalt = new HealthSysHubHashSalt { Hash = hashPassword, Salt = salt };
            return hashSalt;
        }
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}

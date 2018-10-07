using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public static class Security //https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
    {

        private static RNGCryptoServiceProvider cryptoServiceProvider { get; set; } = new RNGCryptoServiceProvider();


        /// <summary>
        /// This method takes a password and an empty string as input.
        /// It returns a hashed password, and outs the salt.
        /// </summary>
        public static string GenerateHashedPassword(string password, out string salt)
        {
            byte[] saltArray = new byte[32];
            cryptoServiceProvider.GetBytes(saltArray);
            salt = Convert.ToBase64String(saltArray);

            return ComputeHashedPassword(password, saltArray);
        }
        /// <summary>
        /// This method takes a password and salt, and returns the hashed password.
        /// </summary>
        public static string GenerateHashedPassword(string password, string savedSalt)
        {
            //byte[] saltArray = Encoding.Unicode.GetBytes(savedSalt);
            byte[] saltArray = Convert.FromBase64String(savedSalt);
            return ComputeHashedPassword(password, saltArray);
        }

        /// <summary>
        /// This method takes a password and salt as inputs.
        /// It salts the password and hashes it. The hash is then returned.
        /// </summary>
        private static string ComputeHashedPassword(string password, byte[] salt)
        {
            byte[] hashedPasswordArray = new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(32);
            string hashedPassword = Convert.ToBase64String(hashedPasswordArray);
            return hashedPassword;
        }
    }
}
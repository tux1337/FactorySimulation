using System.Text;
using System.Security.Cryptography;


namespace CoreLib
{
    public static class SHA256Manager
    {
        public static string sha256(string randomString)
        {
            var crypt = SHA256.Create();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}

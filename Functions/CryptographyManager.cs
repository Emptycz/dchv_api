using System.Text;
using System.Security.Cryptography;  

namespace dchv_api.Functions
{
    public static class CryptographyManager
    {
        public static string SHA256(string input) 
        {
            using (SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashValue = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                if (hashValue is null) throw new Exception("Encrypted value is null");
                return _byteToString(hashValue);
            }
        }

        // Display the byte array in a readable format.
        private static string _byteToString(byte[] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
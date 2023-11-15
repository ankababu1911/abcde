using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;

namespace abcde.Portal.Helpers
{
    public class EncryptionSettings
    {
        public string EncryptionKey { get; set; } = string.Empty;
    }

    public class EncryptionHelper
    {
        private static ILogger? _logger;
        private readonly byte[] _key;
        private const string EncryptionKey = "cESxiqn4vhYpX0RTEzPkzh3U9W6kwG3ME2yeLrItJyg=";

        /// <summary>
        /// Constructor to inject IConfiguration and retrieve the encryption key
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public EncryptionHelper(IOptions<EncryptionSettings> options, ILogger<EncryptionHelper> logger)
        {
            _logger = logger;
            _key = Convert.FromBase64String(!string.IsNullOrEmpty(options.Value.EncryptionKey) ? options.Value.EncryptionKey : EncryptionKey);
        }

        /// <summary>
        ///  Method to encrypt the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string EncryptAES(string data)
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = _key;
                    aesAlg.Mode = CipherMode.CBC;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    // IV (Initialization Vector) should be unique and random for each encryption
                    aesAlg.GenerateIV();
                    byte[] iv = aesAlg.IV;

                    // Create an encryptor to perform the stream transform
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Perform the encryption
                    using (var msEncrypt = new System.IO.MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(dataBytes, 0, dataBytes.Length);
                            csEncrypt.FlushFinalBlock();
                        }

                        // Prepend the IV to the encrypted data
                        byte[] encryptedData = iv.Concat(msEncrypt.ToArray()).ToArray();
                        return Convert.ToBase64String(encryptedData);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError($"EncryptAES {ex.Message}");
            }
            return "";
        }

        /// <summary>
        ///		AES decryption
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        public string DecryptAES(string encryptedData)
        {
            try
            {
                // replacing space with + if any (because + is converting to space while encrypting)
                if (!string.IsNullOrEmpty(encryptedData))
                {
                    encryptedData = encryptedData.Replace(" ", "+");
                }
                byte[] encryptedDataBytes = Convert.FromBase64String(encryptedData);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = _key;
                    aesAlg.Mode = CipherMode.CBC;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    // Extract the IV from the beginning of the encrypted data
                    byte[] iv = encryptedDataBytes.Take(aesAlg.BlockSize / 8).ToArray();
                    aesAlg.IV = iv;

                    // Create a decryptor to perform the stream transform
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Perform the decryption
                    using (var msDecrypt = new System.IO.MemoryStream())
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                        {
                            csDecrypt.Write(encryptedDataBytes, aesAlg.BlockSize / 8, encryptedDataBytes.Length - aesAlg.BlockSize / 8);
                            csDecrypt.Flush();
                        }
                        byte[] decryptedDataBytes = msDecrypt.ToArray();
                        return Encoding.UTF8.GetString(decryptedDataBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError($"DecryptAES {ex.Message}");
            }
            return "";
        }

        /// <summary>
        /// Generate key
        /// </summary>
        /// <returns></returns>
        public static string GenerateAES256Key()
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256; // Set the key size to 256 bits
                aes.GenerateKey(); // Generate a new random key

                // Retrieve the generated key
                byte[] key = aes.Key;

                // Convert the key to a base64 string representation
                string base64Key = Convert.ToBase64String(key);
                return base64Key;
            }
        }

        public bool IsValidJson(string jsonString)
        {
            try
            {
                // Use the JsonDocument.Parse method to check if the string is valid JSON.
                JsonDocument.Parse(jsonString);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
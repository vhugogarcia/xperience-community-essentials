using StreamReader = System.IO.StreamReader;
using StreamWriter = System.IO.StreamWriter;

namespace XperienceCommunity.Essentials.Helpers;

public static class AesEncryptionHelper
{
    public static string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            return string.Empty;
        }

        string? key = ConfigurationHelper.Settings.GetSection("XperienceCommunityEssentials:AesSecureKey").Value ?? throw new InvalidOperationException("Encryption key is not configured.");

        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        using (var sha256 = SHA256.Create())
        {
            keyBytes = sha256.ComputeHash(keyBytes);
        }

        using var aesAlg = Aes.Create();
        aesAlg.Key = keyBytes;
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Padding = PaddingMode.PKCS7;

        aesAlg.GenerateIV();
        byte[] iv = aesAlg.IV;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        msEncrypt.Write(iv, 0, iv.Length);


        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8))
        {
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
        {
            return string.Empty;
        }

        string? key = ConfigurationHelper.Settings.GetSection("XperienceCommunityEssentials:AesSecureKey").Value ?? throw new InvalidOperationException("Decryption key is not configured.");

        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        using (var sha256 = SHA256.Create())
        {
            keyBytes = sha256.ComputeHash(keyBytes);
        }

        using var aesAlg = Aes.Create();
        aesAlg.Key = keyBytes;
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Padding = PaddingMode.PKCS7;

        int ivSize = aesAlg.BlockSize / 8;
        byte[] iv = new byte[ivSize];
        Array.Copy(cipherBytes, 0, iv, 0, iv.Length);
        aesAlg.IV = iv;

        int cipherTextLength = cipherBytes.Length - ivSize;
        byte[] actualCipherText = new byte[cipherTextLength];
        Array.Copy(cipherBytes, ivSize, actualCipherText, 0, cipherTextLength);

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(actualCipherText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8);
        return srDecrypt.ReadToEnd();
    }

    public static string GetMd5Hash(string input)
    {
        // Create a new instance of the MD5 crypto service provider
        using var md5Hash = MD5.Create();
        // Convert the input string to a byte array and compute the hash
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new StringBuilder to collect the bytes
        // and create a string
        var sb = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string
        for (int i = 0; i < data.Length; i++)
        {
            sb.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string
        return sb.ToString();
    }
}

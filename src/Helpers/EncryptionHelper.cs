namespace XperienceCommunity.Essentials.Helpers;

public static class EncryptionHelper
{
    public static string EncryptAes(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            return string.Empty;
        }

        string key = ConfigurationHelper.Settings.GetSection("XperienceCommunityEssentials:AesSecureKey").Value
            ?? throw new InvalidOperationException("Encryption key is not configured.");

        byte[] keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(key));

        using var aesAlg = Aes.Create();
        aesAlg.Key = keyBytes;
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Padding = PaddingMode.PKCS7;
        aesAlg.GenerateIV();

        using var msEncrypt = new MemoryStream();
        msEncrypt.Write(aesAlg.IV);

        using (var csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt, Encoding.UTF8))
        {
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public static string DecryptAes(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
        {
            return string.Empty;
        }

        string key = ConfigurationHelper.Settings.GetSection("XperienceCommunityEssentials:AesSecureKey").Value
            ?? throw new InvalidOperationException("Decryption key is not configured.");

        byte[] keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(key));
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using var aesAlg = Aes.Create();
        aesAlg.Key = keyBytes;
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Padding = PaddingMode.PKCS7;

        int ivSize = aesAlg.BlockSize / 8;
        aesAlg.IV = cipherBytes[..ivSize];

        using var msDecrypt = new MemoryStream(cipherBytes, ivSize, cipherBytes.Length - ivSize);
        using var csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read);
        using var srDecrypt = new System.IO.StreamReader(csDecrypt, Encoding.UTF8);
        return srDecrypt.ReadToEnd();
    }

    public static string GetMd5Hash(string input) =>
        Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(input))).ToLowerInvariant();
}

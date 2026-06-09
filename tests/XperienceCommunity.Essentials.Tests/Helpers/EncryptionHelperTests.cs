namespace XperienceCommunity.Essentials.Tests.Helpers;

[CollectionDefinition("EncryptionHelper", DisableParallelization = true)]
public class EncryptionHelperCollectionDefinition { }

[Collection("EncryptionHelper")]
public class EncryptionHelperTests
{
    public EncryptionHelperTests()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["XperienceCommunityEssentials:AesSecureKey"] = "TestAesKey-ForUnitTests!123456"
            })
            .Build();

        ConfigurationHelper.Initialize(config);
    }

    #region GetMd5Hash

    [Fact]
    public void GetMd5Hash_EmptyString_ReturnsKnownHash()
    {
        var result = EncryptionHelper.GetMd5Hash("");

        Assert.Equal("d41d8cd98f00b204e9800998ecf8427e", result);
    }

    [Fact]
    public void GetMd5Hash_KnownInput_ReturnsExpectedLowercaseHex()
    {
        var result = EncryptionHelper.GetMd5Hash("hello");

        Assert.Equal("5d41402abc4b2a76b9719d911017c592", result);
    }

    [Fact]
    public void GetMd5Hash_Result_IsLowercase()
    {
        var result = EncryptionHelper.GetMd5Hash("AnyInput");

        Assert.Equal(result, result.ToLowerInvariant());
    }

    #endregion

    #region EncryptAes / DecryptAes

    [Fact]
    public void EncryptAes_EmptyString_ReturnsEmpty()
    {
        var result = EncryptionHelper.EncryptAes("");

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void DecryptAes_EmptyString_ReturnsEmpty()
    {
        var result = EncryptionHelper.DecryptAes("");

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void EncryptAes_NonEmptyInput_ReturnsNonEmptyBase64()
    {
        var result = EncryptionHelper.EncryptAes("Hello");

        Assert.NotEmpty(result);
        Assert.True(IsBase64(result));
    }

    [Fact]
    public void Roundtrip_EncryptThenDecrypt_ReturnsOriginalPlaintext()
    {
        const string plaintext = "Xperience Community Essentials — test string 123!";

        var encrypted = EncryptionHelper.EncryptAes(plaintext);
        var decrypted = EncryptionHelper.DecryptAes(encrypted);

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void EncryptAes_TwoCallsOnSameInput_ProduceDifferentCiphertext()
    {
        const string plaintext = "same input";

        var first = EncryptionHelper.EncryptAes(plaintext);
        var second = EncryptionHelper.EncryptAes(plaintext);

        Assert.NotEqual(first, second);
    }

    #endregion

    private static bool IsBase64(string value)
    {
        Span<byte> buffer = new byte[(value.Length * 3 + 3) / 4];
        return Convert.TryFromBase64String(value, buffer, out _);
    }
}

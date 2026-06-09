namespace XperienceCommunity.Essentials.Tests.Helpers;

public class TextHelperTests
{
    #region Truncate

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Truncate_NullOrWhitespace_ReturnsEmpty(string? input)
    {
        var result = input.Truncate(10);

        Assert.Equal("", result);
    }

    [Fact]
    public void Truncate_BelowMaxLength_ReturnsOriginal()
    {
        var result = "Hello".Truncate(10);

        Assert.Equal("Hello", result);
    }

    [Fact]
    public void Truncate_ExactMaxLength_ReturnsOriginal()
    {
        var result = "Hello".Truncate(5);

        Assert.Equal("Hello", result);
    }

    [Fact]
    public void Truncate_AboveMaxLength_ReturnsTruncatedWithDefaultSuffix()
    {
        var result = "Hello World".Truncate(5);

        Assert.Equal("Hello…", result);
    }

    [Fact]
    public void Truncate_CustomSuffix_UsesCustomSuffix()
    {
        var result = "Hello World".Truncate(5, "...");

        Assert.Equal("Hello...", result);
    }

    #endregion

    #region EncodeToBase64 / DecodeFromBase64

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void EncodeToBase64_NullOrWhitespace_ReturnsEmpty(string? input)
    {
        var result = input.EncodeToBase64();

        Assert.Equal("", result);
    }

    [Fact]
    public void EncodeToBase64_ValidString_ReturnsBase64()
    {
        var result = "Hello".EncodeToBase64();

        Assert.Equal("SGVsbG8=", result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DecodeFromBase64_NullOrWhitespace_ReturnsEmpty(string? input)
    {
        var result = input.DecodeFromBase64();

        Assert.Equal("", result);
    }

    [Fact]
    public void RoundTrip_EncodeDecodeBase64_ReturnsOriginal()
    {
        const string original = "Xperience Community Essentials";

        var result = original.EncodeToBase64()!.DecodeFromBase64();

        Assert.Equal(original, result);
    }

    #endregion

    #region GenerateId

    [Fact]
    public void GenerateId_ReturnsStringStartingWithA()
    {
        var result = 42.GenerateId();

        Assert.StartsWith("A", result);
    }

    [Fact]
    public void GenerateId_DefaultLength_Returns10Chars()
    {
        var result = 1.GenerateId();

        Assert.Equal(10, result!.Length);
    }

    [Fact]
    public void GenerateId_CustomLength_ReturnsCorrectLength()
    {
        var result = 1.GenerateId(length: 6);

        Assert.Equal(6, result!.Length);
    }

    [Fact]
    public void GenerateId_SameInput_ReturnsSameId()
    {
        var first = 99.GenerateId();
        var second = 99.GenerateId();

        Assert.Equal(first, second);
    }

    #endregion

    #region CleanHtml

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CleanHtml_EmptyOrWhitespace_ReturnsEmpty(string? input)
    {
        var result = input!.CleanHtml();

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void CleanHtml_NoTags_ReturnsTrimmedString()
    {
        var result = "  Hello World  ".CleanHtml();

        Assert.Equal("Hello World", result);
    }

    [Fact]
    public void CleanHtml_WithHtmlTags_RemovesTags()
    {
        var result = "<p>Hello <b>World</b></p>".CleanHtml();

        Assert.Equal("Hello World", result);
    }

    [Fact]
    public void CleanHtml_MultipleSpaces_CollapsesToSingleSpace()
    {
        var result = "Hello   World".CleanHtml();

        Assert.Equal("Hello World", result);
    }

    #endregion

    #region ReplaceSpecialChars

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ReplaceSpecialChars_EmptyOrNull_ReturnsInput(string? input)
    {
        var result = input!.ReplaceSpecialChars();

        Assert.Equal(input, result);
    }

    [Fact]
    public void ReplaceSpecialChars_AlphanumericOnly_ReturnsUnchanged()
    {
        var result = "Hello123".ReplaceSpecialChars();

        Assert.Equal("Hello123", result);
    }

    [Fact]
    public void ReplaceSpecialChars_SpecialCharsAndSpaces_ReplacedWithHyphens()
    {
        var result = "Hello World!".ReplaceSpecialChars();

        Assert.Equal("Hello-World-", result);
    }

    #endregion

    #region EscapeMarkdown

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void EscapeMarkdown_EmptyOrNull_ReturnsInput(string? input)
    {
        var result = input!.EscapeMarkdown();

        Assert.Equal(input, result);
    }

    [Fact]
    public void EscapeMarkdown_NoSpecialChars_ReturnsUnchanged()
    {
        var result = "Hello World".EscapeMarkdown();

        Assert.Equal("Hello World", result);
    }

    [Fact]
    public void EscapeMarkdown_SpecialChars_AreEscaped()
    {
        var result = "Hello *world*".EscapeMarkdown();

        Assert.Equal(@"Hello \*world\*", result);
    }

    #endregion

    #region ExtractFileNameToTitleCase

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ExtractFileNameToTitleCase_EmptyOrWhitespace_ReturnsEmpty(string? input)
    {
        var result = input!.ExtractFileNameToTitleCase();

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ExtractFileNameToTitleCase_UrlWithExtension_ReturnsTitleCase()
    {
        var result = "/files/my_document.pdf".ExtractFileNameToTitleCase();

        Assert.Equal("My Document", result);
    }

    [Fact]
    public void ExtractFileNameToTitleCase_UrlWithQueryString_IgnoresQueryString()
    {
        var result = "/files/annual_report.pdf?v=2".ExtractFileNameToTitleCase();

        Assert.Equal("Annual Report", result);
    }

    [Fact]
    public void ExtractFileNameToTitleCase_HyphenatedFileName_ReturnsSpacedTitleCase()
    {
        var result = "/assets/user-guide-2024.pdf".ExtractFileNameToTitleCase();

        Assert.Equal("User Guide 2024", result);
    }

    #endregion

    #region CleanPhoneNumber

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CleanPhoneNumber_EmptyOrNull_ReturnsEmpty(string? input)
    {
        var result = input!.CleanPhoneNumber();

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void CleanPhoneNumber_WithDashesAndParens_ReturnsDigitsOnly()
    {
        var result = "(555) 123-4567".CleanPhoneNumber();

        Assert.Equal("5551234567", result);
    }

    [Fact]
    public void CleanPhoneNumber_AlreadyClean_ReturnsUnchanged()
    {
        var result = "5551234567".CleanPhoneNumber();

        Assert.Equal("5551234567", result);
    }

    #endregion

    #region PhoneNumberLettersToNumbers

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void PhoneNumberLettersToNumbers_EmptyOrNull_ReturnsInput(string? input)
    {
        var result = TextHelper.PhoneNumberLettersToNumbers(input!);

        Assert.Equal(input, result);
    }

    [Fact]
    public void PhoneNumberLettersToNumbers_AllLetters_ConvertsCorrectly()
    {
        var result = TextHelper.PhoneNumberLettersToNumbers("ABC");

        Assert.Equal("222", result);
    }

    [Fact]
    public void PhoneNumberLettersToNumbers_MixedFormat_PreservesNonLetters()
    {
        var result = TextHelper.PhoneNumberLettersToNumbers("800-CAR-LOAN");

        Assert.Equal("800-227-5626", result);
    }

    #endregion
}

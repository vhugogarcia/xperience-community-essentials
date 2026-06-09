namespace Microsoft.Extensions.Localization;

public class IStringLocalizerExtensionsTests
{
    [Fact]
    public void GetStringOrDefault_ResourceFound_ReturnsValue()
    {
        var localizer = Substitute.For<IStringLocalizer>();
        localizer.GetString("Key").Returns(new LocalizedString("Key", "Hello", resourceNotFound: false));

        var result = localizer.GetStringOrDefault("Key");

        Assert.Equal("Hello", result);
    }

    [Fact]
    public void GetStringOrDefault_ResourceNotFound_ReturnsDefaultEmpty()
    {
        var localizer = Substitute.For<IStringLocalizer>();
        localizer.GetString("Key").Returns(new LocalizedString("Key", "Key", resourceNotFound: true));

        var result = localizer.GetStringOrDefault("Key");

        Assert.Equal("", result);
    }

    [Fact]
    public void GetStringOrDefault_ValueEqualsKeyName_ReturnsDefault()
    {
        var localizer = Substitute.For<IStringLocalizer>();
        localizer.GetString("MyKey").Returns(new LocalizedString("MyKey", "MyKey", resourceNotFound: false));

        var result = localizer.GetStringOrDefault("MyKey");

        Assert.Equal("", result);
    }

    [Fact]
    public void GetStringOrDefault_ValueIsWhitespace_ReturnsDefault()
    {
        var localizer = Substitute.For<IStringLocalizer>();
        localizer.GetString("Key").Returns(new LocalizedString("Key", "   ", resourceNotFound: false));

        var result = localizer.GetStringOrDefault("Key");

        Assert.Equal("", result);
    }

    [Fact]
    public void GetStringOrDefault_CustomDefault_ReturnsCustomDefault()
    {
        var localizer = Substitute.For<IStringLocalizer>();
        localizer.GetString("Key").Returns(new LocalizedString("Key", "Key", resourceNotFound: true));

        var result = localizer.GetStringOrDefault("Key", "Fallback");

        Assert.Equal("Fallback", result);
    }
}

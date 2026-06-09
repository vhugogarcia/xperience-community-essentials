namespace Microsoft.AspNetCore.Mvc.Localization;

public class IHtmlLocalizerExtensionsTests
{
    [Fact]
    public void GetHtmlStringOrDefault_ResourceFound_ReturnsHtmlString()
    {
        var localizer = Substitute.For<IHtmlLocalizer>();
        localizer.GetHtml("Key").Returns(new LocalizedHtmlString("Key", "<b>Hello</b>", isResourceNotFound: false));

        var result = localizer.GetHtmlStringOrDefault("Key", new HtmlString("default"));

        Assert.Equal("<b>Hello</b>", result.Value);
    }

    [Fact]
    public void GetHtmlStringOrDefault_ResourceNotFound_ReturnsDefaultValue()
    {
        var localizer = Substitute.For<IHtmlLocalizer>();
        localizer.GetHtml("Key").Returns(new LocalizedHtmlString("Key", "Key", isResourceNotFound: true));
        var defaultValue = new HtmlString("<i>default</i>");

        var result = localizer.GetHtmlStringOrDefault("Key", defaultValue);

        Assert.Same(defaultValue, result);
    }

    [Fact]
    public void GetHtmlStringOrDefault_ValueEqualsKeyName_ReturnsDefault()
    {
        var localizer = Substitute.For<IHtmlLocalizer>();
        localizer.GetHtml("MyKey").Returns(new LocalizedHtmlString("MyKey", "MyKey", isResourceNotFound: false));
        var defaultValue = new HtmlString("fallback");

        var result = localizer.GetHtmlStringOrDefault("MyKey", defaultValue);

        Assert.Same(defaultValue, result);
    }

    [Fact]
    public void GetHtmlStringOrDefault_ValueIsWhitespace_ReturnsDefault()
    {
        var localizer = Substitute.For<IHtmlLocalizer>();
        localizer.GetHtml("Key").Returns(new LocalizedHtmlString("Key", "   ", isResourceNotFound: false));
        var defaultValue = new HtmlString("fallback");

        var result = localizer.GetHtmlStringOrDefault("Key", defaultValue);

        Assert.Same(defaultValue, result);
    }

    [Fact]
    public void GetHtmlStringOrDefault_CustomDefaultHtmlString_ReturnsThatDefault()
    {
        var localizer = Substitute.For<IHtmlLocalizer>();
        localizer.GetHtml("Key").Returns(new LocalizedHtmlString("Key", "Key", isResourceNotFound: true));
        var customDefault = new HtmlString("<em>custom</em>");

        var result = localizer.GetHtmlStringOrDefault("Key", customDefault);

        Assert.Same(customDefault, result);
    }
}

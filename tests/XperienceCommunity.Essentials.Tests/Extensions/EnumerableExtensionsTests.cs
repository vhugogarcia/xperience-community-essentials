namespace XperienceCommunity.Essentials.Tests.Extensions;

public class EnumerableExtensionsTests
{
    #region ContainsAny

    [Fact]
    public void ContainsAny_NullSource_ReturnsFalse()
    {
        IEnumerable<string>? source = null;
        var target = new[] { "a", "b" };

        var result = EnumerableExtensions.ContainsAny(source!, target);

        Assert.False(result);
    }

    [Fact]
    public void ContainsAny_NullTarget_ReturnsFalse()
    {
        IEnumerable<string> source = new[] { "a", "b" };

        var result = EnumerableExtensions.ContainsAny(source, null!);

        Assert.False(result);
    }

    [Fact]
    public void ContainsAny_EmptySource_ReturnsFalse()
    {
        IEnumerable<string> source = Array.Empty<string>();
        IEnumerable<string> target = ["a", "b"];

        var result = EnumerableExtensions.ContainsAny(source, target);

        Assert.False(result);
    }

    [Fact]
    public void ContainsAny_NoOverlap_ReturnsFalse()
    {
        IEnumerable<string> source = ["a", "b"];
        IEnumerable<string> target = ["c", "d"];

        var result = EnumerableExtensions.ContainsAny(source, target);

        Assert.False(result);
    }

    [Fact]
    public void ContainsAny_SingleOverlap_ReturnsTrue()
    {
        IEnumerable<string> source = ["a", "b"];
        IEnumerable<string> target = ["b", "c"];

        var result = EnumerableExtensions.ContainsAny(source, target);

        Assert.True(result);
    }

    [Fact]
    public void ContainsAny_FullOverlap_ReturnsTrue()
    {
        IEnumerable<string> source = ["a", "b"];
        IEnumerable<string> target = ["a", "b"];

        var result = EnumerableExtensions.ContainsAny(source, target);

        Assert.True(result);
    }

    #endregion

    #region HasAnyMatch

    [Fact]
    public void HasAnyMatch_NullSource_ReturnsFalse()
    {
        IEnumerable<string>? source = null;
        IEnumerable<string> target = ["a"];

        var result = EnumerableExtensions.HasAnyMatch(source!, target);

        Assert.False(result);
    }

    [Fact]
    public void HasAnyMatch_NullTarget_ReturnsFalse()
    {
        IEnumerable<string> source = ["a"];

        var result = EnumerableExtensions.HasAnyMatch(source, null!);

        Assert.False(result);
    }

    [Fact]
    public void HasAnyMatch_NoOverlap_ReturnsFalse()
    {
        IEnumerable<string> source = ["a", "b"];
        IEnumerable<string> target = ["c", "d"];

        var result = EnumerableExtensions.HasAnyMatch(source, target);

        Assert.False(result);
    }

    [Fact]
    public void HasAnyMatch_SourceContainsTargetItem_ReturnsTrue()
    {
        IEnumerable<string> source = ["a", "b"];
        IEnumerable<string> target = ["b", "c"];

        var result = EnumerableExtensions.HasAnyMatch(source, target);

        Assert.True(result);
    }

    [Fact]
    public void HasAnyMatch_TargetContainsSourceItem_ReturnsTrue()
    {
        // "x" is in target but not in source — validates bidirectionality
        IEnumerable<string> source = ["a"];
        IEnumerable<string> target = ["x", "a"];

        var result = EnumerableExtensions.HasAnyMatch(source, target);

        Assert.True(result);
    }

    #endregion

    #region GetFirstOrDefault

    [Fact]
    public void GetFirstOrDefault_NullSource_ReturnsDefaultValue()
    {
        IEnumerable<Asset>? source = null;

        var result = source.GetFirstOrDefault(a => a.AssetTitle, "fallback");

        Assert.Equal("fallback", result);
    }

    [Fact]
    public void GetFirstOrDefault_EmptySource_ReturnsDefaultValue()
    {
        var result = Array.Empty<Asset>().GetFirstOrDefault(a => a.AssetTitle, "fallback");

        Assert.Equal("fallback", result);
    }

    [Fact]
    public void GetFirstOrDefault_NonEmptySource_ReturnsSelectedValue()
    {
        var assets = new[] { new Asset { AssetTitle = "My Title" } };

        var result = assets.GetFirstOrDefault(a => a.AssetTitle, "fallback");

        Assert.Equal("My Title", result);
    }

    [Fact]
    public void GetFirstOrDefault_SelectorReturnsNull_ReturnsDefaultValue()
    {
        var assets = new[] { new Asset { AssetTitle = null! } };

        var result = assets.GetFirstOrDefault(a => a.AssetTitle, "fallback");

        Assert.Equal("fallback", result);
    }

    #endregion

    #region GetFirstOrEmpty

    [Fact]
    public void GetFirstOrEmpty_NullSource_ReturnsEmptyString()
    {
        IEnumerable<Asset>? source = null;

        var result = source.GetFirstOrEmpty(a => a.AssetTitle);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetFirstOrEmpty_NonEmptySource_ReturnsStringifiedValue()
    {
        var id = Guid.NewGuid();
        var assets = new[] { new Asset { AssetIdentifier = id } };

        var result = assets.GetFirstOrEmpty(a => a.AssetIdentifier);

        Assert.Equal(id.ToString(), result);
    }

    #endregion
}

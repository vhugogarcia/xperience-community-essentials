namespace XperienceCommunity.Essentials.Tests.Models;

public class Asset
{
    public string AssetTitle { get; set; } = string.Empty;
    public string AssetDescription { get; set; } = string.Empty;
    public bool AssetDiscoverableForAI { get; set; }
    public Guid AssetIdentifier { get; set; }
    public IEnumerable<string> TaxonomyTags { get; set; } = [];
    public object? AssetFile { get; set; }
}

namespace XperienceCommunity.Essentials.Helpers;

public static class ConfigurationHelper
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static IConfiguration Settings;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static void Initialize(IConfiguration configuration)
    {
        Settings = configuration;
    }
}

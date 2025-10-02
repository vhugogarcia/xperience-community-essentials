using Path = System.IO.Path;

namespace XperienceCommunity.Essentials.Helpers;

public static class TextHelper
{
    /// <summary>
    /// Truncates a string to the specified maximum length and appends a truncation suffix if necessary.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <param name="truncationSuffix"></param>
    /// <returns></returns>
    public static string? Truncate(this string? value, int maxLength, string truncationSuffix = "…")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "";
        }

        return value?.Length > maxLength
             ? value[..maxLength] + truncationSuffix
             : value;
    }

    /// <summary>
    /// Encodes a string to its Base64 representation.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string? EncodeToBase64(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "";
        }

        byte[] plainTextBytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(plainTextBytes);
    }

    /// <summary>
    /// Decodes a Base64 encoded string back to its original representation.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string? DecodeFromBase64(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "";
        }

        byte[] base64EncodedBytes = Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    /// <summary>
    /// Generates a unique ID based on the SHA-256 hash of the input integer.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string? GenerateId(this int input, int length = 10)
    {
        using var sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
        string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();

        // Ensure the ID starts with 'A'
        return "A" + hash[..(length - 1)];
    }

    /// <summary>
    /// Cleans HTML tags and extra whitespace from the input string.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string CleanHtml(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        // Remove HTML tags if any are still present
        string noHtml = Regex.Replace(input, "<.*?>", string.Empty);

        // Replace multiple whitespace characters with a single space
        string noExtraWhitespace = Regex.Replace(noHtml, @"\s+", " ");

        // Trim leading and trailing whitespace
        return noExtraWhitespace.Trim();
    }

    /// <summary>
    /// Replaces any special character (non-alphanumeric), whitespace, and underscores with hyphens.
    /// Multiple consecutive special characters are replaced with a single hyphen.
    /// Leading and trailing hyphens are removed.
    /// </summary>
    /// <param name="input">The input string to process</param>
    /// <returns>A string with special characters replaced by hyphens</returns>
    public static string ReplaceSpecialChars(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // Replace any non-alphanumeric character (including whitespace and _) with hyphen
        string result = Regex.Replace(input, @"[^a-zA-Z0-9]", "-");

        return result;
    }

    /// <summary>
    /// Escapes Markdown special characters in the input string.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string EscapeMarkdown(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        // Escape all Markdown special characters using regex
        string pattern = @"([\\`*_{}[\]()#+-.!])";

        return Regex.Replace(text, pattern, "\\$1");
    }

    /// <summary>
    /// Extracts the filename from a URL/path string and converts it to title case
    /// (first letter of each word capitalized).
    /// </summary>
    /// <param name="urlPath">The URL or path string containing the filename</param>
    /// <returns>The filename in title case format, or empty string if extraction fails</returns>
    public static string ExtractFileNameToTitleCase(this string urlPath)
    {
        // Validation: Check for null or empty string
        if (string.IsNullOrWhiteSpace(urlPath))
        {
            return string.Empty;
        }

        try
        {
            // Remove query parameters if present (everything after '?')
            string cleanPath = urlPath.Split('?')[0];

            // Extract filename using Path.GetFileName for robust path handling
            string fileName = Path.GetFileName(cleanPath);

            // Additional validation: Check if filename was successfully extracted
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }

            // Remove file extension
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            // Validation: Check if we have content after removing extension
            if (string.IsNullOrWhiteSpace(fileNameWithoutExtension))
            {
                return string.Empty;
            }

            // Convert underscores and hyphens to spaces for better word separation
            string processedName = fileNameWithoutExtension
                .Replace('_', ' ')
                .Replace('-', ' ');

            // Remove extra spaces and convert to title case
            string titleCase = ConvertToTitleCase(processedName);

            return titleCase;
        }
        catch (Exception)
        {
            // Return empty string on any exception to prevent application crashes
            return string.Empty;
        }
    }

    /// <summary>
    /// Converts a string to title case (first letter of each word capitalized).
    /// Handles multiple spaces and special characters properly.
    /// </summary>
    /// <param name="input">The input string to convert</param>
    /// <returns>The string in title case format</returns>
    private static string ConvertToTitleCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        // Normalize spaces (replace multiple spaces with single space)
        string normalized = Regex.Replace(input.Trim(), @"\s+", " ");

        // Split by spaces and capitalize first letter of each word
        string[] words = normalized.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > 0)
            {
                // Capitalize first letter, keep rest in lowercase
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            }
        }

        return string.Join(" ", words);
    }

    // <summary>
    /// Alternative implementation using Regex for phone number cleanup
    /// </summary>
    /// <param name="phoneNumber">The phone number string to clean</param>
    /// <returns>A string containing only digits</returns>
    public static string CleanPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return string.Empty;
        }

        // Remove all non-digit characters using regex
        return Regex.Replace(phoneNumber, @"[^\d]", "");
    }
}

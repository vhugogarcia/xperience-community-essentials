# Xperience Community: Essentials

[![Nuget](https://img.shields.io/nuget/v/XperienceCommunity.Essentials)](https://www.nuget.org/packages/XperienceCommunity.Essentials)

## Description

This package provides a comprehensive collection of common helpers, utilities, and extensions for Xperience by Kentico projects. The package includes encryption helpers, text manipulation utilities, collection extensions, localization helpers, and URL utilities designed to streamline development and reduce boilerplate code in Xperience applications.

## Library Version Matrix

| Xperience Version | Library Version |
|-------------------|-----------------|
| >= 30.6.0         | >= 1.0.0        |

> **Note:** The latest version that has been tested is 30.6.0

## ⚙️ Package Installation

Add the package to your application using the .NET CLI

```bash
dotnet add package XperienceCommunity.Essentials
```

## 🚀 Quick Start

Once the package is installed, you need to initialize the ConfigurationHelper in your `Program.cs` file to enable configuration-dependent features like AES encryption:

```csharp
// Add this line in your Program.cs after building the configuration
ConfigurationHelper.Initialize(builder.Configuration);
```

After initialization, the helpers and extensions will be automatically available in your Xperience application. Most features work out of the box, with optional configuration available for specific helpers like the AES encryption.

## ⚙️ Configuration

The package supports optional configuration for certain features. Add the following section to your `appsettings.json`:

```json
{
  "XperienceCommunityEssentials": {
    "AesSecureKey": ""
  }
}
```

### Configuration Options

| Setting | Description | Required |
|---------|-------------|----------|
| `AesSecureKey` | Base64 encoded key for AES encryption/decryption operations | Yes (when using AesEncryptionHelper) |

## ✨ Features

### AES Encryption Helper

**Secure encryption and decryption**: Provides AES encryption capabilities with configurable keys for securing sensitive data.

- `EncryptAes(string plainText)` - Encrypts plain text using AES with CBC mode
- `DecryptAes(string cipherText)` - Decrypts AES encrypted text back to plain text
- `GetMd5Hash(string input)` - Generates MD5 hash of input string

**Configuration Required**: You must set the `AesSecureKey` in your `appsettings.json` to use this helper.

### Text Helper Extensions

**String manipulation utilities**: A comprehensive set of extension methods for common text operations.

- `Truncate(int maxLength, string truncationSuffix = "…")` - Truncates strings with optional suffix
- `EncodeToBase64()` / `DecodeFromBase64()` - Base64 encoding/decoding
- `GenerateId(int length = 10)` - Generates random ID strings
- `CleanHtml()` - Removes HTML tags and cleans text content
- `ReplaceSpecialChars()` - Replaces special characters for URL-safe strings
- `EscapeMarkdown()` - Escapes markdown special characters
- `ExtractFileNameToTitleCase()` - Converts file paths to title case names
- `CleanPhoneNumber()` - Formats and cleans phone numbers

### Collection Extensions

**Enhanced enumerable operations**: Useful extension methods for working with collections.

- `ContainsAny(IEnumerable<string> target)` - Checks if any source elements exist in target
- `HasAnyMatch(IEnumerable<string> target)` - Bidirectional containment check
- `GetFirstOrDefault<T, TResult>(Func<T, TResult> selector, TResult defaultValue)` - Safe property access with defaults
- `GetFirstOrEmpty<T>(Func<T, string> selector)` - Gets first string value or empty string

### Localization Extensions

**Enhanced localization helpers**: Improved localization with fallback support.

- `IStringLocalizer.GetStringOrDefault(string name, string defaultValue)` - Gets localized string with fallback
- `IHtmlLocalizer.GetHtmlStringOrDefault(string name, HtmlString defaultValue)` - Gets localized HTML with fallback

### Page URL Extensions

**URL manipulation utilities**: Helper methods for working with Xperience page URLs.

- `RelativePathTrimmed()` - Gets relative path without tilde prefix
- `AbsoluteURL(HttpRequest currentRequest)` - Converts relative URLs to absolute URLs
- String extension for relative URL to absolute URL conversion

### Configuration Helper

**Centralized configuration access**: Provides global access to application configuration.

- `Initialize(IConfiguration configuration)` - Initialize the helper with configuration
- `Settings` - Static property for accessing configuration throughout the application

## 📋 Usage Examples

### Encryption

```csharp
// Encrypt sensitive data
string encrypted = EncryptionHelper.EncryptAes("sensitive information");

// Decrypt data
string decrypted = EncryptionHelper.DecryptAes(encrypted);

// Generate hash
string hash = AesEncryptionHelper.GetMd5Hash("password");
```

### Text Manipulation

```csharp
// Truncate long text
string truncated = longText.Truncate(100);

// Clean HTML content
string cleanText = htmlContent.CleanHtml();

// Generate a unique ID
string id = someNumber.GenerateId(12);

// Encode to Base64
string encoded = text.EncodeToBase64();
```

### Collection Operations

```csharp
// Check for any matches
bool hasMatch = sourceList.ContainsAny(targetList);

// Safe property access
var identifier = assetCollection.GetFirstOrEmpty(a => a.Identifier);
var url = assetCollection.GetFirstOrEmpty(a => a.AssetFile.Url);
var name = assetCollection.GetFirstOrEmpty(a => a.AssetFile.Metadata.Name);

var count = assetCollection.GetFirstOrDefault(a => a.SomeIntProperty, 0);
var date = assetCollection.GetFirstOrDefault(a => a.SomeDateTime, DateTime.MinValue);
```

### URL Helpers

```csharp
// Convert to absolute URL
string absoluteUrl = pageUrl.AbsoluteURL(HttpContext.Request);

// Clean relative path
string cleanPath = pageUrl.RelativePathTrimmed();
```

### Localization with Fallbacks

```csharp
// String localization with fallback
string text = localizer.GetStringOrDefault("key", "Default Text");

// HTML localization with fallback
HtmlString html = htmlLocalizer.GetHtmlStringOrDefault("key", new HtmlString("<p>Default</p>"));
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the MIT License.

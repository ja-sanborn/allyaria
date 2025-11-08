using Allyaria.Abstractions.Extensions;

namespace Allyaria.Abstractions.UnitTests.Extensions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class StringExtensionsTests
{
    [Theory]
    [InlineData(null, "", "")]
    [InlineData("", "", "")]
    [InlineData("a", "", "A")]
    [InlineData("HELLO", "", "Hello")]
    [InlineData("héLLo", "", "Héllo")]
    [InlineData("ß", "de-DE", "ß")]
    public void Capitalize_Should_HandleEdgeCases(string? input, string cultureName, string expected)
    {
        // Arrange
        var culture = string.IsNullOrEmpty(value: cultureName)
            ? null
            : new CultureInfo(name: cultureName);

        // Act
        var result = input.Capitalize(culture: culture);

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    public void FromCamelCase_Should_ReturnEmpty_When_NullOrWhitespace(string? input)
    {
        // Act
        var result = input.FromCamelCase();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("camelCaseExample", "camel Case Example")]
    [InlineData("jsonData", "json Data")]
    public void FromCamelCase_Should_SplitWords_When_ValidCamelCase(string input, string expected)
    {
        // Act
        var result = input.FromCamelCase();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData("NotCamelCase")]
    [InlineData("123abc")]
    [InlineData("camel_case")]
    public void FromCamelCase_Should_Throw_When_InvalidIdentifier(string input)
    {
        // Act
        var act = input.FromCamelCase;

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "*must be a camelCase identifier*");
    }

    [Theory]
    [InlineData("kebab-case-example", "kebab case example")]
    [InlineData("Multi---dash", "Multi dash")]
    public void FromKebabCase_Should_ReplaceHyphens_WithSpaces(string input, string expected)
    {
        // Act
        var result = input.FromKebabCase();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    public void FromKebabCase_Should_ReturnEmpty_When_NullOrWhitespace(string? input)
    {
        // Act
        var result = input.FromKebabCase();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Not-Kebab_Case")]
    [InlineData("123-invalid")]
    [InlineData("--double-hyphen")]
    public void FromKebabCase_Should_ThrowArgumentException_When_InvalidIdentifier(string input)
    {
        // Act
        var act = input.FromKebabCase;

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "*must be a kebab-case identifier*");
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\r\n")]
    public void FromPascalCase_Should_ReturnEmpty_When_NullOrWhitespace(string? input)
    {
        // Act
        var result = input.FromPascalCase();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("PascalCaseExample", "Pascal Case Example")]
    [InlineData("HTTPRequest", "HTTP Request")]
    public void FromPascalCase_Should_SplitWords_When_ValidPascalCase(string input, string expected)
    {
        // Act
        var result = input.FromPascalCase();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData("notPascalCase")]
    [InlineData("123Start")]
    public void FromPascalCase_Should_Throw_When_InvalidIdentifier(string input)
    {
        // Act
        var act = input.FromPascalCase;

        // Assert
        act.Should().Throw<AryArgumentException>();
    }

    [Theory]
    [InlineData("PascalCase", "Pascal Case")]
    [InlineData("camelCase", "camel Case")]
    [InlineData("snake_case", "snake case")]
    [InlineData("kebab-case", "kebab case")]
    [InlineData("_camelCase", "camel Case")]
    [InlineData("--PascalCase", "Pascal Case")]
    public void FromPrefixedCase_Should_HandleDifferentConventions(string input, string expected)
    {
        // Act
        var result = input.FromPrefixedCase();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\r\n")]
    public void FromPrefixedCase_Should_ReturnEmpty_When_NullOrWhitespace(string? input)
    {
        // Arrange / Act
        var result = input.FromPrefixedCase();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("-", "Input cannot be reduced to a valid identifier.")]
    [InlineData("_", "Input cannot be reduced to a valid identifier.")]
    [InlineData(
        "invalid value",
        "Input must be PascalCase, camelCase, snake_case, or kebab-case (with optional leading '_' or '-')."
    )]
    public void FromPrefixedCase_Should_Throw_When_Invalid(string input, string expectedMessage)
    {
        // Act
        var act = input.FromPrefixedCase;

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: expectedMessage + "*");
    }

    [Theory]
    [InlineData("snake_case_example", "snake case example")]
    [InlineData("multi___underscore", "multi underscore")]
    public void FromSnakeCase_Should_ReplaceUnderscores_WithSpaces(string input, string expected)
    {
        // Act
        var result = input.FromSnakeCase();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    public void FromSnakeCase_Should_ReturnEmpty_When_NullOrWhitespace(string? input)
    {
        // Act
        var result = input.FromSnakeCase();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Not_Snake-Case")]
    [InlineData("123_invalid")]
    [InlineData("__double__underscore")]
    public void FromSnakeCase_Should_ThrowArgumentException_When_InvalidIdentifier(string input)
    {
        // Act
        var act = input.FromSnakeCase;

        // Assert
        act.Should().Throw<AryArgumentException>()
            .WithMessage(expectedWildcardPattern: "*ust be a snake_case identifier*");
    }

    [Theory]
    [InlineData("café", "cafe")]
    [InlineData("résumé", "resume")]
    [InlineData("naïve", "naive")]
    [InlineData(null, "")]
    [InlineData("   ", "")]
    public void NormalizeAccents_Should_RemoveDiacritics(string? input, string expected)
    {
        // Act
        var result = input.NormalizeAccents();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData(null, "default", "default")]
    [InlineData("", "default", "")]
    [InlineData("value", "default", "value")]
    public void OrDefault_Should_ReturnExpectedResult(string? input, string defaultValue, string expected)
    {
        // Act
        var result = input.OrDefaultIfEmpty(defaultValue: defaultValue);

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData("hello world", "helloWorld")]
    [InlineData("HTTP request", "httpRequest")]
    [InlineData("XML HTTP request", "xmlHttpRequest")]
    [InlineData(null, "")]
    [InlineData("   ", "")]
    public void ToCamelCase_Should_ConvertCorrectly(string? input, string expected)
    {
        // Act
        var result = input.ToCamelCase();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\r\n")]
    public void ToCamelCase_Should_ReturnEmpty_When_NoWords(string? input)
    {
        // Arrange / Act
        var result = input.ToCamelCase();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ToCamelCase_Should_SkipEmptyToken_When_NormalizedWordIsOnlyCombiningMarks()
    {
        // Arrange: string with only combining marks
        var input = "\u0301\u0301"; // combining acute accents

        // Act
        var result = input.ToCamelCase();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("hello world", "hello-world")]
    [InlineData("héllö   Wörld", "hello-world")]
    [InlineData(null, "")]
    [InlineData("   ", "")]
    public void ToKebabCase_Should_ConvertCorrectly(string? input, string expected)
    {
        // Act
        var result = input.ToKebabCase();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData("hello world", "HelloWorld")]
    [InlineData("HTTP request", "HTTPRequest")]
    [InlineData("XML HTTP request", "XMLHTTPRequest")]
    [InlineData(null, "")]
    [InlineData("   ", "")]
    public void ToPascalCase_Should_ConvertCorrectly(string? input, string expected)
    {
        // Act
        var result = input.ToPascalCase();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("   ", "")]
    [InlineData("\u0301\u0301", "")] // only combining accents
    public void ToPascalCase_Should_SkipTokens_When_NormalizedWordIsEmpty(string? input, string expected)
    {
        // Act
        var result = input.ToPascalCase();

        // Assert
        result.Should().Be(expected: expected);
    }

    [Theory]
    [InlineData("hello world", "hello_world")]
    [InlineData("héllö   Wörld", "hello_world")]
    [InlineData(null, "")]
    [InlineData("   ", "")]
    public void ToSnakeCase_Should_ConvertCorrectly(string? input, string expected)
    {
        // Act
        var result = input.ToSnakeCase();

        // Assert
        result.Should().Be(expected: expected);
    }
}

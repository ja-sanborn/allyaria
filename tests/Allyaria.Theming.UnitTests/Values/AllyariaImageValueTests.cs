#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Allyaria.Theming.UnitTests.Values;

public sealed class AllyariaImageValueTests
{
    [Theory]
    [InlineData("http://example.com/img.png")]
    [InlineData("https://example.com/img.png")]
    [InlineData("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAA")]
    [InlineData("blob:https://example.com/9b0f8f3e-9e1b-4d0c-8c0a-123456789abc")]
    public void Ctor_Should_AllowUrl_When_AbsoluteUrlWithAllowedScheme(string input)
    {
        // Arrange

        // Act
        var sut = new AllyariaImageValue(input);
        string result = sut;

        // Assert
        result.Should().Be($@"url(""{input.Replace(@"\", @"\\").Replace(@"""", @"\""")}"")");
    }

    [Fact]
    public void Ctor_Should_EscapeBackslashesAndQuotes_When_PathContainsSpecials()
    {
        // Arrange
        var input = @"images\banner ""big"".png";

        // Act
        var sut = new AllyariaImageValue(input);
        string result = sut;

        // Assert
        result.Should().Be(@"url(""images\\banner ""big"".png"")".Replace("  ", " "));
    }

    [Fact]
    public void Ctor_Should_ExtractFirstUrl_When_InputContainsOtherCssTokens()
    {
        // Arrange
        var input = "linear-gradient(red, blue), url(' hero.png ') no-repeat center / cover";

        // Act
        var sut = new AllyariaImageValue(input);
        string result = sut;

        // Assert
        result.Should().Be(@"url(""hero.png"")");
    }

    [Fact]
    public void Ctor_Should_Handle_DataUri_WithQuotesAndBackslashes()
    {
        // Arrange
        var input = @"data:image/svg+xml;utf8,<svg viewBox=""0 0""></svg>";

        // Act
        var sut = new AllyariaImageValue(input);
        string result = sut;

        // Assert
        result.Should().Be(@"url(""data:image/svg+xml;utf8,<svg viewBox=""0 0""></svg>"")");
    }

    [Fact]
    public void Ctor_Should_PreserveParenthesesAndSpaces_When_InsideQuotes()
    {
        // Arrange
        var input = @"url(""images (final) 01.png"") more stuff";

        // Act
        var sut = new AllyariaImageValue(input);
        string result = sut;

        // Assert
        result.Should().Be(@"url(""images (final) 01.png"")");
    }

    [Fact]
    public void Ctor_Should_ReturnCanonicalUrl_When_RawRelativePath()
    {
        // Arrange
        var input = "images/logo.png";

        // Act
        var sut = new AllyariaImageValue(input);
        string result = sut;

        // Assert
        result.Should().Be(@"url(""images/logo.png"")");
    }

    [Fact]
    public void Ctor_Should_ReturnGradientPlusUrl_When_BackgroundIsDark()
    {
        // Arrange
        AllyariaColorValue background = "#000000";
        var input = "photo.jpg";

        // Act
        var sut = new AllyariaImageValue(input, background);
        string result = sut;

        // Assert
        result.Should().Be(@"linear-gradient(rgba(255, 255, 255, 0.5),rgba(255, 255, 255, 0.5)),url(""photo.jpg"")");
    }

    [Fact]
    public void Ctor_Should_ReturnGradientPlusUrl_When_BackgroundIsLight()
    {
        // Arrange
        AllyariaColorValue background = "#ffffff";
        var input = "photo.jpg";

        // Act
        var sut = new AllyariaImageValue(input, background);
        string result = sut;

        // Assert
        result.Should().Be(@"linear-gradient(rgba(0, 0, 0, 0.5),rgba(0, 0, 0, 0.5)),url(""photo.jpg"")");
    }

    [Theory]
    [InlineData("ftp://example.com/file.png", "ftp")]
    [InlineData("file:///C:/tmp/a.png", "file")]
    [InlineData("mailto:user@example.com", "mailto")]
    [InlineData("ssh://host/path.png", "ssh")]
    public void Ctor_Should_ThrowAllyariaArgumentException_When_AbsoluteUrlWithDisallowedScheme(string input,
        string scheme)
    {
        // Arrange

        // Act
        var act = () => new AllyariaImageValue(input);

        // Assert
        act.Should().Throw<AllyariaArgumentException>()
            .WithMessage($"Unsupported URI scheme '{scheme}'*");
    }

    [Theory]
    [InlineData("   javascript:alert(1)")]
    [InlineData("\tVbScRiPt:msgbox(1)")]
    public void Ctor_Should_ThrowAllyariaArgumentException_When_DangerousScheme(string input)
    {
        // Arrange

        // Act
        var act = () => new AllyariaImageValue(input);

        // Assert
        act.Should().Throw<AllyariaArgumentException>()
            .WithMessage("Unsupported URI scheme for CSS image value.*");
    }

    [Theory]
    [InlineData(@"""foo bar.png""", @"url(""foo bar.png"")")]
    [InlineData(@"'a (b).png'", @"url(""a (b).png"")")]
    public void Ctor_Should_UnwrapQuotes_When_InputQuoted(string input, string expected)
    {
        // Arrange

        // Act
        var sut = new AllyariaImageValue(input);
        string result = sut;

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Ctor_Should_UseFirstUrl_When_MultipleUrlTokensArePresent()
    {
        // Arrange
        var input = "url(a.png), url(b.png), url(c.png)";

        // Act
        var sut = new AllyariaImageValue(input);
        string result = sut;

        // Assert
        result.Should().Be(@"url(""a.png"")");
    }

    [Fact]
    public void ImplicitOperators_Should_RoundTripNormalizedValue()
    {
        // Arrange
        AllyariaImageValue sut = "assets/a.png";

        // Act
        string result = sut;

        // Assert
        result.Should().Be(@"url(""assets/a.png"")");
    }

    [Fact]
    public void Parse_Should_ReturnEquivalentResultToConstructor_When_Valid()
    {
        // Arrange
        var input = "foo.png";

        // Act
        var sut = AllyariaImageValue.Parse(input);
        string result = sut;

        // Assert
        result.Should().Be(@"url(""foo.png"")");
    }

    [Fact]
    public void TryParse_Should_ReturnFalseAndNull_When_Invalid()
    {
        // Arrange
        var input = " vbscript:doEvil() ";

        // Act
        var ok = AllyariaImageValue.TryParse(input, out var parsed);

        // Assert
        ok.Should().BeFalse();
        parsed.Should().BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrueAndResult_When_Valid()
    {
        // Arrange
        var input = "bar/baz.svg";

        // Act
        var ok = AllyariaImageValue.TryParse(input, out var parsed);

        // Assert
        ok.Should().BeTrue();
        parsed.Should().NotBeNull();
        string normalized = parsed!;
        normalized.Should().Be(@"url(""bar/baz.svg"")");
    }
}

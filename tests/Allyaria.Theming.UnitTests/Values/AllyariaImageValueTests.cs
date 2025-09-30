namespace Allyaria.Theming.UnitTests.Values;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AllyariaImageValueTests
{
    [Theory]
    [InlineData("http://example.com/a.png")]
    [InlineData("https://example.com/a.png")]
    [InlineData("data:image/png;base64,AAAA")]
    [InlineData("blob:https://example.com/550e8400-e29b-41d4-a716-446655440000")]
    public void Ctor_Should_Accept_Allowed_Absolute_Schemes(string input)
    {
        // Act
        var sut = new AllyariaImageValue(input);

        // Assert
        sut.Value.Should()
            .Be($@"url(""{input}"")");
    }

    [Fact]
    public void Ctor_Should_Allow_Relative_Paths()
    {
        // Arrange
        var input = "../assets/img/logo.svg";

        // Act
        var sut = new AllyariaImageValue(input);

        // Assert
        sut.Value.Should()
            .Be(@"url(""../assets/img/logo.svg"")");
    }

    [Fact]
    public void Ctor_Should_Escape_Backslashes_And_DoubleQuotes()
    {
        // Arrange
        var input = @"path\with\name""pic"".png";

        // Act
        var sut = new AllyariaImageValue(input);

        // Assert
        sut.Value.Should()
            .Be("url(\"path\\\\with\\\\name\\\"pic\\\".png\")");
    }

    [Fact]
    public void Ctor_Should_Extract_First_Url_From_Composite_Css_And_Discard_Others()
    {
        // Arrange
        var input = "linear-gradient(white, black), URL('first.png') , url(second.png) no-repeat";

        // Act
        var sut = new AllyariaImageValue(input);

        // Assert
        sut.Value.Should()
            .Be(@"url(""first.png"")");
    }

    [Fact]
    public void Ctor_Should_Normalize_Bare_Path_To_Url_Token()
    {
        // Arrange
        var input = "images/photo.png";

        // Act
        var sut = new AllyariaImageValue(input);

        // Assert
        sut.Value.Should()
            .Be(@"url(""images/photo.png"")");

        ((string)sut).Should()
            .Be(@"url(""images/photo.png"")");
    }

    [Fact]
    public void Ctor_Should_Preserve_Mismatched_Quotes_Without_Unwrapping()
    {
        // Arrange
        var input = "\"abc'";

        // Act
        var sut = new AllyariaImageValue(input);

        // Assert
        sut.Value.Should()
            .Be(@"url(""\""abc'"")");
    }

    [Fact]
    public void Ctor_Should_Throw_ArgumentException_For_Dangerous_Scheme_Javascript()
    {
        // Arrange
        var input = "javascript:alert(1)";

        // Act
        var act = () => new AllyariaImageValue(input);

        // Assert
        act.Should()
            .Throw<AllyariaArgumentException>()
            .WithMessage("*Unsupported URI scheme*");
    }

    [Fact]
    public void Ctor_Should_Throw_ArgumentException_For_Dangerous_Scheme_Vbscript()
    {
        // Arrange
        var input = "VBScript:msgbox(1)";

        // Act
        var act = () => new AllyariaImageValue(input);

        // Assert
        act.Should()
            .Throw<AllyariaArgumentException>()
            .WithMessage("*Unsupported URI scheme*");
    }

    [Fact]
    public void Ctor_Should_Throw_ArgumentException_For_Unsupported_Absolute_Scheme()
    {
        // Arrange
        var input = "ftp://example.com/image.png";

        // Act
        var act = () => new AllyariaImageValue(input);

        // Assert
        act.Should()
            .Throw<AllyariaArgumentException>()
            .WithMessage("*Unsupported URI scheme*");
    }

    [Fact]
    public void Ctor_Should_Throw_When_Input_Has_Control_Characters()
    {
        // Arrange
        var input = "ok\u0001bad";

        // Act
        var act = () => new AllyariaImageValue(input);

        // Assert
        act.Should()
            .Throw<AllyariaArgumentException>()
            .WithMessage("*Value contains control characters.*");
    }

    [Fact]
    public void Ctor_Should_Trim_And_Unwrap_Quotes_Preserving_Spaces_And_Parens()
    {
        // Arrange
        var input = "   \"images/my photo (draft).png\"   ";

        // Act
        var sut = new AllyariaImageValue(input);

        // Assert
        sut.Value.Should()
            .Be(@"url(""images/my photo (draft).png"")");
    }

    [Fact]
    public void Implicit_String_To_AllyariaImageValue_Works()
    {
        // Arrange
        AllyariaImageValue sut = "cat.png";

        // Act
        var css = (string)sut;

        // Assert
        css.Should()
            .Be(@"url(""cat.png"")");
    }

    [Fact]
    public void Parse_Should_Create_Equivalent_Instance_As_Ctor()
    {
        // Arrange
        var input = "img.png";

        // Act
        var a = new AllyariaImageValue(input);
        var b = AllyariaImageValue.Parse(input);

        // Assert
        a.Value.Should()
            .Be(b.Value);
    }

    [Fact]
    public void ToCssBackground_Should_Return_Centered_Cover_With_Black_Overlay_For_Light_Background()
    {
        // Arrange
        var img = new AllyariaImageValue("hero.jpg");
        var background = new AllyariaColorValue("#FFFFFF"); // luminance >= 0.5

        // Act
        var css = img.ToCssBackground(background);

        // Assert
        css.Should()
            .Be(
                @"background-image:linear-gradient(rgba(0, 0, 0, 0.5),rgba(0, 0, 0, 0.5)),url(""hero.jpg"");background-position:center;background-repeat:no-repeat;background-size:cover"
            );
    }

    [Fact]
    public void ToCssBackground_Should_Return_Image_Only_With_White_Overlay_For_Dark_Background_When_Not_Stretched()
    {
        // Arrange
        var img = new AllyariaImageValue("hero.jpg");
        var background = new AllyariaColorValue("#000000"); // luminance < 0.5

        // Act
        var css = img.ToCssBackground(background, false);

        // Assert
        css.Should()
            .Be(
                @"background-image:linear-gradient(rgba(255, 255, 255, 0.5),rgba(255, 255, 255, 0.5)),url(""hero.jpg"");"
            );
    }

    [Fact]
    public void ToCssVarsBackground_Should_Return_Image_Var_Only_For_Dark_Background_When_Not_Stretched()
    {
        // Arrange
        var img = new AllyariaImageValue("banner.png");
        var background = new AllyariaColorValue("#0A0A0A");
        var prefix = "--p-";

        // Act
        var css = img.ToCssVarsBackground(prefix, background, false);

        // Assert
        css.Should()
            .Be(
                @"--p-background-image:linear-gradient(rgba(255, 255, 255, 0.5),rgba(255, 255, 255, 0.5)),url(""banner.png"");"
            );
    }

    [Fact]
    public void ToCssVarsBackground_Should_Return_Vars_With_Prefix_For_Light_Background()
    {
        // Arrange
        var img = new AllyariaImageValue("banner.png");
        var background = new AllyariaColorValue("#FAFAFA");
        var prefix = "--x-";

        // Act
        var css = img.ToCssVarsBackground(prefix, background);

        // Assert
        css.Should()
            .Be(
                @"--x-background-image:linear-gradient(rgba(0, 0, 0, 0.5),rgba(0, 0, 0, 0.5)),url(""banner.png"");--x-background-position:center;--x-background-repeat:no-repeat;--x-background-size:cover"
            );
    }

    [Theory]
    [InlineData("   ")]
    [InlineData("javascript:evil()")]
    [InlineData("ftp://host/file.png")]
    public void TryParse_Should_ReturnFalse_And_Null_When_Invalid(string input)
    {
        // Act
        var ok = AllyariaImageValue.TryParse(input, out var result);

        // Assert
        ok.Should()
            .BeFalse();

        result.Should()
            .BeNull();
    }

    [Fact]
    public void TryParse_Should_ReturnTrue_And_Result_When_Valid()
    {
        // Arrange
        var input = "a/b.png";

        // Act
        var ok = AllyariaImageValue.TryParse(input, out var result);

        // Assert
        ok.Should()
            .BeTrue();

        result.Should()
            .NotBeNull();

        ((string)result).Should()
            .Be(@"url(""a/b.png"")");
    }
}

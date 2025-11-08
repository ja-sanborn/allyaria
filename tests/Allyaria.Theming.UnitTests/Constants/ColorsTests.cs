using System.Reflection;

namespace Allyaria.Theming.UnitTests.Constants;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ColorsLookupTests
{
    [Fact]
    public void BuildDictionary_Should_Include_All_Public_Static_HexColor_Members_And_Be_CaseInsensitive()
    {
        // Arrange
        var colorsType = typeof(Colors);

        var fieldCount = colorsType
            .GetFields(bindingAttr: BindingFlags.Public | BindingFlags.Static)
            .Count(predicate: f => f.FieldType == typeof(HexColor));

        var propertyCount = colorsType
            .GetProperties(bindingAttr: BindingFlags.Public | BindingFlags.Static)
            .Count(predicate: p => p.PropertyType == typeof(HexColor) && p.GetIndexParameters().Length == 0);

        var expectedTotal = fieldCount + propertyCount;

        var buildMethod = colorsType.GetMethod(
            name: "BuildDictionary", bindingAttr: BindingFlags.NonPublic | BindingFlags.Static
        );

        buildMethod.Should().NotBeNull(because: "BuildDictionary must exist on Colors.");

        // Act
        var result = (IReadOnlyDictionary<string, HexColor>)buildMethod.Invoke(obj: null, parameters: null)!;

        // Assert
        result.Count.Should().Be(expected: expectedTotal);

        result.ContainsKey(key: "red").Should().BeTrue(because: "lookup should be case-insensitive when key exists");
        result.ContainsKey(key: "ReD").Should().BeTrue();
        result[key: "red"].Should().Be(expected: Colors.Red);
        result[key: "ReD"].Should().Be(expected: Colors.Red);

        result.ContainsKey(key: nameof(Colors.White)).Should().BeTrue();
        result.ContainsKey(key: nameof(Colors.Blue500)).Should().BeTrue();

        result.ContainsKey(key: "notacolor").Should().BeFalse();
        result.ContainsKey(key: "NOTACOLOR").Should().BeFalse();
    }

    [Fact]
    public void Contains_Should_ReturnFalse_When_ColorName_DoesNot_Exist()
    {
        // Arrange
        var name = "fake_fake_fake";

        // Act
        var has = Colors.Contains(name: name);

        // Assert
        has.Should().BeFalse();
    }

    [Fact]
    public void Contains_Should_ReturnTrue_When_ColorName_Exists_Ignoring_Case()
    {
        // Arrange
        var name1 = "WHITE";
        var name2 = "royalblue";

        // Act
        var has1 = Colors.Contains(name: name1);
        var has2 = Colors.Contains(name: name2);

        // Assert
        has1.Should().BeTrue();
        has2.Should().BeTrue();
    }

    [Fact]
    public void Contains_Should_Throw_ArgumentNullException_When_Name_Is_Null()
    {
        // Arrange
        string? name = null;

        // Act
        var act = () => Colors.Contains(name: name!);

        // Assert
        act.Should().Throw<AryArgumentException>(because: "*name cannot be null, empty or whitespace*");
    }

    [Fact]
    public void TryGet_Should_ReturnFalse_And_Default_When_ColorName_DoesNot_Exist()
    {
        // Arrange
        var name = "definitely_not_a_real_color";

        // Act
        var ok = Colors.TryGet(name: name, value: out var value);

        // Assert
        ok.Should().BeFalse();
        value.Should().Be(expected: default(HexColor));
    }

    [Fact]
    public void TryGet_Should_ReturnTrue_And_OutputValue_When_ColorName_Exists_Ignoring_Case()
    {
        // Arrange
        var nameLower = "aliceblue";
        var nameMixed = "bLuE500";

        // Act
        var ok1 = Colors.TryGet(name: nameLower, value: out var c1);
        var ok2 = Colors.TryGet(name: nameMixed, value: out var c2);

        // Assert
        ok1.Should().BeTrue();
        c1.Should().Be(expected: Colors.Aliceblue);

        ok2.Should().BeTrue();
        c2.Should().Be(expected: Colors.Blue500);
    }

    [Fact]
    public void TryGet_Should_Throw_ArgumentNullException_When_Name_Is_Null()
    {
        // Arrange
        string? name = null;

        // Act
        var act = () => Colors.TryGet(name: name!, value: out _);

        // Assert
        act.Should().Throw<AryArgumentException>(because: "*name cannot be null, empty or whitespace*");
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Contains_Should_Throw_AryArgumentException_When_Name_Is_NullOrWhitespace(string? name)
    {
        // Arrange
        var invalidName = name;

        // Act
        var act = () => Colors.Contains(name: invalidName!);

        // Assert
        act.Should().Throw<AryArgumentException>(because: "*name cannot be null, empty or whitespace*");
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("")]
    [InlineData("   ")]
    public void TryGet_Should_Throw_AryArgumentException_When_Name_Is_NullOrWhitespace(string? name)
    {
        // Arrange
        var invalidName = name;

        // Act
        var act = () => Colors.TryGet(name: invalidName!, value: out _);

        // Assert
        act.Should().Throw<AryArgumentException>();
    }
}

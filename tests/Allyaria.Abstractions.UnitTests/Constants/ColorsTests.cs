using Allyaria.Abstractions.Constants;
using Allyaria.Abstractions.Types;
using System.Reflection;

namespace Allyaria.Abstractions.UnitTests.Constants;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ColorsLookupTests
{
    [Fact]
    public void BuildDictionary_Should_Include_All_Public_Static_HexColor_Members_And_Be_CaseInsensitive()
    {
        // Arrange
        var colorsType = typeof(Colors);

        var fieldCount = colorsType
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Count(f => f.FieldType == typeof(HexColor));

        var propertyCount = colorsType
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Count(p => p.PropertyType == typeof(HexColor) && p.GetIndexParameters().Length == 0);

        var expectedTotal = fieldCount + propertyCount;

        var buildMethod = colorsType.GetMethod("BuildDictionary", BindingFlags.NonPublic | BindingFlags.Static);
        buildMethod.Should().NotBeNull("BuildDictionary must exist on Colors.");

        // Act
        var result = (IReadOnlyDictionary<string, HexColor>)buildMethod!.Invoke(null, null)!;

        // Assert
        result.Count.Should().Be(expectedTotal);

        result.ContainsKey("red").Should().BeTrue("lookup should be case-insensitive when key exists");
        result.ContainsKey("ReD").Should().BeTrue();
        result["red"].Should().Be(Colors.Red);
        result["ReD"].Should().Be(Colors.Red);

        result.ContainsKey(nameof(Colors.White)).Should().BeTrue();
        result.ContainsKey(nameof(Colors.Blue500)).Should().BeTrue();

        result.ContainsKey("notacolor").Should().BeFalse();
        result.ContainsKey("NOTACOLOR").Should().BeFalse();
    }

    [Fact]
    public void Contains_Should_ReturnFalse_When_ColorName_DoesNot_Exist()
    {
        // Arrange
        var name = "fake_fake_fake";

        // Act
        var has = Colors.Contains(name);

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
        var has1 = Colors.Contains(name1);
        var has2 = Colors.Contains(name2);

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
        var act = () => Colors.Contains(name!);

        // Assert
        act.Should().Throw<ArgumentNullException>("contains on a case-insensitive dictionary throws for null key");
    }

    [Fact]
    public void TryGet_Should_ReturnFalse_And_Default_When_ColorName_DoesNot_Exist()
    {
        // Arrange
        var name = "definitely_not_a_real_color";

        // Act
        var ok = Colors.TryGet(name, out var value);

        // Assert
        ok.Should().BeFalse();
        value.Should().Be(default(HexColor));
    }

    [Fact]
    public void TryGet_Should_ReturnTrue_And_OutputValue_When_ColorName_Exists_Ignoring_Case()
    {
        // Arrange
        var nameLower = "aliceblue";
        var nameMixed = "bLuE500";

        // Act
        var ok1 = Colors.TryGet(nameLower, out var c1);
        var ok2 = Colors.TryGet(nameMixed, out var c2);

        // Assert
        ok1.Should().BeTrue();
        c1.Should().Be(Colors.Aliceblue);

        ok2.Should().BeTrue();
        c2.Should().Be(Colors.Blue500);
    }

    [Fact]
    public void TryGet_Should_Throw_ArgumentNullException_When_Name_Is_Null()
    {
        // Arrange
        string? name = null;

        // Act
        var act = () => Colors.TryGet(name!, out _);

        // Assert
        act.Should().Throw<ArgumentNullException>("the underlying dictionary lookup throws when key is null");
    }
}

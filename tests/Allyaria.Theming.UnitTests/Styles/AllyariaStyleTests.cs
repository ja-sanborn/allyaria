using Allyaria.Theming.Styles;

namespace Allyaria.Theming.UnitTests.Styles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class AllyariaStyleTests
{
    [Fact]
    public void Cascade_With_No_Args_Returns_Same_Values()
    {
        // Arrange
        var original = new AllyariaStyle();

        // Act
        var cascaded = original.Cascade();

        // Assert
        cascaded.Default.Should().Be(original.Default);
        cascaded.Disabled.Should().Be(original.Disabled);
        cascaded.Hover.Should().Be(original.Hover);
    }

    [Fact]
    public void Cascade_With_Partial_Overrides_Replaces_Only_Specified()
    {
        // Arrange
        var original = new AllyariaStyle();
        var newDefault = new AllyariaStyleVariant();
        var newDisabled = newDefault.Cascade(newDefault.Palette.ToDisabled());
        var newHover = newDefault.Cascade(newDefault.Palette.ToHover());

        // Act
        var changedDefault = original.Cascade(newDefault);
        var changedDisabled = original.Cascade(disabledStyle: newDisabled);
        var changedHover = original.Cascade(hoverStyle: newHover);
        var changedAll = original.Cascade(newDefault, newDisabled, newHover);

        // Assert
        changedDefault.Default.Should().Be(newDefault);
        changedDefault.Disabled.Should().Be(original.Disabled);
        changedDefault.Hover.Should().Be(original.Hover);

        changedDisabled.Default.Should().Be(original.Default);
        changedDisabled.Disabled.Should().Be(newDisabled);
        changedDisabled.Hover.Should().Be(original.Hover);

        changedHover.Default.Should().Be(original.Default);
        changedHover.Disabled.Should().Be(original.Disabled);
        changedHover.Hover.Should().Be(newHover);

        changedAll.Default.Should().Be(newDefault);
        changedAll.Disabled.Should().Be(newDisabled);
        changedAll.Hover.Should().Be(newHover);
    }

    [Fact]
    public void Ctor_Defaults_When_All_Null()
    {
        // Arrange
        AllyariaStyleVariant? @default = null;
        AllyariaStyleVariant? disabled = null;
        AllyariaStyleVariant? hover = null;

        // Act
        var style = new AllyariaStyle(@default, disabled, hover);

        // Assert
        style.Default.Should().NotBeNull();
        style.Disabled.Should().NotBeNull();
        style.Hover.Should().NotBeNull();

        style.Disabled.Should().NotBe(style.Default);
        style.Hover.Should().NotBe(style.Default);
    }

    [Fact]
    public void Ctor_Respects_All_Provided_Variants()
    {
        // Arrange
        var providedDefault = new AllyariaStyleVariant();
        var providedDisabled = providedDefault.Cascade(providedDefault.Palette.ToDisabled());
        var providedHover = providedDefault.Cascade(providedDefault.Palette.ToHover());

        // Act
        var style = new AllyariaStyle(providedDefault, providedDisabled, providedHover);

        // Assert
        style.Default.Should().Be(providedDefault);
        style.Disabled.Should().Be(providedDisabled);
        style.Hover.Should().Be(providedHover);
    }

    [Fact]
    public void Ctor_Uses_Provided_Variants_And_Derives_Missing_Ones()
    {
        // Arrange
        var providedDefault = new AllyariaStyleVariant();
        AllyariaStyleVariant? providedDisabled = null;
        AllyariaStyleVariant? providedHover = null;

        // Act
        var style = new AllyariaStyle(providedDefault, providedDisabled, providedHover);

        // Assert
        style.Default.Should().Be(providedDefault);
        style.Disabled.Should().NotBeNull().And.NotBe(providedDefault);
        style.Hover.Should().NotBeNull().And.NotBe(providedDefault);
    }
}

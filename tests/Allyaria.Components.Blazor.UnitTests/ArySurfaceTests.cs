using System.Reflection;

namespace Allyaria.Components.Blazor.UnitTests;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
[SuppressMessage(category: "Usage", checkId: "BL0005:Component parameter should not be set outside of its component.")]
public sealed class ArySurfaceTests : BunitContext
{
    private readonly IThemingService _themingService;

    public ArySurfaceTests()
    {
        _themingService = Substitute.For<IThemingService>();
        Services.AddSingleton(implementationInstance: _themingService);
    }

    [Fact]
    public void BaseClass_Should_ReturnArySurfaceCssClass_When_AccessedThroughReflection()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService
        };

        // Act
        var baseClass = GetProtectedProperty<string>(instance: sut, propertyName: "BaseClass");

        // Assert
        baseClass.Should().Be(expected: "ary-surface");
    }

    [Fact]
    public void ComponentType_Should_ReturnSurface_When_AccessedThroughReflection()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService
        };

        // Act
        var componentType = GetProtectedProperty<ComponentType>(instance: sut, propertyName: "ComponentType");

        // Assert
        componentType.Should().Be(expected: ComponentType.Surface);
    }

    [Fact]
    public void DerivedStyle_Should_CombineUserStyleAndThemeStyles_When_StyleProvided()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            ComponentState = ComponentState.Default,
            ThemeType = ThemeType.Dark,
            EffectiveThemeType = ThemeType.Light,
            Style = "background:blue;"
        };

        _themingService
            .GetComponentCssVars(
                themeType: ThemeType.Dark, componentType: ComponentType.Surface, componentState: ComponentState.Default
            )
            .Returns(returnThis: "color:red;");

        // Act
        var derivedStyle = GetProtectedProperty<string?>(instance: sut, propertyName: "DerivedStyle");

        // Assert
        derivedStyle.Should().NotBeNull();
        derivedStyle.Should().Contain(expected: "background:blue;");
        derivedStyle.Should().Contain(expected: "color:red;");

        _themingService.Received(requiredNumberOfCalls: 1)
            .GetComponentCssVars(
                themeType: ThemeType.Dark, componentType: ComponentType.Surface, componentState: ComponentState.Default
            );
    }

    [Fact]
    public void DerivedStyle_Should_ReturnNull_When_StyleAndThemeStylesAreEmpty()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            ComponentState = ComponentState.Default,
            ThemeType = ThemeType.System,
            EffectiveThemeType = ThemeType.System,
            Style = "   "
        };

        _themingService
            .GetComponentCssVars(
                themeType: ThemeType.System, componentType: ComponentType.Surface,
                componentState: ComponentState.Default
            )
            .Returns(returnThis: "   ");

        // Act
        var derivedStyle = GetProtectedProperty<string?>(instance: sut, propertyName: "DerivedStyle");

        // Assert
        derivedStyle.Should().BeNull();
    }

    [Fact]
    public void DerivedStyle_Should_ReturnThemeStyles_When_StyleIsNull()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            ComponentState = ComponentState.Default,
            ThemeType = ThemeType.System,
            EffectiveThemeType = ThemeType.Light,
            Style = null
        };

        _themingService
            .GetComponentCssVars(
                themeType: ThemeType.System, componentType: ComponentType.Surface,
                componentState: ComponentState.Default
            )
            .Returns(returnThis: "color:red;");

        // Act
        var derivedStyle = GetProtectedProperty<string?>(instance: sut, propertyName: "DerivedStyle");

        // Assert
        derivedStyle.Should().Be(expected: "color:red;");

        _themingService.Received(requiredNumberOfCalls: 1)
            .GetComponentCssVars(
                themeType: ThemeType.System, componentType: ComponentType.Surface,
                componentState: ComponentState.Default
            );
    }

    [Fact]
    public void GetFilteredAttributes_Should_ExcludeManagedAttributes_When_OnlyDisallowedAttributesPresent()
    {
        // Arrange
        var additional = new Dictionary<string, object>(comparer: StringComparer.OrdinalIgnoreCase)
        {
            [key: "class"] = "cls",
            [key: "aria-label"] = "lbl",
            [key: "style"] = "color:red;",
            [key: "tabindex"] = 1,
            [key: "id"] = "id"
        };

        var sut = new ArySurface
        {
            ThemingService = _themingService,
            AdditionalAttributes = additional
        };

        // Act
        var result = InvokeGetFilteredAttributes(instance: sut, additionalDisallowed: Array.Empty<string>());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetFilteredAttributes_Should_FilterUsingAdditionalDisallowed_When_Provided()
    {
        // Arrange
        var additional = new Dictionary<string, object>(comparer: StringComparer.OrdinalIgnoreCase)
        {
            [key: "data-keep"] = "keep",
            [key: "DATA-REMOVE"] = "remove",
            [key: "aria-label"] = "ignored"
        };

        var sut = new ArySurface
        {
            ThemingService = _themingService,
            AdditionalAttributes = additional
        };

        // Act
        var result = InvokeGetFilteredAttributes(instance: sut, "data-remove", "   ");

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(expected: 1);
        result.Should().ContainKey(expected: "data-keep");
        result.Should().ContainValue(expected: "keep");
    }

    [Fact]
    public void GetFilteredAttributes_Should_ReturnNull_When_AdditionalAttributesIsEmpty()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            AdditionalAttributes = new Dictionary<string, object>()
        };

        // Act
        var result = InvokeGetFilteredAttributes(instance: sut, additionalDisallowed: Array.Empty<string>());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetFilteredAttributes_Should_ReturnNull_When_AdditionalAttributesIsNull()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            AdditionalAttributes = null
        };

        // Act
        var result = InvokeGetFilteredAttributes(instance: sut, additionalDisallowed: Array.Empty<string>());

        // Assert
        result.Should().BeNull();
    }

    private static T GetProtectedProperty<T>(object instance, string propertyName)
    {
        var property = instance.GetType().GetProperty(
            name: propertyName,
            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
        );

        property.Should().NotBeNull(because: $"Property '{propertyName}' should exist on {instance.GetType().Name}");

        var value = property.GetValue(obj: instance);

        return value is T typed
            ? typed
            : default(T)!;
    }

    private static IReadOnlyDictionary<string, object>? InvokeGetFilteredAttributes(ArySurface instance,
        params string[] additionalDisallowed)
    {
        var method = typeof(ArySurface).GetMethod(
            name: "GetFilteredAttributes",
            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        );

        method.Should().NotBeNull(because: "GetFilteredAttributes should exist on ArySurface");

        var result = method.Invoke(
            obj: instance, parameters: new object[]
            {
                additionalDisallowed
            }
        );

        return (IReadOnlyDictionary<string, object>?)result;
    }

    [Fact]
    public void Refresh_Should_NotThrow_When_CalledOnRenderedComponent()
    {
        // Arrange
        var rendered = Render<ArySurface>(
            parameterBuilder: parameters => parameters
                .Add(parameterSelector: p => p.ComponentState, value: ComponentState.Default)
                .AddChildContent(markup: "content")
        );

        var instance = rendered.Instance;

        var method = typeof(ArySurface).GetMethod(
            name: "Refresh", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        );

        method.Should().NotBeNull();

        // Act
        var act = () => method.Invoke(obj: instance, parameters: Array.Empty<object>());

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public async Task RefreshAsync_Should_NotThrowAndReturnTask_When_CalledOnRenderedComponent()
    {
        // Arrange
        var rendered = Render<ArySurface>(
            parameterBuilder: parameters => parameters
                .Add(parameterSelector: p => p.ComponentState, value: ComponentState.Default)
                .AddChildContent(markup: "content")
        );

        var instance = rendered.Instance;

        var method = typeof(ArySurface).GetMethod(
            name: "RefreshAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        );

        method.Should().NotBeNull();

        // Act
        var act = async () =>
        {
            var task = (Task)method.Invoke(obj: instance, parameters: Array.Empty<object>())!;
            await task;
        };

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public void Render_Should_ApplyAccessibilityAttributes_When_ParametersProvided()
    {
        // Arrange
        _themingService
            .GetComponentCssVars(
                themeType: Arg.Any<ThemeType>(), componentType: Arg.Any<ComponentType>(),
                componentState: Arg.Any<ComponentState>()
            )
            .Returns(returnThis: "color:red;");

        var sut = Render<ArySurface>(
            parameterBuilder: parameters => parameters
                .Add(parameterSelector: p => p.ComponentState, value: ComponentState.Default)
                .Add(parameterSelector: p => p.AriaDescribedBy, value: "desc-id")
                .Add(parameterSelector: p => p.AriaHidden, value: true)
                .Add(parameterSelector: p => p.AriaLabel, value: "label-text")
                .Add(parameterSelector: p => p.AriaLabelledBy, value: "labelled-by-id")
                .Add(parameterSelector: p => p.AriaRole, value: "button")
                .Add(parameterSelector: p => p.Id, value: "surface-id")
                .Add(parameterSelector: p => p.TabIndex, value: 3)
                .AddChildContent(markup: "content")
        );

        // Act
        var div = sut.Find(cssSelector: "div");

        // Assert
        div.GetAttribute(name: "aria-describedby").Should().Be(expected: "desc-id");
        div.GetAttribute(name: "aria-hidden").Should().Be(expected: "true");
        div.GetAttribute(name: "aria-label").Should().Be(expected: "label-text");
        div.GetAttribute(name: "aria-labelledby").Should().Be(expected: "labelled-by-id");
        div.GetAttribute(name: "role").Should().Be(expected: "button");
        div.GetAttribute(name: "id").Should().Be(expected: "surface-id");
        div.GetAttribute(name: "tabindex").Should().Be(expected: "3");
        div.GetAttribute(name: "style").Should().Contain(expected: "color:red;");
    }

    [Fact]
    public void Render_Should_CombineBaseClassAndAdditionalClass_When_ClassProvided()
    {
        // Arrange
        _themingService
            .GetComponentCssVars(
                themeType: Arg.Any<ThemeType>(), componentType: Arg.Any<ComponentType>(),
                componentState: Arg.Any<ComponentState>()
            )
            .Returns(returnThis: (string?)null);

        var sut = Render<ArySurface>(
            parameterBuilder: parameters => parameters
                .Add(parameterSelector: p => p.ComponentState, value: ComponentState.Default)
                .Add(parameterSelector: p => p.Class, value: "custom-class")
                .AddChildContent(markup: "content")
        );

        // Act
        var div = sut.Find(cssSelector: "div");

        // Assert
        div.ClassList.Should().Contain(expected: "ary-surface");
        div.ClassList.Should().Contain(expected: "custom-class");
    }

    [Fact]
    public void Render_Should_IncludeAdditionalHtmlAttributes_When_AttributesNotManagedByComponent()
    {
        // Arrange
        var additional = new Dictionary<string, object>(comparer: StringComparer.OrdinalIgnoreCase)
        {
            [key: "data-test"] = "123",
            [key: "custom-attr"] = "custom",
            [key: "class"] = "ignored-class",
            [key: "id"] = "ignored-id",
            [key: "role"] = "ignored-role",
            [key: "style"] = "color:blue;"
        };

        _themingService
            .GetComponentCssVars(
                themeType: Arg.Any<ThemeType>(), componentType: Arg.Any<ComponentType>(),
                componentState: Arg.Any<ComponentState>()
            )
            .Returns(returnThis: "color:red;");

        var sut = Render<ArySurface>(
            parameterBuilder: parameters => parameters
                .Add(parameterSelector: p => p.ComponentState, value: ComponentState.Default)
                .Add(parameterSelector: p => p.AdditionalAttributes, value: additional)
                .AddChildContent(markup: "content")
        );

        // Act
        var div = sut.Find(cssSelector: "div");

        // Assert
        div.GetAttribute(name: "data-test").Should().Be(expected: "123");
        div.GetAttribute(name: "custom-attr").Should().Be(expected: "custom");
        div.GetAttribute(name: "class").Should().Contain(expected: "ary-surface");
        div.GetAttribute(name: "id").Should().NotBe(unexpected: "ignored-id");
        div.GetAttribute(name: "role").Should().NotBe(unexpected: "ignored-role");
        div.GetAttribute(name: "style").Should().Contain(expected: "color:red;");
    }

    [Fact]
    public void Render_Should_NotRenderDiv_When_ComponentStateIsHidden()
    {
        // Arrange
        var sut = Render<ArySurface>(
            parameterBuilder: parameters => parameters
                .Add(parameterSelector: p => p.ComponentState, value: ComponentState.Hidden)
                .AddChildContent(markup: "<span>content</span>")
        );

        // Act
        var markup = sut.Markup;

        // Assert
        markup.Should().BeEmpty();
    }

    [Fact]
    public void Render_Should_OmitOptionalAttributes_When_ValuesNotProvided()
    {
        // Arrange
        _themingService
            .GetComponentCssVars(
                themeType: Arg.Any<ThemeType>(), componentType: Arg.Any<ComponentType>(),
                componentState: Arg.Any<ComponentState>()
            )
            .Returns(returnThis: (string?)null);

        var sut = Render<ArySurface>(
            parameterBuilder: parameters => parameters
                .Add(parameterSelector: p => p.ComponentState, value: ComponentState.Default)
                .AddChildContent(markup: "content")
        );

        // Act
        var div = sut.Find(cssSelector: "div");

        // Assert
        div.HasAttribute(name: "aria-describedby").Should().BeFalse();
        div.HasAttribute(name: "aria-hidden").Should().BeFalse();
        div.HasAttribute(name: "aria-label").Should().BeFalse();
        div.HasAttribute(name: "aria-labelledby").Should().BeFalse();
        div.HasAttribute(name: "role").Should().BeFalse();
        div.HasAttribute(name: "tabindex").Should().BeFalse();
        div.HasAttribute(name: "style").Should().BeFalse();
    }

    [Fact]
    public void Render_Should_RenderDivAndChildContent_When_ComponentStateIsDefault()
    {
        // Arrange
        _themingService
            .GetComponentCssVars(
                themeType: Arg.Any<ThemeType>(), componentType: Arg.Any<ComponentType>(),
                componentState: Arg.Any<ComponentState>()
            )
            .Returns(returnThis: (string?)null);

        var sut = Render<ArySurface>(
            parameterBuilder: parameters => parameters
                .Add(parameterSelector: p => p.ComponentState, value: ComponentState.Default)
                .AddChildContent(markup: "<span>content</span>")
        );

        // Act
        var div = sut.Find(cssSelector: "div");

        // Assert
        div.TextContent.Should().Contain(expected: "content");
        div.TagName.Should().Be(expected: "DIV");
    }

    [Fact]
    public void ResolvedAriaHidden_Should_ReturnNull_When_AriaHiddenIsNullOrFalse()
    {
        // Arrange
        var sutFalse = new ArySurface
        {
            ThemingService = _themingService,
            AriaHidden = false
        };

        var sutNull = new ArySurface
        {
            ThemingService = _themingService,
            AriaHidden = null
        };

        // Act
        var resolvedFalse = GetProtectedProperty<string?>(instance: sutFalse, propertyName: "ResolvedAriaHidden");
        var resolvedNull = GetProtectedProperty<string?>(instance: sutNull, propertyName: "ResolvedAriaHidden");

        // Assert
        resolvedFalse.Should().BeNull();
        resolvedNull.Should().BeNull();
    }

    [Fact]
    public void ResolvedAriaHidden_Should_ReturnTrueString_When_AriaHiddenIsTrue()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            AriaHidden = true
        };

        // Act
        var resolved = GetProtectedProperty<string?>(instance: sut, propertyName: "ResolvedAriaHidden");

        // Assert
        resolved.Should().Be(expected: "true");
    }

    [Fact]
    public void ResolvedTabIndex_Should_ReturnNull_When_TabIndexIsNull()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            TabIndex = null
        };

        // Act
        var resolved = GetProtectedProperty<string?>(instance: sut, propertyName: "ResolvedTabIndex");

        // Assert
        resolved.Should().BeNull();
    }

    [Fact]
    public void ResolvedTabIndex_Should_ReturnStringValue_When_TabIndexIsSet()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            TabIndex = 5
        };

        // Act
        var resolved = GetProtectedProperty<string?>(instance: sut, propertyName: "ResolvedTabIndex");

        // Assert
        resolved.Should().Be(expected: "5");
    }

    [Fact]
    public void ResolvedThemeType_Should_ReturnEffectiveTheme_When_ThemeTypeIsSystem()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            ThemeType = ThemeType.System,
            EffectiveThemeType = ThemeType.Dark
        };

        // Act
        var resolved = GetProtectedProperty<ThemeType>(instance: sut, propertyName: "ResolvedThemeType");

        // Assert
        resolved.Should().Be(expected: ThemeType.Dark);
    }

    [Fact]
    public void ResolvedThemeType_Should_ReturnExplicitTheme_When_ThemeTypeIsNotSystem()
    {
        // Arrange
        var sut = new ArySurface
        {
            ThemingService = _themingService,
            ThemeType = ThemeType.Light,
            EffectiveThemeType = ThemeType.Dark
        };

        // Act
        var resolved = GetProtectedProperty<ThemeType>(instance: sut, propertyName: "ResolvedThemeType");

        // Assert
        resolved.Should().Be(expected: ThemeType.Light);
    }
}

using Allyaria.Theming.Contracts;
using Allyaria.Theming.Enumerations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Reflection;

namespace Allyaria.Theming.Blazor.UnitTests;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class AryThemeProviderTests : BunitContext
{
    [Fact]
    public async Task DetectThemeAsync_Should_ReturnDark_When_ModuleDetectsDark()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var module = new TestJsModule
        {
            DetectResult = "dark"
        };

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CancellationTokenSource());

        // Act
        var result = await sut.DetectThemeAsync();

        // Assert
        result.Should().Be(expected: ThemeType.Dark);
    }

    [Fact]
    public async Task DetectThemeAsync_Should_ReturnNull_When_CancellationRequested()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var module = new TestJsModule
        {
            DetectResult = "dark"
        };

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: cts);

        // Act
        var result = await sut.DetectThemeAsync(cancellationToken: cts.Token);

        // Assert
        result.Should().BeNull();
        module.Calls.Should().BeEmpty(because: "detection should short-circuit when cancellation is requested");
    }

    [Fact]
    public async Task DetectThemeAsync_Should_ReturnNull_When_JsInteropThrows()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var throwingModule = new ThrowingJsObjectReference();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        typeof(AryThemeProvider)
            .GetField(name: "_module", bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance)!
            .SetValue(obj: sut, value: throwingModule);

        // Act
        var result = await sut.DetectThemeAsync(cancellationToken: CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DetectThemeAsync_Should_ReturnNull_When_JsThrowsAndNotCancelled()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var module = new TestJsModule
        {
            ThrowOnDetect = true
        };

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CancellationTokenSource());

        // Act
        var result = await sut.DetectThemeAsync();

        // Assert
        result.Should().BeNull();
        module.Calls.Should().ContainSingle(predicate: c => c.Identifier == "detect");
    }

    [Fact]
    public async Task DetectThemeAsync_Should_ReturnNull_When_ModuleDetectsSystem()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var module = new TestJsModule
        {
            DetectResult = "system"
        };

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CancellationTokenSource());

        // Act
        var result = await sut.DetectThemeAsync();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DetectThemeAsync_Should_ReturnNull_When_ModuleIsNull()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        // Act
        var result = await sut.DetectThemeAsync(cancellationToken: CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DisposeAsync_Should_DisposeModule_And_CancelCts_WithoutThrowing()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var module = Substitute.For<IJSObjectReference>();
        var themingService = Substitute.For<IThemingService>();

        Services.AddSingleton(implementationInstance: jsRuntime);
        Services.AddSingleton(implementationInstance: themingService);

        jsRuntime
            .InvokeAsync<IJSObjectReference>(
                identifier: "import",
                cancellationToken: Arg.Any<CancellationToken>(),
                args: Arg.Any<object[]?>()
            )
            .Returns(returnThis: new ValueTask<IJSObjectReference>(result: module));

        var cut = Render<AryThemeProvider>(
            parameterBuilder: p => p
                .AddChildContent(markup: "<p>content</p>")
        );

        var sut = cut.Instance;

        // Act
        var act = async () => await sut.DisposeAsync();

        // Assert
        await act.Should().NotThrowAsync();

        await module.Received(requiredNumberOfCalls: 1).InvokeVoidAsync(
            identifier: "dispose",
            args: Arg.Any<object[]?>()
        );

        await module.Received(requiredNumberOfCalls: 1).DisposeAsync();
    }

    [Fact]
    public async Task GetStoredTypeAsync_Should_ReturnStoredTheme_When_ModuleReturnsValidValue()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var module = new TestJsModule
        {
            StoredThemeResult = "Dark"
        };

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CancellationTokenSource());

        var method = type.GetMethod(
            name: "GetStoredTypeAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task = (Task<ThemeType>)method.Invoke(
            obj: sut, parameters: new object?[]
            {
                CancellationToken.None
            }
        )!;

        var result = await task;

        // Assert
        result.Should().Be(expected: ThemeType.Dark);
        module.Calls.Should().ContainSingle(predicate: c => c.Identifier == "getStoredTheme");
    }

    [Fact]
    public async Task GetStoredTypeAsync_Should_ReturnSystem_When_JsThrows()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var module = new TestJsModule
        {
            ThrowOnGetStoredTheme = true
        };

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CancellationTokenSource());

        var method = type.GetMethod(
            name: "GetStoredTypeAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task = (Task<ThemeType>)method.Invoke(
            obj: sut, parameters: new object?[]
            {
                CancellationToken.None
            }
        )!;

        var result = await task;

        // Assert
        result.Should().Be(expected: ThemeType.System);
        module.Calls.Should().ContainSingle(predicate: c => c.Identifier == "getStoredTheme");
    }

    [Fact]
    public async Task GetStoredTypeAsync_Should_ReturnSystem_When_ModuleIsNull()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        var method = type.GetMethod(
            name: "GetStoredTypeAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task = (Task<ThemeType>)method.Invoke(
            obj: sut, parameters: new object?[]
            {
                CancellationToken.None
            }
        )!;

        var result = await task;

        // Assert
        result.Should().Be(expected: ThemeType.System);
    }

    [Fact]
    public void
        OnAfterRender_Should_ImportModule_StartDetection_And_SetEffectiveType_When_StoredTypeIsSystemAndDetectionSucceeds()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var module = Substitute.For<IJSObjectReference>();
        var themingService = Substitute.For<IThemingService>();

        Services.AddSingleton(implementationInstance: jsRuntime);
        Services.AddSingleton(implementationInstance: themingService);

        themingService.StoredType.Returns(returnThis: ThemeType.System);
        themingService.GetDocumentCss().Returns(returnThis: "body{color:black;}");

        jsRuntime
            .InvokeAsync<IJSObjectReference>(
                identifier: "import",
                cancellationToken: Arg.Any<CancellationToken>(),
                args: Arg.Any<object?[]?>()
            )
            .Returns(returnThis: new ValueTask<IJSObjectReference>(result: module));

        var cut = Render<AryThemeProvider>(
            parameterBuilder: p => p
                .Add(
                    parameterSelector: c => c.ChildContent, value: builder =>
                    {
                        builder.OpenComponent<ThemeConsumer>(sequence: 0);
                        builder.CloseComponent();
                    }
                )
        );

        // Act
        var themeSpan = cut.Find(cssSelector: "#theme-type");

        // Assert
        themeSpan.TextContent.Should().Be(expected: nameof(ThemeType.System));

        jsRuntime.Received(requiredNumberOfCalls: 1).InvokeAsync<IJSObjectReference>(
            identifier: "import",
            cancellationToken: Arg.Any<CancellationToken>(),
            args: Arg.Any<object?[]?>()
        );

        themingService.Received(requiredNumberOfCalls: 1).SetEffectiveType(themeType: ThemeType.Light);
    }

    [Fact]
    public async Task OnAfterRenderAsync_Should_ReturnImmediately_When_NotFirstRender()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        Services.AddSingleton(implementationInstance: jsRuntime);
        Services.AddSingleton(implementationInstance: themingService);

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        var method = type.GetMethod(
            name: "OnAfterRenderAsync",
            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task = (Task)method.Invoke(
            obj: sut, parameters: new object?[]
            {
                false
            }
        )!;

        await task;

        // Assert
        await jsRuntime.DidNotReceive().InvokeAsync<IJSObjectReference>(
            identifier: "import",
            cancellationToken: Arg.Any<CancellationToken>(),
            args: Arg.Any<object?[]?>()
        );

        var ctsField = type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!;
        ctsField.GetValue(obj: sut).Should().BeNull();
    }

    [Fact]
    public async Task OnAfterRenderAsync_Should_SetStoredType_When_StoredTypeDiffersFromService()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        Services.AddSingleton(implementationInstance: jsRuntime);
        Services.AddSingleton(implementationInstance: themingService);

        themingService.StoredType.Returns(returnThis: ThemeType.Dark);

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new ThrowingJsObjectReference());

        var method = type.GetMethod(
            name: "OnAfterRenderAsync",
            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task = (Task)method.Invoke(
            obj: sut, parameters: new object?[]
            {
                true
            }
        )!;

        await task;

        // Assert
        themingService.Received(requiredNumberOfCalls: 1).SetStoredType(themeType: ThemeType.System);
    }

    [Fact]
    public async Task OnParametersSetAsync_Should_CallSetDirection_When_CultureChangesViaCurrentUICulture()
    {
        // Arrange
        var jsRuntime = new RecordingJsRuntime();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_lastCulture", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CultureInfo(name: "en-US"));

        var originalCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = new CultureInfo(name: "fr-FR");

        try
        {
            var method = type.GetMethod(
                name: "OnParametersSetAsync",
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
            )!;

            // Act
            var task = (Task)method.Invoke(obj: sut, parameters: Array.Empty<object?>())!;
            await task;
        }
        finally
        {
            CultureInfo.CurrentUICulture = originalCulture;
        }

        // Assert
        jsRuntime.Calls.Should().NotBeEmpty(
            because: "SetDirectionAsync should be invoked when the effective culture (via CurrentUICulture) changes"
        );

        var lastCulture = (CultureInfo?)type
            .GetField(name: "_lastCulture", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .GetValue(obj: sut);

        lastCulture.Should().NotBeNull();
        lastCulture.Name.Should().Be(expected: "fr-FR");
    }

    [Fact]
    public async Task OnParametersSetAsync_Should_CallSetDirection_When_LastCultureIsNull()
    {
        // Arrange
        var jsRuntime = new RecordingJsRuntime();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService,
            Culture = new CultureInfo(name: "en-US")
        };

        var type = typeof(AryThemeProvider);

        var method = type.GetMethod(
            name: "OnParametersSetAsync",
            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task = (Task)method.Invoke(obj: sut, parameters: Array.Empty<object?>())!;
        await task;

        // Assert
        jsRuntime.Calls.Should().NotBeEmpty(because: "SetDirectionAsync should be invoked when last culture is unset");

        var lastCulture = (CultureInfo?)type
            .GetField(name: "_lastCulture", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .GetValue(obj: sut);

        lastCulture.Should().NotBeNull();
        lastCulture.Name.Should().Be(expected: "en-US");
    }

    [Fact]
    public async Task OnParametersSetAsync_Should_NotCallSetDirection_When_CultureNameUnchanged()
    {
        // Arrange
        var jsRuntime = new RecordingJsRuntime();
        var themingService = Substitute.For<IThemingService>();
        var culture = new CultureInfo(name: "en-US");

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService,
            Culture = culture
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_lastCulture", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CultureInfo(name: culture.Name));

        var method = type.GetMethod(
            name: "OnParametersSetAsync",
            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task = (Task)method.Invoke(obj: sut, parameters: Array.Empty<object?>())!;
        await task;

        // Assert
        jsRuntime.Calls.Should().BeEmpty(
            because: "SetDirectionAsync should not be called when the culture name has not changed"
        );

        var lastCulture = (CultureInfo?)type
            .GetField(name: "_lastCulture", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .GetValue(obj: sut);

        lastCulture.Should().NotBeNull();
        lastCulture.Name.Should().Be(expected: culture.Name);
    }

    [Fact]
    public void OnThemeChangedAsync_Should_NotDoWork_When_CtsIsNull()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        // Ensure _cts is null
        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: null);

        var method = type.GetMethod(
            name: "OnThemeChangedAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        method.Invoke(
            obj: sut, parameters: new object?[]
            {
                null,
                EventArgs.Empty
            }
        );

        // Assert
        themingService.DidNotReceive().SetEffectiveType(themeType: Arg.Any<ThemeType>());
    }

    [Fact]
    public void OnThemeChangedAsync_Should_ReturnEarly_When_EffectiveTypeUnchanged()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        themingService.StoredType.Returns(returnThis: ThemeType.Dark);
        themingService.EffectiveType.Returns(returnThis: ThemeType.Dark);

        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var type = typeof(AryThemeProvider);

        type.GetField(name: "_cts", bindingAttr: flags)!.SetValue(obj: sut, value: new CancellationTokenSource());

        type.GetField(name: "_storedType", bindingAttr: flags)!.SetValue(obj: sut, value: ThemeType.Dark);
        type.GetField(name: "_effectiveType", bindingAttr: flags)!.SetValue(obj: sut, value: ThemeType.Dark);

        var effectiveField = type.GetField(name: "_effectiveType", bindingAttr: flags)!;
        var effectiveBefore = (ThemeType)effectiveField.GetValue(obj: sut)!;

        var method = type.GetMethod(name: "OnThemeChangedAsync", bindingAttr: flags)!;

        // Act
        method.Invoke(
            obj: sut, parameters: new object?[]
            {
                null,
                EventArgs.Empty
            }
        );

        // Assert
        var effectiveAfter = (ThemeType)effectiveField.GetValue(obj: sut)!;
        effectiveAfter.Should().Be(expected: effectiveBefore);
    }

    [Fact]
    public void OnThemeChangedAsync_Should_UpdateStoredAndEffectiveType_When_ValuesDiffer()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();
        var module = new TestJsModule();

        themingService.StoredType.Returns(returnThis: ThemeType.HighContrastLight);
        themingService.EffectiveType.Returns(returnThis: ThemeType.HighContrastDark);

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CancellationTokenSource());

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_storedType", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: ThemeType.Dark);

        type.GetField(name: "_effectiveType", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: ThemeType.Light);

        var method = type.GetMethod(
            name: "OnThemeChangedAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        method.Invoke(
            obj: sut, parameters: new object?[]
            {
                null,
                EventArgs.Empty
            }
        );

        // Assert
        var storedField = type.GetField(
            name: "_storedType", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        var effectiveField = type.GetField(
            name: "_effectiveType", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        ((ThemeType)storedField.GetValue(obj: sut)!).Should().Be(expected: ThemeType.HighContrastLight);
        ((ThemeType)effectiveField.GetValue(obj: sut)!).Should().Be(expected: ThemeType.HighContrastDark);
    }

    [Fact]
    public void Render_Should_RenderHostDiv_StyleTag_AndChildContent_When_ComponentIsRendered()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var module = Substitute.For<IJSObjectReference>();
        var themingService = Substitute.For<IThemingService>();

        Services.AddSingleton(implementationInstance: jsRuntime);
        Services.AddSingleton(implementationInstance: themingService);

        themingService.StoredType.Returns(returnThis: ThemeType.System);
        themingService.GetDocumentCss().Returns(returnThis: "body{color:red;}");

        jsRuntime
            .InvokeAsync<IJSObjectReference>(
                identifier: "import",
                cancellationToken: Arg.Any<CancellationToken>(),
                args: Arg.Any<object?[]?>()
            )
            .Returns(returnThis: new ValueTask<IJSObjectReference>(result: module));

        var cut = Render<AryThemeProvider>(
            parameterBuilder: p =>
                p.AddChildContent(markup: "<p id=\"inner\">Hello</p>")
        );

        // Act
        var hostDiv = cut.Find(cssSelector: "div[hidden][aria-hidden=\"true\"]");
        var styleTag = cut.Find(cssSelector: "style#ary-theme-style");
        var inner = cut.Find(cssSelector: "#inner");

        // Assert
        hostDiv.Should().NotBeNull();
        styleTag.TextContent.Should().Be(expected: "body{color:red;}");
        inner.TextContent.Should().Be(expected: "Hello");
    }

    [Fact]
    public async Task SetDirectionAsync_Should_NotThrow_When_JsInteropThrows()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService,
            Culture = new CultureInfo(name: "en-US")
        };

        jsRuntime
            .InvokeAsync<object>(
                identifier: Arg.Any<string>(),
                args: Arg.Any<object?[]?>()
            )
            .Returns(
                returnThis: _ => new ValueTask<object>(
                    task: Task.FromException<object>(exception: new JSException(message: "boom"))
                )
            );

        // Act
        var act = async () => await sut.SetDirectionAsync();

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task SetDirectionAsync_Should_SetDirLangAndRtlClass_When_CultureIsRtl()
    {
        // Arrange
        var jsRuntime = new RecordingJsRuntime();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService,
            Culture = new CultureInfo(name: "ar-SA")
        };

        // Act
        await sut.SetDirectionAsync();

        // Assert
        jsRuntime.Calls.Should().ContainSingle(
            predicate: c =>
                c.Identifier == "document.documentElement.setAttribute" &&
                c.Args.Length == 2 &&
                Equals(c.Args[0], "dir") &&
                Equals(c.Args[1], "rtl")
        );

        jsRuntime.Calls.Should().ContainSingle(
            predicate: c =>
                c.Identifier == "document.documentElement.setAttribute" &&
                c.Args.Length == 2 &&
                Equals(c.Args[0], "lang") &&
                c.Args[1]!.ToString()!.StartsWith("ar", StringComparison.OrdinalIgnoreCase) == true
        );

        jsRuntime.Calls.Should().ContainSingle(
            predicate: c =>
                c.Identifier == "(d,cls,add)=>add?d.body.classList.add(cls):d.body.classList.remove(cls)" &&
                c.Args.Length == 3 &&
                Equals(c.Args[0], "document") &&
                Equals(c.Args[1], "rtl") &&
                Equals(c.Args[2], true)
        );
    }

    [Theory]
    [InlineData("dark", ThemeType.Dark)]
    [InlineData("light", ThemeType.Light)]
    [InlineData("highcontrast", ThemeType.HighContrastLight)]
    [InlineData("hc", ThemeType.HighContrastLight)]
    [InlineData("forced", ThemeType.HighContrastLight)]
    [InlineData("highcontrastlight", ThemeType.HighContrastLight)]
    [InlineData("hcl", ThemeType.HighContrastLight)]
    [InlineData("highcontrastdark", ThemeType.HighContrastDark)]
    [InlineData("hcd", ThemeType.HighContrastDark)]
    [InlineData("unknown", ThemeType.System)]
    [InlineData(null, ThemeType.System)]
    public void SetFromJs_Should_UpdateEffectiveTypeViaService_When_RawValueProvided(string? raw, ThemeType expected)
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        // Act
        sut.SetFromJs(raw: raw!);

        // Assert
        if (expected is ThemeType.System)
        {
            themingService.DidNotReceive().SetEffectiveType(themeType: Arg.Any<ThemeType>());
        }
        else
        {
            themingService.Received(requiredNumberOfCalls: 1).SetEffectiveType(themeType: expected);
        }
    }

    [Fact]
    public async Task SetStoredTypeAsync_Should_CallSetStoredTheme_When_ModulePresentAndNotCancelled()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();
        var module = new TestJsModule();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CancellationTokenSource());

        type.GetField(name: "_storedType", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: ThemeType.Dark);

        var method = type.GetMethod(
            name: "SetStoredTypeAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task = (Task)method.Invoke(
            obj: sut, parameters: new object?[]
            {
                CancellationToken.None
            }
        )!;

        await task;

        // Assert
        module.Calls.Should().ContainSingle(
            predicate: c =>
                c.Identifier == "setStoredTheme" &&
                c.Args!.Length == 2 &&
                c.Args[1]!.Equals("Dark")
        );
    }

    [Fact]
    public async Task SetStoredTypeAsync_Should_SwallowJsException_When_ModuleThrows()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var module = new TestJsModule
        {
            ThrowOnSetStoredTheme = true
        };

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CancellationTokenSource());

        type.GetField(name: "_storedType", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: ThemeType.Dark);

        var method = type.GetMethod(
            name: "SetStoredTypeAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var act = async () =>
        {
            var task = (Task)method.Invoke(
                obj: sut, parameters: new object?[]
                {
                    CancellationToken.None
                }
            )!;

            await task;
        };

        // Assert
        await act.Should().NotThrowAsync();
        module.Calls.Should().ContainSingle(predicate: c => c.Identifier == "setStoredTheme");
    }

    [Fact]
    public async Task StartDetectAsync_Should_SwallowException_And_ResetDetection_When_InitThrows()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: flags)!.SetValue(obj: sut, value: new ThrowingJsObjectReference());
        type.GetField(name: "_cts", bindingAttr: flags)!.SetValue(obj: sut, value: new CancellationTokenSource());
        type.GetField(name: "_isStarted", bindingAttr: flags)!.SetValue(obj: sut, value: false);

        var method = type.GetMethod(name: "StartDetectAsync", bindingAttr: flags)!;

        // Act
        var act = async () =>
        {
            var task = (Task)method.Invoke(
                obj: sut, parameters: new object?[]
                {
                    CancellationToken.None
                }
            )!;

            await task;
        };

        // Assert
        await act.Should().NotThrowAsync();

        var dotNetRefField = type.GetField(name: "_dotNetRef", bindingAttr: flags)!;
        var isStartedField = type.GetField(name: "_isStarted", bindingAttr: flags)!;

        dotNetRefField.GetValue(obj: sut).Should().BeNull();
        ((bool)isStartedField.GetValue(obj: sut)!).Should().BeFalse();
    }

    [Fact]
    public async Task StopDetectAsync_Should_NotCallDispose_When_CancellationRequested()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();
        var module = new TestJsModule();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);
        var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: cts);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_isStarted", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: true);

        var method = type.GetMethod(
            name: "StopDetectAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task = (Task)method.Invoke(obj: sut, parameters: Array.Empty<object?>())!;
        await task;

        // Assert
        module.Calls.Should().NotContain(predicate: c => c.Identifier == "dispose");
    }

    [Fact]
    public async Task UpdateStoredTypeAsync_Should_StartAndStopDetection_BasedOnStoredType()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();
        var themingService = Substitute.For<IThemingService>();
        var module = new TestJsModule();

        var sut = new AryThemeProvider
        {
            JsRuntime = jsRuntime,
            ThemingService = themingService
        };

        var type = typeof(AryThemeProvider);

        type.GetField(name: "_module", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: module);

        type.GetField(name: "_cts", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: new CancellationTokenSource());

        type.GetField(name: "_storedType", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(obj: sut, value: ThemeType.Dark);

        var method = type.GetMethod(
            name: "UpdateStoredTypeAsync", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        )!;

        // Act
        var task1 = (Task)method.Invoke(
            obj: sut, parameters: new object?[]
            {
                ThemeType.System,
                CancellationToken.None
            }
        )!;

        await task1;

        var task2 = (Task)method.Invoke(
            obj: sut, parameters: new object?[]
            {
                ThemeType.Light,
                CancellationToken.None
            }
        )!;

        await task2;

        // Assert
        module.Calls.Should().Contain(predicate: c => c.Identifier == "init");
        module.Calls.Should().Contain(predicate: c => c.Identifier == "dispose");

        module.Calls.Count(predicate: c => c.Identifier == "setStoredTheme").Should()
            .BeGreaterThanOrEqualTo(expected: 2);
    }

    private sealed class RecordingJsRuntime : IJSRuntime
    {
        public List<(string Identifier, object?[] Args)> Calls { get; } = new();

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
        {
            Calls.Add(item: (identifier, args ?? Array.Empty<object?>()));

            return new ValueTask<TValue>(result: default(TValue)!);
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier,
            CancellationToken cancellationToken,
            object?[]? args)
        {
            Calls.Add(item: (identifier, args ?? Array.Empty<object?>()));

            return new ValueTask<TValue>(result: default(TValue)!);
        }
    }

    private sealed class TestJsModule : IJSObjectReference
    {
        public List<(string Identifier, object?[]? Args)> Calls { get; } = new();

        public string? DetectResult { get; init; }

        public string? StoredThemeResult { get; init; }

        public bool ThrowOnDetect { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public bool ThrowOnDispose { get; set; }

        public bool ThrowOnGetStoredTheme { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public bool ThrowOnInit { get; set; }

        public bool ThrowOnSetStoredTheme { get; init; }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
            => InvokeAsync<TValue>(identifier: identifier, cancellationToken: CancellationToken.None, args: args);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier,
            CancellationToken cancellationToken,
            object?[]? args)
        {
            Calls.Add(item: (identifier, args ?? Array.Empty<object?>()));

            try
            {
                switch (identifier)
                {
                    case "detect":
                        return ThrowOnDetect
                            ? throw new JSException(message: "detect boom")
                            : Return<TValue>(value: DetectResult);

                    case "getStoredTheme":
                        return ThrowOnGetStoredTheme
                            ? throw new JSException(message: "getStoredTheme boom")
                            : Return<TValue>(value: StoredThemeResult);

                    case "setStoredTheme":
                        return ThrowOnSetStoredTheme
                            ? throw new JSException(message: "setStoredTheme boom")
                            : Return<TValue>(value: true);

                    case "init":
                        return ThrowOnInit
                            ? throw new JSException(message: "init boom")
                            : Return<TValue>(value: null);

                    case "dispose":
                        return ThrowOnDispose
                            ? throw new JSException(message: "dispose boom")
                            : Return<TValue>(value: null);

                    default:
                        return Return<TValue>(value: null);
                }
            }
            catch (JSException ex)
            {
                return new ValueTask<TValue>(task: Task.FromException<TValue>(exception: ex));
            }
        }

        private static ValueTask<TValue> Return<TValue>(object? value)
            => value is null
                ? new ValueTask<TValue>(result: default(TValue)!)
                : new ValueTask<TValue>(result: (TValue)value);
    }

    private sealed class ThemeConsumer : ComponentBase
    {
        [CascadingParameter(Name = "ThemeType")]
        public ThemeType ThemeType { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(sequence: 0, elementName: "span");
            builder.AddAttribute(sequence: 1, name: "id", value: "theme-type");
            builder.AddContent(sequence: 2, textContent: ThemeType.ToString());
            builder.CloseElement();
        }
    }

    private sealed class ThrowingJsObjectReference : IJSObjectReference
    {
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
            => new(task: Task.FromException<TValue>(exception: new JSException(message: "boom")));

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier,
            CancellationToken cancellationToken,
            object?[]? args)
            => new(task: Task.FromException<TValue>(exception: new JSException(message: "boom")));
    }
}

# Testing Standards

> *Version 1: 2025-09-18*

These standards define conventions for testing within the Allyaria project. They apply across **xUnit**, **bUnit**, *
*AwesomeAssertions** (FluentAssertions fork), and **NSubstitute**.

---

## 1. General Guidance

* **Structure:** Arrange / Act / Assert (comment the sections).
* **Naming:** `Method_Scenario_Expected` (or `Behavior_Scenario_Expected` for component tests).

    * Example: `RecomputeStyles_WithBackgroundImage_UsesOverlayAndTransparentRegions`
* **Determinism:** No randomness, time sleeps, or environment dependencies.
* **Culture/time:** If relevant, set `CultureInfo.CurrentCulture/CurrentUICulture` within a test and restore in
  `finally`.
* **Parallelization:** Default parallel execution is OK; use `[Collection]` only when necessary for shared state.
* **Layering:**

    * **bUnit (component tests):** rendering, parameters, markup/DOM, events, interop wiring, CSS/output strings.
    * **xUnit (unit tests):** pure logic (helpers, theming math, mappers), small services without rendering.
    * Avoid duplicating assertions across layers.

---

## 2. xUnit (Unit Tests)

* Use `[Fact]` for fixed inputs; `[Theory]` + `[InlineData]` for table-driven coverage.
* Prefer **small, focused tests** over large end-to-end checks.
* Use **AwesomeAssertions** for expressive assertions.

**Examples:**

```csharp
[Fact]
public void DefaultOrOverride_WhenNullOrWhitespace_ReturnsFallback()
{
    EditorUtils.DefaultOrOverride("  ", "fallback").Should().Be("fallback");
}

[Fact]
public void Style_WhenValueProvided_BuildsCssDeclaration()
{
    EditorUtils.Style("color", "#fff").Should().Be("color: #fff;");
}
```

* For theming, verify style computation precedence:

    * Transparent clears backgrounds.
    * Background image applies overlay + transparent regions.
    * Explicit overrides beat defaults.
    * Border on/off logic.
    * Width/Height `0` → `100%`.

---

## 3. bUnit (Component Tests)

* **Base:** derive test classes from `TestContext`.
* **JS interop:** use `JSInterop` helpers:

```csharp
JSInterop.Setup<string>("Allyaria_Editor_sanitizeLabelledBy", _ => true).SetResult("");
```

* Prefer `IJSRuntime` DI over direct `JSRuntime` usage.
* **Rendering:** `RenderComponent<T>(ps => ps.Add(p => p.Param, value))`
* **Assertions:** prefer stable selectors (`#id`, `.class`). Assert **intent**, not incidental details.
* **Events:** trigger with `TriggerEventAsync` and assert resulting state/parameters.
* **System theme detection:** simulate interop responses ("hc", "dark", etc.) and assert styles.

**Example:**

```csharp
[Fact]
public void Placeholder_Shown_And_Announced_When_Text_Empty()
{
    JSInterop.Setup<string>("Allyaria_Editor_sanitizeLabelledBy", _ => true).SetResult("");

    var cut = RenderComponent<AllyariaContent>(ps => ps
        .Add(p => p.Text, string.Empty)
        .Add(p => p.Placeholder, "Start typing...")
    );

    cut.Find("#ae-placeholder").TextContent.Trim().Should().Be("Start typing...");
    cut.Find("#ae-content").GetAttribute("aria-describedby").Should().Contain("ae-placeholder");
}
```

---

## 4. Assertions with AwesomeAssertions

* Prefer **fluent assertions** for readability and failure messages.
* Common patterns:

    * Strings: `.Should().Be(...)`, `.Contain(...)`, `.NotBeNullOrEmpty()`, `.MatchRegex(...)`
    * Objects: `.BeEquivalentTo(...)`
    * Booleans: `.BeTrue()`, `.BeFalse()`
    * Collections: `.HaveCount(...)`, `.Contain(...)`, `.OnlyContain(...)`

**Examples:**

```csharp
style.Should().Contain("background-color: #ffffff");
updated.Should().Be("Hello world");
elements.Should().HaveCount(3);
```

---

## 5. Test Doubles with NSubstitute

* Use **NSubstitute** for interface stubs/mocks when plain fakes are noisy.
* Typical flow:

```csharp
var interop = Substitute.For<IEditorJsInterop>();
interop.SanitizeLabelledByAsync("id").Returns("id");
// Inject into component/service under test
interop.Received(1).SanitizeLabelledByAsync(Arg.Is<string>(s => s.Contains("heading")));
```

* Prefer substitutes for **behavioral verification**; stub only what the test needs.

---

## 6. Test Project Layout

```
tests/
  Allyaria.Tests.Component/         // bUnit
    Content/...
    Editor/...
    Toolbar/...
    Helpers/JsInteropSetups.cs
  Allyaria.Tests.Unit/              // xUnit
    Helpers/EditorUtilsTests.cs
    Theming/ThemeDefaultsTests.cs
    Theming/StyleEngineTests.cs
    Resources/EditorResourcesTests.cs
```

* Mirror `src/` folder names.
* Keep helper extensions (e.g., interop setups) in shared `Helpers`.

---

## 7. What Not to Test

* Don’t assert private implementation details.
* Don’t duplicate checks across bUnit and xUnit.
* Don’t serialize/deserialize unless API contract demands it.

---

## 8. Build & CI

* All tests must run in CI with `dotnet test`.
* Aim for **fast** unit tests; component tests may be slower but must remain concise.

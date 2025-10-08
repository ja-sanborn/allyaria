# Allyaria.Abstractions.Enumerations.LabelPosition

`LabelPosition` defines the **visual and accessibility behavior of labels** associated with input or control components
in Allyaria UI.
It determines whether a label is hidden, displayed above/below the input outline, or rendered as a placeholder that
moves depending on focus or value state.

This enumeration supports **WCAG 2.2 AA accessibility**, **bidirectional layouts (LTR/RTL)**, and consistent behavior
across Allyaria components with outlined or filled styles.

---

## Constructors

*None*

---

## Properties

*None*

---

## Methods

*None*

---

## Operators

*None*

---

## Events

*None*

---

## Exceptions

*None*

---

## Behavior Notes

* **Hidden:** The label remains accessible to screen readers but is visually removed (`display: none` or off-screen).
* **Above/Below:** Used for static labels positioned relative to an input’s outline border. Alignment respects layout
  direction (`dir="rtl"`).
* **PlaceholderHidden:** Displays placeholder text only when empty; the label itself remains visually hidden.
* **PlaceholderAbove / PlaceholderBelow:** Animate or transition the label between placeholder position (inside input)
  and edge position (above/below) when focus or text changes.
* **Accessibility:** Regardless of visual position, the label text is linked via `for`/`aria-labelledby` to ensure
  screen readers announce it.
* **Styling:** Controlled by component CSS isolation and Allyaria theming; colors and animations follow theme rules (
  light/dark/high contrast).

---

## Examples

### Minimal Example

```csharp
using Allyaria.Abstractions.Enumerations;

public class ExampleUsage
{
    public LabelPosition Position { get; set; } = LabelPosition.PlaceholderAbove;
}
```

### Expanded Example

```razor
@using Allyaria.Abstractions.Enumerations

<label for="email">@Localizer["Label_Email"]</label>
<InputText id="email"
           class="input--outlined"
           @bind-Value="Email"
           data-label-position="@LabelPosition.PlaceholderAbove" />

@code {
    private string Email = string.Empty;

    // Example usage of LabelPosition enumeration
    private LabelPosition LabelPosition => LabelPosition.PlaceholderAbove;
}
```

> *Rev Date: 2025-10-07*

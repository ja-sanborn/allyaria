# Allyaria.Theming.Enumerations.StyleType

`StyleType` is an enumeration listing all CSS style properties recognized by the Allyaria theming system. Each value
maps directly to a specific CSS property name through a `[Description]` attribute, enabling strongly typed style
definitions and dynamic CSS generation within theme engines.

## Summary

`StyleType` is an enum representing individual CSS properties used throughout the Allyaria theming framework. It
provides a typed, extensible catalog of style rules, allowing components and theme builders to reference CSS properties
without relying on raw strings, improving consistency, refactorability, and tooling support.

## Constructors

*None*

## Properties

*None*

## Methods

*None*

## Fields

| Name                       | Type        | Description                                                                               |
|----------------------------|-------------|-------------------------------------------------------------------------------------------|
| `AccentColor`              | `StyleType` | Sets the accent color for UI controls (`accent-color`).                                   |
| `AlignContent`             | `StyleType` | Controls distribution of space along the cross/block axis (`align-content`).              |
| `AlignItems`               | `StyleType` | Aligns flex/grid children along the cross/block axis (`align-items`).                     |
| `AlignmentBaseline`        | `StyleType` | Sets which baseline is used for text and inline content alignment (`alignment-baseline`). |
| `AlignSelf`                | `StyleType` | Overrides `align-items` for a specific flex/grid item (`align-self`).                     |
| `BackgroundColor`          | `StyleType` | Sets the element’s background color (`background-color`).                                 |
| `BorderColor`              | `StyleType` | Sets an element’s border color (`border-color`).                                          |
| `BorderRadius`             | `StyleType` | Rounds an element’s corners (`border-radius`).                                            |
| `BorderStyle`              | `StyleType` | Sets border line style for all sides (`border-style`).                                    |
| `BorderWidth`              | `StyleType` | Sets border width for all sides (`border-width`).                                         |
| `BoxSizing`                | `StyleType` | Controls width/height calculation (`box-sizing`).                                         |
| `CaretColor`               | `StyleType` | Sets the color of text input cursors (`caret-color`).                                     |
| `Color`                    | `StyleType` | Sets text and foreground color (`color`).                                                 |
| `Display`                  | `StyleType` | Controls element display mode (block, inline, flex, grid, etc.) (`display`).              |
| `FontFamily`               | `StyleType` | Sets the font family or prioritized font list (`font-family`).                            |
| `FontSize`                 | `StyleType` | Sets font size (`font-size`).                                                             |
| `FontStyle`                | `StyleType` | Sets italic/oblique/normal font styling (`font-style`).                                   |
| `FontWeight`               | `StyleType` | Sets font boldness (`font-weight`).                                                       |
| `Height`                   | `StyleType` | Sets element height (`height`).                                                           |
| `Hyphens`                  | `StyleType` | Controls hyphenation behavior (`hyphens`).                                                |
| `JustifyContent`           | `StyleType` | Distributes space along the main axis (`justify-content`).                                |
| `JustifyItems`             | `StyleType` | Sets the default inline-axis self-alignment (`justify-items`).                            |
| `JustifySelf`              | `StyleType` | Aligns a single flex/grid item along the main axis (`justify-self`).                      |
| `LetterSpacing`            | `StyleType` | Sets spacing between text characters (`letter-spacing`).                                  |
| `LineBreak`                | `StyleType` | Controls line-breaking rules for CJK text (`line-break`).                                 |
| `LineHeight`               | `StyleType` | Sets line height or block-size line box (`line-height`).                                  |
| `Margin`                   | `StyleType` | Sets margin on all four sides (`margin`).                                                 |
| `MaxHeight`                | `StyleType` | Sets maximum height (`max-height`).                                                       |
| `MaxWidth`                 | `StyleType` | Sets maximum width (`max-width`).                                                         |
| `MinHeight`                | `StyleType` | Sets minimum height (`min-height`).                                                       |
| `MinWidth`                 | `StyleType` | Sets minimum width (`min-width`).                                                         |
| `OutlineColor`             | `StyleType` | Sets outline color (`outline-color`).                                                     |
| `OutlineOffset`            | `StyleType` | Defines space between outline and border (`outline-offset`).                              |
| `OutlineStyle`             | `StyleType` | Sets outline line style (`outline-style`).                                                |
| `OutlineWidth`             | `StyleType` | Sets outline thickness (`outline-width`).                                                 |
| `Overflow`                 | `StyleType` | Controls how overflow content is handled (`overflow`).                                    |
| `OverflowBlock`            | `StyleType` | Controls overflow behavior in block direction (`overflow-block`).                         |
| `OverflowInline`           | `StyleType` | Controls overflow behavior in inline direction (`overflow-inline`).                       |
| `OverflowWrap`             | `StyleType` | Sets whether long words may be broken (`overflow-wrap`).                                  |
| `OverscrollBehavior`       | `StyleType` | Controls browser behavior at scroll boundary (`overscroll-behavior`).                     |
| `OverscrollBehaviorBlock`  | `StyleType` | Block-axis overscroll behavior (`overscroll-behavior-block`).                             |
| `OverscrollBehaviorInline` | `StyleType` | Inline-axis overscroll behavior (`overscroll-behavior-inline`).                           |
| `Padding`                  | `StyleType` | Sets padding on all four sides (`padding`).                                               |
| `Position`                 | `StyleType` | Controls element positioning (`position`).                                                |
| `ScrollBehavior`           | `StyleType` | Sets behavior of programmatic scrolling (`scroll-behavior`).                              |
| `TextAlign`                | `StyleType` | Sets horizontal alignment of inline content (`text-align`).                               |
| `TextDecorationColor`      | `StyleType` | Sets color of text decoration lines (`text-decoration-color`).                            |
| `TextDecorationLine`       | `StyleType` | Controls decoration types (underline, overline, etc.) (`text-decoration-line`).           |
| `TextDecorationStyle`      | `StyleType` | Styles decoration lines (`text-decoration-style`).                                        |
| `TextDecorationThickness`  | `StyleType` | Sets decoration stroke thickness (`text-decoration-thickness`).                           |
| `TextIndent`               | `StyleType` | Sets indentation before text lines (`text-indent`).                                       |
| `TextOrientation`          | `StyleType` | Sets orientation of text in vertical writing modes (`text-orientation`).                  |
| `TextOverflow`             | `StyleType` | Controls how overflowed inline content is displayed (`text-overflow`).                    |
| `TextSizeAdjust`           | `StyleType` | Adjusts font size for display devices (`text-size-adjust`).                               |
| `TextTransform`            | `StyleType` | Controls capitalization/transformation of text (`text-transform`).                        |
| `TextUnderlineOffset`      | `StyleType` | Controls underline offset (`text-underline-offset`).                                      |
| `TextWrapStyle`            | `StyleType` | Controls wrapping behavior of text (`text-wrap-style`).                                   |
| `VerticalAlign`            | `StyleType` | Sets vertical alignment for inline and table-cell boxes (`vertical-align`).               |
| `WhiteSpace`               | `StyleType` | Controls white-space processing (`white-space`).                                          |
| `Width`                    | `StyleType` | Sets element width (`width`).                                                             |
| `WordBreak`                | `StyleType` | Controls line-breaking behavior for words (`word-break`).                                 |
| `WordSpacing`              | `StyleType` | Sets spacing between words (`word-spacing`).                                              |
| `WritingMode`              | `StyleType` | Sets block/in-line flow direction (`writing-mode`).                                       |
| `ZIndex`                   | `StyleType` | Controls stacking order of positioned elements (`z-index`).                               |
| `Zoom`                     | `StyleType` | Applies zoom/magnification to an element (`zoom`).                                        |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
using Allyaria.Theming.Enumerations;

public class StyleRule
{
    public StyleType Property { get; set; } = StyleType.Color;
    public string Value { get; set; } = "red";

    public override string ToString()
    {
        var cssName = Property.GetType()
                              .GetField(Property.ToString())?
                              .GetCustomAttributes(typeof(DescriptionAttribute), false)
                              is DescriptionAttribute[] { Length: > 0 } desc
            ? desc[0].Description
            : Property.ToString().ToLower();

        return $"{cssName}: {Value};";
    }
}
```

---

*Revision Date: 2025-11-15*

# Allyaria.Theming.Styles.AllyariaTypography

`AllyariaTypography` is a lightweight, immutable **record struct** representing a set of typography tokens
(e.g., family, size, weight) and providing **inline CSS** and **CSS custom property** emission. Only **non-null**
values are rendered, and output uses concise `prop:value;` declarations in a deterministic order.

---

## Constructors

`AllyariaTypography(AllyariaStringValue? fontFamily = null, AllyariaStringValue? fontSize = null, AllyariaStringValue? fontStyle = null, AllyariaStringValue? fontWeight = null, AllyariaStringValue? letterSpacing = null, AllyariaStringValue? lineHeight = null, AllyariaStringValue? textAlign = null, AllyariaStringValue? textDecoration = null, AllyariaStringValue? textTransform = null, AllyariaStringValue? verticalAlign = null, AllyariaStringValue? wordSpacing = null)`  
Initializes an immutable typography definition. All parameters are optional; **null** values are omitted from output.

---

## Properties

- `AllyariaStringValue? FontFamily` — The font family to use (e.g., `"Inter, Segoe UI, sans-serif"`).
- `AllyariaStringValue? FontSize` — The font size (e.g., `14px`, `1rem`).
- `AllyariaStringValue? FontStyle` — The font style (e.g., `normal`, `italic`).
- `AllyariaStringValue? FontWeight` — The font weight (e.g., `400`, `bold`).
- `AllyariaStringValue? LetterSpacing` — The letter spacing (e.g., `0.5px`).
- `AllyariaStringValue? LineHeight` — The line height (e.g., `1.5`, `24px`).
- `AllyariaStringValue? TextAlign` — The text alignment (e.g., `left`, `center`).
- `AllyariaStringValue? TextDecoration` — The text decoration (e.g., `underline`).
- `AllyariaStringValue? TextTransform` — The text transform (e.g., `uppercase`).
- `AllyariaStringValue? VerticalAlign` — The vertical alignment (e.g., `middle`, `baseline`).
- `AllyariaStringValue? WordSpacing` — The word spacing (e.g., `2px`).

---

## Events

* *None*

---

## Methods

- `AllyariaTypography Cascade(AllyariaStringValue? fontFamily = null, AllyariaStringValue? fontSize = null, AllyariaStringValue? fontStyle = null, AllyariaStringValue? fontWeight = null, AllyariaStringValue? letterSpacing = null, AllyariaStringValue? lineHeight = null, AllyariaStringValue? textAlign = null, AllyariaStringValue? textDecoration = null, AllyariaStringValue? textTransform = null, AllyariaStringValue? verticalAlign = null, AllyariaStringValue? wordSpacing = null)` —
Creates a new instance by applying non-null overrides to the current values.

- `string ToCss()` — Builds an **inline CSS** declaration list, appending a declaration **only when the corresponding
  value is non-null**.  
  Emits (in order):
  `font-family; font-size; font-style; font-weight; letter-spacing; line-height; text-align; text-decoration; text-transform; vertical-align; word-spacing`.  
  Each declaration is emitted as `prop:value;` (lower-cased property names).

- `string ToCssVars(string prefix = "")` — Builds a **CSS custom properties** string using the same omission and order
  rules.  
  The prefix is normalized by trimming whitespace and dashes, converting to lowercase, and replacing spaces with
  hyphens. If empty or whitespace, defaults to `--aa-`.  
  Emits variables like:
  `--{prefix}font-family; --{prefix}font-size; --{prefix}font-style; --{prefix}font-weight; --{prefix}letter-spacing; --{prefix}line-height; --{prefix}text-align; --{prefix}text-decoration; --{prefix}text-transform; --{prefix}vertical-align; --{prefix}word-spacing`.

---

## Operators

- `==`, `!=` — Value equality/inequality provided by `record struct` semantics. No ordering operators are defined.

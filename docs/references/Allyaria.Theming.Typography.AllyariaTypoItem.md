# Allyaria.Theming.Typography.AllyariaTypoItem

`AllyariaTypoItem` represents a **validated, immutable typography settings** value object (non-color) with CSS-oriented
normalization and formatting. It accepts nullable inputs, lower-cases CSS keywords (except font family names), validates
against allow-lists, and renders only non-empty declarations as a **single-line CSS** string in a fixed order. :
contentReference[oaicite:0]{index=0}

---

## Constructors

*
`AllyariaTypoItem(string[]? fontFamily = null, string? fontSize = null, string? fontStyle = null, string? fontWeight = null, string? letterSpacing = null, string? lineHeight = null, string? textAlign = null, string? textDecoration = null, string? textTransform = null, string? verticalAlign = null, string? wordSpacing = null)`  
Initializes a new, immutable instance. All parameters are **nullable**; invalid inputs throw `ArgumentException`.  
*Keywords are trimmed and lower-cased (except `fontFamily` entries). Bare numbers are assumed `px` where lengths are
required; numeric parsing uses `InvariantCulture`.* :contentReference[oaicite:3]{index=3}

---

## Properties

* `string[]? FontFamily` — Canonicalized family list (quoted where needed, generics unquoted, deduped,
  order-preserving).  
  If `null` or empty after filtering, the `font-family` declaration is omitted from CSS. :contentReference[oaicite:4]
  {index=4}

* `string? FontSize` — Size token (CSS keyword `xx-small..larger`, length `px|em|rem|%`, or `var()/calc()`).  
  Bare numeric values are normalized to `px`. Invalid values throw. :contentReference[oaicite:5]{index=5}

* `string? FontStyle` — One of: `normal`, `italic`, `oblique`. :contentReference[oaicite:6]{index=6}

* `string? FontWeight` — One of: `normal`, `bold`, `lighter`, `bolder`, or a **multiple of 100** from `100` to `900`. :
  contentReference[oaicite:7]{index=7}

* `string? LetterSpacing` — `normal`, a length (`px|em|rem`), `var()`, or `calc()`. Bare number → `px`. :
  contentReference[oaicite:8]{index=8}

* `string? LineHeight` — `normal`, **unitless positive number**, length (`px|em|rem|%`), `var()`, or `calc()`.  
  Negative lengths are rejected. :contentReference[oaicite:9]{index=9}

* `string? TextAlign` — One of: `left`, `right`, `center`, `justify`, `start`, `end`. :contentReference[oaicite:10]
  {index=10}

* `string? TextDecoration` — Space-separated subset of: `none`, `underline`, `overline`, `line-through`.  
  `none` cannot be combined with other values; unknown tokens are rejected. :contentReference[oaicite:11]{index=11}

* `string? TextTransform` — One of: `none`, `capitalize`, `uppercase`, `lowercase`. :contentReference[oaicite:12]
  {index=12}

* `string? VerticalAlign` — One of: `baseline`, `top`, `middle`, `bottom`, `sub`, `text-top`. :
  contentReference[oaicite:13]{index=13}

* `string? WordSpacing` — `normal`, a length (`px|em|rem`), `var()`, or `calc()`. Bare number → `px`. :
  contentReference[oaicite:14]{index=14}

---

## Events

* *None*

---

## Methods

* `string ToCss()` — Builds a **single-line** CSS declaration list in fixed order, omitting any `null`/whitespace
  properties.  
  **Order:**
  `font-family; font-size; font-weight; line-height; font-style; text-align; letter-spacing; word-spacing; text-transform; text-decoration; vertical-align`.  
  Declarations are emitted as `prop:value;` with **no spaces** around `:` or `;`. :contentReference[oaicite:15]
  {index=15}

* `override string ToString()` — Returns the same string as `ToCss()`. :contentReference[oaicite:16]{index=16}

---

## Exceptions

* `ArgumentException` — Thrown by the constructor’s validation pipeline for:
    - Unknown keywords for **FontStyle**, **FontWeight** (when not a valid 100-step), **TextAlign**, **TextTransform**,
      **TextDecoration** (including mixing `none` with others), and **VerticalAlign**.
    - **FontSize** values that are not a keyword, length/percent, number (→ `px`), or `var()`/`calc()`.
    - **LineHeight** negative lengths or invalid tokens; non-positive unitless values.
    - **LetterSpacing/WordSpacing** values that are not `normal`, a valid length, number (→ `px`), or `var()`/
      `calc()`. :contentReference[oaicite:17]{index=17}

---

## Operators

* *None*

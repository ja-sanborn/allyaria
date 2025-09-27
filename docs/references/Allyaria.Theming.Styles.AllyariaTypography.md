# Allyaria.Theming.Typography.AllyariaTypography

`AllyariaTypography` is a lightweight, immutable **record struct** representing a set of typography tokens
(e.g., family, size, weight) and providing **inline CSS** and **CSS custom property** emission. Only **non-null**
values are rendered, and output uses concise `prop:value;` declarations in a deterministic order.

---

## Constructors

*

`AllyariaTypography(AllyariaStringValue? fontFamily = null, AllyariaStringValue? fontSize = null, AllyariaStringValue? fontStyle = null, AllyariaStringValue? fontWeight = null, AllyariaStringValue? letterSpacing = null, AllyariaStringValue? lineHeight = null, AllyariaStringValue? textAlign = null, AllyariaStringValue? textDecoration = null, AllyariaStringValue? textTransform = null, AllyariaStringValue? verticalAlign = null, AllyariaStringValue? wordSpacing = null)`  
Initializes an immutable typography definition. All parameters are optional; **null** values are omitted from output.

---

## Properties

* *None*

---

## Events

* *None*

---

## Methods

* `string ToCss()` — Builds an **inline CSS** declaration list, appending a declaration **only when the corresponding
  value is non-null**.  
  `font-family; font-size; font-style; font-weight; letter-spacing; line-height; text-align; text-decoration; text-transform; vertical-align; word-spacing`.  
  Each declaration is emitted as `prop:value;` (lower-cased property names).

* `string ToCssVars()` — Builds a **CSS custom properties** string using the same omission and order rules, with
  Allyaria prefixes:  
  `--aa-font-family; --aa-font-size; --aa-font-style; --aa-font-weight; --aa-letter-spacing; --aa-line-height; --aa-text-align; --aa-text-decoration; --aa-text-transform; --aa-vertical-align; --aa-word-spacing`.

---

## Operators

* `==`, `!=` — Value equality/inequality provided by `record struct` semantics. No ordering operators are defined.

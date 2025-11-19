# C# Coding Standards

> *Version 2: 2025-10-06*

These standards codify our team’s C# practices. They align with our .editorconfig and conventions observed in
Allyaria.Abstractions and Allyaria.Theming. They follow Microsoft’s guidelines with explicit project rules for
consistency, readability, and reliability.

## 1. Universal Formatting

* **Encoding:** UTF-8
* **Line endings:** LF
* **Final newline:** Required
* **Trim trailing whitespace:** Yes (except in Markdown)
* **Indentation:** 4 spaces (no tabs)

> Rationale: Matches common cross-platform tooling defaults, ensures clean diffs, and consistent formatting across
> editors/IDEs.

## 2. Language Version & Project Defaults

* **C# version:** C# 12+ (net8.0)
* **Nullable reference types:** Enabled
* **Implicit usings:** Prefer explicit usings in libraries
* **XML documentation file:** Generated for public and private members *(see §9)*.

## 3. Namespaces & File Layout

* Prefer *file-scoped* namespaces.
* One *type* per file.
* Member order: fields → constructors → properties → methods → operators → events → nested types.

```csharp
namespace Allyaria.{Project}.{Path};

public sealed class {Class}
{
    // fields → constructors → properties → methods...
}

```

## 4. Modifier Order

Use exactly this order (enforced by .editorconfig):

* public
* private
* protected
* internal
* file
* new
* static
* abstract
* virtual
* sealed
* readonly
* override
* extern
* unsafe
* volatile
* async
* required

## 5. Braces and Expression Bodies

* Braces required for all control statements.
* Expression-bodied members preferred for accessors, constructors, methods, properties when clear.

```csharp
if (isReady)
{
    DoWork();
}

public override string ToString() => $"{Name} ({Id})";
```

## 6. Type Usage and var

* Prefer var everywhere (including built-in types and when type is apparent).
* Prefer predefined types for declarations and member access (int, string, bool, etc.).

```csharp
var list = new List<string>();
int count = 0;
var name = string.Empty;
```

## 7. Qualification and Usings

* Do not qualify with this. or type name for members.
* Do not sort System usings first; keep consistent grouping within a project.

## 8. Parentheses and Clarity

* **Arithmetic/relational:** avoid redundant parentheses.
* **Other binary operators:** add parentheses when they materially improve clarity.

## 9. XML Documentation

> **Policy:** XML documentation comments are **required for all public, protected, internal, and private** types and
> members: fields, properties, constructors, methods, events, operators, and delegates. Tests may disable CS1591.

* Documentation must state purpose, behavior, contracts, side effects, and noteworthy exceptions.
* Use <summary>, <param>, <returns>, <remarks>, <example>, <exception> as needed.
* Keep succinct, correct, and up to date.

## 10. Naming

* **PascalCase:** public/protected/internal types, methods, properties, events, constants.
* **\_camelCase:** private fields (underscore prefix required).
* **camelCase:** locals and parameters.
* **Async methods:** end with Async (`LongAsync`).
* **Interfaces:** I prefix (`IService`).
* **Type parameters:** T prefix (`TItem`).
* **Acronyms:** IO stays uppercase; longer acronyms are Pascalized (`XmlReader`).
* **Abstract Base Class:** end with Base (`ValueBase`).
* **Exceptions:** end with Exception (`AlyException`).

## 11. Asynchronous Code

* All async methods end with Async.
* Prefer CancellationToken as the last parameter with default = default.
* Honor and propagate the token.
* Use Task/Task<T>; async void only for event handlers.

```csharp
public async Task<Result> FetchItemsAsync(int page, CancellationToken cancellationToken = default)
{
    // Pass the token down to all awaited calls
    using var response = await _http.GetAsync(BuildUrl(page), cancellationToken);
    // ...
}
```

## 12. Exceptions and Errors

* **Exceptions are for fatal/critical situations** only (library policy).
    * For expected/recoverable outcomes, prefer result types (e.g., `OneOf`, `Result<T>`) or documented return
      contracts.
* Throw the **most specific** exception type; avoid leaking sensitive/internal details.
* Use **guard clauses** for parameter validation.
* **Localization is required** for all user-visible messages *(see §13)*.
* Do not use exceptions for flow control; avoid large try/catch blocks that obscure logic.
* Prefer using *Allyaria* defined exceptions (prefix with Aly).

## 13. Globalization and Localization

* All **user-facing strings must be localized** (resource-based).
* Use culture-aware formatting (`ToString(CultureInfo)`, `string.Format` with culture, or `FormattableString.Invariant`
  where appropriate).
* Avoid string concatenation for user messages; prefer localized **composite** resources.
* Developer related messages are en-US, which is the default culture.

## 14. Immutability and Initialization

* Prefer readonly fields and immutable types.
* Use required members to express construction contracts.
* Prefer object initializers where concise.

## 15. ReSharper-oriented Formatting Cues

These reflect .editorconfig guidance used in the codebase:

* Keep one blank line around single-line members where configured (properties, accessors, local methods).
* Empty blocks can be on the same line when appropriate; otherwise follow standard brace rules.
* Wrap long argument/parameter lists (chop_if_long) and chained calls (chop_always).
* Prefer expression bodies for local functions when concise.

Note: The IDE configuration governs exact wrapping and blank line rules; follow automatic formatting.

## 16. Deviations and Changes

Where Microsoft defaults differ (e.g., System-first usings), this document follows our .editorconfig. Propose changes
via PR.

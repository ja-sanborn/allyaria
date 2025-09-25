using Allyaria.Theming.Contracts;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a normalized CSS function expression of the strict form <c>name(inner)</c>. Validates the function
/// identifier against a known list and canonicalizes the stored value to the function's canonical name (case preserved for
/// case-sensitive functions; lowercase for case-insensitive), producing <c>name(inner)</c>.
/// </summary>
public sealed class AllyariaFunctionValue : ValueBase
{
    /// <summary>
    /// Case-insensitive CSS function names. For these functions, browsers accept mixed casing (<c>RGB()</c>, <c>hSl()</c>,
    /// etc.), but the canonical form is lowercase. This set contains all other functions from MDN’s “CSS value functions”
    /// reference.
    /// </summary>
    private static readonly HashSet<string> CaseInsensitiveFunctions = new(
        [
            "abs",
            "acos",
            "anchor",
            "anchor-size",
            "asin",
            "atan",
            "atan2",
            "attr",
            "blur",
            "brightness",
            "calc",
            "calc-size",
            "character-variant",
            "clamp",
            "color",
            "color-mix",
            "conic-gradient",
            "contrast",
            "contrast-color",
            "cos",
            "counter",
            "counters",
            "cross-fade",
            "cubic-bezier",
            "device-cmyk",
            "drop-shadow",
            "element",
            "ellipse",
            "env",
            "exp",
            "fit-content",
            "grayscale",
            "hsl",
            "hwb",
            "hypot",
            "if",
            "image",
            "image-set",
            "invert",
            "inset",
            "lab",
            "lch",
            "light-dark",
            "linear",
            "linear-gradient",
            "log",
            "matrix",
            "matrix3d",
            "max",
            "min",
            "minmax",
            "mod",
            "oklab",
            "oklch",
            "opacity",
            "paint",
            "path",
            "perspective",
            "polygon",
            "pow",
            "radial-gradient",
            "ray",
            "rect",
            "rem",
            "repeat",
            "repeating-conic-gradient",
            "repeating-linear-gradient",
            "repeating-radial-gradient",
            "rgb",
            "rotate",
            "rotate3d",
            "round",
            "saturate",
            "scale",
            "scale3d",
            "scroll",
            "sepia",
            "shape",
            "sibling-count",
            "sibling-index",
            "sign",
            "sin",
            "skew",
            "sqrt",
            "steps",
            "stylistic",
            "styleset",
            "swash",
            "symbols",
            "tan",
            "translate",
            "translate3d",
            "url",
            "var",
            "view",
            "xywh"
        ],
        StringComparer.OrdinalIgnoreCase
    );

    /// <summary>
    /// Case-sensitive CSS function names. These transform functions are explicitly defined with capital letters in the CSS
    /// spec (e.g., <c>rotateX()</c>) and must be preserved exactly as written. Using <c>rotatex</c> (all lowercase) will throw
    /// a CSS parse error in browsers.
    /// </summary>
    private static readonly HashSet<string> CaseSensitiveFunctions = new(
        [
            "rotateX",
            "rotateY",
            "rotateZ",
            "scaleX",
            "scaleY",
            "scaleZ",
            "translateX",
            "translateY",
            "translateZ",
            "skewX",
            "skewY"
        ],
        StringComparer.Ordinal
    );

    /// <summary>
    /// Initializes a new instance of the <see cref="AllyariaFunctionValue" /> class from a raw string. The input must be in
    /// the exact shape <c>name(inner)</c>. Nested parentheses are allowed inside <c>inner</c>; only the first <c>(</c> and the
    /// final trailing <c>)</c> are considered delimiters for the outer function.
    /// </summary>
    /// <param name="value">Raw CSS text in the form <c>name(inner)</c>.</param>
    /// <remarks>
    /// This constructor throws a <see cref="FormatException" /> on invalid input. Use
    /// <see cref="TryParse(string, out AllyariaFunctionValue?)" /> if you prefer a non-throwing API.
    /// </remarks>
    /// <exception cref="FormatException">
    /// Thrown when <paramref name="value" /> is null/whitespace, malformed, or contains an unknown function identifier.
    /// </exception>
    public AllyariaFunctionValue(string value)
        : base(Normalize(value)) { }

    /// <summary>Determines whether a function identifier is known, returning the canonical name.</summary>
    /// <param name="name">The function identifier to check.</param>
    /// <param name="canonical">
    /// When this method returns <see langword="true" />, the canonical function name. For case-sensitive functions, this is
    /// the exact-cased spec identifier (e.g., <c>rotateX</c>). For case-insensitive functions, this is the lowercase
    /// identifier (e.g., <c>rgb</c>). When the function is not known, this is <see cref="string.Empty" />.
    /// </param>
    /// <returns><see langword="true" /> if the identifier is recognized; otherwise <see langword="false" />.</returns>
    private static bool IsKnownFunction(string name, out string canonical)
    {
        if (CaseSensitiveFunctions.Contains(name))
        {
            canonical = name;

            return true;
        }

        if (CaseInsensitiveFunctions.TryGetValue(name, out var canonicalName))
        {
            canonical = canonicalName.ToLowerInvariant();

            return true;
        }

        canonical = string.Empty;

        return false;
    }

    /// <summary>
    /// Normalizes a raw CSS function string to a standard <c>name(inner)</c> format. Performs validation of the function
    /// identifier and outer parentheses, and canonicalizes the function name per its sensitivity rules.
    /// </summary>
    /// <param name="value">Raw CSS text to normalize.</param>
    /// <returns>The normalized CSS string.</returns>
    /// <exception cref="FormatException">
    /// Thrown when <paramref name="value" /> is null/whitespace, malformed, or contains an unknown function identifier.
    /// </exception>
    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new FormatException("Value is null or whitespace.");
        }

        var text = value.Trim();

        if (TrySplitFunc(text, out var funcName, out var innerExpr))
        {
            return $"{funcName}({innerExpr})";
        }

        throw new FormatException("Unable to parse CSS function expression.");
    }

    /// <summary>Parses the supplied value into an <see cref="AllyariaFunctionValue" /> and throws if invalid.</summary>
    /// <param name="value">Raw CSS function string in the form <c>name(inner)</c>.</param>
    /// <returns>A valid <see cref="AllyariaFunctionValue" /> instance.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value" /> is not a valid CSS function expression.</exception>
    public static AllyariaFunctionValue Parse(string value) => new(value);

    /// <summary>Attempts to parse the supplied value into an <see cref="AllyariaFunctionValue" />.</summary>
    /// <param name="value">Raw CSS function string in the form <c>name(inner)</c>.</param>
    /// <param name="result">
    /// When this method returns <see langword="true" />, contains the parsed instance; otherwise <see langword="null" />.
    /// </param>
    /// <returns><see langword="true" /> if parsing succeeded; otherwise <see langword="false" />.</returns>
    public static bool TryParse(string value, out AllyariaFunctionValue? result)
    {
        try
        {
            result = new AllyariaFunctionValue(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>
    /// Splits a candidate function string into <c>name</c> and <c>inner</c>. Uses the position of the FIRST <c>(</c> and the
    /// LAST <c>)</c> as the outer delimiters, so any parentheses inside <c>inner</c> are ignored for the purpose of splitting.
    /// The last non-whitespace character of the string must be <c>)</c>.
    /// </summary>
    /// <param name="text">Input text that should represent a function call. Leading/trailing whitespace is allowed.</param>
    /// <param name="name">Outputs the canonical function name (trimmed).</param>
    /// <param name="inner">Outputs the inner expression (trimmed), excluding the outer parentheses.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="text" /> is well-formed and the function identifier is recognized; otherwise
    /// <see langword="false" />.
    /// </returns>
    private static bool TrySplitFunc(string text, out string name, out string inner)
    {
        name = string.Empty;
        inner = string.Empty;

        if (!text.EndsWith(')'))
        {
            return false;
        }

        var start = text.IndexOf('(');

        if (start <= 0)
        {
            return false;
        }

        var end = text.Length - 1;
        var rawName = text[..start].Trim();
        var rawInner = text.Substring(start + 1, end - start - 1).Trim();

        if (rawName.Length is 0 || rawInner.Length is 0)
        {
            return false;
        }

        if (!IsKnownFunction(rawName, out var canonicalName))
        {
            return false;
        }

        name = canonicalName;
        inner = rawInner;

        return true;
    }

    /// <summary>Implicitly converts a <see cref="string" /> to an <see cref="AllyariaFunctionValue" />.</summary>
    /// <param name="value">Raw CSS function string <c>name(inner)</c>.</param>
    /// <returns>A new <see cref="AllyariaFunctionValue" /> constructed from <paramref name="value" />.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value" /> is not a valid CSS function expression.</exception>
    public static implicit operator AllyariaFunctionValue(string value) => new(value);

    /// <summary>Implicitly converts an <see cref="AllyariaFunctionValue" /> to <see cref="string" />.</summary>
    /// <param name="value">Instance to convert.</param>
    /// <returns>
    /// The normalized CSS string, or <see cref="string.Empty" /> if <paramref name="value" /> is <see langword="null" />.
    /// </returns>
    public static implicit operator string(AllyariaFunctionValue value) => value.Value;
}

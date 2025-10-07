using System.Collections.Concurrent;
using System.ComponentModel;

namespace Allyaria.Abstractions.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="Enum" /> values, including retrieving user-friendly descriptions
/// from <see cref="DescriptionAttribute" /> with sensible fallbacks and caching for performance.
/// </summary>
/// <remarks>
/// Descriptions are cached per (enum <see cref="Type" />, raw enum <c>name</c>) to avoid repeated reflection. For
/// <c>[Flags]</c> combinations (e.g., <c>"Read, Write"</c>), each constituent name is resolved independently and then
/// re-joined using <c>", "</c>.
/// </remarks>
public static class EnumExtensions
{
    /// <summary>
    /// Cache of resolved descriptions keyed by a tuple of the enum <see cref="Type" /> and the raw member <c>name</c>. This
    /// avoids repeated reflection and attribute lookups across calls.
    /// </summary>
    private static readonly ConcurrentDictionary<(Type type, string name), string> DescriptionCache = new();

    /// <summary>Retrieves a user-friendly description for an <see cref="Enum" /> value.</summary>
    /// <param name="value">The enumeration value whose description to retrieve.</param>
    /// <returns>
    /// The <see cref="DescriptionAttribute.Description" /> value when present. Otherwise, a humanized fallback derived from
    /// the enum member name(s). For <c>[Flags]</c> combinations, each constituent name is individually humanized and the
    /// results are joined with <c>", "</c>.
    /// </returns>
    /// <remarks>
    /// Fallback humanization is performed via <see cref="StringExtensions.FromPascalCase(string?)" />. The result is cached
    /// per (enum type, raw name) pair.
    /// </remarks>
    /// <exception cref="AryArgumentException">Thrown if <paramref name="value" /> is <see langword="null" />.</exception>
    public static string GetDescription(this Enum value)
    {
        AryArgumentException.ThrowIfNull(value, nameof(value));

        var type = value.GetType();
        var name = value.ToString(); // May be "A" or "A, B" for [Flags].

        return DescriptionCache.GetOrAdd(
            (type, name),
            static key =>
            {
                (var t, var n) = key;

                // Handle [Flags] combined names like "Read, Write".
                if (!n.Contains(','))
                {
                    return GetSingleDescription(t, n);
                }

                var parts = n.Split(',')
                    .Select(s => s.Trim())
                    .Where(s => s.Length > 0)
                    .Select(part => GetSingleDescription(t, part));

                return string.Join(", ", parts);
            }
        );
    }

    /// <summary>Strongly-typed convenience overload that defers to <see cref="GetDescription(Enum)" />.</summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="value">The enumeration value whose description to retrieve.</param>
    /// <returns>The description string as defined by <see cref="GetDescription(Enum)" />.</returns>
    public static string GetDescription<TEnum>(this TEnum value)
        where TEnum : struct, Enum
        => ((Enum)value).GetDescription();

    /// <summary>Resolves the description for a single enum member name on a given enum <see cref="Type" />.</summary>
    /// <param name="enumType">The enum <see cref="Type" /> that defines the member.</param>
    /// <param name="memberName">The exact member name to resolve (not a composite <c>[Flags]</c> string).</param>
    /// <returns>
    /// The <see cref="DescriptionAttribute.Description" /> value if present and non-whitespace; otherwise the
    /// <paramref name="memberName" /> humanized via <see cref="StringExtensions.FromPascalCase(string?)" />.
    /// </returns>
    private static string GetSingleDescription(Type enumType, string memberName)
    {
        var mi = enumType.GetMember(memberName);

        if (mi.Length > 0)
        {
            var attr = mi[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>()
                .FirstOrDefault();

            if (attr is not null && !string.IsNullOrWhiteSpace(attr.Description))
            {
                return attr.Description;
            }
        }

        // Fallback: humanize the single member name.
        return memberName.FromPascalCase();
    }
}

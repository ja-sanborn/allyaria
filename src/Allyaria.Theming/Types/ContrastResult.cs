using Allyaria.Theming.Values;
using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Types;

/// <summary>
/// Immutable result that describes the outcome of a contrast resolution operation, including the chosen foreground color,
/// the background color, the achieved contrast ratio, and whether the minimum requirement was satisfied.
/// </summary>
/// <param name="ForegroundColor">The resolved foreground color (opaque).</param>
/// <param name="BackgroundColor">The background color (opaque).</param>
/// <param name="ContrastRatio">
/// The computed contrast ratio between <paramref name="ForegroundColor" /> and <paramref name="BackgroundColor" />.
/// </param>
/// <param name="MeetsMinimum">
/// <c>true</c> if the computed ratio meets or exceeds the required minimum; otherwise
/// <c>false</c>.
/// </param>
[ExcludeFromCodeCoverage(Justification = "This is a simple readonly record struct with no logic.")]
internal readonly record struct ContrastResult(
    AllyariaColorValue ForegroundColor,
    AllyariaColorValue BackgroundColor,
    double ContrastRatio,
    bool MeetsMinimum
);

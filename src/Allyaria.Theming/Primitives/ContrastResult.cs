using Allyaria.Theming.Values;
using System.Diagnostics.CodeAnalysis;

namespace Allyaria.Theming.Primitives;

/// <summary>Immutable result describing the resolved foreground, the background, and the achieved ratio.</summary>
/// <param name="ForegroundColor">Resolved foreground (opaque).</param>
/// <param name="BackgroundColor">BackgroundColor (opaque).</param>
/// <param name="ContrastRatio">Computed contrast ratio.</param>
/// <param name="MeetsMinimum">Whether the minimum was achieved.</param>
[ExcludeFromCodeCoverage(Justification = "This is a simple readonly record struct with no logic.")]
internal readonly record struct ContrastResult(
    AllyariaColorValue ForegroundColor,
    AllyariaColorValue BackgroundColor,
    double ContrastRatio,
    bool MeetsMinimum
);

namespace Allyaria.Abstractions.Types;

/// <summary>
/// Immutable result that describes the outcome of a contrast resolution operation, including the derived foreground color,
/// the achieved contrast ratio, and whether the minimum requirement was satisfied.
/// </summary>
/// <param name="ForegroundColor">The resolved foreground color (opaque).</param>
/// <param name="ContrastRatio">The computed contrast ratio between the foreground and background colors.</param>
/// <param name="IsMinimumMet">
/// <c>true</c> if the computed ratio meets or exceeds the required minimum; otherwise
/// <c>false</c>.
/// </param>
internal readonly record struct ContrastResult(
    HexColor ForegroundColor,
    double ContrastRatio,
    bool IsMinimumMet
);

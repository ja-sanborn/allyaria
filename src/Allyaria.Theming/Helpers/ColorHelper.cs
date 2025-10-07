using Allyaria.Theming.Constants;
using Allyaria.Theming.Styles;
using Allyaria.Theming.Types;

namespace Allyaria.Theming.Helpers;

/// <summary>
/// Color utilities for Allyaria theming. Provides WCAG contrast computation, hue-preserving contrast repair for opaque
/// colors, and small reusable helpers (e.g., scalar blending, HSVA search utilities). Designed as allocation-free static
/// helpers; no alpha/canvas blending.
/// </summary>
internal static class ColorHelper
{
    /// <summary>Linearly blends a scalar value toward a target by a factor in the closed interval <c>[0, 1]</c>.</summary>
    /// <param name="start">Starting value.</param>
    /// <param name="target">Target value.</param>
    /// <param name="t">
    /// Blend factor clamped to <c>[0, 1]</c>. A value of <c>0</c> returns <paramref name="start" />; a value of <c>1</c>
    /// returns <paramref name="target" />.
    /// </param>
    /// <returns>The blended scalar.</returns>
    private static double Blend(double start, double target, double t)
    {
        t = Math.Clamp(t, 0.0, 1.0);

        return start + (target - start) * t;
    }

    /// <summary>
    /// Chooses the initial direction to adjust HSV Value (V) for the foreground in order to locally increase contrast against
    /// the background. Returns <c>+1</c> to brighten or <c>-1</c> to darken.
    /// </summary>
    /// <param name="foreground">Foreground color (opaque).</param>
    /// <param name="background">Background color (opaque).</param>
    /// <returns><c>+1</c> if brightening increases contrast more; otherwise <c>-1</c>.</returns>
    private static int ChooseValueDirection(AryColorValue foreground, AryColorValue background)
    {
        const double step = 2.0; // percent V

        var h = foreground.H;
        var s = foreground.S;
        var v = foreground.V;

        var up = AryColorValue.FromHsva(h, s, v + step);
        var dn = AryColorValue.FromHsva(h, s, v - step);

        var rUp = ContrastRatio(up, background);
        var rDn = ContrastRatio(dn, background);

        return rUp > rDn
            ? +1
            : -1;
    }

    /// <summary>Computes the WCAG contrast ratio between two opaque sRGB colors using relative luminance.</summary>
    /// <param name="foreground">Foreground color (opaque).</param>
    /// <param name="background">Background color (opaque).</param>
    /// <returns>
    /// The contrast ratio defined as <c>(Lighter + 0.05) / (Darker + 0.05)</c>, where <c>L</c> is WCAG relative luminance.
    /// </returns>
    public static double ContrastRatio(AryColorValue foreground, AryColorValue background)
    {
        var lf = RelativeLuminance(foreground);
        var lb = RelativeLuminance(background);

        var lighter = Math.Max(lf, lb);
        var darker = Math.Min(lf, lb);

        return (lighter + 0.05) / (darker + 0.05);
    }

    /// <summary>
    /// Derives a disabled state palette by desaturating colors, biasing value toward mid-tone, and ensuring readable
    /// foreground contrast at a relaxed requirement (defaults to 3.0).
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="desaturateBy">Amount to reduce saturation (percent points).</param>
    /// <param name="valueBlendTowardMid">Blend factor toward V=50% mid-tone.</param>
    /// <param name="minContrast">Minimum contrast ratio for disabled text.</param>
    /// <returns>A derived palette suitable for disabled state.</returns>
    public static AryPalette DeriveDisabled(AryPalette palette,
        double desaturateBy = 60.0,
        double valueBlendTowardMid = 0.15,
        double minContrast = 3.0)
    {
        var background = AryColorValue.FromHsva(
            palette.BackgroundColor.H,
            palette.BackgroundColor.S - desaturateBy,
            Blend(palette.BackgroundColor.V, 50.0, valueBlendTowardMid)
        );

        var border = AryColorValue.FromHsva(
            palette.BorderColor.H,
            palette.BorderColor.S - desaturateBy,
            Blend(palette.BorderColor.V, 50.0, valueBlendTowardMid)
        );

        var foreground = EnsureMinimumContrast(palette.ForegroundColor, background, minContrast).ForegroundColor;

        return palette.Cascade(background, foreground, border);
    }

    /// <summary>
    /// Derives a dragged state palette (for drag-and-drop affordances) with the strongest delta among pointer states, keeping
    /// foreground contrast above <paramref name="minContrast" />.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="backgroundDeltaV">Background value (V) delta.</param>
    /// <param name="borderDeltaV">Border value (V) delta.</param>
    /// <param name="minContrast">Minimum required foreground contrast ratio.</param>
    /// <returns>A derived palette suitable for the dragged state.</returns>
    public static AryPalette DeriveDragged(AryPalette palette,
        double backgroundDeltaV = 16.0,
        double borderDeltaV = 18.0,
        double minContrast = 4.5)
        => NudgeState(palette, backgroundDeltaV, borderDeltaV, minContrast);

    /// <summary>
    /// Derives a focused state palette with stronger nudge than hover to increase perceptibility while maintaining minimum
    /// foreground contrast.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="backgroundDeltaV">Background value (V) delta.</param>
    /// <param name="borderDeltaV">Border value (V) delta.</param>
    /// <param name="minContrast">Minimum required foreground contrast ratio.</param>
    /// <returns>A derived palette suitable for the focused state.</returns>
    public static AryPalette DeriveFocused(AryPalette palette,
        double backgroundDeltaV = 8.0,
        double borderDeltaV = 10.0,
        double minContrast = 4.5)
        => NudgeState(palette, backgroundDeltaV, borderDeltaV, minContrast);

    /// <summary>
    /// Derives a higher elevation palette (high) by nudging background/border and re-ensuring minimum foreground contrast.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="delta">Magnitude of value (V) change.</param>
    /// <returns>The derived high elevation palette.</returns>
    public static AryPalette DeriveHigh(AryPalette palette, double delta = 8.0)
        => NudgeElevation(palette, delta, false);

    /// <summary>
    /// Derives a higher-tier elevation palette (highest) by nudging background/border in the opposite direction of the lower
    /// tiers relative to theme brightness.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="delta">Magnitude of value (V) change.</param>
    /// <returns>The derived highest elevation palette.</returns>
    public static AryPalette DeriveHighest(AryPalette palette, double delta = 12.0)
        => NudgeElevation(palette, delta, false);

    /// <summary>
    /// Derives a hovered state palette by nudging background/border contrast while preserving hue and ensuring foreground
    /// contrast is at least <paramref name="minContrast" />.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="backgroundDeltaV">Magnitude of background value (V) change in percent.</param>
    /// <param name="borderDeltaV">Magnitude of border value (V) change in percent.</param>
    /// <param name="minContrast">Minimum required foreground contrast ratio.</param>
    /// <returns>A derived palette suitable for the hovered state.</returns>
    public static AryPalette DeriveHovered(AryPalette palette,
        double backgroundDeltaV = 6.0,
        double borderDeltaV = 8.0,
        double minContrast = 4.5)
        => NudgeState(palette, backgroundDeltaV, borderDeltaV, minContrast);

    /// <summary>
    /// Derives a lower elevation palette (low) by nudging background/border appropriately for current brightness.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="delta">Magnitude of value (V) change.</param>
    /// <returns>The derived low elevation palette.</returns>
    public static AryPalette DeriveLow(AryPalette palette, double delta = 8.0) => NudgeElevation(palette, delta, true);

    /// <summary>
    /// Derives a lower-tier elevation palette (lowest) by nudging background toward the appropriate darker/lighter direction
    /// for the current theme brightness.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="delta">Magnitude of value (V) change.</param>
    /// <returns>The derived lowest elevation palette.</returns>
    public static AryPalette DeriveLowest(AryPalette palette, double delta = 12.0)
        => NudgeElevation(palette, delta, true);

    /// <summary>
    /// Derives a pressed (active) state palette with a stronger delta to convey interaction depth while maintaining minimum
    /// foreground contrast.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="backgroundDeltaV">Background value (V) delta.</param>
    /// <param name="borderDeltaV">Border value (V) delta.</param>
    /// <param name="minContrast">Minimum required foreground contrast ratio.</param>
    /// <returns>A derived palette suitable for the pressed state.</returns>
    public static AryPalette DerivePressed(AryPalette palette,
        double backgroundDeltaV = 12.0,
        double borderDeltaV = 14.0,
        double minContrast = 4.5)
        => NudgeState(palette, backgroundDeltaV, borderDeltaV, minContrast);

    /// <summary>
    /// Resolves a foreground color that meets (or best-approaches) a minimum contrast ratio over the background by preserving
    /// the foreground hue and saturation (HSV H/S) and adjusting only value (V). If that hue rail cannot reach the target
    /// (even at V = 0% or 100%), the method mixes toward black and white and returns the closest solution that meets—or
    /// best-approaches—the target.
    /// </summary>
    /// <param name="foreground">Starting foreground color (opaque).</param>
    /// <param name="background">Background color (opaque).</param>
    /// <param name="minimumRatio">Required minimum contrast ratio (e.g., <c>4.5</c> for body text).</param>
    /// <returns>
    /// A <see cref="ContrastResult" /> containing the resolved color, background, achieved ratio, and a flag indicating
    /// whether the minimum was met.
    /// </returns>
    public static ContrastResult EnsureMinimumContrast(AryColorValue foreground,
        AryColorValue background,
        double minimumRatio = 3.0)
    {
        // Early accept
        var startRatio = ContrastRatio(foreground, background);

        if (startRatio >= minimumRatio)
        {
            return new ContrastResult(foreground, background, startRatio, true);
        }

        // Reuse AryColorValue’s HSV API to avoid duplicating conversions.
        var h = foreground.H; // degrees
        var s = foreground.S; // percent
        var v = foreground.V; // percent

        // Choose the V direction that locally increases contrast.
        var initialDir = ChooseValueDirection(foreground, background);

        // 1) Try along the hue rail in the better direction first.
        var first = SearchValueRail(h, s, v, initialDir, background, minimumRatio);

        if (first.MeetsMinimum)
        {
            return first;
        }

        // 2) Try the opposite direction on the hue rail.
        var second = SearchValueRail(h, s, v, -initialDir, background, minimumRatio);

        if (second.MeetsMinimum)
        {
            return second;
        }

        // 3) Guarantee path: mix toward white; prefer any that meets; otherwise best-approaching.
        var towardWhite = SearchTowardPole(foreground, Colors.White, background, minimumRatio);

        if (towardWhite.MeetsMinimum)
        {
            return towardWhite;
        }

        // 4) Guarantee path: mix toward black; prefer any that meets; otherwise best-approaching.
        var towardBlack = SearchTowardPole(foreground, Colors.Black, background, minimumRatio);

        if (towardBlack.MeetsMinimum && towardWhite.MeetsMinimum)
        {
            return towardWhite.ContrastRatio >= towardBlack.ContrastRatio
                ? towardWhite
                : towardBlack;
        }

        if (towardBlack.MeetsMinimum)
        {
            return towardBlack;
        }

        // 5) Still not met: return the best-approaching overall.
        var best = first;

        if (second.ContrastRatio > best.ContrastRatio)
        {
            best = second;
        }

        if (towardWhite.ContrastRatio > best.ContrastRatio)
        {
            best = towardWhite;
        }

        return best;
    }

    /// <summary>
    /// Linear interpolation (LERP) in sRGB between two opaque colors. Note: sRGB LERP is not perceptually uniform.
    /// </summary>
    /// <param name="start">Start color.</param>
    /// <param name="end">End color.</param>
    /// <param name="t">Interpolation factor clamped to <c>[0, 1]</c>.</param>
    /// <returns>The interpolated color.</returns>
    private static AryColorValue LerpSrgb(AryColorValue start, AryColorValue end, double t)
    {
        t = Math.Clamp(t, 0.0, 1.0);

        static byte Lerp(byte a, byte b, double tt)
            => (byte)Math.Clamp((int)Math.Round(a + (b - a) * tt, MidpointRounding.AwayFromZero), 0, 255);

        return AryColorValue.FromRgba(
            Lerp(start.R, end.R, t),
            Lerp(start.G, end.G, t),
            Lerp(start.B, end.B, t)
        );
    }

    /// <summary>
    /// Adjusts elevation by nudging background and border value (V) in the direction appropriate for the current brightness
    /// and re-ensures foreground contrast.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="delta">Magnitude of value (V) change.</param>
    /// <param name="lowerTier">
    /// If <see langword="true" />, derives a lower tier (darker on light themes, lighter on dark themes); otherwise derives a
    /// higher tier.
    /// </param>
    /// <returns>The derived elevation palette.</returns>
    private static AryPalette NudgeElevation(AryPalette palette,
        double delta,
        bool lowerTier)
    {
        var isLight = palette.BackgroundColor.V >= 50.0;

        var sign = (isLight, lowerTier) switch
        {
            (true, true) => -1, // darker on light
            (true, false) => +1, // lighter on light
            (false, true) => +1, // lighter on dark
            (false, false) => -1 // darker on dark
        };

        var background = AryColorValue.FromHsva(
            palette.BackgroundColor.H,
            palette.BackgroundColor.S,
            palette.BackgroundColor.V + sign * delta
        );

        var border = AryColorValue.FromHsva(
            palette.BorderColor.H,
            palette.BorderColor.S,
            palette.BorderColor.V + sign * (delta + 2.0)
        );

        var foreground = EnsureMinimumContrast(palette.ForegroundColor, background, 4.5).ForegroundColor;

        return palette.Cascade(background, foreground, border);
    }

    /// <summary>
    /// Adjusts interaction state by nudging background and border value (V) relative to theme brightness and then ensuring
    /// minimum foreground contrast.
    /// </summary>
    /// <param name="palette">Base palette.</param>
    /// <param name="backgroundDelta">Magnitude of background value (V) change.</param>
    /// <param name="borderDelta">Magnitude of border value (V) change.</param>
    /// <param name="minContrast">Minimum required foreground contrast ratio.</param>
    /// <returns>The derived state palette.</returns>
    private static AryPalette NudgeState(AryPalette palette,
        double backgroundDelta,
        double borderDelta,
        double minContrast)
    {
        var direction = palette.BackgroundColor.V >= 50.0
            ? -1.0
            : 1.0; // lighten dark; darken light

        var background = AryColorValue.FromHsva(
            palette.BackgroundColor.H,
            palette.BackgroundColor.S,
            palette.BackgroundColor.V + direction * backgroundDelta
        );

        var border = AryColorValue.FromHsva(
            palette.BorderColor.H,
            palette.BorderColor.S,
            palette.BorderColor.V + direction * borderDelta
        );

        var foreground = EnsureMinimumContrast(palette.ForegroundColor, background, minContrast).ForegroundColor;

        return palette.Cascade(background, foreground, border);
    }

    /// <summary>Computes WCAG relative luminance from an opaque sRGB color.</summary>
    /// <param name="color">Opaque sRGB color.</param>
    /// <returns>Relative luminance in <c>[0, 1]</c>.</returns>
    public static double RelativeLuminance(AryColorValue color)
    {
        var rl = SrgbToLinear(color.R);
        var gl = SrgbToLinear(color.G);
        var bl = SrgbToLinear(color.B);

        return 0.2126 * rl + 0.7152 * gl + 0.0722 * bl;
    }

    /// <summary>
    /// Binary-search mixing of a starting foreground toward a pole (black or white) in sRGB, returning the closest solution
    /// that meets—or best-approaches—the target contrast ratio.
    /// </summary>
    /// <param name="start">Starting foreground color (opaque).</param>
    /// <param name="pole">Target pole (typically <see cref="Colors.Black" /> or <see cref="Colors.White" />).</param>
    /// <param name="background">Background color (opaque).</param>
    /// <param name="minimumRatio">Target contrast ratio.</param>
    /// <returns>The resolution result for this pole.</returns>
    private static ContrastResult SearchTowardPole(AryColorValue start,
        AryColorValue pole,
        AryColorValue background,
        double minimumRatio)
    {
        var lo = 0.0;
        var hi = 1.0;
        const int iters = 18;
        var bestRatio = -1.0;
        var bestColor = start;
        var met = false;

        for (var i = 0; i < iters; i++)
        {
            var mid = 0.5 * (lo + hi);
            var candidate = LerpSrgb(start, pole, mid);
            var ratio = ContrastRatio(candidate, background);

            if (ratio > bestRatio)
            {
                bestRatio = ratio;
                bestColor = candidate;
            }

            if (ratio >= minimumRatio)
            {
                met = true;
                hi = mid; // seek closest-to-start satisfying mix
            }
            else
            {
                lo = mid;
            }
        }

        var finalColor = met
            ? LerpSrgb(start, pole, hi)
            : bestColor;

        var finalRatio = ContrastRatio(finalColor, background);

        return new ContrastResult(finalColor, background, finalRatio, met);
    }

    /// <summary>
    /// Binary search along the HSV value rail (holding H and S constant) to find the minimum-change V that meets a required
    /// contrast ratio; returns the best-approaching candidate when unreachable.
    /// </summary>
    /// <param name="h">Hue in degrees.</param>
    /// <param name="s">Saturation in percent.</param>
    /// <param name="vStart">Starting Value (V) in percent.</param>
    /// <param name="direction"><c>+1</c> to brighten; <c>-1</c> to darken.</param>
    /// <param name="background">Background color (opaque).</param>
    /// <param name="minimumRatio">Target contrast ratio.</param>
    /// <returns>Resolution result for this search branch.</returns>
    private static ContrastResult SearchValueRail(double h,
        double s,
        double vStart,
        int direction,
        AryColorValue background,
        double minimumRatio)
    {
        double lo, hi;

        if (direction > 0)
        {
            lo = vStart;
            hi = 100.0;
        }
        else
        {
            lo = vStart;
            hi = 0.0;
        }

        const int iters = 18;
        double? found = null;
        var bestRatio = -1.0;
        var bestColor = AryColorValue.FromHsva(h, s, vStart);

        for (var i = 0; i < iters; i++)
        {
            var mid = 0.5 * (lo + hi);
            var candidate = AryColorValue.FromHsva(h, s, mid);
            var ratio = ContrastRatio(candidate, background);

            if (ratio > bestRatio)
            {
                bestRatio = ratio;
                bestColor = candidate;
            }

            if (ratio >= minimumRatio)
            {
                found = mid;
                hi = mid; // tighten toward smallest change
            }
            else
            {
                lo = mid;
            }
        }

        if (found.HasValue)
        {
            var final = AryColorValue.FromHsva(h, s, hi);
            var r = ContrastRatio(final, background);

            return new ContrastResult(final, background, r, true);
        }

        return new ContrastResult(bestColor, background, bestRatio, false);
    }

    /// <summary>
    /// Converts an 8-bit sRGB channel to linear-light <c>[0, 1]</c> for luminance computation, using the standard sRGB
    /// electro-optical transfer function (EOTF).
    /// </summary>
    /// <param name="c8">The 8-bit channel value.</param>
    /// <returns>The linear-light channel value.</returns>
    private static double SrgbToLinear(byte c8)
    {
        var c = c8 / 255.0;

        return c <= 0.04045
            ? c / 12.92
            : Math.Pow((c + 0.055) / 1.055, 2.4);
    }
}

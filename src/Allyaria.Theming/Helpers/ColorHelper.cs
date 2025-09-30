using Allyaria.Theming.Constants;
using Allyaria.Theming.Primitives;
using Allyaria.Theming.Values;

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
    public static double Blend(double start, double target, double t)
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
    private static int ChooseValueDirection(AllyariaColorValue foreground, AllyariaColorValue background)
    {
        const double step = 2.0; // percent V

        double h = foreground.H, s = foreground.S, v = foreground.V;

        var up = AllyariaColorValue.FromHsva(h, s, Math.Clamp(v + step, 0.0, 100.0));
        var dn = AllyariaColorValue.FromHsva(h, s, Math.Clamp(v - step, 0.0, 100.0));

        var rUp = ContrastRatio(up, background);
        var rDn = ContrastRatio(dn, background);

        if (Math.Abs(rUp - rDn) < 1e-6)
        {
            // Tie-break: push away from mid to reach an extreme sooner
            return v >= 50.0
                ? -1
                : +1;
        }

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
    public static double ContrastRatio(AllyariaColorValue foreground, AllyariaColorValue background)
    {
        var lf = RelativeLuminance(foreground);
        var lb = RelativeLuminance(background);

        var lighter = Math.Max(lf, lb);
        var darker = Math.Min(lf, lb);

        return (lighter + 0.05) / (darker + 0.05);
    }

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
    public static ContrastResult EnsureMinimumContrast(AllyariaColorValue foreground,
        AllyariaColorValue background,
        double minimumRatio = 3.0)
    {
        // Early accept
        var startRatio = ContrastRatio(foreground, background);

        if (startRatio >= minimumRatio)
        {
            return new ContrastResult(foreground, background, startRatio, true);
        }

        // Reuse AllyariaColorValue’s HSV API to avoid duplicating conversions.
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
        var towardPole = SearchTowardPole(foreground, Colors.White, background, minimumRatio);

        if (towardPole.MeetsMinimum)
        {
            return towardPole;
        }

        // 4) Still not met: return the best-approaching overall.
        var best = first;

        if (second.ContrastRatio > best.ContrastRatio)
        {
            best = second;
        }

        if (towardPole.ContrastRatio > best.ContrastRatio)
        {
            best = towardPole;
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
    private static AllyariaColorValue LerpSrgb(AllyariaColorValue start, AllyariaColorValue end, double t)
    {
        t = Math.Clamp(t, 0.0, 1.0);

        static byte Lerp(byte a, byte b, double tt)
            => (byte)Math.Clamp((int)Math.Round(a + (b - a) * tt, MidpointRounding.AwayFromZero), 0, 255);

        return AllyariaColorValue.FromRgba(
            Lerp(start.R, end.R, t),
            Lerp(start.G, end.G, t),
            Lerp(start.B, end.B, t)
        );
    }

    /// <summary>sRGB-space linear interpolation between two opaque colors.</summary>
    /// <param name="a">Start color.</param>
    /// <param name="b">End color.</param>
    /// <param name="t">Blend factor clamped to <c>[0, 1]</c>.</param>
    /// <returns>Blended color in sRGB.</returns>
    public static AllyariaColorValue MixSrgb(AllyariaColorValue a, AllyariaColorValue b, double t) => LerpSrgb(a, b, t);

    /// <summary>Computes WCAG relative luminance from an opaque sRGB color.</summary>
    /// <param name="color">Opaque sRGB color.</param>
    /// <returns>Relative luminance in <c>[0, 1]</c>.</returns>
    public static double RelativeLuminance(AllyariaColorValue color)
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
    private static ContrastResult SearchTowardPole(AllyariaColorValue start,
        AllyariaColorValue pole,
        AllyariaColorValue background,
        double minimumRatio)
    {
        double lo = 0.0, hi = 1.0;
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
        AllyariaColorValue background,
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
        var bestColor = AllyariaColorValue.FromHsva(h, s, vStart);

        for (var i = 0; i < iters; i++)
        {
            var mid = 0.5 * (lo + hi);
            var candidate = AllyariaColorValue.FromHsva(h, s, mid);
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
            var final = AllyariaColorValue.FromHsva(h, s, hi);
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

        return c <= 0.03928
            ? c / 12.92
            : Math.Pow((c + 0.055) / 1.055, 2.4);
    }
}

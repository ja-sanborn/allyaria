using Allyaria.Theming.Constants;
using Allyaria.Theming.Primitives;
using Allyaria.Theming.Values;

namespace Allyaria.Theming.Helpers;

/// <summary>
/// Color utilities: WCAG contrast computation, hue-preserving contrast repair for opaque colors, and small reusable
/// helpers used by palette derivations (e.g., HSVA clamped creation, scalar blending). No allocations (static helper), no
/// alpha/canvas blending.
/// </summary>
internal static class ColorHelper
{
    /// <summary>Linearly blends a scalar value toward a target by a factor in [0..1].</summary>
    /// <param name="start">Starting value.</param>
    /// <param name="target">Target value.</param>
    /// <param name="t">
    /// Blend factor in [0..1]. <c>0</c> returns <paramref name="start" />, <c>1</c> returns <paramref name="target" />.
    /// </param>
    /// <returns>The blended scalar.</returns>
    public static double Blend(double start, double target, double t)
    {
        t = Math.Clamp(t, 0.0, 1.0);

        return start + (target - start) * t;
    }

    /// <summary>
    /// Chooses the initial direction to adjust V (HSV Value) to locally increase contrast (+1 brighten, -1 darken).
    /// </summary>
    /// <param name="foreground">ForegroundColor (opaque).</param>
    /// <param name="background">BackgroundColor (opaque).</param>
    /// <returns>+1 if brightening increases contrast more; otherwise -1.</returns>
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

    /// <summary>Computes WCAG contrast ratio between two opaque sRGB colors.</summary>
    /// <param name="foreground">ForegroundColor color (opaque).</param>
    /// <param name="background">BackgroundColor color (opaque).</param>
    /// <returns>(L1 + 0.05) / (L2 + 0.05) with WCAG relative luminance.</returns>
    public static double ContrastRatio(AllyariaColorValue foreground, AllyariaColorValue background)
    {
        var lf = RelativeLuminance(foreground);
        var lb = RelativeLuminance(background);

        var lighter = Math.Max(lf, lb);
        var darker = Math.Min(lf, lb);

        return (lighter + 0.05) / (darker + 0.05);
    }

    /// <summary>
    /// Resolves a foreground color that meets a minimum contrast over the background by preserving the foreground hue and
    /// saturation (HSV H/S) and adjusting only value (V). If that hue rail cannot reach the target (even at V=0% or V=100%),
    /// mixes toward black and white and returns the closest solution that meets (or best-approaches) the target.
    /// </summary>
    /// <param name="foreground">Starting foreground (opaque).</param>
    /// <param name="background">BackgroundColor (opaque).</param>
    /// <param name="minimumRatio">Required minimum ratio (e.g., 4.5 for body text).</param>
    /// <returns><see cref="ContrastResult" /> with final color and achieved ratio.</returns>
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

        // 3) Guarantee path: mix toward black and white; prefer any that meets; otherwise best-approaching.
        var towardWhite = SearchTowardPole(foreground, Colors.White, background, minimumRatio);
        var towardBlack = SearchTowardPole(foreground, Colors.Black, background, minimumRatio);

        if (towardWhite.MeetsMinimum && towardBlack.MeetsMinimum)
        {
            // Prefer higher ratio (or the one closer to the threshold depending on your policy).
            return towardWhite.ContrastRatio >= towardBlack.ContrastRatio
                ? towardWhite
                : towardBlack;
        }

        if (towardWhite.MeetsMinimum)
        {
            return towardWhite;
        }

        if (towardBlack.MeetsMinimum)
        {
            return towardBlack;
        }

        // 4) Still not met: return the best-approaching overall.
        var best = first;

        if (second.ContrastRatio > best.ContrastRatio)
        {
            best = second;
        }

        if (towardWhite.ContrastRatio > best.ContrastRatio)
        {
            best = towardWhite;
        }

        if (towardBlack.ContrastRatio > best.ContrastRatio)
        {
            best = towardBlack;
        }

        return best;
    }

    /// <summary>Linear interpolation in sRGB between two opaque colors.</summary>
    /// <param name="start">Start color.</param>
    /// <param name="end">End color.</param>
    /// <param name="t">Mix factor in [0,1].</param>
    /// <returns>Interpolated color.</returns>
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

    /// <summary>
    /// sRGB-space linear interpolation between two opaque colors. Note this is *not* perceptually uniform.
    /// </summary>
    /// <param name="a">Start color.</param>
    /// <param name="b">End color.</param>
    /// <param name="t">Blend factor in [0..1].</param>
    /// <returns>Blended color in sRGB.</returns>
    public static AllyariaColorValue MixSrgb(AllyariaColorValue a, AllyariaColorValue b, double t) => LerpSrgb(a, b, t);

    /// <summary>WCAG relative luminance from sRGB bytes.</summary>
    /// <param name="color">Opaque sRGB color.</param>
    /// <returns>Relative luminance [0..1].</returns>
    public static double RelativeLuminance(AllyariaColorValue color)
    {
        var rl = SrgbToLinear(color.R);
        var gl = SrgbToLinear(color.G);
        var bl = SrgbToLinear(color.B);

        return 0.2126 * rl + 0.7152 * gl + 0.0722 * bl;
    }

    /// <summary>
    /// Binary-search mixing the starting foreground toward a pole (black or white) in sRGB, returning the closest solution
    /// that meets (or best-approaches) the target.
    /// </summary>
    /// <param name="start">Starting foreground (opaque).</param>
    /// <param name="pole">Target pole (<see cref="Colors.Black" /> or <see cref="Colors.White" />).</param>
    /// <param name="background">BackgroundColor (opaque).</param>
    /// <param name="minimumRatio">Target ratio.</param>
    /// <returns>Resolution result for this pole.</returns>
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
    /// Binary search along the HSV Value rail (keeping H and S) to find the minimum-change V that meets the contrast
    /// requirement; returns best-approaching if unreachable.
    /// </summary>
    /// <param name="h">Hue (degrees).</param>
    /// <param name="s">Saturation (percent).</param>
    /// <param name="vStart">Starting Value (percent).</param>
    /// <param name="direction">+1 brighten, -1 darken.</param>
    /// <param name="background">BackgroundColor (opaque).</param>
    /// <param name="minimumRatio">Target ratio.</param>
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
            lo = 0.0;
            hi = vStart;
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

    /// <summary>Converts sRGB 8-bit channel to linear-light [0..1] for luminance computation.</summary>
    /// <param name="c8">Channel byte.</param>
    /// <returns>Linear-light value.</returns>
    private static double SrgbToLinear(byte c8)
    {
        var c = c8 / 255.0;

        return c <= 0.03928
            ? c / 12.92
            : Math.Pow((c + 0.055) / 1.055, 2.4);
    }
}

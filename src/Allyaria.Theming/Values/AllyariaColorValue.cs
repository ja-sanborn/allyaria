using Allyaria.Theming.Constants;
using Allyaria.Theming.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a framework-agnostic color value with CSS-oriented parsing and formatting, immutable value semantics, and
/// total ordering by the uppercase <c>#RRGGBBAA</c> form.
/// </summary>
/// <remarks>
/// This type is an immutable reference type. It supports:
/// <list type="bullet">
///     <item>
///         <description>
///         Parsing from <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, <c>#RRGGBBAA</c>, <c>rgb()</c>, <c>rgba()</c>,
///         <c>hsv()</c>, <c>hsva()</c>, CSS Web color names, and Material color names.
///         </description>
///     </item>
///     <item>
///         <description>Conversions between RGBA and HSVA (H: 0–360°, S/V: 0–100%, A: 0–1).</description>
///     </item>
///     <item>
///         <description>Formatting to multiple string forms and CSS declarations.</description>
///     </item>
///     <item>
///         <description>Value equality and ordering by the canonical <c>#RRGGBBAA</c> string.</description>
///     </item>
/// </list>
/// All numeric parsing/formatting uses <see cref="CultureInfo.InvariantCulture" />.
/// </remarks>
public sealed class AllyariaColorValue : ValueBase
{
    /// <summary>Material Design color lookup table.</summary>
    private static readonly Dictionary<string, AllyariaColorValue> MaterialMap = new(StringComparer.OrdinalIgnoreCase)
    {
        // Red
        ["red50"] = Colors.Red50,
        ["red100"] = Colors.Red100,
        ["red200"] = Colors.Red200,
        ["red300"] = Colors.Red300,
        ["red400"] = Colors.Red400,
        ["red500"] = Colors.Red500,
        ["red600"] = Colors.Red600,
        ["red700"] = Colors.Red700,
        ["red800"] = Colors.Red800,
        ["red900"] = Colors.Red900,
        ["reda100"] = Colors.RedA100,
        ["reda200"] = Colors.RedA200,
        ["reda400"] = Colors.RedA400,
        ["reda700"] = Colors.RedA700,

        // Pink
        ["pink50"] = Colors.Pink50,
        ["pink100"] = Colors.Pink100,
        ["pink200"] = Colors.Pink200,
        ["pink300"] = Colors.Pink300,
        ["pink400"] = Colors.Pink400,
        ["pink500"] = Colors.Pink500,
        ["pink600"] = Colors.Pink600,
        ["pink700"] = Colors.Pink700,
        ["pink800"] = Colors.Pink800,
        ["pink900"] = Colors.Pink900,
        ["pinka100"] = Colors.PinkA100,
        ["pinka200"] = Colors.PinkA200,
        ["pinka400"] = Colors.PinkA400,
        ["pinka700"] = Colors.PinkA700,

        // Purple
        ["purple50"] = Colors.Purple50,
        ["purple100"] = Colors.Purple100,
        ["purple200"] = Colors.Purple200,
        ["purple300"] = Colors.Purple300,
        ["purple400"] = Colors.Purple400,
        ["purple500"] = Colors.Purple500,
        ["purple600"] = Colors.Purple600,
        ["purple700"] = Colors.Purple700,
        ["purple800"] = Colors.Purple800,
        ["purple900"] = Colors.Purple900,
        ["purplea100"] = Colors.PurpleA100,
        ["purplea200"] = Colors.PurpleA200,
        ["purplea400"] = Colors.PurpleA400,
        ["purplea700"] = Colors.PurpleA700,

        // Deep Purple
        ["deeppurple50"] = Colors.Deeppurple50,
        ["deeppurple100"] = Colors.Deeppurple100,
        ["deeppurple200"] = Colors.Deeppurple200,
        ["deeppurple300"] = Colors.Deeppurple300,
        ["deeppurple400"] = Colors.Deeppurple400,
        ["deeppurple500"] = Colors.Deeppurple500,
        ["deeppurple600"] = Colors.Deeppurple600,
        ["deeppurple700"] = Colors.Deeppurple700,
        ["deeppurple800"] = Colors.Deeppurple800,
        ["deeppurple900"] = Colors.Deeppurple900,
        ["deeppurplea100"] = Colors.DeeppurpleA100,
        ["deeppurplea200"] = Colors.DeeppurpleA200,
        ["deeppurplea400"] = Colors.DeeppurpleA400,
        ["deeppurplea700"] = Colors.DeeppurpleA700,

        // Indigo
        ["indigo50"] = Colors.Indigo50,
        ["indigo100"] = Colors.Indigo100,
        ["indigo200"] = Colors.Indigo200,
        ["indigo300"] = Colors.Indigo300,
        ["indigo400"] = Colors.Indigo400,
        ["indigo500"] = Colors.Indigo500,
        ["indigo600"] = Colors.Indigo600,
        ["indigo700"] = Colors.Indigo700,
        ["indigo800"] = Colors.Indigo800,
        ["indigo900"] = Colors.Indigo900,
        ["indigoa100"] = Colors.IndigoA100,
        ["indigoa200"] = Colors.IndigoA200,
        ["indigoa400"] = Colors.IndigoA400,
        ["indigoa700"] = Colors.IndigoA700,

        // Blue
        ["blue50"] = Colors.Blue50,
        ["blue100"] = Colors.Blue100,
        ["blue200"] = Colors.Blue200,
        ["blue300"] = Colors.Blue300,
        ["blue400"] = Colors.Blue400,
        ["blue500"] = Colors.Blue500,
        ["blue600"] = Colors.Blue600,
        ["blue700"] = Colors.Blue700,
        ["blue800"] = Colors.Blue800,
        ["blue900"] = Colors.Blue900,
        ["bluea100"] = Colors.BlueA100,
        ["bluea200"] = Colors.BlueA200,
        ["bluea400"] = Colors.BlueA400,
        ["bluea700"] = Colors.BlueA700,

        // Light Blue
        ["lightblue50"] = Colors.Lightblue50,
        ["lightblue100"] = Colors.Lightblue100,
        ["lightblue200"] = Colors.Lightblue200,
        ["lightblue300"] = Colors.Lightblue300,
        ["lightblue400"] = Colors.Lightblue400,
        ["lightblue500"] = Colors.Lightblue500,
        ["lightblue600"] = Colors.Lightblue600,
        ["lightblue700"] = Colors.Lightblue700,
        ["lightblue800"] = Colors.Lightblue800,
        ["lightblue900"] = Colors.Lightblue900,
        ["lightbluea100"] = Colors.LightblueA100,
        ["lightbluea200"] = Colors.LightblueA200,
        ["lightbluea400"] = Colors.LightblueA400,
        ["lightbluea700"] = Colors.LightblueA700,

        // Cyan
        ["cyan50"] = Colors.Cyan50,
        ["cyan100"] = Colors.Cyan100,
        ["cyan200"] = Colors.Cyan200,
        ["cyan300"] = Colors.Cyan300,
        ["cyan400"] = Colors.Cyan400,
        ["cyan500"] = Colors.Cyan500,
        ["cyan600"] = Colors.Cyan600,
        ["cyan700"] = Colors.Cyan700,
        ["cyan800"] = Colors.Cyan800,
        ["cyan900"] = Colors.Cyan900,
        ["cyana100"] = Colors.CyanA100,
        ["cyana200"] = Colors.CyanA200,
        ["cyana400"] = Colors.CyanA400,
        ["cyana700"] = Colors.CyanA700,

        // Teal
        ["teal50"] = Colors.Teal50,
        ["teal100"] = Colors.Teal100,
        ["teal200"] = Colors.Teal200,
        ["teal300"] = Colors.Teal300,
        ["teal400"] = Colors.Teal400,
        ["teal500"] = Colors.Teal500,
        ["teal600"] = Colors.Teal600,
        ["teal700"] = Colors.Teal700,
        ["teal800"] = Colors.Teal800,
        ["teal900"] = Colors.Teal900,
        ["teala100"] = Colors.TealA100,
        ["teala200"] = Colors.TealA200,
        ["teala400"] = Colors.TealA400,
        ["teala700"] = Colors.TealA700,

        // Green
        ["green50"] = Colors.Green50,
        ["green100"] = Colors.Green100,
        ["green200"] = Colors.Green200,
        ["green300"] = Colors.Green300,
        ["green400"] = Colors.Green400,
        ["green500"] = Colors.Green500,
        ["green600"] = Colors.Green600,
        ["green700"] = Colors.Green700,
        ["green800"] = Colors.Green800,
        ["green900"] = Colors.Green900,
        ["greena100"] = Colors.GreenA100,
        ["greena200"] = Colors.GreenA200,
        ["greena400"] = Colors.GreenA400,
        ["greena700"] = Colors.GreenA700,

        // Light Green
        ["lightgreen50"] = Colors.Lightgreen50,
        ["lightgreen100"] = Colors.Lightgreen100,
        ["lightgreen200"] = Colors.Lightgreen200,
        ["lightgreen300"] = Colors.Lightgreen300,
        ["lightgreen400"] = Colors.Lightgreen400,
        ["lightgreen500"] = Colors.Lightgreen500,
        ["lightgreen600"] = Colors.Lightgreen600,
        ["lightgreen700"] = Colors.Lightgreen700,
        ["lightgreen800"] = Colors.Lightgreen800,
        ["lightgreen900"] = Colors.Lightgreen900,
        ["lightgreena100"] = Colors.LightgreenA100,
        ["lightgreena200"] = Colors.LightgreenA200,
        ["lightgreena400"] = Colors.LightgreenA400,
        ["lightgreena700"] = Colors.LightgreenA700,

        // Lime
        ["lime50"] = Colors.Lime50,
        ["lime100"] = Colors.Lime100,
        ["lime200"] = Colors.Lime200,
        ["lime300"] = Colors.Lime300,
        ["lime400"] = Colors.Lime400,
        ["lime500"] = Colors.Lime500,
        ["lime600"] = Colors.Lime600,
        ["lime700"] = Colors.Lime700,
        ["lime800"] = Colors.Lime800,
        ["lime900"] = Colors.Lime900,
        ["limea100"] = Colors.LimeA100,
        ["limea200"] = Colors.LimeA200,
        ["limea400"] = Colors.LimeA400,
        ["limea700"] = Colors.LimeA700,

        // Yellow
        ["yellow50"] = Colors.Yellow50,
        ["yellow100"] = Colors.Yellow100,
        ["yellow200"] = Colors.Yellow200,
        ["yellow300"] = Colors.Yellow300,
        ["yellow400"] = Colors.Yellow400,
        ["yellow500"] = Colors.Yellow500,
        ["yellow600"] = Colors.Yellow600,
        ["yellow700"] = Colors.Yellow700,
        ["yellow800"] = Colors.Yellow800,
        ["yellow900"] = Colors.Yellow900,
        ["yellowa100"] = Colors.YellowA100,
        ["yellowa200"] = Colors.YellowA200,
        ["yellowa400"] = Colors.YellowA400,
        ["yellowa700"] = Colors.YellowA700,

        // Amber
        ["amber50"] = Colors.Amber50,
        ["amber100"] = Colors.Amber100,
        ["amber200"] = Colors.Amber200,
        ["amber300"] = Colors.Amber300,
        ["amber400"] = Colors.Amber400,
        ["amber500"] = Colors.Amber500,
        ["amber600"] = Colors.Amber600,
        ["amber700"] = Colors.Amber700,
        ["amber800"] = Colors.Amber800,
        ["amber900"] = Colors.Amber900,
        ["ambera100"] = Colors.AmberA100,
        ["ambera200"] = Colors.AmberA200,
        ["ambera400"] = Colors.AmberA400,
        ["ambera700"] = Colors.AmberA700,

        // Orange
        ["orange50"] = Colors.Orange50,
        ["orange100"] = Colors.Orange100,
        ["orange200"] = Colors.Orange200,
        ["orange300"] = Colors.Orange300,
        ["orange400"] = Colors.Orange400,
        ["orange500"] = Colors.Orange500,
        ["orange600"] = Colors.Orange600,
        ["orange700"] = Colors.Orange700,
        ["orange800"] = Colors.Orange800,
        ["orange900"] = Colors.Orange900,
        ["orangea100"] = Colors.OrangeA100,
        ["orangea200"] = Colors.OrangeA200,
        ["orangea400"] = Colors.OrangeA400,
        ["orangea700"] = Colors.OrangeA700,

        // Deep Orange
        ["deeporange50"] = Colors.Deeporange50,
        ["deeporange100"] = Colors.Deeporange100,
        ["deeporange200"] = Colors.Deeporange200,
        ["deeporange300"] = Colors.Deeporange300,
        ["deeporange400"] = Colors.Deeporange400,
        ["deeporange500"] = Colors.Deeporange500,
        ["deeporange600"] = Colors.Deeporange600,
        ["deeporange700"] = Colors.Deeporange700,
        ["deeporange800"] = Colors.Deeporange800,
        ["deeporange900"] = Colors.Deeporange900,
        ["deeporangea100"] = Colors.DeeporangeA100,
        ["deeporangea200"] = Colors.DeeporangeA200,
        ["deeporangea400"] = Colors.DeeporangeA400,
        ["deeporangea700"] = Colors.DeeporangeA700,

        // Brown (no accents in Material 2 paletteVariant)
        ["brown50"] = Colors.Brown50,
        ["brown100"] = Colors.Brown100,
        ["brown200"] = Colors.Brown200,
        ["brown300"] = Colors.Brown300,
        ["brown400"] = Colors.Brown400,
        ["brown500"] = Colors.Brown500,
        ["brown600"] = Colors.Brown600,
        ["brown700"] = Colors.Brown700,
        ["brown800"] = Colors.Brown800,
        ["brown900"] = Colors.Brown900,

        // Blue Grey (no accents)
        ["bluegrey50"] = Colors.Bluegrey50,
        ["bluegrey100"] = Colors.Bluegrey100,
        ["bluegrey200"] = Colors.Bluegrey200,
        ["bluegrey300"] = Colors.Bluegrey300,
        ["bluegrey400"] = Colors.Bluegrey400,
        ["bluegrey500"] = Colors.Bluegrey500,
        ["bluegrey600"] = Colors.Bluegrey600,
        ["bluegrey700"] = Colors.Bluegrey700,
        ["bluegrey800"] = Colors.Bluegrey800,
        ["bluegrey900"] = Colors.Bluegrey900,

        // Grey (no accents)
        ["grey50"] = Colors.Grey50,
        ["grey100"] = Colors.Grey100,
        ["grey200"] = Colors.Grey200,
        ["grey300"] = Colors.Grey300,
        ["grey400"] = Colors.Grey400,
        ["grey500"] = Colors.Grey500,
        ["grey600"] = Colors.Grey600,
        ["grey700"] = Colors.Grey700,
        ["grey800"] = Colors.Grey800,
        ["grey900"] = Colors.Grey900,

        // Monochrome convenience entries
        ["black"] = Colors.Black,
        ["white"] = Colors.White
    };

    /// <summary>
    /// Compiled regular expression for parsing CSS <c>hsv()</c> and <c>hsva()</c> functions with a safe timeout.
    /// </summary>
    private static readonly Regex RxHsv = new(
        @"^hsva?\s*\(\s*(?<h>(\d*\.)?\d+)\s*,\s*(?<s>(\d*\.)?\d+)\s*%\s*,\s*(?<v>(\d*\.)?\d+)\s*%(?:\s*,\s*(?<a>((\d*\.)?\d+)))?\s*\)\s*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
        TimeSpan.FromMilliseconds(250)
    );

    /// <summary>
    /// Compiled regular expression for parsing CSS <c>rgb()</c> and <c>rgba()</c> functions with a safe timeout.
    /// </summary>
    private static readonly Regex RxRgb = new(
        @"^rgba?\s*\(\s*(?<r>\d{1,3})\s*,\s*(?<g>\d{1,3})\s*,\s*(?<b>\d{1,3})(?:\s*,\s*(?<a>((\d*\.)?\d+)))?\s*\)\s*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
        TimeSpan.FromMilliseconds(250)
    );

    /// <summary>CSS Web color lookup table.</summary>
    private static readonly Dictionary<string, AllyariaColorValue> WebNameMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["aliceblue"] = Colors.Aliceblue,
        ["antiquewhite"] = Colors.Antiquewhite,
        ["aqua"] = Colors.Aqua,
        ["aquamarine"] = Colors.Aquamarine,
        ["azure"] = Colors.Azure,
        ["beige"] = Colors.Beige,
        ["bisque"] = Colors.Bisque,
        ["black"] = Colors.Black,
        ["blanchedalmond"] = Colors.Blanchedalmond,
        ["blue"] = Colors.Blue,
        ["blueviolet"] = Colors.Blueviolet,
        ["brown"] = Colors.Brown,
        ["burlywood"] = Colors.Burlywood,
        ["cadetblue"] = Colors.Cadetblue,
        ["chartreuse"] = Colors.Chartreuse,
        ["chocolate"] = Colors.Chocolate,
        ["coral"] = Colors.Coral,
        ["cornflowerblue"] = Colors.Cornflowerblue,
        ["cornsilk"] = Colors.Cornsilk,
        ["crimson"] = Colors.Crimson,
        ["cyan"] = Colors.Cyan,
        ["darkblue"] = Colors.Darkblue,
        ["darkcyan"] = Colors.Darkcyan,
        ["darkgoldenrod"] = Colors.Darkgoldenrod,
        ["darkgray"] = Colors.Darkgray,
        ["darkgreen"] = Colors.Darkgreen,
        ["darkkhaki"] = Colors.Darkkhaki,
        ["darkmagenta"] = Colors.Darkmagenta,
        ["darkolivegreen"] = Colors.Darkolivegreen,
        ["darkorange"] = Colors.Darkorange,
        ["darkorchid"] = Colors.Darkorchid,
        ["darkred"] = Colors.Darkred,
        ["darksalmon"] = Colors.Darksalmon,
        ["darkseagreen"] = Colors.Darkseagreen,
        ["darkslateblue"] = Colors.Darkslateblue,
        ["darkslategray"] = Colors.Darkslategray,
        ["darkturquoise"] = Colors.Darkturquoise,
        ["darkviolet"] = Colors.Darkviolet,
        ["deeppink"] = Colors.Deeppink,
        ["deepskyblue"] = Colors.Deepskyblue,
        ["dimgray"] = Colors.Dimgray,
        ["dodgerblue"] = Colors.Dodgerblue,
        ["firebrick"] = Colors.Firebrick,
        ["floralwhite"] = Colors.Floralwhite,
        ["forestgreen"] = Colors.Forestgreen,
        ["fuchsia"] = Colors.Fuchsia,
        ["gainsboro"] = Colors.Gainsboro,
        ["ghostwhite"] = Colors.Ghostwhite,
        ["gold"] = Colors.Gold,
        ["goldenrod"] = Colors.Goldenrod,
        ["gray"] = Colors.Gray,
        ["green"] = Colors.Green,
        ["greenyellow"] = Colors.Greenyellow,
        ["honeydew"] = Colors.Honeydew,
        ["hotpink"] = Colors.Hotpink,
        ["indianred"] = Colors.Indianred,
        ["indigo"] = Colors.Indigo,
        ["ivory"] = Colors.Ivory,
        ["khaki"] = Colors.Khaki,
        ["lavender"] = Colors.Lavender,
        ["lavenderblush"] = Colors.Lavenderblush,
        ["lawngreen"] = Colors.Lawngreen,
        ["lemonchiffon"] = Colors.Lemonchiffon,
        ["lightblue"] = Colors.Lightblue,
        ["lightcoral"] = Colors.Lightcoral,
        ["lightcyan"] = Colors.Lightcyan,
        ["lightgoldenrodyellow"] = Colors.Lightgoldenrodyellow,
        ["lightgray"] = Colors.Lightgray,
        ["lightgreen"] = Colors.Lightgreen,
        ["lightpink"] = Colors.Lightpink,
        ["lightsalmon"] = Colors.Lightsalmon,
        ["lightseagreen"] = Colors.Lightseagreen,
        ["lightskyblue"] = Colors.Lightskyblue,
        ["lightslategray"] = Colors.Lightslategray,
        ["lightsteelblue"] = Colors.Lightsteelblue,
        ["lightyellow"] = Colors.Lightyellow,
        ["lime"] = Colors.Lime,
        ["limegreen"] = Colors.LimeGreen,
        ["linen"] = Colors.Linen,
        ["magenta"] = Colors.Magenta,
        ["maroon"] = Colors.Maroon,
        ["mediumaquamarine"] = Colors.Mediumaquamarine,
        ["mediumblue"] = Colors.Mediumblue,
        ["mediumorchid"] = Colors.Mediumorchid,
        ["mediumpurple"] = Colors.Mediumpurple,
        ["mediumseagreen"] = Colors.Mediumseagreen,
        ["mediumslateblue"] = Colors.Mediumslateblue,
        ["mediumspringgreen"] = Colors.Mediumspringgreen,
        ["mediumturquoise"] = Colors.Mediumturquoise,
        ["mediumvioletred"] = Colors.Mediumvioletred,
        ["midnightblue"] = Colors.Midnightblue,
        ["mintcream"] = Colors.Mintcream,
        ["mistyrose"] = Colors.Mistyrose,
        ["moccasin"] = Colors.Moccasin,
        ["navajowhite"] = Colors.Navajowhite,
        ["navy"] = Colors.Navy,
        ["oldlace"] = Colors.Oldlace,
        ["olive"] = Colors.Olive,
        ["olivedrab"] = Colors.Olivedrab,
        ["orange"] = Colors.Orange,
        ["orangered"] = Colors.Orangered,
        ["orchid"] = Colors.Orchid,
        ["palegoldenrod"] = Colors.Palegoldenrod,
        ["palegreen"] = Colors.Palegreen,
        ["paleturquoise"] = Colors.Paleturquoise,
        ["palevioletred"] = Colors.Palevioletred,
        ["papayawhip"] = Colors.Papayawhip,
        ["peachpuff"] = Colors.Peachpuff,
        ["peru"] = Colors.Peru,
        ["pink"] = Colors.Pink,
        ["plum"] = Colors.Plum,
        ["powderblue"] = Colors.Powderblue,
        ["purple"] = Colors.Purple,
        ["red"] = Colors.Red,
        ["rosybrown"] = Colors.Rosybrown,
        ["royalblue"] = Colors.Royalblue,
        ["saddlebrown"] = Colors.Saddlebrown,
        ["salmon"] = Colors.Salmon,
        ["sandybrown"] = Colors.Sandybrown,
        ["seagreen"] = Colors.Seagreen,
        ["seashell"] = Colors.Seashell,
        ["sienna"] = Colors.Sienna,
        ["silver"] = Colors.Silver,
        ["skyblue"] = Colors.Skyblue,
        ["slateblue"] = Colors.Slateblue,
        ["slategray"] = Colors.Slategray,
        ["snow"] = Colors.Snow,
        ["springgreen"] = Colors.Springgreen,
        ["steelblue"] = Colors.Steelblue,
        ["tan"] = Colors.Tan,
        ["teal"] = Colors.Teal,
        ["thistle"] = Colors.Thistle,
        ["tomato"] = Colors.Tomato,
        ["turquoise"] = Colors.Turquoise,
        ["violet"] = Colors.Violet,
        ["wheat"] = Colors.Wheat,
        ["white"] = Colors.White,
        ["whitesmoke"] = Colors.Whitesmoke,
        ["yellow"] = Colors.Yellow,
        ["yellowgreen"] = Colors.Yellowgreen,
        ["transparent"] = Colors.Transparent
    };

    /// <summary>Initializes an instance from HSV(A) channels after validation and conversion to RGBA.</summary>
    /// <param name="h">Hue in degrees (0–360).</param>
    /// <param name="s">Saturation in percent (0–100).</param>
    /// <param name="v">Value (brightness) in percent (0–100).</param>
    /// <param name="a">Alpha in [0–1].</param>
    /// <exception cref="AllyariaArgumentException">Thrown if any channel lies outside the valid range.</exception>
    private AllyariaColorValue(double h, double s, double v, double a = 1.0)
        : base(string.Empty)
    {
        AllyariaArgumentException.ThrowIfOutOfRange<double>(h, 0.0, 360.0, nameof(h));
        AllyariaArgumentException.ThrowIfOutOfRange<double>(s, 0.0, 100.0, nameof(s));
        AllyariaArgumentException.ThrowIfOutOfRange<double>(v, 0.0, 100.0, nameof(v));
        AllyariaArgumentException.ThrowIfOutOfRange<double>(a, 0.0, 1.0, nameof(a));

        HsvToRgb(h, s, v, out var r, out var g, out var b);
        R = r;
        G = g;
        B = b;
        A = a;
    }

    /// <summary>Initializes an instance from RGB(A) channels after validation.</summary>
    /// <param name="r">Red channel in [0–255].</param>
    /// <param name="g">Green channel in [0–255].</param>
    /// <param name="b">Blue channel in [0–255].</param>
    /// <param name="a">Alpha in [0–1].</param>
    /// <exception cref="AllyariaArgumentException">Thrown if <paramref name="a" /> lies outside [0,1].</exception>
    private AllyariaColorValue(byte r, byte g, byte b, double a = 1.0)
        : base(string.Empty)
    {
        AllyariaArgumentException.ThrowIfOutOfRange<double>(a, 0.0, 1.0, nameof(a));

        R = r;
        G = g;
        B = b;
        A = a;
    }

    /// <summary>Initializes an instance by parsing the provided CSS/hex/name color string.</summary>
    /// <param name="value">
    /// A color value in <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, <c>#RRGGBBAA</c>, <c>rgb()</c>, <c>rgba()</c>, <c>hsv()</c>
    /// , <c>hsva()</c>, a CSS Web color name, or a Material color name.
    /// </param>
    /// <exception cref="AllyariaArgumentException">Thrown when the value cannot be parsed.</exception>
    public AllyariaColorValue(string value)
        : base(string.Empty)
    {
        try
        {
            AllyariaArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            var s = value.Trim();

            // Hex forms
            if (s.StartsWith("#", StringComparison.Ordinal))
            {
                FromHexString(s, out var r, out var g, out var b, out var a);
                R = r;
                G = g;
                B = b;
                A = a;

                return;
            }

            // rgb()/rgba()
            if (s.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
            {
                FromRgbString(s, out var r, out var g, out var b, out var a);
                R = r;
                G = g;
                B = b;
                A = a;

                return;
            }

            // hsv()/hsva()
            if (s.StartsWith("hsv", StringComparison.OrdinalIgnoreCase))
            {
                FromHsvString(s, out var r, out var g, out var b, out var a);
                R = r;
                G = g;
                B = b;
                A = a;

                return;
            }

            // Named palettes
            if (TryFromMaterialName(s, out var mat))
            {
                R = mat.R;
                G = mat.G;
                B = mat.B;
                A = mat.A;

                return;
            }

            if (TryFromWebName(s, out var web))
            {
                R = web.R;
                G = web.G;
                B = web.B;
                A = web.A;

                return;
            }

            throw new AllyariaArgumentException("Unable to parse value.", nameof(value), value);
        }
        catch (Exception exception)
        {
            throw new AllyariaArgumentException(
                $"Unrecognized color: '{value}'. Expected #RRGGBB, #RRGGBBAA, rgb(), rgba(), hsv(), hsva(), a CSS Web color name, or a Material color name.",
                nameof(value), value, exception
            );
        }
    }

    /// <summary>Gets the alpha channel as a unit value in the range [0..1].</summary>
    public double A { get; }

    /// <summary>Gets the alpha component rendered as a byte in the range <c>[0..255]</c>.</summary>
    /// <remarks>
    /// The underlying <see cref="A" /> value (unit interval) is multiplied by 255 and rounded using
    /// <see cref="MidpointRounding.AwayFromZero" />; the result is then clamped to <c>[0..255]</c>.
    /// </remarks>
    private byte AlphaByte => (byte)Math.Clamp((int)Math.Round(A * 255.0, MidpointRounding.AwayFromZero), 0, 255);

    /// <summary>Gets the blue channel in the range [0..255].</summary>
    public byte B { get; }

    /// <summary>Gets the green channel in the range [0..255].</summary>
    public byte G { get; }

    /// <summary>Gets the hue in degrees in the range [0..360].</summary>
    /// <remarks>The value is computed from the underlying RGB channels.</remarks>
    public double H
    {
        get
        {
            RgbToHsv(R, G, B, out var h, out _, out _);

            return h;
        }
    }

    /// <summary>Gets the uppercase <c>#RRGGBB</c> representation of the color (alpha omitted).</summary>
    public string HexRgb => $"#{R:X2}{G:X2}{B:X2}";

    /// <summary>Gets the uppercase <c>#RRGGBBAA</c> representation of the color (alpha included).</summary>
    public string HexRgba => $"#{R:X2}{G:X2}{B:X2}{AlphaByte:X2}";

    /// <summary>Gets the <c>hsv(H, S%, V%)</c> representation using invariant culture.</summary>
    public string Hsv
    {
        get
        {
            RgbToHsv(R, G, B, out var h, out var s, out var v);

            return string.Create(CultureInfo.InvariantCulture, $"hsv({h:0.##}, {s:0.##}%, {v:0.##}%)");
        }
    }

    /// <summary>Gets the <c>hsva(H, S%, V%, A)</c> representation using invariant culture.</summary>
    public string Hsva
    {
        get
        {
            RgbToHsv(R, G, B, out var h, out var s, out var v);

            return string.Create(CultureInfo.InvariantCulture, $"hsva({h:0.##}, {s:0.##}%, {v:0.##}%, {A:0.###})");
        }
    }

    /// <summary>Gets the red channel in the range [0..255].</summary>
    public byte R { get; }

    /// <summary>Gets the <c>rgb(r, g, b)</c> representation.</summary>
    public string Rgb => $"rgb({R}, {G}, {B})";

    /// <summary>
    /// Gets the <c>rgba(r, g, b, a)</c> representation using invariant culture, where <c>a</c> is shown in [0..1].
    /// </summary>
    public string Rgba => string.Create(CultureInfo.InvariantCulture, $"rgba({R}, {G}, {B}, {A:0.###})");

    /// <summary>Gets the saturation in percent in the range [0..100].</summary>
    /// <remarks>The value is computed from the underlying RGB channels.</remarks>
    public double S
    {
        get
        {
            RgbToHsv(R, G, B, out _, out var s, out _);

            return s;
        }
    }

    /// <summary>Gets the value (brightness) in percent in the range [0..100].</summary>
    /// <remarks>The value is computed from the underlying RGB channels.</remarks>
    public double V
    {
        get
        {
            RgbToHsv(R, G, B, out _, out _, out var v);

            return v;
        }
    }

    /// <summary>Gets the canonical string value for styles (uppercase <c>#RRGGBBAA</c>).</summary>
    public override string Value => HexRgba;

    /// <summary>Parses a hexadecimal CSS color literal.</summary>
    /// <param name="s">A hex color string of the form <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, or <c>#RRGGBBAA</c>.</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <param name="a">Outputs the alpha channel (0–1).</param>
    /// <exception cref="AllyariaArgumentException">
    /// Thrown when <paramref name="s" /> is not a supported hex format or has an
    /// invalid length.
    /// </exception>
    private static void FromHexString(string s, out byte r, out byte g, out byte b, out double a)
    {
        var hex = s.Trim();
        hex = hex[1..];

        if (hex.Length is 3 or 4)
        {
            // #RGB or #RGBA -> expand nibbles
            var r1 = ToHexNibble(hex[0]);
            var g1 = ToHexNibble(hex[1]);
            var b1 = ToHexNibble(hex[2]);
            r = (byte)(r1 * 17);
            g = (byte)(g1 * 17);
            b = (byte)(b1 * 17);

            a = hex.Length == 4
                ? Math.Clamp(ToHexNibble(hex[3]) * 17 / 255.0, 0.0, 1.0)
                : 1.0;

            return;
        }

        if (hex.Length is 6 or 8)
        {
            r = Convert.ToByte(hex.Substring(0, 2), 16);
            g = Convert.ToByte(hex.Substring(2, 2), 16);
            b = Convert.ToByte(hex.Substring(4, 2), 16);

            a = hex.Length == 8
                ? Convert.ToByte(hex.Substring(6, 2), 16) / 255.0
                : 1.0;

            return;
        }

        throw new AllyariaArgumentException(
            $"Hex color must be #RGB, #RGBA, #RRGGBB, or #RRGGBBAA: '{s}'.", nameof(s), s
        );
    }

    /// <summary>Returns a color from HSVA channels.</summary>
    /// <param name="h">Hue in degrees, clamped to [0..360].</param>
    /// <param name="s">Saturation in percent, clamped to [0..100].</param>
    /// <param name="v">Value (brightness) in percent, clamped to [0..100].</param>
    /// <param name="a">Alpha in [0..1], clamped.</param>
    /// <returns>The <see cref="AllyariaColorValue" /> from the HSVA channels.</returns>
    public static AllyariaColorValue FromHsva(double h, double s, double v, double a = 1.0)
    {
        var hh = h % 360.0;

        if (hh < 0.0)
        {
            hh += 360.0;
        }

        var ss = Math.Clamp(s, 0.0, 100.0);
        var vv = Math.Clamp(v, 0.0, 100.0);
        var aa = Math.Clamp(a, 0.0, 1.0);

        return new AllyariaColorValue(hh, ss, vv, aa);
    }

    /// <summary>Parses an <c>hsv(H,S%,V%)</c> or <c>hsva(H,S%,V%,A)</c> CSS color function.</summary>
    /// <param name="s">The input string to parse.</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <param name="a">Outputs the alpha channel (0–1). Defaults to 1.0 when omitted.</param>
    /// <exception cref="AllyariaArgumentException">
    /// Thrown when the string is not in <c>hsv()</c>/<c>hsva()</c> form or
    /// contains out-of-range values.
    /// </exception>
    private static void FromHsvString(string s, out byte r, out byte g, out byte b, out double a)
    {
        var m = RxHsv.Match(s);

        if (!m.Success)
        {
            throw new AllyariaArgumentException(
                $"Invalid hsv/hsva() format: '{s}'. Expected hsv(H,S%,V%) or hsva(H,S%,V%,A).", nameof(s), s
            );
        }

        var h = Math.Clamp(ParseDouble(m.Groups["h"].Value, "H", 0, 360), 0, 360);
        var sp = Math.Clamp(ParseDouble(m.Groups["s"].Value, "S%", 0, 100), 0, 100);
        var vp = Math.Clamp(ParseDouble(m.Groups["v"].Value, "V%", 0, 100), 0, 100);

        a = m.Groups["a"].Success
            ? Math.Clamp(ParseDouble(m.Groups["a"].Value, "A", 0, 1), 0.0, 1.0)
            : 1.0;

        HsvToRgb(h, sp, vp, out r, out g, out b);
    }

    /// <summary>Returns a color from RGBA channels.</summary>
    /// <param name="r">Red in [0..255].</param>
    /// <param name="g">Green in [0..255].</param>
    /// <param name="b">Blue in [0..255].</param>
    /// <param name="a">Alpha in [0..1], clamped.</param>
    /// <returns>The <see cref="AllyariaColorValue" /> from the RGBA channels.</returns>
    public static AllyariaColorValue FromRgba(byte r, byte g, byte b, double a = 1.0) => new(r, g, b, a);

    /// <summary>Parses an <c>rgb(r,g,b)</c> or <c>rgba(r,g,b,a)</c> CSS color function.</summary>
    /// <param name="s">The input string to parse.</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <param name="a">Outputs the alpha channel (0–1). Defaults to 1.0 when omitted.</param>
    /// <exception cref="AllyariaArgumentException">
    /// Thrown when the string is not in <c>rgb()</c>/<c>rgba()</c> form or
    /// contains out-of-range values.
    /// </exception>
    private static void FromRgbString(string s, out byte r, out byte g, out byte b, out double a)
    {
        var m = RxRgb.Match(s);

        if (!m.Success)
        {
            throw new AllyariaArgumentException(
                $"Invalid rgb/rgba() format: '{s}'. Expected rgb(r,g,b) or rgba(r,g,b,a).", nameof(s), s
            );
        }

        r = (byte)Math.Clamp(ParseInt(m.Groups["r"].Value, "r", 0, 255), 0, 255);
        g = (byte)Math.Clamp(ParseInt(m.Groups["g"].Value, "g", 0, 255), 0, 255);
        b = (byte)Math.Clamp(ParseInt(m.Groups["b"].Value, "b", 0, 255), 0, 255);

        a = m.Groups["a"].Success
            ? Math.Clamp(ParseDouble(m.Groups["a"].Value, "a", 0, 1), 0.0, 1.0)
            : 1.0;
    }

    /// <summary>Converts HSV to RGB bytes.</summary>
    /// <param name="h">Hue in degrees (<c>0..360</c>).</param>
    /// <param name="s">Saturation in percent (<c>0..100</c>).</param>
    /// <param name="v">Value (brightness) in percent (<c>0..100</c>).</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <remarks>
    /// This method normalizes <paramref name="h" /> to <c>[0,360)</c>, converts <paramref name="s" /> and
    /// <paramref name="v" /> from percent to unit, performs the sector-based conversion, and rounds the results to bytes.
    /// </remarks>
    private static void HsvToRgb(double h,
        double s,
        double v,
        out byte r,
        out byte g,
        out byte b)
    {
        s = Math.Clamp(s / 100.0, 0.0, 1.0);
        v = Math.Clamp(v / 100.0, 0.0, 1.0);

        if (s <= 0.0)
        {
            var c = (byte)Math.Round(v * 255.0);
            r = g = b = c;

            return;
        }

        h = (h % 360 + 360) % 360;
        var hh = h / 60.0;
        var i = (int)Math.Floor(hh);
        var ff = hh - i;

        var p = v * (1.0 - s);
        var q = v * (1.0 - s * ff);
        var t = v * (1.0 - s * (1.0 - ff));

        double r1, g1, b1;

        switch (i)
        {
            case 0:
                r1 = v;
                g1 = t;
                b1 = p;

                break;
            case 1:
                r1 = q;
                g1 = v;
                b1 = p;

                break;
            case 2:
                r1 = p;
                g1 = v;
                b1 = t;

                break;
            case 3:
                r1 = p;
                g1 = q;
                b1 = v;

                break;
            case 4:
                r1 = t;
                g1 = p;
                b1 = v;

                break;
            default:
                r1 = v;
                g1 = p;
                b1 = q;

                break;
        }

        r = (byte)Math.Clamp((int)Math.Round(r1 * 255.0), 0, 255);
        g = (byte)Math.Clamp((int)Math.Round(g1 * 255.0), 0, 255);
        b = (byte)Math.Clamp((int)Math.Round(b1 * 255.0), 0, 255);
    }

    /// <summary>Normalizes a Material color key for lookup.</summary>
    /// <param name="input">An input such as <c>"Deep Purple 200"</c>, <c>"deep-purple-200"</c>, or <c>"red500"</c>.</param>
    /// <returns>A normalized, lower-case key without spaces, dashes, or underscores (e.g., <c>"deeppurple200"</c>).</returns>
    private static string NormalizeMaterialKey(string input)
        => input.Trim().ToLowerInvariant()
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Replace("_", string.Empty);

    /// <summary>Parses a color string into an <see cref="AllyariaColorValue" />.</summary>
    /// <param name="value">The color value to parse.</param>
    /// <returns>The parsed <see cref="AllyariaColorValue" />.</returns>
    /// <exception cref="AllyariaArgumentException">Thrown when parsing fails.</exception>
    public static AllyariaColorValue Parse(string value) => new(value);

    /// <summary>
    /// Parses a floating-point number using invariant culture and validates the result against the inclusive range.
    /// </summary>
    /// <param name="value">The source text.</param>
    /// <param name="param">A parameter name used in exception messages.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <returns>The parsed number (or <c>0</c> if parsing fails before range validation).</returns>
    /// <exception cref="AllyariaArgumentException">Thrown when the resulting value lies outside the provided range.</exception>
    private static double ParseDouble(string value, string param, int min, int max)
    {
        double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var v);
        AllyariaArgumentException.ThrowIfOutOfRange<double>(v, min, max, param);

        return v;
    }

    /// <summary>Parses an integer using invariant culture and validates it against the inclusive range.</summary>
    /// <param name="value">The source text.</param>
    /// <param name="param">A parameter name used in exception messages.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <returns>The parsed integer (or <c>0</c> if parsing fails before range validation).</returns>
    /// <exception cref="AllyariaArgumentException">Thrown when the resulting value lies outside the provided range.</exception>
    private static int ParseInt(string value, string param, int min, int max)
    {
        int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v);
        AllyariaArgumentException.ThrowIfOutOfRange<double>(v, min, max, param);

        return v;
    }

    /// <summary>Converts RGB bytes to HSV.</summary>
    /// <param name="r">Red (0–255).</param>
    /// <param name="g">Green (0–255).</param>
    /// <param name="b">Blue (0–255).</param>
    /// <param name="h">Outputs hue in degrees (<c>0..360</c>).</param>
    /// <param name="s">Outputs saturation in percent (<c>0..100</c>).</param>
    /// <param name="v">Outputs value (brightness) in percent (<c>0..100</c>).</param>
    /// <remarks>
    /// When <paramref name="r" />, <paramref name="g" />, and <paramref name="b" /> are equal, hue is defined as <c>0</c> and
    /// saturation as <c>0</c>.
    /// </remarks>
    private static void RgbToHsv(byte r,
        byte g,
        byte b,
        out double h,
        out double s,
        out double v)
    {
        var rf = r / 255.0;
        var gf = g / 255.0;
        var bf = b / 255.0;

        var max = Math.Max(rf, Math.Max(gf, bf));
        var min = Math.Min(rf, Math.Min(gf, bf));
        var delta = max - min;

        // Hue
        if (delta < 1e-9)
        {
            h = 0.0;
        }
        else if (Math.Abs(max - rf) < 1e-9)
        {
            h = 60.0 * ((gf - bf) / delta % 6.0);
        }
        else if (Math.Abs(max - gf) < 1e-9)
        {
            h = 60.0 * ((bf - rf) / delta + 2.0);
        }
        else
        {
            h = 60.0 * ((rf - gf) / delta + 4.0);
        }

        if (h < 0)
        {
            h += 360.0;
        }

        // Saturation
        s = max <= 0
            ? 0
            : delta / max * 100.0;

        // Value
        v = max * 100.0;
    }

    /// <summary>Converts a single hexadecimal digit to its numeric nibble value.</summary>
    /// <param name="c">A character in the set <c>0–9</c>, <c>a–f</c>, or <c>A–F</c>.</param>
    /// <returns>The integer value <c>0..15</c> represented by <paramref name="c" />.</returns>
    /// <exception cref="AllyariaArgumentException">Thrown when <paramref name="c" /> is not a valid hex digit.</exception>
    private static int ToHexNibble(char c)
        => c switch
        {
            >= '0' and <= '9' => c - '0',
            >= 'a' and <= 'f' => c - 'a' + 10,
            >= 'A' and <= 'F' => c - 'A' + 10,
            _ => throw new AllyariaArgumentException($"Invalid hex digit '{c}'.")
        };

    /// <summary>Attempts to parse a Material color name of the form <c>{Hue}{Tone}</c>.</summary>
    /// <param name="name">
    /// Examples include <c>"DeepPurple200"</c>, <c>"red500"</c>, or <c>"deep-purple a700"</c> (whitespace, dashes, and
    /// underscores are ignored).
    /// </param>
    /// <param name="color">When this method returns, contains the parsed color if successful; otherwise the default value.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
    private static bool TryFromMaterialName(string name, out AllyariaColorValue color)
    {
        var norm = NormalizeMaterialKey(name);

        if (MaterialMap.TryGetValue(norm, out var rgba))
        {
            color = rgba;

            return true;
        }

        color = Colors.Transparent;

        return false;
    }

    /// <summary>Attempts to parse a CSS Web color name (case-insensitive).</summary>
    /// <param name="name">The color name (e.g., <c>"dodgerblue"</c>, <c>"white"</c>).</param>
    /// <param name="color">When this method returns, contains the parsed color if successful; otherwise the default value.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
    private static bool TryFromWebName(string name, out AllyariaColorValue color)
    {
        var key = name.Trim().ToLowerInvariant();

        if (WebNameMap.TryGetValue(key, out var rgba))
        {
            color = rgba;

            return true;
        }

        color = Colors.Transparent;

        return false;
    }

    /// <summary>Attempts to parse a color string into an <see cref="AllyariaColorValue" />.</summary>
    /// <param name="value">The color value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed color if successful; otherwise <c>null</c>.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
    public static bool TryParse(string value, out AllyariaColorValue? result)
    {
        try
        {
            result = new AllyariaColorValue(value);

            return true;
        }
        catch
        {
            result = null;

            return false;
        }
    }

    /// <summary>Implicit cast from <see cref="string" /> to <see cref="AllyariaColorValue" />.</summary>
    /// <param name="value">The color value to parse.</param>
    /// <returns>A new <see cref="AllyariaColorValue" />.</returns>
    public static implicit operator AllyariaColorValue(string value) => new(value);

    /// <summary>
    /// Implicit cast from <see cref="AllyariaColorValue" /> to its canonical string representation (<c>#RRGGBBAA</c>).
    /// </summary>
    /// <param name="value">The color value.</param>
    /// <returns>The uppercase <c>#RRGGBBAA</c> string.</returns>
    public static implicit operator string(AllyariaColorValue value) => value.Value;
}

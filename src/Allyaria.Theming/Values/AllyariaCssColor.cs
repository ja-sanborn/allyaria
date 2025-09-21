using Allyaria.Theming.Abstractions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values;

/// <summary>
/// Represents a framework-agnostic color value with CSS-oriented parsing and formatting, immutable value semantics, and
/// total ordering by the uppercase <c>#RRGGBBAA</c> form.
/// </summary>
/// <remarks>
/// This type is a small, immutable value type (readonly struct). It supports:
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
public sealed record AllyariaCssColor : StyleValueBase
{
    /// <summary>Material Design color lookup table.</summary>
    private static readonly Dictionary<string, AllyariaCssColor> MaterialMap = new(StringComparer.OrdinalIgnoreCase)
    {
        // Red
        ["red50"] = FromHexInline("#FFEBEEFF"),
        ["red100"] = FromHexInline("#FFCDD2FF"),
        ["red200"] = FromHexInline("#EF9A9AFF"),
        ["red300"] = FromHexInline("#E57373FF"),
        ["red400"] = FromHexInline("#EF5350FF"),
        ["red500"] = FromHexInline("#F44336FF"),
        ["red600"] = FromHexInline("#E53935FF"),
        ["red700"] = FromHexInline("#D32F2FFF"),
        ["red800"] = FromHexInline("#C62828FF"),
        ["red900"] = FromHexInline("#B71C1CFF"),
        ["reda100"] = FromHexInline("#FF8A80FF"),
        ["reda200"] = FromHexInline("#FF5252FF"),
        ["reda400"] = FromHexInline("#FF1744FF"),
        ["reda700"] = FromHexInline("#D50000FF"),

        // Pink
        ["pink50"] = FromHexInline("#FCE4ECFF"),
        ["pink100"] = FromHexInline("#F8BBD0FF"),
        ["pink200"] = FromHexInline("#F48FB1FF"),
        ["pink300"] = FromHexInline("#F06292FF"),
        ["pink400"] = FromHexInline("#EC407AFF"),
        ["pink500"] = FromHexInline("#E91E63FF"),
        ["pink600"] = FromHexInline("#D81B60FF"),
        ["pink700"] = FromHexInline("#C2185BFF"),
        ["pink800"] = FromHexInline("#AD1457FF"),
        ["pink900"] = FromHexInline("#880E4FFF"),
        ["pinka100"] = FromHexInline("#FF80ABFF"),
        ["pinka200"] = FromHexInline("#FF4081FF"),
        ["pinka400"] = FromHexInline("#F50057FF"),
        ["pinka700"] = FromHexInline("#C51162FF"),

        // Purple
        ["purple50"] = FromHexInline("#F3E5F5FF"),
        ["purple100"] = FromHexInline("#E1BEE7FF"),
        ["purple200"] = FromHexInline("#CE93D8FF"),
        ["purple300"] = FromHexInline("#BA68C8FF"),
        ["purple400"] = FromHexInline("#AB47BCFF"),
        ["purple500"] = FromHexInline("#9C27B0FF"),
        ["purple600"] = FromHexInline("#8E24AAFF"),
        ["purple700"] = FromHexInline("#7B1FA2FF"),
        ["purple800"] = FromHexInline("#6A1B9AFF"),
        ["purple900"] = FromHexInline("#4A148CFF"),
        ["purplea100"] = FromHexInline("#EA80FCFF"),
        ["purplea200"] = FromHexInline("#E040FBFF"),
        ["purplea400"] = FromHexInline("#D500F9FF"),
        ["purplea700"] = FromHexInline("#AA00FFFF"),

        // Deep Purple
        ["deeppurple50"] = FromHexInline("#EDE7F6FF"),
        ["deeppurple100"] = FromHexInline("#D1C4E9FF"),
        ["deeppurple200"] = FromHexInline("#B39DDBFF"),
        ["deeppurple300"] = FromHexInline("#9575CDFF"),
        ["deeppurple400"] = FromHexInline("#7E57C2FF"),
        ["deeppurple500"] = FromHexInline("#673AB7FF"),
        ["deeppurple600"] = FromHexInline("#5E35B1FF"),
        ["deeppurple700"] = FromHexInline("#512DA8FF"),
        ["deeppurple800"] = FromHexInline("#4527A0FF"),
        ["deeppurple900"] = FromHexInline("#311B92FF"),
        ["deeppurplea100"] = FromHexInline("#B388FFFF"),
        ["deeppurplea200"] = FromHexInline("#7C4DFFFF"),
        ["deeppurplea400"] = FromHexInline("#651FFFFF"),
        ["deeppurplea700"] = FromHexInline("#6200EAFF"),

        // Indigo
        ["indigo50"] = FromHexInline("#E8EAF6FF"),
        ["indigo100"] = FromHexInline("#C5CAE9FF"),
        ["indigo200"] = FromHexInline("#9FA8DAFF"),
        ["indigo300"] = FromHexInline("#7986CBFF"),
        ["indigo400"] = FromHexInline("#5C6BC0FF"),
        ["indigo500"] = FromHexInline("#3F51B5FF"),
        ["indigo600"] = FromHexInline("#3949ABFF"),
        ["indigo700"] = FromHexInline("#303F9FFF"),
        ["indigo800"] = FromHexInline("#283593FF"),
        ["indigo900"] = FromHexInline("#1A237EFF"),
        ["indigoa100"] = FromHexInline("#8C9EFFFF"),
        ["indigoa200"] = FromHexInline("#536DFEFF"),
        ["indigoa400"] = FromHexInline("#3D5AFEFF"),
        ["indigoa700"] = FromHexInline("#304FFEFF"),

        // Blue
        ["blue50"] = FromHexInline("#E3F2FDFF"),
        ["blue100"] = FromHexInline("#BBDEFBFF"),
        ["blue200"] = FromHexInline("#90CAF9FF"),
        ["blue300"] = FromHexInline("#64B5F6FF"),
        ["blue400"] = FromHexInline("#42A5F5FF"),
        ["blue500"] = FromHexInline("#2196F3FF"),
        ["blue600"] = FromHexInline("#1E88E5FF"),
        ["blue700"] = FromHexInline("#1976D2FF"),
        ["blue800"] = FromHexInline("#1565C0FF"),
        ["blue900"] = FromHexInline("#0D47A1FF"),
        ["bluea100"] = FromHexInline("#82B1FFFF"),
        ["bluea200"] = FromHexInline("#448AFFFF"),
        ["bluea400"] = FromHexInline("#2979FFFF"),
        ["bluea700"] = FromHexInline("#2962FFFF"),

        // Light Blue
        ["lightblue50"] = FromHexInline("#E1F5FEFF"),
        ["lightblue100"] = FromHexInline("#B3E5FCFF"),
        ["lightblue200"] = FromHexInline("#81D4FAFF"),
        ["lightblue300"] = FromHexInline("#4FC3F7FF"),
        ["lightblue400"] = FromHexInline("#29B6F6FF"),
        ["lightblue500"] = FromHexInline("#03A9F4FF"),
        ["lightblue600"] = FromHexInline("#039BE5FF"),
        ["lightblue700"] = FromHexInline("#0288D1FF"),
        ["lightblue800"] = FromHexInline("#0277BDFF"),
        ["lightblue900"] = FromHexInline("#01579BFF"),
        ["lightbluea100"] = FromHexInline("#80D8FFFF"),
        ["lightbluea200"] = FromHexInline("#40C4FFFF"),
        ["lightbluea400"] = FromHexInline("#00B0FFFF"),
        ["lightbluea700"] = FromHexInline("#0091EAFF"),

        // Cyan
        ["cyan50"] = FromHexInline("#E0F7FAFF"),
        ["cyan100"] = FromHexInline("#B2EBF2FF"),
        ["cyan200"] = FromHexInline("#80DEEAFF"),
        ["cyan300"] = FromHexInline("#4DD0E1FF"),
        ["cyan400"] = FromHexInline("#26C6DAFF"),
        ["cyan500"] = FromHexInline("#00BCD4FF"),
        ["cyan600"] = FromHexInline("#00ACC1FF"),
        ["cyan700"] = FromHexInline("#0097A7FF"),
        ["cyan800"] = FromHexInline("#00838FFF"),
        ["cyan900"] = FromHexInline("#006064FF"),
        ["cyana100"] = FromHexInline("#84FFFFFF"),
        ["cyana200"] = FromHexInline("#18FFFFFF"),
        ["cyana400"] = FromHexInline("#00E5FFFF"),
        ["cyana700"] = FromHexInline("#00B8D4FF"),

        // Teal
        ["teal50"] = FromHexInline("#E0F2F1FF"),
        ["teal100"] = FromHexInline("#B2DFDBFF"),
        ["teal200"] = FromHexInline("#80CBC4FF"),
        ["teal300"] = FromHexInline("#4DB6ACFF"),
        ["teal400"] = FromHexInline("#26A69AFF"),
        ["teal500"] = FromHexInline("#009688FF"),
        ["teal600"] = FromHexInline("#00897BFF"),
        ["teal700"] = FromHexInline("#00796BFF"),
        ["teal800"] = FromHexInline("#00695CFF"),
        ["teal900"] = FromHexInline("#004D40FF"),
        ["teala100"] = FromHexInline("#A7FFEBFF"),
        ["teala200"] = FromHexInline("#64FFDAFF"),
        ["teala400"] = FromHexInline("#1DE9B6FF"),
        ["teala700"] = FromHexInline("#00BFA5FF"),

        // Green
        ["green50"] = FromHexInline("#E8F5E9FF"),
        ["green100"] = FromHexInline("#C8E6C9FF"),
        ["green200"] = FromHexInline("#A5D6A7FF"),
        ["green300"] = FromHexInline("#81C784FF"),
        ["green400"] = FromHexInline("#66BB6AFF"),
        ["green500"] = FromHexInline("#4CAF50FF"),
        ["green600"] = FromHexInline("#43A047FF"),
        ["green700"] = FromHexInline("#388E3CFF"),
        ["green800"] = FromHexInline("#2E7D32FF"),
        ["green900"] = FromHexInline("#1B5E20FF"),
        ["greena100"] = FromHexInline("#B9F6CAFF"),
        ["greena200"] = FromHexInline("#69F0AEFF"),
        ["greena400"] = FromHexInline("#00E676FF"),
        ["greena700"] = FromHexInline("#00C853FF"),

        // Light Green
        ["lightgreen50"] = FromHexInline("#F1F8E9FF"),
        ["lightgreen100"] = FromHexInline("#DCEDC8FF"),
        ["lightgreen200"] = FromHexInline("#C5E1A5FF"),
        ["lightgreen300"] = FromHexInline("#AED581FF"),
        ["lightgreen400"] = FromHexInline("#9CCC65FF"),
        ["lightgreen500"] = FromHexInline("#8BC34AFF"),
        ["lightgreen600"] = FromHexInline("#7CB342FF"),
        ["lightgreen700"] = FromHexInline("#689F38FF"),
        ["lightgreen800"] = FromHexInline("#558B2FFF"),
        ["lightgreen900"] = FromHexInline("#33691EFF"),
        ["lightgreena100"] = FromHexInline("#CCFF90FF"),
        ["lightgreena200"] = FromHexInline("#B2FF59FF"),
        ["lightgreena400"] = FromHexInline("#76FF03FF"),
        ["lightgreena700"] = FromHexInline("#64DD17FF"),

        // Lime
        ["lime50"] = FromHexInline("#F9FBE7FF"),
        ["lime100"] = FromHexInline("#F0F4C3FF"),
        ["lime200"] = FromHexInline("#E6EE9CFF"),
        ["lime300"] = FromHexInline("#DCE775FF"),
        ["lime400"] = FromHexInline("#D4E157FF"),
        ["lime500"] = FromHexInline("#CDDC39FF"),
        ["lime600"] = FromHexInline("#C0CA33FF"),
        ["lime700"] = FromHexInline("#AFB42BFF"),
        ["lime800"] = FromHexInline("#9E9D24FF"),
        ["lime900"] = FromHexInline("#827717FF"),
        ["limea100"] = FromHexInline("#F4FF81FF"),
        ["limea200"] = FromHexInline("#EEFF41FF"),
        ["limea400"] = FromHexInline("#C6FF00FF"),
        ["limea700"] = FromHexInline("#AEEA00FF"),

        // Yellow
        ["yellow50"] = FromHexInline("#FFFDE7FF"),
        ["yellow100"] = FromHexInline("#FFF9C4FF"),
        ["yellow200"] = FromHexInline("#FFF59DFF"),
        ["yellow300"] = FromHexInline("#FFF176FF"),
        ["yellow400"] = FromHexInline("#FFEE58FF"),
        ["yellow500"] = FromHexInline("#FFEB3BFF"),
        ["yellow600"] = FromHexInline("#FDD835FF"),
        ["yellow700"] = FromHexInline("#FBC02DFF"),
        ["yellow800"] = FromHexInline("#F9A825FF"),
        ["yellow900"] = FromHexInline("#F57F17FF"),
        ["yellowa100"] = FromHexInline("#FFFF8DFF"),
        ["yellowa200"] = FromHexInline("#FFFF00FF"),
        ["yellowa400"] = FromHexInline("#FFEA00FF"),
        ["yellowa700"] = FromHexInline("#FFD600FF"),

        // Amber
        ["amber50"] = FromHexInline("#FFF8E1FF"),
        ["amber100"] = FromHexInline("#FFECB3FF"),
        ["amber200"] = FromHexInline("#FFE082FF"),
        ["amber300"] = FromHexInline("#FFD54FFF"),
        ["amber400"] = FromHexInline("#FFCA28FF"),
        ["amber500"] = FromHexInline("#FFC107FF"),
        ["amber600"] = FromHexInline("#FFB300FF"),
        ["amber700"] = FromHexInline("#FFA000FF"),
        ["amber800"] = FromHexInline("#FF8F00FF"),
        ["amber900"] = FromHexInline("#FF6F00FF"),
        ["ambera100"] = FromHexInline("#FFE57FFF"),
        ["ambera200"] = FromHexInline("#FFD740FF"),
        ["ambera400"] = FromHexInline("#FFC400FF"),
        ["ambera700"] = FromHexInline("#FFAB00FF"),

        // Orange
        ["orange50"] = FromHexInline("#FFF3E0FF"),
        ["orange100"] = FromHexInline("#FFE0B2FF"),
        ["orange200"] = FromHexInline("#FFCC80FF"),
        ["orange300"] = FromHexInline("#FFB74DFF"),
        ["orange400"] = FromHexInline("#FFA726FF"),
        ["orange500"] = FromHexInline("#FF9800FF"),
        ["orange600"] = FromHexInline("#FB8C00FF"),
        ["orange700"] = FromHexInline("#F57C00FF"),
        ["orange800"] = FromHexInline("#EF6C00FF"),
        ["orange900"] = FromHexInline("#E65100FF"),
        ["orangea100"] = FromHexInline("#FFD180FF"),
        ["orangea200"] = FromHexInline("#FFAB40FF"),
        ["orangea400"] = FromHexInline("#FF9100FF"),
        ["orangea700"] = FromHexInline("#FF6D00FF"),

        // Deep Orange
        ["deeporange50"] = FromHexInline("#FBE9E7FF"),
        ["deeporange100"] = FromHexInline("#FFCCBCFF"),
        ["deeporange200"] = FromHexInline("#FFAB91FF"),
        ["deeporange300"] = FromHexInline("#FF8A65FF"),
        ["deeporange400"] = FromHexInline("#FF7043FF"),
        ["deeporange500"] = FromHexInline("#FF5722FF"),
        ["deeporange600"] = FromHexInline("#F4511EFF"),
        ["deeporange700"] = FromHexInline("#E64A19FF"),
        ["deeporange800"] = FromHexInline("#D84315FF"),
        ["deeporange900"] = FromHexInline("#BF360CFF"),
        ["deeporangea100"] = FromHexInline("#FF9E80FF"),
        ["deeporangea200"] = FromHexInline("#FF6E40FF"),
        ["deeporangea400"] = FromHexInline("#FF3D00FF"),
        ["deeporangea700"] = FromHexInline("#DD2C00FF"),

        // Brown (no accents in Material 2 palette)
        ["brown50"] = FromHexInline("#EFEBE9FF"),
        ["brown100"] = FromHexInline("#D7CCC8FF"),
        ["brown200"] = FromHexInline("#BCAAA4FF"),
        ["brown300"] = FromHexInline("#A1887FFF"),
        ["brown400"] = FromHexInline("#8D6E63FF"),
        ["brown500"] = FromHexInline("#795548FF"),
        ["brown600"] = FromHexInline("#6D4C41FF"),
        ["brown700"] = FromHexInline("#5D4037FF"),
        ["brown800"] = FromHexInline("#4E342EFF"),
        ["brown900"] = FromHexInline("#3E2723FF"),

        // Blue Grey (no accents)
        ["bluegrey50"] = FromHexInline("#ECEFF1FF"),
        ["bluegrey100"] = FromHexInline("#CFD8DCFF"),
        ["bluegrey200"] = FromHexInline("#B0BEC5FF"),
        ["bluegrey300"] = FromHexInline("#90A4AEFF"),
        ["bluegrey400"] = FromHexInline("#78909CFF"),
        ["bluegrey500"] = FromHexInline("#607D8BFF"),
        ["bluegrey600"] = FromHexInline("#546E7AFF"),
        ["bluegrey700"] = FromHexInline("#455A64FF"),
        ["bluegrey800"] = FromHexInline("#37474FFF"),
        ["bluegrey900"] = FromHexInline("#263238FF"),

        // Grey (no accents)
        ["grey50"] = FromHexInline("#FAFAFAFF"),
        ["grey100"] = FromHexInline("#F5F5F5FF"),
        ["grey200"] = FromHexInline("#EEEEEEFF"),
        ["grey300"] = FromHexInline("#E0E0E0FF"),
        ["grey400"] = FromHexInline("#BDBDBDFF"),
        ["grey500"] = FromHexInline("#9E9E9EFF"),
        ["grey600"] = FromHexInline("#757575FF"),
        ["grey700"] = FromHexInline("#616161FF"),
        ["grey800"] = FromHexInline("#424242FF"),
        ["grey900"] = FromHexInline("#212121FF"),

        // Monochrome convenience entries
        ["black"] = FromHexInline("#000000FF"),
        ["white"] = FromHexInline("#FFFFFFFF")
    };

    /// <summary>Compiled regular expression for parsing CSS <c>hsv()</c> and <c>hsva()</c> functions.</summary>
    private static readonly Regex RxHsv = new(
        @"^hsva?\s*\(\s*(?<h>(\d*\.)?\d+)\s*,\s*(?<s>(\d*\.)?\d+)\s*%\s*,\s*(?<v>(\d*\.)?\d+)\s*%(?:\s*,\s*(?<a>((\d*\.)?\d+)))?\s*\)\s*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase
    );

    /// <summary>Compiled regular expression for parsing CSS <c>rgb()</c> and <c>rgba()</c> functions.</summary>
    private static readonly Regex RxRgb = new(
        @"^rgba?\s*\(\s*(?<r>\d{1,3})\s*,\s*(?<g>\d{1,3})\s*,\s*(?<b>\d{1,3})(?:\s*,\s*(?<a>((\d*\.)?\d+)))?\s*\)\s*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase
    );

    /// <summary>CSS Web color lookup table.</summary>
    private static readonly Dictionary<string, AllyariaCssColor> WebNameMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["aliceblue"] = FromHexInline("#F0F8FFFF"),
        ["antiquewhite"] = FromHexInline("#FAEBD7FF"),
        ["aqua"] = FromHexInline("#00FFFFFF"),
        ["aquamarine"] = FromHexInline("#7FFFD4FF"),
        ["azure"] = FromHexInline("#F0FFFFFF"),
        ["beige"] = FromHexInline("#F5F5DCFF"),
        ["bisque"] = FromHexInline("#FFE4C4FF"),
        ["black"] = FromHexInline("#000000FF"),
        ["blanchedalmond"] = FromHexInline("#FFEBCDFF"),
        ["blue"] = FromHexInline("#0000FFFF"),
        ["blueviolet"] = FromHexInline("#8A2BE2FF"),
        ["brown"] = FromHexInline("#A52A2AFF"),
        ["burlywood"] = FromHexInline("#DEB887FF"),
        ["cadetblue"] = FromHexInline("#5F9EA0FF"),
        ["chartreuse"] = FromHexInline("#7FFF00FF"),
        ["chocolate"] = FromHexInline("#D2691EFF"),
        ["coral"] = FromHexInline("#FF7F50FF"),
        ["cornflowerblue"] = FromHexInline("#6495EDFF"),
        ["cornsilk"] = FromHexInline("#FFF8DCFF"),
        ["crimson"] = FromHexInline("#DC143CFF"),
        ["cyan"] = FromHexInline("#00FFFFFF"),
        ["darkblue"] = FromHexInline("#00008BFF"),
        ["darkcyan"] = FromHexInline("#008B8BFF"),
        ["darkgoldenrod"] = FromHexInline("#B8860BFF"),
        ["darkgray"] = FromHexInline("#A9A9A9FF"),
        ["darkgreen"] = FromHexInline("#006400FF"),
        ["darkkhaki"] = FromHexInline("#BDB76BFF"),
        ["darkmagenta"] = FromHexInline("#8B008BFF"),
        ["darkolivegreen"] = FromHexInline("#556B2FFF"),
        ["darkorange"] = FromHexInline("#FF8C00FF"),
        ["darkorchid"] = FromHexInline("#9932CCFF"),
        ["darkred"] = FromHexInline("#8B0000FF"),
        ["darksalmon"] = FromHexInline("#E9967AFF"),
        ["darkseagreen"] = FromHexInline("#8FBC8FFF"),
        ["darkslateblue"] = FromHexInline("#483D8BFF"),
        ["darkslategray"] = FromHexInline("#2F4F4FFF"),
        ["darkturquoise"] = FromHexInline("#00CED1FF"),
        ["darkviolet"] = FromHexInline("#9400D3FF"),
        ["deeppink"] = FromHexInline("#FF1493FF"),
        ["deepskyblue"] = FromHexInline("#00BFFFFF"),
        ["dimgray"] = FromHexInline("#696969FF"),
        ["dodgerblue"] = FromHexInline("#1E90FFFF"),
        ["firebrick"] = FromHexInline("#B22222FF"),
        ["floralwhite"] = FromHexInline("#FFFAF0FF"),
        ["forestgreen"] = FromHexInline("#228B22FF"),
        ["fuchsia"] = FromHexInline("#FF00FFFF"),
        ["gainsboro"] = FromHexInline("#DCDCDCFF"),
        ["ghostwhite"] = FromHexInline("#F8F8FFFF"),
        ["gold"] = FromHexInline("#FFD700FF"),
        ["goldenrod"] = FromHexInline("#DAA520FF"),
        ["gray"] = FromHexInline("#808080FF"),
        ["green"] = FromHexInline("#008000FF"),
        ["greenyellow"] = FromHexInline("#ADFF2FFF"),
        ["honeydew"] = FromHexInline("#F0FFF0FF"),
        ["hotpink"] = FromHexInline("#FF69B4FF"),
        ["indianred"] = FromHexInline("#CD5C5CFF"),
        ["indigo"] = FromHexInline("#4B0082FF"),
        ["ivory"] = FromHexInline("#FFFFF0FF"),
        ["khaki"] = FromHexInline("#F0E68CFF"),
        ["lavender"] = FromHexInline("#E6E6FAFF"),
        ["lavenderblush"] = FromHexInline("#FFF0F5FF"),
        ["lawngreen"] = FromHexInline("#7CFC00FF"),
        ["lemonchiffon"] = FromHexInline("#FFFACDFF"),
        ["lightblue"] = FromHexInline("#ADD8E6FF"),
        ["lightcoral"] = FromHexInline("#F08080FF"),
        ["lightcyan"] = FromHexInline("#E0FFFFFF"),
        ["lightgoldenrodyellow"] = FromHexInline("#FAFAD2FF"),
        ["lightgray"] = FromHexInline("#D3D3D3FF"),
        ["lightgreen"] = FromHexInline("#90EE90FF"),
        ["lightpink"] = FromHexInline("#FFB6C1FF"),
        ["lightsalmon"] = FromHexInline("#FFA07AFF"),
        ["lightseagreen"] = FromHexInline("#20B2AAFF"),
        ["lightskyblue"] = FromHexInline("#87CEFAFF"),
        ["lightslategray"] = FromHexInline("#778899FF"),
        ["lightsteelblue"] = FromHexInline("#B0C4DEFF"),
        ["lightyellow"] = FromHexInline("#FFFFE0FF"),
        ["lime"] = FromHexInline("#00FF00FF"),
        ["limegreen"] = FromHexInline("#32CD32FF"),
        ["linen"] = FromHexInline("#FAF0E6FF"),
        ["magenta"] = FromHexInline("#FF00FFFF"),
        ["maroon"] = FromHexInline("#800000FF"),
        ["mediumaquamarine"] = FromHexInline("#66CDAAFF"),
        ["mediumblue"] = FromHexInline("#0000CDFF"),
        ["mediumorchid"] = FromHexInline("#BA55D3FF"),
        ["mediumpurple"] = FromHexInline("#9370DBFF"),
        ["mediumseagreen"] = FromHexInline("#3CB371FF"),
        ["mediumslateblue"] = FromHexInline("#7B68EEFF"),
        ["mediumspringgreen"] = FromHexInline("#00FA9AFF"),
        ["mediumturquoise"] = FromHexInline("#48D1CCFF"),
        ["mediumvioletred"] = FromHexInline("#C71585FF"),
        ["midnightblue"] = FromHexInline("#191970FF"),
        ["mintcream"] = FromHexInline("#F5FFFAFF"),
        ["mistyrose"] = FromHexInline("#FFE4E1FF"),
        ["moccasin"] = FromHexInline("#FFE4B5FF"),
        ["navajowhite"] = FromHexInline("#FFDEADFF"),
        ["navy"] = FromHexInline("#000080FF"),
        ["oldlace"] = FromHexInline("#FDF5E6FF"),
        ["olive"] = FromHexInline("#808000FF"),
        ["olivedrab"] = FromHexInline("#6B8E23FF"),
        ["orange"] = FromHexInline("#FFA500FF"),
        ["orangered"] = FromHexInline("#FF4500FF"),
        ["orchid"] = FromHexInline("#DA70D6FF"),
        ["palegoldenrod"] = FromHexInline("#EEE8AAFF"),
        ["palegreen"] = FromHexInline("#98FB98FF"),
        ["paleturquoise"] = FromHexInline("#AFEEEEFF"),
        ["palevioletred"] = FromHexInline("#DB7093FF"),
        ["papayawhip"] = FromHexInline("#FFEFD5FF"),
        ["peachpuff"] = FromHexInline("#FFDAB9FF"),
        ["peru"] = FromHexInline("#CD853FFF"),
        ["pink"] = FromHexInline("#FFC0CBFF"),
        ["plum"] = FromHexInline("#DDA0DDFF"),
        ["powderblue"] = FromHexInline("#B0E0E6FF"),
        ["purple"] = FromHexInline("#800080FF"),
        ["red"] = FromHexInline("#FF0000FF"),
        ["rosybrown"] = FromHexInline("#BC8F8FFF"),
        ["royalblue"] = FromHexInline("#4169E1FF"),
        ["saddlebrown"] = FromHexInline("#8B4513FF"),
        ["salmon"] = FromHexInline("#FA8072FF"),
        ["sandybrown"] = FromHexInline("#F4A460FF"),
        ["seagreen"] = FromHexInline("#2E8B57FF"),
        ["seashell"] = FromHexInline("#FFF5EEFF"),
        ["sienna"] = FromHexInline("#A0522DFF"),
        ["silver"] = FromHexInline("#C0C0C0FF"),
        ["skyblue"] = FromHexInline("#87CEEBFF"),
        ["slateblue"] = FromHexInline("#6A5ACDFF"),
        ["slategray"] = FromHexInline("#708090FF"),
        ["snow"] = FromHexInline("#FFFAFAFF"),
        ["springgreen"] = FromHexInline("#00FF7FFF"),
        ["steelblue"] = FromHexInline("#4682B4FF"),
        ["tan"] = FromHexInline("#D2B48CFF"),
        ["teal"] = FromHexInline("#008080FF"),
        ["thistle"] = FromHexInline("#D8BFD8FF"),
        ["tomato"] = FromHexInline("#FF6347FF"),
        ["turquoise"] = FromHexInline("#40E0D0FF"),
        ["violet"] = FromHexInline("#EE82EEFF"),
        ["wheat"] = FromHexInline("#F5DEB3FF"),
        ["white"] = FromHexInline("#FFFFFFFF"),
        ["whitesmoke"] = FromHexInline("#F5F5F5FF"),
        ["yellow"] = FromHexInline("#FFFF00FF"),
        ["yellowgreen"] = FromHexInline("#9ACD32FF"),
        ["transparent"] = new AllyariaCssColor(0, 0, 0, 0)
    };

    /// <summary>Initializes a color from HSVA channels.</summary>
    /// <param name="h">Hue in degrees, clamped to [0..360].</param>
    /// <param name="s">Saturation in percent, clamped to [0..100].</param>
    /// <param name="v">Value (brightness) in percent, clamped to [0..100].</param>
    /// <param name="a">Alpha in [0..1], clamped.</param>
    private AllyariaCssColor(double h, double s, double v, double a = 1.0)
        : base(string.Empty)
    {
        HsvToRgb(Math.Clamp(h, 0, 360), Math.Clamp(s, 0, 100), Math.Clamp(v, 0, 100), out var r, out var g, out var b);
        R = r;
        G = g;
        B = b;
        A = Math.Clamp(a, 0, 1.0);
    }

    /// <summary>Initializes a color from RGBA channels.</summary>
    /// <param name="r">Red in [0..255].</param>
    /// <param name="g">Green in [0..255].</param>
    /// <param name="b">Blue in [0..255].</param>
    /// <param name="a">Alpha in [0..1], clamped.</param>
    private AllyariaCssColor(byte r, byte g, byte b, double a = 1.0)
        : base(string.Empty)
    {
        R = r;
        G = g;
        B = b;
        A = Math.Clamp(a, 0, 1.0);
    }

    /// <summary>
    /// Initializes a color by parsing a CSS-like string: <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, <c>#RRGGBBAA</c>,
    /// <c>rgb()</c>, <c>rgba()</c>, <c>hsv(H,S%,V%)</c>, <c>hsva(H,S%,V%,A)</c>, Web color names, or Material color names.
    /// </summary>
    /// <param name="value">The input string to parse.</param>
    /// <exception cref="ArgumentException">Thrown when the value is not a recognized color format or name.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value" /> is <c>null</c>.</exception>
    public AllyariaCssColor(string value)
        : base(string.Empty)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(value);

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
            if (TryFromWebName(s, out var web))
            {
                R = web.R;
                G = web.G;
                B = web.B;
                A = web.A;

                return;
            }

            if (TryFromMaterialName(s, out var mat))
            {
                R = mat.R;
                G = mat.G;
                B = mat.B;
                A = mat.A;

                return;
            }

            throw new ArgumentException("Color not found.", nameof(value));
        }

        catch (Exception exception)
        {
            throw new ArgumentException(
                $"Unrecognized color: '{value}'. Expected #RRGGBB, #RRGGBBAA, rgb(), rgba(), hsv(), hsva(), a CSS Web color name, or a Material color name.",
                nameof(value), exception
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

    /// <summary>Gets the style value represented as a string.</summary>
    public override string Value => HexRgba;

    /// <summary>Creates an <see cref="AllyariaCssColor" /> from a hex literal (helper used by color tables).</summary>
    /// <param name="hex">A hex color string of the form <c>#RRGGBB</c> or <c>#RRGGBBAA</c>.</param>
    /// <returns>A new <see cref="AllyariaCssColor" /> parsed from <paramref name="hex" />.</returns>
    private static AllyariaCssColor FromHexInline(string hex)
    {
        FromHexString(hex, out var r, out var g, out var b, out var a);

        return new AllyariaCssColor(r, g, b, a);
    }

    /// <summary>Parses a hexadecimal CSS color literal.</summary>
    /// <param name="s">A hex color string of the form <c>#RGB</c>, <c>#RGBA</c>, <c>#RRGGBB</c>, or <c>#RRGGBBAA</c>.</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <param name="a">Outputs the alpha channel (0–1).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="s" /> is not a supported hex format or does not begin
    /// with <c>#</c>.
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

        throw new ArgumentException($"Hex color must be #RGB, #RGBA, #RRGGBB, or #RRGGBBAA: '{s}'.", nameof(s));
    }

    /// <summary>Returns a color from HSVA channels.</summary>
    /// <param name="h">Hue in degrees, clamped to [0..360].</param>
    /// <param name="s">Saturation in percent, clamped to [0..100].</param>
    /// <param name="v">Value (brightness) in percent, clamped to [0..100].</param>
    /// <param name="a">Alpha in [0..1], clamped.</param>
    /// <returns>The AllyariaCssColor from the HSVA channels.</returns>
    public static AllyariaCssColor FromHsva(double h, double s, double v, double a = 1.0) => new(h, s, v, a);

    /// <summary>Parses an <c>hsv(H,S%,V%)</c> or <c>hsva(H,S%,V%,A)</c> CSS color function.</summary>
    /// <param name="s">The input string to parse.</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <param name="a">Outputs the alpha channel (0–1). Defaults to 1.0 when omitted.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the string is not in <c>hsv()</c>/<c>hsva()</c> form or contains
    /// out-of-range values.
    /// </exception>
    private static void FromHsvString(string s, out byte r, out byte g, out byte b, out double a)
    {
        var m = RxHsv.Match(s);

        if (!m.Success)
        {
            throw new ArgumentException(
                $"Invalid hsv/hsva() format: '{s}'. Expected hsv(H,S%,V%) or hsva(H,S%,V%,A).", nameof(s)
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
    /// <returns>The AllyariaCssColor from the RGBA channels.</returns>
    public static AllyariaCssColor FromRgba(byte r, byte g, byte b, double a = 1.0) => new(r, g, b, a);

    /// <summary>Parses an <c>rgb(r,g,b)</c> or <c>rgba(r,g,b,a)</c> CSS color function.</summary>
    /// <param name="s">The input string to parse.</param>
    /// <param name="r">Outputs the red channel (0–255).</param>
    /// <param name="g">Outputs the green channel (0–255).</param>
    /// <param name="b">Outputs the blue channel (0–255).</param>
    /// <param name="a">Outputs the alpha channel (0–1). Defaults to 1.0 when omitted.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the string is not in <c>rgb()</c>/<c>rgba()</c> form or contains
    /// out-of-range values.
    /// </exception>
    private static void FromRgbString(string s, out byte r, out byte g, out byte b, out double a)
    {
        var m = RxRgb.Match(s);

        if (!m.Success)
        {
            throw new ArgumentException(
                $"Invalid rgb/rgba() format: '{s}'. Expected rgb(r,g,b) or rgba(r,g,b,a).", nameof(s)
            );
        }

        r = (byte)Math.Clamp(ParseInt(m.Groups["r"].Value, "r", 0, 255), 0, 255);
        g = (byte)Math.Clamp(ParseInt(m.Groups["g"].Value, "g", 0, 255), 0, 255);
        b = (byte)Math.Clamp(ParseInt(m.Groups["b"].Value, "b", 0, 255), 0, 255);

        a = m.Groups["a"].Success
            ? Math.Clamp(ParseDouble(m.Groups["a"].Value, "a", 0, 1), 0.0, 1.0)
            : 1.0;
    }

    /// <summary>
    /// Produces a hover-friendly variant: if <see cref="V" /> &lt; 50, lightens by 20; otherwise darkens by 20. Alpha is
    /// preserved.
    /// </summary>
    public AllyariaCssColor HoverColor()
    {
        var delta = V < 50
            ? 20
            : -20;

        return ShiftColor(delta);
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

        h = (h % 360 + 360) % 360; // normalize to [0,360]
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

                break; // case 5
        }

        r = (byte)Math.Clamp((int)Math.Round(r1 * 255.0), 0, 255);
        g = (byte)Math.Clamp((int)Math.Round(g1 * 255.0), 0, 255);
        b = (byte)Math.Clamp((int)Math.Round(b1 * 255.0), 0, 255);
    }

    /// <summary>Normalizes a Material color key for lookup.</summary>
    /// <param name="input">An input such as <c>"Deep Purple 200"</c>, <c>"deep-purple-200"</c>, or <c>"red500"</c>.</param>
    /// <returns>A normalized, lower-case key without spaces, dashes, or underscores (e.g., <c>"deeppurple200"</c>).</returns>
    private static string NormalizeMaterialKey(string input)
    {
        var s = input.Trim().ToLowerInvariant()
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Replace("_", string.Empty);

        return s;
    }

    /// <summary>Parses a floating-point number using invariant culture.</summary>
    /// <param name="s">The source text.</param>
    /// <param name="param">A parameter name used in exception messages.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <returns>The parsed number.</returns>
    /// <exception cref="ArgumentException">Thrown when parsing fails.</exception>
    private static double ParseDouble(string s, string param, int min, int max)
    {
        double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var v);

        if (v < min || v > max)
        {
            throw new ArgumentOutOfRangeException(param, v, $"Expected {param} in [{min}..{max}].");
        }

        return v;
    }

    /// <summary>Parses an integer and validates it against the provided inclusive range.</summary>
    /// <param name="s">The source text.</param>
    /// <param name="param">A parameter name used in exception messages.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <returns>The parsed integer.</returns>
    /// <exception cref="ArgumentException">Thrown when parsing fails.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the parsed value is out of range.</exception>
    private static int ParseInt(string s, string param, int min, int max)
    {
        int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v);

        if (v < min || v > max)
        {
            throw new ArgumentOutOfRangeException(param, v, $"Expected {param} in [{min}..{max}].");
        }

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

    /// <summary>Adjusts <see cref="V" /> (value/brightness) by the specified percentage.</summary>
    /// <param name="percent">A value in [-100..100]. Positive to lighten; negative to darken.</param>
    /// <returns>A new color with adjusted brightness; alpha is preserved.</returns>
    public AllyariaCssColor ShiftColor(double percent)
    {
        percent = Math.Clamp(percent, -100, 100);
        RgbToHsv(R, G, B, out var h, out var s, out var v);
        var v2 = Math.Clamp(v + percent, 0, 100);
        HsvToRgb(h, s, v2, out var r2, out var g2, out var b2);

        return new AllyariaCssColor(r2, g2, b2, A);
    }

    /// <summary>Converts a single hexadecimal digit to its numeric nibble value.</summary>
    /// <param name="c">A character in the set <c>0–9</c>, <c>a–f</c>, or <c>A–F</c>.</param>
    /// <returns>The integer value <c>0..15</c> represented by <paramref name="c" />.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="c" /> is not a valid hex digit.</exception>
    private static int ToHexNibble(char c)
        => c switch
        {
            >= '0' and <= '9' => c - '0',
            >= 'a' and <= 'f' => c - 'a' + 10,
            >= 'A' and <= 'F' => c - 'A' + 10,
            _ => throw new ArgumentException($"Invalid hex digit '{c}'.")
        };

    /// <summary>Attempts to parse a Material color name of the form <c>{Hue}{Tone}</c>.</summary>
    /// <param name="name">
    /// Examples include <c>"DeepPurple200"</c>, <c>"red500"</c>, or <c>"deep-purple a700"</c> (whitespace, dashes, and
    /// underscores are ignored).
    /// </param>
    /// <param name="color">When this method returns, contains the parsed color if successful; otherwise the default value.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
    private static bool TryFromMaterialName(string name, out AllyariaCssColor color)
    {
        var norm = NormalizeMaterialKey(name);

        if (MaterialMap.TryGetValue(norm, out var rgba))
        {
            color = rgba;

            return true;
        }

        color = new AllyariaCssColor("black");

        return false;
    }

    /// <summary>Attempts to parse a CSS Web color name (case-insensitive).</summary>
    /// <param name="name">The color name (e.g., <c>"dodgerblue"</c>, <c>"white"</c>).</param>
    /// <param name="color">When this method returns, contains the parsed color if successful; otherwise the default value.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise <c>false</c>.</returns>
    private static bool TryFromWebName(string name, out AllyariaCssColor color)
    {
        var key = name.Trim().ToLowerInvariant();

        if (WebNameMap.TryGetValue(key, out var rgba))
        {
            color = rgba;

            return true;
        }

        color = new AllyariaCssColor("black");

        return false;
    }

    /// <summary>Attempts to parse a color string.</summary>
    /// <param name="value">The input color string.</param>
    /// <param name="color">When successful, receives the parsed color; otherwise set to black.</param>
    /// <returns><c>true</c> when parsing succeeds; otherwise <c>false</c>.</returns>
    public static bool TryParse(string value, out AllyariaCssColor color)
    {
        try
        {
            color = new AllyariaCssColor(value);

            return true;
        }
        catch
        {
            color = new AllyariaCssColor("black");

            return false;
        }
    }

    /// <summary>Implicit conversion from <see cref="string" /> by parsing.</summary>
    /// <param name="value">A supported color string.</param>
    public static implicit operator AllyariaCssColor(string value) => new(value);

    /// <summary>Implicit conversion to <see cref="string" /> using <see cref="ToString" /> (i.e., <c>#RRGGBBAA</c>).</summary>
    public static implicit operator string(AllyariaCssColor value) => value.HexRgba;
}

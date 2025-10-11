using Allyaria.Abstractions.Types;
using System.Collections.Immutable;
using System.Reflection;

namespace Allyaria.Abstractions.Constants;

/// <summary>
/// Provides a consolidated, alphabetically sorted library of named colors (CSS and Material), exposed as strongly-typed
/// <see cref="HexColor" /> properties.
/// </summary>
public static class Colors
{
    /// <summary>
    /// Represents the <see cref="Aliceblue" /> <see cref="HexColor" /> with the hex value of <c>#F0F8FFFF</c>
    /// </summary>
    public static readonly HexColor Aliceblue = new("#F0F8FFFF");

    /// <summary>
    /// Represents the <see cref="Amber100" /> <see cref="HexColor" /> with the hex value of <c>#FFECB3FF</c>
    /// </summary>
    public static readonly HexColor Amber100 = new("#FFECB3FF");

    /// <summary>
    /// Represents the <see cref="Amber200" /> <see cref="HexColor" /> with the hex value of <c>#FFE082FF</c>
    /// </summary>
    public static readonly HexColor Amber200 = new("#FFE082FF");

    /// <summary>
    /// Represents the <see cref="Amber300" /> <see cref="HexColor" /> with the hex value of <c>#FFD54FFF</c>
    /// </summary>
    public static readonly HexColor Amber300 = new("#FFD54FFF");

    /// <summary>
    /// Represents the <see cref="Amber400" /> <see cref="HexColor" /> with the hex value of <c>#FFCA28FF</c>
    /// </summary>
    public static readonly HexColor Amber400 = new("#FFCA28FF");

    /// <summary>Represents the <see cref="Amber50" /> <see cref="HexColor" /> with the hex value of <c>#FFF8E1FF</c></summary>
    public static readonly HexColor Amber50 = new("#FFF8E1FF");

    /// <summary>
    /// Represents the <see cref="Amber500" /> <see cref="HexColor" /> with the hex value of <c>#FFC107FF</c>
    /// </summary>
    public static readonly HexColor Amber500 = new("#FFC107FF");

    /// <summary>
    /// Represents the <see cref="Amber600" /> <see cref="HexColor" /> with the hex value of <c>#FFB300FF</c>
    /// </summary>
    public static readonly HexColor Amber600 = new("#FFB300FF");

    /// <summary>
    /// Represents the <see cref="Amber700" /> <see cref="HexColor" /> with the hex value of <c>#FFA000FF</c>
    /// </summary>
    public static readonly HexColor Amber700 = new("#FFA000FF");

    /// <summary>
    /// Represents the <see cref="Amber800" /> <see cref="HexColor" /> with the hex value of <c>#FF8F00FF</c>
    /// </summary>
    public static readonly HexColor Amber800 = new("#FF8F00FF");

    /// <summary>
    /// Represents the <see cref="Amber900" /> <see cref="HexColor" /> with the hex value of <c>#FF6F00FF</c>
    /// </summary>
    public static readonly HexColor Amber900 = new("#FF6F00FF");

    /// <summary>
    /// Represents the <see cref="AmberA100" /> <see cref="HexColor" /> with the hex value of <c>#FFE57FFF</c>
    /// </summary>
    public static readonly HexColor AmberA100 = new("#FFE57FFF");

    /// <summary>
    /// Represents the <see cref="AmberA200" /> <see cref="HexColor" /> with the hex value of <c>#FFD740FF</c>
    /// </summary>
    public static readonly HexColor AmberA200 = new("#FFD740FF");

    /// <summary>
    /// Represents the <see cref="AmberA400" /> <see cref="HexColor" /> with the hex value of <c>#FFC400FF</c>
    /// </summary>
    public static readonly HexColor AmberA400 = new("#FFC400FF");

    /// <summary>
    /// Represents the <see cref="AmberA700" /> <see cref="HexColor" /> with the hex value of <c>#FFAB00FF</c>
    /// </summary>
    public static readonly HexColor AmberA700 = new("#FFAB00FF");

    /// <summary>
    /// Represents the <see cref="Antiquewhite" /> <see cref="HexColor" /> with the hex value of <c>#FAEBD7FF</c>
    /// </summary>
    public static readonly HexColor Antiquewhite = new("#FAEBD7FF");

    /// <summary>Represents the <see cref="Aqua" /> <see cref="HexColor" /> with the hex value of <c>#00FFFFFF</c></summary>
    public static readonly HexColor Aqua = new("#00FFFFFF");

    /// <summary>
    /// Represents the <see cref="Aquamarine" /> <see cref="HexColor" /> with the hex value of <c>#7FFFD4FF</c>
    /// </summary>
    public static readonly HexColor Aquamarine = new("#7FFFD4FF");

    /// <summary>Represents the <see cref="Azure" /> <see cref="HexColor" /> with the hex value of <c>#F0FFFFFF</c></summary>
    public static readonly HexColor Azure = new("#F0FFFFFF");

    /// <summary>Represents the <see cref="Beige" /> <see cref="HexColor" /> with the hex value of <c>#F5F5DCFF</c></summary>
    public static readonly HexColor Beige = new("#F5F5DCFF");

    /// <summary>Represents the <see cref="Bisque" /> <see cref="HexColor" /> with the hex value of <c>#FFE4C4FF</c></summary>
    public static readonly HexColor Bisque = new("#FFE4C4FF");

    /// <summary>Represents the <see cref="Black" /> <see cref="HexColor" /> with the hex value of <c>#000000FF</c></summary>
    public static readonly HexColor Black = new("#000000FF");

    /// <summary>
    /// Represents the <see cref="Blanchedalmond" /> <see cref="HexColor" /> with the hex value of <c>#FFEBCDFF</c>
    /// </summary>
    public static readonly HexColor Blanchedalmond = new("#FFEBCDFF");

    /// <summary>Represents the <see cref="Blue" /> <see cref="HexColor" /> with the hex value of <c>#0000FFFF</c></summary>
    public static readonly HexColor Blue = new("#0000FFFF");

    /// <summary>Represents the <see cref="Blue100" /> <see cref="HexColor" /> with the hex value of <c>#BBDEFBFF</c></summary>
    public static readonly HexColor Blue100 = new("#BBDEFBFF");

    /// <summary>Represents the <see cref="Blue200" /> <see cref="HexColor" /> with the hex value of <c>#90CAF9FF</c></summary>
    public static readonly HexColor Blue200 = new("#90CAF9FF");

    /// <summary>Represents the <see cref="Blue300" /> <see cref="HexColor" /> with the hex value of <c>#64B5F6FF</c></summary>
    public static readonly HexColor Blue300 = new("#64B5F6FF");

    /// <summary>Represents the <see cref="Blue400" /> <see cref="HexColor" /> with the hex value of <c>#42A5F5FF</c></summary>
    public static readonly HexColor Blue400 = new("#42A5F5FF");

    /// <summary>Represents the <see cref="Blue50" /> <see cref="HexColor" /> with the hex value of <c>#E3F2FDFF</c></summary>
    public static readonly HexColor Blue50 = new("#E3F2FDFF");

    /// <summary>Represents the <see cref="Blue500" /> <see cref="HexColor" /> with the hex value of <c>#2196F3FF</c></summary>
    public static readonly HexColor Blue500 = new("#2196F3FF");

    /// <summary>Represents the <see cref="Blue600" /> <see cref="HexColor" /> with the hex value of <c>#1E88E5FF</c></summary>
    public static readonly HexColor Blue600 = new("#1E88E5FF");

    /// <summary>Represents the <see cref="Blue700" /> <see cref="HexColor" /> with the hex value of <c>#1976D2FF</c></summary>
    public static readonly HexColor Blue700 = new("#1976D2FF");

    /// <summary>Represents the <see cref="Blue800" /> <see cref="HexColor" /> with the hex value of <c>#1565C0FF</c></summary>
    public static readonly HexColor Blue800 = new("#1565C0FF");

    /// <summary>Represents the <see cref="Blue900" /> <see cref="HexColor" /> with the hex value of <c>#0D47A1FF</c></summary>
    public static readonly HexColor Blue900 = new("#0D47A1FF");

    /// <summary>
    /// Represents the <see cref="BlueA100" /> <see cref="HexColor" /> with the hex value of <c>#82B1FFFF</c>
    /// </summary>
    public static readonly HexColor BlueA100 = new("#82B1FFFF");

    /// <summary>
    /// Represents the <see cref="BlueA200" /> <see cref="HexColor" /> with the hex value of <c>#448AFFFF</c>
    /// </summary>
    public static readonly HexColor BlueA200 = new("#448AFFFF");

    /// <summary>
    /// Represents the <see cref="BlueA400" /> <see cref="HexColor" /> with the hex value of <c>#2979FFFF</c>
    /// </summary>
    public static readonly HexColor BlueA400 = new("#2979FFFF");

    /// <summary>
    /// Represents the <see cref="BlueA700" /> <see cref="HexColor" /> with the hex value of <c>#2962FFFF</c>
    /// </summary>
    public static readonly HexColor BlueA700 = new("#2962FFFF");

    /// <summary>
    /// Represents the <see cref="Bluegrey100" /> <see cref="HexColor" /> with the hex value of <c>#CFD8DCFF</c>
    /// </summary>
    public static readonly HexColor Bluegrey100 = new("#CFD8DCFF");

    /// <summary>
    /// Represents the <see cref="Bluegrey200" /> <see cref="HexColor" /> with the hex value of <c>#B0BEC5FF</c>
    /// </summary>
    public static readonly HexColor Bluegrey200 = new("#B0BEC5FF");

    /// <summary>
    /// Represents the <see cref="Bluegrey300" /> <see cref="HexColor" /> with the hex value of <c>#90A4AEFF</c>
    /// </summary>
    public static readonly HexColor Bluegrey300 = new("#90A4AEFF");

    /// <summary>
    /// Represents the <see cref="Bluegrey400" /> <see cref="HexColor" /> with the hex value of <c>#78909CFF</c>
    /// </summary>
    public static readonly HexColor Bluegrey400 = new("#78909CFF");

    /// <summary>
    /// Represents the <see cref="Bluegrey50" /> <see cref="HexColor" /> with the hex value of <c>#ECEFF1FF</c>
    /// </summary>
    public static readonly HexColor Bluegrey50 = new("#ECEFF1FF");

    /// <summary>
    /// Represents the <see cref="Bluegrey500" /> <see cref="HexColor" /> with the hex value of <c>#607D8BFF</c>
    /// </summary>
    public static readonly HexColor Bluegrey500 = new("#607D8BFF");

    /// <summary>
    /// Represents the <see cref="Bluegrey600" /> <see cref="HexColor" /> with the hex value of <c>#546E7AFF</c>
    /// </summary>
    public static readonly HexColor Bluegrey600 = new("#546E7AFF");

    /// <summary>
    /// Represents the <see cref="Bluegrey700" /> <see cref="HexColor" /> with the hex value of <c>#455A64FF</c>
    /// </summary>
    public static readonly HexColor Bluegrey700 = new("#455A64FF");

    /// <summary>
    /// Represents the <see cref="Bluegrey800" /> <see cref="HexColor" /> with the hex value of <c>#37474FFF</c>
    /// </summary>
    public static readonly HexColor Bluegrey800 = new("#37474FFF");

    /// <summary>
    /// Represents the <see cref="Bluegrey900" /> <see cref="HexColor" /> with the hex value of <c>#263238FF</c>
    /// </summary>
    public static readonly HexColor Bluegrey900 = new("#263238FF");

    /// <summary>
    /// Represents the <see cref="Blueviolet" /> <see cref="HexColor" /> with the hex value of <c>#8A2BE2FF</c>
    /// </summary>
    public static readonly HexColor Blueviolet = new("#8A2BE2FF");

    /// <summary>Represents the <see cref="Brown" /> <see cref="HexColor" /> with the hex value of <c>#A52A2AFF</c></summary>
    public static readonly HexColor Brown = new("#A52A2AFF");

    /// <summary>
    /// Represents the <see cref="Brown100" /> <see cref="HexColor" /> with the hex value of <c>#D7CCC8FF</c>
    /// </summary>
    public static readonly HexColor Brown100 = new("#D7CCC8FF");

    /// <summary>
    /// Represents the <see cref="Brown200" /> <see cref="HexColor" /> with the hex value of <c>#BCAAA4FF</c>
    /// </summary>
    public static readonly HexColor Brown200 = new("#BCAAA4FF");

    /// <summary>
    /// Represents the <see cref="Brown300" /> <see cref="HexColor" /> with the hex value of <c>#A1887FFF</c>
    /// </summary>
    public static readonly HexColor Brown300 = new("#A1887FFF");

    /// <summary>
    /// Represents the <see cref="Brown400" /> <see cref="HexColor" /> with the hex value of <c>#8D6E63FF</c>
    /// </summary>
    public static readonly HexColor Brown400 = new("#8D6E63FF");

    /// <summary>Represents the <see cref="Brown50" /> <see cref="HexColor" /> with the hex value of <c>#EFEBE9FF</c></summary>
    public static readonly HexColor Brown50 = new("#EFEBE9FF");

    /// <summary>
    /// Represents the <see cref="Brown500" /> <see cref="HexColor" /> with the hex value of <c>#795548FF</c>
    /// </summary>
    public static readonly HexColor Brown500 = new("#795548FF");

    /// <summary>
    /// Represents the <see cref="Brown600" /> <see cref="HexColor" /> with the hex value of <c>#6D4C41FF</c>
    /// </summary>
    public static readonly HexColor Brown600 = new("#6D4C41FF");

    /// <summary>
    /// Represents the <see cref="Brown700" /> <see cref="HexColor" /> with the hex value of <c>#5D4037FF</c>
    /// </summary>
    public static readonly HexColor Brown700 = new("#5D4037FF");

    /// <summary>
    /// Represents the <see cref="Brown800" /> <see cref="HexColor" /> with the hex value of <c>#4E342EFF</c>
    /// </summary>
    public static readonly HexColor Brown800 = new("#4E342EFF");

    /// <summary>
    /// Represents the <see cref="Brown900" /> <see cref="HexColor" /> with the hex value of <c>#3E2723FF</c>
    /// </summary>
    public static readonly HexColor Brown900 = new("#3E2723FF");

    /// <summary>
    /// Represents the <see cref="Burlywood" /> <see cref="HexColor" /> with the hex value of <c>#DEB887FF</c>
    /// </summary>
    public static readonly HexColor Burlywood = new("#DEB887FF");

    /// <summary>
    /// Represents the <see cref="Cadetblue" /> <see cref="HexColor" /> with the hex value of <c>#5F9EA0FF</c>
    /// </summary>
    public static readonly HexColor Cadetblue = new("#5F9EA0FF");

    /// <summary>
    /// Represents the <see cref="Chartreuse" /> <see cref="HexColor" /> with the hex value of <c>#7FFF00FF</c>
    /// </summary>
    public static readonly HexColor Chartreuse = new("#7FFF00FF");

    /// <summary>
    /// Represents the <see cref="Chocolate" /> <see cref="HexColor" /> with the hex value of <c>#D2691EFF</c>
    /// </summary>
    public static readonly HexColor Chocolate = new("#D2691EFF");

    /// <summary>Represents the <see cref="Coral" /> <see cref="HexColor" /> with the hex value of <c>#FF7F50FF</c></summary>
    public static readonly HexColor Coral = new("#FF7F50FF");

    /// <summary>
    /// Represents the <see cref="Cornflowerblue" /> <see cref="HexColor" /> with the hex value of <c>#6495EDFF</c>
    /// </summary>
    public static readonly HexColor Cornflowerblue = new("#6495EDFF");

    /// <summary>
    /// Represents the <see cref="Cornsilk" /> <see cref="HexColor" /> with the hex value of <c>#FFF8DCFF</c>
    /// </summary>
    public static readonly HexColor Cornsilk = new("#FFF8DCFF");

    /// <summary>Represents the <see cref="Crimson" /> <see cref="HexColor" /> with the hex value of <c>#DC143CFF</c></summary>
    public static readonly HexColor Crimson = new("#DC143CFF");

    /// <summary>Represents the <see cref="Cyan" /> <see cref="HexColor" /> with the hex value of <c>#00FFFFFF</c></summary>
    public static readonly HexColor Cyan = new("#00FFFFFF");

    /// <summary>Represents the <see cref="Cyan100" /> <see cref="HexColor" /> with the hex value of <c>#B2EBF2FF</c></summary>
    public static readonly HexColor Cyan100 = new("#B2EBF2FF");

    /// <summary>Represents the <see cref="Cyan200" /> <see cref="HexColor" /> with the hex value of <c>#80DEEAFF</c></summary>
    public static readonly HexColor Cyan200 = new("#80DEEAFF");

    /// <summary>Represents the <see cref="Cyan300" /> <see cref="HexColor" /> with the hex value of <c>#4DD0E1FF</c></summary>
    public static readonly HexColor Cyan300 = new("#4DD0E1FF");

    /// <summary>Represents the <see cref="Cyan400" /> <see cref="HexColor" /> with the hex value of <c>#26C6DAFF</c></summary>
    public static readonly HexColor Cyan400 = new("#26C6DAFF");

    /// <summary>Represents the <see cref="Cyan50" /> <see cref="HexColor" /> with the hex value of <c>#E0F7FAFF</c></summary>
    public static readonly HexColor Cyan50 = new("#E0F7FAFF");

    /// <summary>Represents the <see cref="Cyan500" /> <see cref="HexColor" /> with the hex value of <c>#00BCD4FF</c></summary>
    public static readonly HexColor Cyan500 = new("#00BCD4FF");

    /// <summary>Represents the <see cref="Cyan600" /> <see cref="HexColor" /> with the hex value of <c>#00ACC1FF</c></summary>
    public static readonly HexColor Cyan600 = new("#00ACC1FF");

    /// <summary>Represents the <see cref="Cyan700" /> <see cref="HexColor" /> with the hex value of <c>#0097A7FF</c></summary>
    public static readonly HexColor Cyan700 = new("#0097A7FF");

    /// <summary>Represents the <see cref="Cyan800" /> <see cref="HexColor" /> with the hex value of <c>#00838FFF</c></summary>
    public static readonly HexColor Cyan800 = new("#00838FFF");

    /// <summary>Represents the <see cref="Cyan900" /> <see cref="HexColor" /> with the hex value of <c>#006064FF</c></summary>
    public static readonly HexColor Cyan900 = new("#006064FF");

    /// <summary>
    /// Represents the <see cref="CyanA100" /> <see cref="HexColor" /> with the hex value of <c>#84FFFFFF</c>
    /// </summary>
    public static readonly HexColor CyanA100 = new("#84FFFFFF");

    /// <summary>
    /// Represents the <see cref="CyanA200" /> <see cref="HexColor" /> with the hex value of <c>#18FFFFFF</c>
    /// </summary>
    public static readonly HexColor CyanA200 = new("#18FFFFFF");

    /// <summary>
    /// Represents the <see cref="CyanA400" /> <see cref="HexColor" /> with the hex value of <c>#00E5FFFF</c>
    /// </summary>
    public static readonly HexColor CyanA400 = new("#00E5FFFF");

    /// <summary>
    /// Represents the <see cref="CyanA700" /> <see cref="HexColor" /> with the hex value of <c>#00B8D4FF</c>
    /// </summary>
    public static readonly HexColor CyanA700 = new("#00B8D4FF");

    /// <summary>
    /// Represents the <see cref="Darkblue" /> <see cref="HexColor" /> with the hex value of <c>#00008BFF</c>
    /// </summary>
    public static readonly HexColor Darkblue = new("#00008BFF");

    /// <summary>
    /// Represents the <see cref="Darkcyan" /> <see cref="HexColor" /> with the hex value of <c>#008B8BFF</c>
    /// </summary>
    public static readonly HexColor Darkcyan = new("#008B8BFF");

    /// <summary>
    /// Represents the <see cref="Darkgoldenrod" /> <see cref="HexColor" /> with the hex value of <c>#B8860BFF</c>
    /// </summary>
    public static readonly HexColor Darkgoldenrod = new("#B8860BFF");

    /// <summary>
    /// Represents the <see cref="Darkgray" /> <see cref="HexColor" /> with the hex value of <c>#A9A9A9FF</c>
    /// </summary>
    public static readonly HexColor Darkgray = new("#A9A9A9FF");

    /// <summary>
    /// Represents the <see cref="Darkgreen" /> <see cref="HexColor" /> with the hex value of <c>#006400FF</c>
    /// </summary>
    public static readonly HexColor Darkgreen = new("#006400FF");

    /// <summary>
    /// Represents the <see cref="Darkkhaki" /> <see cref="HexColor" /> with the hex value of <c>#BDB76BFF</c>
    /// </summary>
    public static readonly HexColor Darkkhaki = new("#BDB76BFF");

    /// <summary>
    /// Represents the <see cref="Darkmagenta" /> <see cref="HexColor" /> with the hex value of <c>#8B008BFF</c>
    /// </summary>
    public static readonly HexColor Darkmagenta = new("#8B008BFF");

    /// <summary>
    /// Represents the <see cref="Darkolivegreen" /> <see cref="HexColor" /> with the hex value of <c>#556B2FFF</c>
    /// </summary>
    public static readonly HexColor Darkolivegreen = new("#556B2FFF");

    /// <summary>
    /// Represents the <see cref="Darkorange" /> <see cref="HexColor" /> with the hex value of <c>#FF8C00FF</c>
    /// </summary>
    public static readonly HexColor Darkorange = new("#FF8C00FF");

    /// <summary>
    /// Represents the <see cref="Darkorchid" /> <see cref="HexColor" /> with the hex value of <c>#9932CCFF</c>
    /// </summary>
    public static readonly HexColor Darkorchid = new("#9932CCFF");

    /// <summary>Represents the <see cref="Darkred" /> <see cref="HexColor" /> with the hex value of <c>#8B0000FF</c></summary>
    public static readonly HexColor Darkred = new("#8B0000FF");

    /// <summary>
    /// Represents the <see cref="Darksalmon" /> <see cref="HexColor" /> with the hex value of <c>#E9967AFF</c>
    /// </summary>
    public static readonly HexColor Darksalmon = new("#E9967AFF");

    /// <summary>
    /// Represents the <see cref="Darkseagreen" /> <see cref="HexColor" /> with the hex value of <c>#8FBC8FFF</c>
    /// </summary>
    public static readonly HexColor Darkseagreen = new("#8FBC8FFF");

    /// <summary>
    /// Represents the <see cref="Darkslateblue" /> <see cref="HexColor" /> with the hex value of <c>#483D8BFF</c>
    /// </summary>
    public static readonly HexColor Darkslateblue = new("#483D8BFF");

    /// <summary>
    /// Represents the <see cref="Darkslategray" /> <see cref="HexColor" /> with the hex value of <c>#2F4F4FFF</c>
    /// </summary>
    public static readonly HexColor Darkslategray = new("#2F4F4FFF");

    /// <summary>
    /// Represents the <see cref="Darkturquoise" /> <see cref="HexColor" /> with the hex value of <c>#00CED1FF</c>
    /// </summary>
    public static readonly HexColor Darkturquoise = new("#00CED1FF");

    /// <summary>
    /// Represents the <see cref="Darkviolet" /> <see cref="HexColor" /> with the hex value of <c>#9400D3FF</c>
    /// </summary>
    public static readonly HexColor Darkviolet = new("#9400D3FF");

    /// <summary>
    /// Represents the <see cref="Deeporange100" /> <see cref="HexColor" /> with the hex value of <c>#FFCCBCFF</c>
    /// </summary>
    public static readonly HexColor Deeporange100 = new("#FFCCBCFF");

    /// <summary>
    /// Represents the <see cref="Deeporange200" /> <see cref="HexColor" /> with the hex value of <c>#FFAB91FF</c>
    /// </summary>
    public static readonly HexColor Deeporange200 = new("#FFAB91FF");

    /// <summary>
    /// Represents the <see cref="Deeporange300" /> <see cref="HexColor" /> with the hex value of <c>#FF8A65FF</c>
    /// </summary>
    public static readonly HexColor Deeporange300 = new("#FF8A65FF");

    /// <summary>
    /// Represents the <see cref="Deeporange400" /> <see cref="HexColor" /> with the hex value of <c>#FF7043FF</c>
    /// </summary>
    public static readonly HexColor Deeporange400 = new("#FF7043FF");

    /// <summary>
    /// Represents the <see cref="Deeporange50" /> <see cref="HexColor" /> with the hex value of <c>#FBE9E7FF</c>
    /// </summary>
    public static readonly HexColor Deeporange50 = new("#FBE9E7FF");

    /// <summary>
    /// Represents the <see cref="Deeporange500" /> <see cref="HexColor" /> with the hex value of <c>#FF5722FF</c>
    /// </summary>
    public static readonly HexColor Deeporange500 = new("#FF5722FF");

    /// <summary>
    /// Represents the <see cref="Deeporange600" /> <see cref="HexColor" /> with the hex value of <c>#F4511EFF</c>
    /// </summary>
    public static readonly HexColor Deeporange600 = new("#F4511EFF");

    /// <summary>
    /// Represents the <see cref="Deeporange700" /> <see cref="HexColor" /> with the hex value of <c>#E64A19FF</c>
    /// </summary>
    public static readonly HexColor Deeporange700 = new("#E64A19FF");

    /// <summary>
    /// Represents the <see cref="Deeporange800" /> <see cref="HexColor" /> with the hex value of <c>#D84315FF</c>
    /// </summary>
    public static readonly HexColor Deeporange800 = new("#D84315FF");

    /// <summary>
    /// Represents the <see cref="Deeporange900" /> <see cref="HexColor" /> with the hex value of <c>#BF360CFF</c>
    /// </summary>
    public static readonly HexColor Deeporange900 = new("#BF360CFF");

    /// <summary>
    /// Represents the <see cref="DeeporangeA100" /> <see cref="HexColor" /> with the hex value of <c>#FF9E80FF</c>
    /// </summary>
    public static readonly HexColor DeeporangeA100 = new("#FF9E80FF");

    /// <summary>
    /// Represents the <see cref="DeeporangeA200" /> <see cref="HexColor" /> with the hex value of <c>#FF6E40FF</c>
    /// </summary>
    public static readonly HexColor DeeporangeA200 = new("#FF6E40FF");

    /// <summary>
    /// Represents the <see cref="DeeporangeA400" /> <see cref="HexColor" /> with the hex value of <c>#FF3D00FF</c>
    /// </summary>
    public static readonly HexColor DeeporangeA400 = new("#FF3D00FF");

    /// <summary>
    /// Represents the <see cref="DeeporangeA700" /> <see cref="HexColor" /> with the hex value of <c>#DD2C00FF</c>
    /// </summary>
    public static readonly HexColor DeeporangeA700 = new("#DD2C00FF");

    /// <summary>
    /// Represents the <see cref="Deeppink" /> <see cref="HexColor" /> with the hex value of <c>#FF1493FF</c>
    /// </summary>
    public static readonly HexColor Deeppink = new("#FF1493FF");

    /// <summary>
    /// Represents the <see cref="Deeppurple100" /> <see cref="HexColor" /> with the hex value of <c>#D1C4E9FF</c>
    /// </summary>
    public static readonly HexColor Deeppurple100 = new("#D1C4E9FF");

    /// <summary>
    /// Represents the <see cref="Deeppurple200" /> <see cref="HexColor" /> with the hex value of <c>#B39DDBFF</c>
    /// </summary>
    public static readonly HexColor Deeppurple200 = new("#B39DDBFF");

    /// <summary>
    /// Represents the <see cref="Deeppurple300" /> <see cref="HexColor" /> with the hex value of <c>#9575CDFF</c>
    /// </summary>
    public static readonly HexColor Deeppurple300 = new("#9575CDFF");

    /// <summary>
    /// Represents the <see cref="Deeppurple400" /> <see cref="HexColor" /> with the hex value of <c>#7E57C2FF</c>
    /// </summary>
    public static readonly HexColor Deeppurple400 = new("#7E57C2FF");

    /// <summary>
    /// Represents the <see cref="Deeppurple50" /> <see cref="HexColor" /> with the hex value of <c>#EDE7F6FF</c>
    /// </summary>
    public static readonly HexColor Deeppurple50 = new("#EDE7F6FF");

    /// <summary>
    /// Represents the <see cref="Deeppurple500" /> <see cref="HexColor" /> with the hex value of <c>#673AB7FF</c>
    /// </summary>
    public static readonly HexColor Deeppurple500 = new("#673AB7FF");

    /// <summary>
    /// Represents the <see cref="Deeppurple600" /> <see cref="HexColor" /> with the hex value of <c>#5E35B1FF</c>
    /// </summary>
    public static readonly HexColor Deeppurple600 = new("#5E35B1FF");

    /// <summary>
    /// Represents the <see cref="Deeppurple700" /> <see cref="HexColor" /> with the hex value of <c>#512DA8FF</c>
    /// </summary>
    public static readonly HexColor Deeppurple700 = new("#512DA8FF");

    /// <summary>
    /// Represents the <see cref="Deeppurple800" /> <see cref="HexColor" /> with the hex value of <c>#4527A0FF</c>
    /// </summary>
    public static readonly HexColor Deeppurple800 = new("#4527A0FF");

    /// <summary>
    /// Represents the <see cref="Deeppurple900" /> <see cref="HexColor" /> with the hex value of <c>#311B92FF</c>
    /// </summary>
    public static readonly HexColor Deeppurple900 = new("#311B92FF");

    /// <summary>
    /// Represents the <see cref="DeeppurpleA100" /> <see cref="HexColor" /> with the hex value of <c>#B388FFFF</c>
    /// </summary>
    public static readonly HexColor DeeppurpleA100 = new("#B388FFFF");

    /// <summary>
    /// Represents the <see cref="DeeppurpleA200" /> <see cref="HexColor" /> with the hex value of <c>#7C4DFFFF</c>
    /// </summary>
    public static readonly HexColor DeeppurpleA200 = new("#7C4DFFFF");

    /// <summary>
    /// Represents the <see cref="DeeppurpleA400" /> <see cref="HexColor" /> with the hex value of <c>#651FFFFF</c>
    /// </summary>
    public static readonly HexColor DeeppurpleA400 = new("#651FFFFF");

    /// <summary>
    /// Represents the <see cref="DeeppurpleA700" /> <see cref="HexColor" /> with the hex value of <c>#6200EAFF</c>
    /// </summary>
    public static readonly HexColor DeeppurpleA700 = new("#6200EAFF");

    /// <summary>
    /// Represents the <see cref="Deepskyblue" /> <see cref="HexColor" /> with the hex value of <c>#00BFFFFF</c>
    /// </summary>
    public static readonly HexColor Deepskyblue = new("#00BFFFFF");

    /// <summary>Represents the <see cref="Dimgray" /> <see cref="HexColor" /> with the hex value of <c>#696969FF</c></summary>
    public static readonly HexColor Dimgray = new("#696969FF");

    /// <summary>
    /// Represents the <see cref="Dodgerblue" /> <see cref="HexColor" /> with the hex value of <c>#1E90FFFF</c>
    /// </summary>
    public static readonly HexColor Dodgerblue = new("#1E90FFFF");

    /// <summary>
    /// Represents the <see cref="Firebrick" /> <see cref="HexColor" /> with the hex value of <c>#B22222FF</c>
    /// </summary>
    public static readonly HexColor Firebrick = new("#B22222FF");

    /// <summary>
    /// Represents the <see cref="Floralwhite" /> <see cref="HexColor" /> with the hex value of <c>#FFFAF0FF</c>
    /// </summary>
    public static readonly HexColor Floralwhite = new("#FFFAF0FF");

    /// <summary>
    /// Represents the <see cref="Forestgreen" /> <see cref="HexColor" /> with the hex value of <c>#228B22FF</c>
    /// </summary>
    public static readonly HexColor Forestgreen = new("#228B22FF");

    /// <summary>Represents the <see cref="Fuchsia" /> <see cref="HexColor" /> with the hex value of <c>#FF00FFFF</c></summary>
    public static readonly HexColor Fuchsia = new("#FF00FFFF");

    /// <summary>
    /// Represents the <see cref="Gainsboro" /> <see cref="HexColor" /> with the hex value of <c>#DCDCDCFF</c>
    /// </summary>
    public static readonly HexColor Gainsboro = new("#DCDCDCFF");

    /// <summary>
    /// Represents the <see cref="Ghostwhite" /> <see cref="HexColor" /> with the hex value of <c>#F8F8FFFF</c>
    /// </summary>
    public static readonly HexColor Ghostwhite = new("#F8F8FFFF");

    /// <summary>Represents the <see cref="Gold" /> <see cref="HexColor" /> with the hex value of <c>#FFD700FF</c></summary>
    public static readonly HexColor Gold = new("#FFD700FF");

    /// <summary>
    /// Represents the <see cref="Goldenrod" /> <see cref="HexColor" /> with the hex value of <c>#DAA520FF</c>
    /// </summary>
    public static readonly HexColor Goldenrod = new("#DAA520FF");

    /// <summary>Represents the <see cref="Gray" /> <see cref="HexColor" /> with the hex value of <c>#808080FF</c></summary>
    public static readonly HexColor Gray = new("#808080FF");

    /// <summary>Represents the <see cref="Green" /> <see cref="HexColor" /> with the hex value of <c>#008000FF</c></summary>
    public static readonly HexColor Green = new("#008000FF");

    /// <summary>
    /// Represents the <see cref="Green100" /> <see cref="HexColor" /> with the hex value of <c>#C8E6C9FF</c>
    /// </summary>
    public static readonly HexColor Green100 = new("#C8E6C9FF");

    /// <summary>
    /// Represents the <see cref="Green200" /> <see cref="HexColor" /> with the hex value of <c>#A5D6A7FF</c>
    /// </summary>
    public static readonly HexColor Green200 = new("#A5D6A7FF");

    /// <summary>
    /// Represents the <see cref="Green300" /> <see cref="HexColor" /> with the hex value of <c>#81C784FF</c>
    /// </summary>
    public static readonly HexColor Green300 = new("#81C784FF");

    /// <summary>
    /// Represents the <see cref="Green400" /> <see cref="HexColor" /> with the hex value of <c>#66BB6AFF</c>
    /// </summary>
    public static readonly HexColor Green400 = new("#66BB6AFF");

    /// <summary>Represents the <see cref="Green50" /> <see cref="HexColor" /> with the hex value of <c>#E8F5E9FF</c></summary>
    public static readonly HexColor Green50 = new("#E8F5E9FF");

    /// <summary>
    /// Represents the <see cref="Green500" /> <see cref="HexColor" /> with the hex value of <c>#4CAF50FF</c>
    /// </summary>
    public static readonly HexColor Green500 = new("#4CAF50FF");

    /// <summary>
    /// Represents the <see cref="Green600" /> <see cref="HexColor" /> with the hex value of <c>#43A047FF</c>
    /// </summary>
    public static readonly HexColor Green600 = new("#43A047FF");

    /// <summary>
    /// Represents the <see cref="Green700" /> <see cref="HexColor" /> with the hex value of <c>#388E3CFF</c>
    /// </summary>
    public static readonly HexColor Green700 = new("#388E3CFF");

    /// <summary>
    /// Represents the <see cref="Green800" /> <see cref="HexColor" /> with the hex value of <c>#2E7D32FF</c>
    /// </summary>
    public static readonly HexColor Green800 = new("#2E7D32FF");

    /// <summary>
    /// Represents the <see cref="Green900" /> <see cref="HexColor" /> with the hex value of <c>#1B5E20FF</c>
    /// </summary>
    public static readonly HexColor Green900 = new("#1B5E20FF");

    /// <summary>
    /// Represents the <see cref="GreenA100" /> <see cref="HexColor" /> with the hex value of <c>#B9F6CAFF</c>
    /// </summary>
    public static readonly HexColor GreenA100 = new("#B9F6CAFF");

    /// <summary>
    /// Represents the <see cref="GreenA200" /> <see cref="HexColor" /> with the hex value of <c>#69F0AEFF</c>
    /// </summary>
    public static readonly HexColor GreenA200 = new("#69F0AEFF");

    /// <summary>
    /// Represents the <see cref="GreenA400" /> <see cref="HexColor" /> with the hex value of <c>#00E676FF</c>
    /// </summary>
    public static readonly HexColor GreenA400 = new("#00E676FF");

    /// <summary>
    /// Represents the <see cref="GreenA700" /> <see cref="HexColor" /> with the hex value of <c>#00C853FF</c>
    /// </summary>
    public static readonly HexColor GreenA700 = new("#00C853FF");

    /// <summary>
    /// Represents the <see cref="Greenyellow" /> <see cref="HexColor" /> with the hex value of <c>#ADFF2FFF</c>
    /// </summary>
    public static readonly HexColor Greenyellow = new("#ADFF2FFF");

    /// <summary>Represents the <see cref="Grey100" /> <see cref="HexColor" /> with the hex value of <c>#F5F5F5FF</c></summary>
    public static readonly HexColor Grey100 = new("#F5F5F5FF");

    /// <summary>Represents the <see cref="Grey200" /> <see cref="HexColor" /> with the hex value of <c>#EEEEEEFF</c></summary>
    public static readonly HexColor Grey200 = new("#EEEEEEFF");

    /// <summary>Represents the <see cref="Grey300" /> <see cref="HexColor" /> with the hex value of <c>#E0E0E0FF</c></summary>
    public static readonly HexColor Grey300 = new("#E0E0E0FF");

    /// <summary>Represents the <see cref="Grey400" /> <see cref="HexColor" /> with the hex value of <c>#BDBDBDFF</c></summary>
    public static readonly HexColor Grey400 = new("#BDBDBDFF");

    /// <summary>Represents the <see cref="Grey50" /> <see cref="HexColor" /> with the hex value of <c>#FAFAFAFF</c></summary>
    public static readonly HexColor Grey50 = new("#FAFAFAFF");

    /// <summary>Represents the <see cref="Grey500" /> <see cref="HexColor" /> with the hex value of <c>#9E9E9EFF</c></summary>
    public static readonly HexColor Grey500 = new("#9E9E9EFF");

    /// <summary>Represents the <see cref="Grey600" /> <see cref="HexColor" /> with the hex value of <c>#757575FF</c></summary>
    public static readonly HexColor Grey600 = new("#757575FF");

    /// <summary>Represents the <see cref="Grey700" /> <see cref="HexColor" /> with the hex value of <c>#616161FF</c></summary>
    public static readonly HexColor Grey700 = new("#616161FF");

    /// <summary>Represents the <see cref="Grey800" /> <see cref="HexColor" /> with the hex value of <c>#424242FF</c></summary>
    public static readonly HexColor Grey800 = new("#424242FF");

    /// <summary>Represents the <see cref="Grey900" /> <see cref="HexColor" /> with the hex value of <c>#212121FF</c></summary>
    public static readonly HexColor Grey900 = new("#212121FF");

    /// <summary>
    /// Represents the <see cref="Honeydew" /> <see cref="HexColor" /> with the hex value of <c>#F0FFF0FF</c>
    /// </summary>
    public static readonly HexColor Honeydew = new("#F0FFF0FF");

    /// <summary>Represents the <see cref="Hotpink" /> <see cref="HexColor" /> with the hex value of <c>#FF69B4FF</c></summary>
    public static readonly HexColor Hotpink = new("#FF69B4FF");

    /// <summary>
    /// Represents the <see cref="Indianred" /> <see cref="HexColor" /> with the hex value of <c>#CD5C5CFF</c>
    /// </summary>
    public static readonly HexColor Indianred = new("#CD5C5CFF");

    /// <summary>Represents the <see cref="Indigo" /> <see cref="HexColor" /> with the hex value of <c>#4B0082FF</c></summary>
    public static readonly HexColor Indigo = new("#4B0082FF");

    /// <summary>
    /// Represents the <see cref="Indigo100" /> <see cref="HexColor" /> with the hex value of <c>#C5CAE9FF</c>
    /// </summary>
    public static readonly HexColor Indigo100 = new("#C5CAE9FF");

    /// <summary>
    /// Represents the <see cref="Indigo200" /> <see cref="HexColor" /> with the hex value of <c>#9FA8DAFF</c>
    /// </summary>
    public static readonly HexColor Indigo200 = new("#9FA8DAFF");

    /// <summary>
    /// Represents the <see cref="Indigo300" /> <see cref="HexColor" /> with the hex value of <c>#7986CBFF</c>
    /// </summary>
    public static readonly HexColor Indigo300 = new("#7986CBFF");

    /// <summary>
    /// Represents the <see cref="Indigo400" /> <see cref="HexColor" /> with the hex value of <c>#5C6BC0FF</c>
    /// </summary>
    public static readonly HexColor Indigo400 = new("#5C6BC0FF");

    /// <summary>
    /// Represents the <see cref="Indigo50" /> <see cref="HexColor" /> with the hex value of <c>#E8EAF6FF</c>
    /// </summary>
    public static readonly HexColor Indigo50 = new("#E8EAF6FF");

    /// <summary>
    /// Represents the <see cref="Indigo500" /> <see cref="HexColor" /> with the hex value of <c>#3F51B5FF</c>
    /// </summary>
    public static readonly HexColor Indigo500 = new("#3F51B5FF");

    /// <summary>
    /// Represents the <see cref="Indigo600" /> <see cref="HexColor" /> with the hex value of <c>#3949ABFF</c>
    /// </summary>
    public static readonly HexColor Indigo600 = new("#3949ABFF");

    /// <summary>
    /// Represents the <see cref="Indigo700" /> <see cref="HexColor" /> with the hex value of <c>#303F9FFF</c>
    /// </summary>
    public static readonly HexColor Indigo700 = new("#303F9FFF");

    /// <summary>
    /// Represents the <see cref="Indigo800" /> <see cref="HexColor" /> with the hex value of <c>#283593FF</c>
    /// </summary>
    public static readonly HexColor Indigo800 = new("#283593FF");

    /// <summary>
    /// Represents the <see cref="Indigo900" /> <see cref="HexColor" /> with the hex value of <c>#1A237EFF</c>
    /// </summary>
    public static readonly HexColor Indigo900 = new("#1A237EFF");

    /// <summary>
    /// Represents the <see cref="IndigoA100" /> <see cref="HexColor" /> with the hex value of <c>#8C9EFFFF</c>
    /// </summary>
    public static readonly HexColor IndigoA100 = new("#8C9EFFFF");

    /// <summary>
    /// Represents the <see cref="IndigoA200" /> <see cref="HexColor" /> with the hex value of <c>#536DFEFF</c>
    /// </summary>
    public static readonly HexColor IndigoA200 = new("#536DFEFF");

    /// <summary>
    /// Represents the <see cref="IndigoA400" /> <see cref="HexColor" /> with the hex value of <c>#3D5AFEFF</c>
    /// </summary>
    public static readonly HexColor IndigoA400 = new("#3D5AFEFF");

    /// <summary>
    /// Represents the <see cref="IndigoA700" /> <see cref="HexColor" /> with the hex value of <c>#304FFEFF</c>
    /// </summary>
    public static readonly HexColor IndigoA700 = new("#304FFEFF");

    /// <summary>Represents the <see cref="Ivory" /> <see cref="HexColor" /> with the hex value of <c>#FFFFF0FF</c></summary>
    public static readonly HexColor Ivory = new("#FFFFF0FF");

    /// <summary>Represents the <see cref="Khaki" /> <see cref="HexColor" /> with the hex value of <c>#F0E68CFF</c></summary>
    public static readonly HexColor Khaki = new("#F0E68CFF");

    /// <summary>
    /// Represents the <see cref="Lavender" /> <see cref="HexColor" /> with the hex value of <c>#E6E6FAFF</c>
    /// </summary>
    public static readonly HexColor Lavender = new("#E6E6FAFF");

    /// <summary>
    /// Represents the <see cref="Lavenderblush" /> <see cref="HexColor" /> with the hex value of <c>#FFF0F5FF</c>
    /// </summary>
    public static readonly HexColor Lavenderblush = new("#FFF0F5FF");

    /// <summary>
    /// Represents the <see cref="Lawngreen" /> <see cref="HexColor" /> with the hex value of <c>#7CFC00FF</c>
    /// </summary>
    public static readonly HexColor Lawngreen = new("#7CFC00FF");

    /// <summary>
    /// Represents the <see cref="Lemonchiffon" /> <see cref="HexColor" /> with the hex value of <c>#FFFACDFF</c>
    /// </summary>
    public static readonly HexColor Lemonchiffon = new("#FFFACDFF");

    /// <summary>
    /// Represents the <see cref="Lightblue" /> <see cref="HexColor" /> with the hex value of <c>#ADD8E6FF</c>
    /// </summary>
    public static readonly HexColor Lightblue = new("#ADD8E6FF");

    /// <summary>
    /// Represents the <see cref="Lightblue100" /> <see cref="HexColor" /> with the hex value of <c>#B3E5FCFF</c>
    /// </summary>
    public static readonly HexColor Lightblue100 = new("#B3E5FCFF");

    /// <summary>
    /// Represents the <see cref="Lightblue200" /> <see cref="HexColor" /> with the hex value of <c>#81D4FAFF</c>
    /// </summary>
    public static readonly HexColor Lightblue200 = new("#81D4FAFF");

    /// <summary>
    /// Represents the <see cref="Lightblue300" /> <see cref="HexColor" /> with the hex value of <c>#4FC3F7FF</c>
    /// </summary>
    public static readonly HexColor Lightblue300 = new("#4FC3F7FF");

    /// <summary>
    /// Represents the <see cref="Lightblue400" /> <see cref="HexColor" /> with the hex value of <c>#29B6F6FF</c>
    /// </summary>
    public static readonly HexColor Lightblue400 = new("#29B6F6FF");

    /// <summary>
    /// Represents the <see cref="Lightblue50" /> <see cref="HexColor" /> with the hex value of <c>#E1F5FEFF</c>
    /// </summary>
    public static readonly HexColor Lightblue50 = new("#E1F5FEFF");

    /// <summary>
    /// Represents the <see cref="Lightblue500" /> <see cref="HexColor" /> with the hex value of <c>#03A9F4FF</c>
    /// </summary>
    public static readonly HexColor Lightblue500 = new("#03A9F4FF");

    /// <summary>
    /// Represents the <see cref="Lightblue600" /> <see cref="HexColor" /> with the hex value of <c>#039BE5FF</c>
    /// </summary>
    public static readonly HexColor Lightblue600 = new("#039BE5FF");

    /// <summary>
    /// Represents the <see cref="Lightblue700" /> <see cref="HexColor" /> with the hex value of <c>#0288D1FF</c>
    /// </summary>
    public static readonly HexColor Lightblue700 = new("#0288D1FF");

    /// <summary>
    /// Represents the <see cref="Lightblue800" /> <see cref="HexColor" /> with the hex value of <c>#0277BDFF</c>
    /// </summary>
    public static readonly HexColor Lightblue800 = new("#0277BDFF");

    /// <summary>
    /// Represents the <see cref="Lightblue900" /> <see cref="HexColor" /> with the hex value of <c>#01579BFF</c>
    /// </summary>
    public static readonly HexColor Lightblue900 = new("#01579BFF");

    /// <summary>
    /// Represents the <see cref="LightblueA100" /> <see cref="HexColor" /> with the hex value of <c>#80D8FFFF</c>
    /// </summary>
    public static readonly HexColor LightblueA100 = new("#80D8FFFF");

    /// <summary>
    /// Represents the <see cref="LightblueA200" /> <see cref="HexColor" /> with the hex value of <c>#40C4FFFF</c>
    /// </summary>
    public static readonly HexColor LightblueA200 = new("#40C4FFFF");

    /// <summary>
    /// Represents the <see cref="LightblueA400" /> <see cref="HexColor" /> with the hex value of <c>#00B0FFFF</c>
    /// </summary>
    public static readonly HexColor LightblueA400 = new("#00B0FFFF");

    /// <summary>
    /// Represents the <see cref="LightblueA700" /> <see cref="HexColor" /> with the hex value of <c>#0091EAFF</c>
    /// </summary>
    public static readonly HexColor LightblueA700 = new("#0091EAFF");

    /// <summary>
    /// Represents the <see cref="Lightcoral" /> <see cref="HexColor" /> with the hex value of <c>#F08080FF</c>
    /// </summary>
    public static readonly HexColor Lightcoral = new("#F08080FF");

    /// <summary>
    /// Represents the <see cref="Lightcyan" /> <see cref="HexColor" /> with the hex value of <c>#E0FFFFFF</c>
    /// </summary>
    public static readonly HexColor Lightcyan = new("#E0FFFFFF");

    /// <summary>
    /// Represents the <see cref="Lightgoldenrodyellow" /> <see cref="HexColor" /> with the hex value of <c>#FAFAD2FF</c>
    /// </summary>
    public static readonly HexColor Lightgoldenrodyellow = new("#FAFAD2FF");

    /// <summary>
    /// Represents the <see cref="Lightgray" /> <see cref="HexColor" /> with the hex value of <c>#D3D3D3FF</c>
    /// </summary>
    public static readonly HexColor Lightgray = new("#D3D3D3FF");

    /// <summary>
    /// Represents the <see cref="Lightgreen" /> <see cref="HexColor" /> with the hex value of <c>#90EE90FF</c>
    /// </summary>
    public static readonly HexColor Lightgreen = new("#90EE90FF");

    /// <summary>
    /// Represents the <see cref="Lightgreen100" /> <see cref="HexColor" /> with the hex value of <c>#DCEDC8FF</c>
    /// </summary>
    public static readonly HexColor Lightgreen100 = new("#DCEDC8FF");

    /// <summary>
    /// Represents the <see cref="Lightgreen200" /> <see cref="HexColor" /> with the hex value of <c>#C5E1A5FF</c>
    /// </summary>
    public static readonly HexColor Lightgreen200 = new("#C5E1A5FF");

    /// <summary>
    /// Represents the <see cref="Lightgreen300" /> <see cref="HexColor" /> with the hex value of <c>#AED581FF</c>
    /// </summary>
    public static readonly HexColor Lightgreen300 = new("#AED581FF");

    /// <summary>
    /// Represents the <see cref="Lightgreen400" /> <see cref="HexColor" /> with the hex value of <c>#9CCC65FF</c>
    /// </summary>
    public static readonly HexColor Lightgreen400 = new("#9CCC65FF");

    /// <summary>
    /// Represents the <see cref="Lightgreen50" /> <see cref="HexColor" /> with the hex value of <c>#F1F8E9FF</c>
    /// </summary>
    public static readonly HexColor Lightgreen50 = new("#F1F8E9FF");

    /// <summary>
    /// Represents the <see cref="Lightgreen500" /> <see cref="HexColor" /> with the hex value of <c>#8BC34AFF</c>
    /// </summary>
    public static readonly HexColor Lightgreen500 = new("#8BC34AFF");

    /// <summary>
    /// Represents the <see cref="Lightgreen600" /> <see cref="HexColor" /> with the hex value of <c>#7CB342FF</c>
    /// </summary>
    public static readonly HexColor Lightgreen600 = new("#7CB342FF");

    /// <summary>
    /// Represents the <see cref="Lightgreen700" /> <see cref="HexColor" /> with the hex value of <c>#689F38FF</c>
    /// </summary>
    public static readonly HexColor Lightgreen700 = new("#689F38FF");

    /// <summary>
    /// Represents the <see cref="Lightgreen800" /> <see cref="HexColor" /> with the hex value of <c>#558B2FFF</c>
    /// </summary>
    public static readonly HexColor Lightgreen800 = new("#558B2FFF");

    /// <summary>
    /// Represents the <see cref="Lightgreen900" /> <see cref="HexColor" /> with the hex value of <c>#33691EFF</c>
    /// </summary>
    public static readonly HexColor Lightgreen900 = new("#33691EFF");

    /// <summary>
    /// Represents the <see cref="LightgreenA100" /> <see cref="HexColor" /> with the hex value of <c>#CCFF90FF</c>
    /// </summary>
    public static readonly HexColor LightgreenA100 = new("#CCFF90FF");

    /// <summary>
    /// Represents the <see cref="LightgreenA200" /> <see cref="HexColor" /> with the hex value of <c>#B2FF59FF</c>
    /// </summary>
    public static readonly HexColor LightgreenA200 = new("#B2FF59FF");

    /// <summary>
    /// Represents the <see cref="LightgreenA400" /> <see cref="HexColor" /> with the hex value of <c>#76FF03FF</c>
    /// </summary>
    public static readonly HexColor LightgreenA400 = new("#76FF03FF");

    /// <summary>
    /// Represents the <see cref="LightgreenA700" /> <see cref="HexColor" /> with the hex value of <c>#64DD17FF</c>
    /// </summary>
    public static readonly HexColor LightgreenA700 = new("#64DD17FF");

    /// <summary>
    /// Represents the <see cref="Lightpink" /> <see cref="HexColor" /> with the hex value of <c>#FFB6C1FF</c>
    /// </summary>
    public static readonly HexColor Lightpink = new("#FFB6C1FF");

    /// <summary>
    /// Represents the <see cref="Lightsalmon" /> <see cref="HexColor" /> with the hex value of <c>#FFA07AFF</c>
    /// </summary>
    public static readonly HexColor Lightsalmon = new("#FFA07AFF");

    /// <summary>
    /// Represents the <see cref="Lightseagreen" /> <see cref="HexColor" /> with the hex value of <c>#20B2AAFF</c>
    /// </summary>
    public static readonly HexColor Lightseagreen = new("#20B2AAFF");

    /// <summary>
    /// Represents the <see cref="Lightskyblue" /> <see cref="HexColor" /> with the hex value of <c>#87CEFAFF</c>
    /// </summary>
    public static readonly HexColor Lightskyblue = new("#87CEFAFF");

    /// <summary>
    /// Represents the <see cref="Lightslategray" /> <see cref="HexColor" /> with the hex value of <c>#778899FF</c>
    /// </summary>
    public static readonly HexColor Lightslategray = new("#778899FF");

    /// <summary>
    /// Represents the <see cref="Lightsteelblue" /> <see cref="HexColor" /> with the hex value of <c>#B0C4DEFF</c>
    /// </summary>
    public static readonly HexColor Lightsteelblue = new("#B0C4DEFF");

    /// <summary>
    /// Represents the <see cref="Lightyellow" /> <see cref="HexColor" /> with the hex value of <c>#FFFFE0FF</c>
    /// </summary>
    public static readonly HexColor Lightyellow = new("#FFFFE0FF");

    /// <summary>Represents the <see cref="Lime" /> <see cref="HexColor" /> with the hex value of <c>#00FF00FF</c></summary>
    public static readonly HexColor Lime = new("#00FF00FF");

    /// <summary>Represents the <see cref="Lime100" /> <see cref="HexColor" /> with the hex value of <c>#F0F4C3FF</c></summary>
    public static readonly HexColor Lime100 = new("#F0F4C3FF");

    /// <summary>Represents the <see cref="Lime200" /> <see cref="HexColor" /> with the hex value of <c>#E6EE9CFF</c></summary>
    public static readonly HexColor Lime200 = new("#E6EE9CFF");

    /// <summary>Represents the <see cref="Lime300" /> <see cref="HexColor" /> with the hex value of <c>#DCE775FF</c></summary>
    public static readonly HexColor Lime300 = new("#DCE775FF");

    /// <summary>Represents the <see cref="Lime400" /> <see cref="HexColor" /> with the hex value of <c>#D4E157FF</c></summary>
    public static readonly HexColor Lime400 = new("#D4E157FF");

    /// <summary>Represents the <see cref="Lime50" /> <see cref="HexColor" /> with the hex value of <c>#F9FBE7FF</c></summary>
    public static readonly HexColor Lime50 = new("#F9FBE7FF");

    /// <summary>Represents the <see cref="Lime500" /> <see cref="HexColor" /> with the hex value of <c>#CDDC39FF</c></summary>
    public static readonly HexColor Lime500 = new("#CDDC39FF");

    /// <summary>Represents the <see cref="Lime600" /> <see cref="HexColor" /> with the hex value of <c>#C0CA33FF</c></summary>
    public static readonly HexColor Lime600 = new("#C0CA33FF");

    /// <summary>Represents the <see cref="Lime700" /> <see cref="HexColor" /> with the hex value of <c>#AFB42BFF</c></summary>
    public static readonly HexColor Lime700 = new("#AFB42BFF");

    /// <summary>Represents the <see cref="Lime800" /> <see cref="HexColor" /> with the hex value of <c>#9E9D24FF</c></summary>
    public static readonly HexColor Lime800 = new("#9E9D24FF");

    /// <summary>Represents the <see cref="Lime900" /> <see cref="HexColor" /> with the hex value of <c>#827717FF</c></summary>
    public static readonly HexColor Lime900 = new("#827717FF");

    /// <summary>
    /// Represents the <see cref="LimeA100" /> <see cref="HexColor" /> with the hex value of <c>#F4FF81FF</c>
    /// </summary>
    public static readonly HexColor LimeA100 = new("#F4FF81FF");

    /// <summary>
    /// Represents the <see cref="LimeA200" /> <see cref="HexColor" /> with the hex value of <c>#EEFF41FF</c>
    /// </summary>
    public static readonly HexColor LimeA200 = new("#EEFF41FF");

    /// <summary>
    /// Represents the <see cref="LimeA400" /> <see cref="HexColor" /> with the hex value of <c>#C6FF00FF</c>
    /// </summary>
    public static readonly HexColor LimeA400 = new("#C6FF00FF");

    /// <summary>
    /// Represents the <see cref="LimeA700" /> <see cref="HexColor" /> with the hex value of <c>#AEEA00FF</c>
    /// </summary>
    public static readonly HexColor LimeA700 = new("#AEEA00FF");

    /// <summary>
    /// Represents the <see cref="LimeGreen" /> <see cref="HexColor" /> with the hex value of <c>#32CD32FF</c>
    /// </summary>
    public static readonly HexColor LimeGreen = new("#32CD32FF");

    /// <summary>Represents the <see cref="Linen" /> <see cref="HexColor" /> with the hex value of <c>#FAF0E6FF</c></summary>
    public static readonly HexColor Linen = new("#FAF0E6FF");

    /// <summary>Represents the <see cref="Magenta" /> <see cref="HexColor" /> with the hex value of <c>#FF00FFFF</c></summary>
    public static readonly HexColor Magenta = new("#FF00FFFF");

    /// <summary>Represents the <see cref="Maroon" /> <see cref="HexColor" /> with the hex value of <c>#800000FF</c></summary>
    public static readonly HexColor Maroon = new("#800000FF");

    /// <summary>
    /// Represents the <see cref="Mediumaquamarine" /> <see cref="HexColor" /> with the hex value of <c>#66CDAAFF</c>
    /// </summary>
    public static readonly HexColor Mediumaquamarine = new("#66CDAAFF");

    /// <summary>
    /// Represents the <see cref="Mediumblue" /> <see cref="HexColor" /> with the hex value of <c>#0000CDFF</c>
    /// </summary>
    public static readonly HexColor Mediumblue = new("#0000CDFF");

    /// <summary>
    /// Represents the <see cref="Mediumorchid" /> <see cref="HexColor" /> with the hex value of <c>#BA55D3FF</c>
    /// </summary>
    public static readonly HexColor Mediumorchid = new("#BA55D3FF");

    /// <summary>
    /// Represents the <see cref="Mediumpurple" /> <see cref="HexColor" /> with the hex value of <c>#9370DBFF</c>
    /// </summary>
    public static readonly HexColor Mediumpurple = new("#9370DBFF");

    /// <summary>
    /// Represents the <see cref="Mediumseagreen" /> <see cref="HexColor" /> with the hex value of <c>#3CB371FF</c>
    /// </summary>
    public static readonly HexColor Mediumseagreen = new("#3CB371FF");

    /// <summary>
    /// Represents the <see cref="Mediumslateblue" /> <see cref="HexColor" /> with the hex value of <c>#7B68EEFF</c>
    /// </summary>
    public static readonly HexColor Mediumslateblue = new("#7B68EEFF");

    /// <summary>
    /// Represents the <see cref="Mediumspringgreen" /> <see cref="HexColor" /> with the hex value of <c>#00FA9AFF</c>
    /// </summary>
    public static readonly HexColor Mediumspringgreen = new("#00FA9AFF");

    /// <summary>
    /// Represents the <see cref="Mediumturquoise" /> <see cref="HexColor" /> with the hex value of <c>#48D1CCFF</c>
    /// </summary>
    public static readonly HexColor Mediumturquoise = new("#48D1CCFF");

    /// <summary>
    /// Represents the <see cref="Mediumvioletred" /> <see cref="HexColor" /> with the hex value of <c>#C71585FF</c>
    /// </summary>
    public static readonly HexColor Mediumvioletred = new("#C71585FF");

    /// <summary>
    /// Represents the <see cref="Midnightblue" /> <see cref="HexColor" /> with the hex value of <c>#191970FF</c>
    /// </summary>
    public static readonly HexColor Midnightblue = new("#191970FF");

    /// <summary>
    /// Represents the <see cref="Mintcream" /> <see cref="HexColor" /> with the hex value of <c>#F5FFFAFF</c>
    /// </summary>
    public static readonly HexColor Mintcream = new("#F5FFFAFF");

    /// <summary>
    /// Represents the <see cref="Mistyrose" /> <see cref="HexColor" /> with the hex value of <c>#FFE4E1FF</c>
    /// </summary>
    public static readonly HexColor Mistyrose = new("#FFE4E1FF");

    /// <summary>
    /// Represents the <see cref="Moccasin" /> <see cref="HexColor" /> with the hex value of <c>#FFE4B5FF</c>
    /// </summary>
    public static readonly HexColor Moccasin = new("#FFE4B5FF");

    /// <summary>
    /// Represents the <see cref="Navajowhite" /> <see cref="HexColor" /> with the hex value of <c>#FFDEADFF</c>
    /// </summary>
    public static readonly HexColor Navajowhite = new("#FFDEADFF");

    /// <summary>Represents the <see cref="Navy" /> <see cref="HexColor" /> with the hex value of <c>#000080FF</c></summary>
    public static readonly HexColor Navy = new("#000080FF");

    /// <summary>Represents the <see cref="Oldlace" /> <see cref="HexColor" /> with the hex value of <c>#FDF5E6FF</c></summary>
    public static readonly HexColor Oldlace = new("#FDF5E6FF");

    /// <summary>Represents the <see cref="Olive" /> <see cref="HexColor" /> with the hex value of <c>#808000FF</c></summary>
    public static readonly HexColor Olive = new("#808000FF");

    /// <summary>
    /// Represents the <see cref="Olivedrab" /> <see cref="HexColor" /> with the hex value of <c>#6B8E23FF</c>
    /// </summary>
    public static readonly HexColor Olivedrab = new("#6B8E23FF");

    /// <summary>Represents the <see cref="Orange" /> <see cref="HexColor" /> with the hex value of <c>#FFA500FF</c></summary>
    public static readonly HexColor Orange = new("#FFA500FF");

    /// <summary>
    /// Represents the <see cref="Orange100" /> <see cref="HexColor" /> with the hex value of <c>#FFE0B2FF</c>
    /// </summary>
    public static readonly HexColor Orange100 = new("#FFE0B2FF");

    /// <summary>
    /// Represents the <see cref="Orange200" /> <see cref="HexColor" /> with the hex value of <c>#FFCC80FF</c>
    /// </summary>
    public static readonly HexColor Orange200 = new("#FFCC80FF");

    /// <summary>
    /// Represents the <see cref="Orange300" /> <see cref="HexColor" /> with the hex value of <c>#FFB74DFF</c>
    /// </summary>
    public static readonly HexColor Orange300 = new("#FFB74DFF");

    /// <summary>
    /// Represents the <see cref="Orange400" /> <see cref="HexColor" /> with the hex value of <c>#FFA726FF</c>
    /// </summary>
    public static readonly HexColor Orange400 = new("#FFA726FF");

    /// <summary>
    /// Represents the <see cref="Orange50" /> <see cref="HexColor" /> with the hex value of <c>#FFF3E0FF</c>
    /// </summary>
    public static readonly HexColor Orange50 = new("#FFF3E0FF");

    /// <summary>
    /// Represents the <see cref="Orange500" /> <see cref="HexColor" /> with the hex value of <c>#FF9800FF</c>
    /// </summary>
    public static readonly HexColor Orange500 = new("#FF9800FF");

    /// <summary>
    /// Represents the <see cref="Orange600" /> <see cref="HexColor" /> with the hex value of <c>#FB8C00FF</c>
    /// </summary>
    public static readonly HexColor Orange600 = new("#FB8C00FF");

    /// <summary>
    /// Represents the <see cref="Orange700" /> <see cref="HexColor" /> with the hex value of <c>#F57C00FF</c>
    /// </summary>
    public static readonly HexColor Orange700 = new("#F57C00FF");

    /// <summary>
    /// Represents the <see cref="Orange800" /> <see cref="HexColor" /> with the hex value of <c>#EF6C00FF</c>
    /// </summary>
    public static readonly HexColor Orange800 = new("#EF6C00FF");

    /// <summary>
    /// Represents the <see cref="Orange900" /> <see cref="HexColor" /> with the hex value of <c>#E65100FF</c>
    /// </summary>
    public static readonly HexColor Orange900 = new("#E65100FF");

    /// <summary>
    /// Represents the <see cref="OrangeA100" /> <see cref="HexColor" /> with the hex value of <c>#FFD180FF</c>
    /// </summary>
    public static readonly HexColor OrangeA100 = new("#FFD180FF");

    /// <summary>
    /// Represents the <see cref="OrangeA200" /> <see cref="HexColor" /> with the hex value of <c>#FFAB40FF</c>
    /// </summary>
    public static readonly HexColor OrangeA200 = new("#FFAB40FF");

    /// <summary>
    /// Represents the <see cref="OrangeA400" /> <see cref="HexColor" /> with the hex value of <c>#FF9100FF</c>
    /// </summary>
    public static readonly HexColor OrangeA400 = new("#FF9100FF");

    /// <summary>
    /// Represents the <see cref="OrangeA700" /> <see cref="HexColor" /> with the hex value of <c>#FF6D00FF</c>
    /// </summary>
    public static readonly HexColor OrangeA700 = new("#FF6D00FF");

    /// <summary>
    /// Represents the <see cref="Orangered" /> <see cref="HexColor" /> with the hex value of <c>#FF4500FF</c>
    /// </summary>
    public static readonly HexColor Orangered = new("#FF4500FF");

    /// <summary>Represents the <see cref="Orchid" /> <see cref="HexColor" /> with the hex value of <c>#DA70D6FF</c></summary>
    public static readonly HexColor Orchid = new("#DA70D6FF");

    /// <summary>
    /// Represents the <see cref="Palegoldenrod" /> <see cref="HexColor" /> with the hex value of <c>#EEE8AAFF</c>
    /// </summary>
    public static readonly HexColor Palegoldenrod = new("#EEE8AAFF");

    /// <summary>
    /// Represents the <see cref="Palegreen" /> <see cref="HexColor" /> with the hex value of <c>#98FB98FF</c>
    /// </summary>
    public static readonly HexColor Palegreen = new("#98FB98FF");

    /// <summary>
    /// Represents the <see cref="Paleturquoise" /> <see cref="HexColor" /> with the hex value of <c>#AFEEEEFF</c>
    /// </summary>
    public static readonly HexColor Paleturquoise = new("#AFEEEEFF");

    /// <summary>
    /// Represents the <see cref="Palevioletred" /> <see cref="HexColor" /> with the hex value of <c>#DB7093FF</c>
    /// </summary>
    public static readonly HexColor Palevioletred = new("#DB7093FF");

    /// <summary>
    /// Represents the <see cref="Papayawhip" /> <see cref="HexColor" /> with the hex value of <c>#FFEFD5FF</c>
    /// </summary>
    public static readonly HexColor Papayawhip = new("#FFEFD5FF");

    /// <summary>
    /// Represents the <see cref="Peachpuff" /> <see cref="HexColor" /> with the hex value of <c>#FFDAB9FF</c>
    /// </summary>
    public static readonly HexColor Peachpuff = new("#FFDAB9FF");

    /// <summary>Represents the <see cref="Peru" /> <see cref="HexColor" /> with the hex value of <c>#CD853FFF</c></summary>
    public static readonly HexColor Peru = new("#CD853FFF");

    /// <summary>Represents the <see cref="Pink" /> <see cref="HexColor" /> with the hex value of <c>#FFC0CBFF</c></summary>
    public static readonly HexColor Pink = new("#FFC0CBFF");

    /// <summary>Represents the <see cref="Pink100" /> <see cref="HexColor" /> with the hex value of <c>#F8BBD0FF</c></summary>
    public static readonly HexColor Pink100 = new("#F8BBD0FF");

    /// <summary>Represents the <see cref="Pink200" /> <see cref="HexColor" /> with the hex value of <c>#F48FB1FF</c></summary>
    public static readonly HexColor Pink200 = new("#F48FB1FF");

    /// <summary>Represents the <see cref="Pink300" /> <see cref="HexColor" /> with the hex value of <c>#F06292FF</c></summary>
    public static readonly HexColor Pink300 = new("#F06292FF");

    /// <summary>Represents the <see cref="Pink400" /> <see cref="HexColor" /> with the hex value of <c>#EC407AFF</c></summary>
    public static readonly HexColor Pink400 = new("#EC407AFF");

    /// <summary>Represents the <see cref="Pink50" /> <see cref="HexColor" /> with the hex value of <c>#FCE4ECFF</c></summary>
    public static readonly HexColor Pink50 = new("#FCE4ECFF");

    /// <summary>Represents the <see cref="Pink500" /> <see cref="HexColor" /> with the hex value of <c>#E91E63FF</c></summary>
    public static readonly HexColor Pink500 = new("#E91E63FF");

    /// <summary>Represents the <see cref="Pink600" /> <see cref="HexColor" /> with the hex value of <c>#D81B60FF</c></summary>
    public static readonly HexColor Pink600 = new("#D81B60FF");

    /// <summary>Represents the <see cref="Pink700" /> <see cref="HexColor" /> with the hex value of <c>#C2185BFF</c></summary>
    public static readonly HexColor Pink700 = new("#C2185BFF");

    /// <summary>Represents the <see cref="Pink800" /> <see cref="HexColor" /> with the hex value of <c>#AD1457FF</c></summary>
    public static readonly HexColor Pink800 = new("#AD1457FF");

    /// <summary>Represents the <see cref="Pink900" /> <see cref="HexColor" /> with the hex value of <c>#880E4FFF</c></summary>
    public static readonly HexColor Pink900 = new("#880E4FFF");

    /// <summary>
    /// Represents the <see cref="PinkA100" /> <see cref="HexColor" /> with the hex value of <c>#FF80ABFF</c>
    /// </summary>
    public static readonly HexColor PinkA100 = new("#FF80ABFF");

    /// <summary>
    /// Represents the <see cref="PinkA200" /> <see cref="HexColor" /> with the hex value of <c>#FF4081FF</c>
    /// </summary>
    public static readonly HexColor PinkA200 = new("#FF4081FF");

    /// <summary>
    /// Represents the <see cref="PinkA400" /> <see cref="HexColor" /> with the hex value of <c>#F50057FF</c>
    /// </summary>
    public static readonly HexColor PinkA400 = new("#F50057FF");

    /// <summary>
    /// Represents the <see cref="PinkA700" /> <see cref="HexColor" /> with the hex value of <c>#C51162FF</c>
    /// </summary>
    public static readonly HexColor PinkA700 = new("#C51162FF");

    /// <summary>Represents the <see cref="Plum" /> <see cref="HexColor" /> with the hex value of <c>#DDA0DDFF</c></summary>
    public static readonly HexColor Plum = new("#DDA0DDFF");

    /// <summary>
    /// Represents the <see cref="Powderblue" /> <see cref="HexColor" /> with the hex value of <c>#B0E0E6FF</c>
    /// </summary>
    public static readonly HexColor Powderblue = new("#B0E0E6FF");

    /// <summary>Represents the <see cref="Purple" /> <see cref="HexColor" /> with the hex value of <c>#800080FF</c></summary>
    public static readonly HexColor Purple = new("#800080FF");

    /// <summary>
    /// Represents the <see cref="Purple100" /> <see cref="HexColor" /> with the hex value of <c>#E1BEE7FF</c>
    /// </summary>
    public static readonly HexColor Purple100 = new("#E1BEE7FF");

    /// <summary>
    /// Represents the <see cref="Purple200" /> <see cref="HexColor" /> with the hex value of <c>#CE93D8FF</c>
    /// </summary>
    public static readonly HexColor Purple200 = new("#CE93D8FF");

    /// <summary>
    /// Represents the <see cref="Purple300" /> <see cref="HexColor" /> with the hex value of <c>#BA68C8FF</c>
    /// </summary>
    public static readonly HexColor Purple300 = new("#BA68C8FF");

    /// <summary>
    /// Represents the <see cref="Purple400" /> <see cref="HexColor" /> with the hex value of <c>#AB47BCFF</c>
    /// </summary>
    public static readonly HexColor Purple400 = new("#AB47BCFF");

    /// <summary>
    /// Represents the <see cref="Purple50" /> <see cref="HexColor" /> with the hex value of <c>#F3E5F5FF</c>
    /// </summary>
    public static readonly HexColor Purple50 = new("#F3E5F5FF");

    /// <summary>
    /// Represents the <see cref="Purple500" /> <see cref="HexColor" /> with the hex value of <c>#9C27B0FF</c>
    /// </summary>
    public static readonly HexColor Purple500 = new("#9C27B0FF");

    /// <summary>
    /// Represents the <see cref="Purple600" /> <see cref="HexColor" /> with the hex value of <c>#8E24AAFF</c>
    /// </summary>
    public static readonly HexColor Purple600 = new("#8E24AAFF");

    /// <summary>
    /// Represents the <see cref="Purple700" /> <see cref="HexColor" /> with the hex value of <c>#7B1FA2FF</c>
    /// </summary>
    public static readonly HexColor Purple700 = new("#7B1FA2FF");

    /// <summary>
    /// Represents the <see cref="Purple800" /> <see cref="HexColor" /> with the hex value of <c>#6A1B9AFF</c>
    /// </summary>
    public static readonly HexColor Purple800 = new("#6A1B9AFF");

    /// <summary>
    /// Represents the <see cref="Purple900" /> <see cref="HexColor" /> with the hex value of <c>#4A148CFF</c>
    /// </summary>
    public static readonly HexColor Purple900 = new("#4A148CFF");

    /// <summary>
    /// Represents the <see cref="PurpleA100" /> <see cref="HexColor" /> with the hex value of <c>#EA80FCFF</c>
    /// </summary>
    public static readonly HexColor PurpleA100 = new("#EA80FCFF");

    /// <summary>
    /// Represents the <see cref="PurpleA200" /> <see cref="HexColor" /> with the hex value of <c>#E040FBFF</c>
    /// </summary>
    public static readonly HexColor PurpleA200 = new("#E040FBFF");

    /// <summary>
    /// Represents the <see cref="PurpleA400" /> <see cref="HexColor" /> with the hex value of <c>#D500F9FF</c>
    /// </summary>
    public static readonly HexColor PurpleA400 = new("#D500F9FF");

    /// <summary>
    /// Represents the <see cref="PurpleA700" /> <see cref="HexColor" /> with the hex value of <c>#AA00FFFF</c>
    /// </summary>
    public static readonly HexColor PurpleA700 = new("#AA00FFFF");

    /// <summary>Represents the <see cref="Red" /> <see cref="HexColor" /> with the hex value of <c>#FF0000FF</c></summary>
    public static readonly HexColor Red = new("#FF0000FF");

    /// <summary>Represents the <see cref="Red100" /> <see cref="HexColor" /> with the hex value of <c>#FFCDD2FF</c></summary>
    public static readonly HexColor Red100 = new("#FFCDD2FF");

    /// <summary>Represents the <see cref="Red200" /> <see cref="HexColor" /> with the hex value of <c>#EF9A9AFF</c></summary>
    public static readonly HexColor Red200 = new("#EF9A9AFF");

    /// <summary>Represents the <see cref="Red300" /> <see cref="HexColor" /> with the hex value of <c>#E57373FF</c></summary>
    public static readonly HexColor Red300 = new("#E57373FF");

    /// <summary>Represents the <see cref="Red400" /> <see cref="HexColor" /> with the hex value of <c>#EF5350FF</c></summary>
    public static readonly HexColor Red400 = new("#EF5350FF");

    /// <summary>Represents the <see cref="Red50" /> <see cref="HexColor" /> with the hex value of <c>#FFEBEEFF</c></summary>
    public static readonly HexColor Red50 = new("#FFEBEEFF");

    /// <summary>Represents the <see cref="Red500" /> <see cref="HexColor" /> with the hex value of <c>#F44336FF</c></summary>
    public static readonly HexColor Red500 = new("#F44336FF");

    /// <summary>Represents the <see cref="Red600" /> <see cref="HexColor" /> with the hex value of <c>#E53935FF</c></summary>
    public static readonly HexColor Red600 = new("#E53935FF");

    /// <summary>Represents the <see cref="Red700" /> <see cref="HexColor" /> with the hex value of <c>#D32F2FFF</c></summary>
    public static readonly HexColor Red700 = new("#D32F2FFF");

    /// <summary>Represents the <see cref="Red800" /> <see cref="HexColor" /> with the hex value of <c>#C62828FF</c></summary>
    public static readonly HexColor Red800 = new("#C62828FF");

    /// <summary>Represents the <see cref="Red900" /> <see cref="HexColor" /> with the hex value of <c>#B71C1CFF</c></summary>
    public static readonly HexColor Red900 = new("#B71C1CFF");

    /// <summary>Represents the <see cref="RedA100" /> <see cref="HexColor" /> with the hex value of <c>#FF8A80FF</c></summary>
    public static readonly HexColor RedA100 = new("#FF8A80FF");

    /// <summary>Represents the <see cref="RedA200" /> <see cref="HexColor" /> with the hex value of <c>#FF5252FF</c></summary>
    public static readonly HexColor RedA200 = new("#FF5252FF");

    /// <summary>Represents the <see cref="RedA400" /> <see cref="HexColor" /> with the hex value of <c>#FF1744FF</c></summary>
    public static readonly HexColor RedA400 = new("#FF1744FF");

    /// <summary>Represents the <see cref="RedA700" /> <see cref="HexColor" /> with the hex value of <c>#D50000FF</c></summary>
    public static readonly HexColor RedA700 = new("#D50000FF");

    /// <summary>
    /// Represents the <see cref="Rosybrown" /> <see cref="HexColor" /> with the hex value of <c>#BC8F8FFF</c>
    /// </summary>
    public static readonly HexColor Rosybrown = new("#BC8F8FFF");

    /// <summary>
    /// Represents the <see cref="Royalblue" /> <see cref="HexColor" /> with the hex value of <c>#4169E1FF</c>
    /// </summary>
    public static readonly HexColor Royalblue = new("#4169E1FF");

    /// <summary>
    /// Represents the <see cref="Saddlebrown" /> <see cref="HexColor" /> with the hex value of <c>#8B4513FF</c>
    /// </summary>
    public static readonly HexColor Saddlebrown = new("#8B4513FF");

    /// <summary>Represents the <see cref="Salmon" /> <see cref="HexColor" /> with the hex value of <c>#FA8072FF</c></summary>
    public static readonly HexColor Salmon = new("#FA8072FF");

    /// <summary>
    /// Represents the <see cref="Sandybrown" /> <see cref="HexColor" /> with the hex value of <c>#F4A460FF</c>
    /// </summary>
    public static readonly HexColor Sandybrown = new("#F4A460FF");

    /// <summary>
    /// Represents the <see cref="Seagreen" /> <see cref="HexColor" /> with the hex value of <c>#2E8B57FF</c>
    /// </summary>
    public static readonly HexColor Seagreen = new("#2E8B57FF");

    /// <summary>
    /// Represents the <see cref="Seashell" /> <see cref="HexColor" /> with the hex value of <c>#FFF5EEFF</c>
    /// </summary>
    public static readonly HexColor Seashell = new("#FFF5EEFF");

    /// <summary>Represents the <see cref="Sienna" /> <see cref="HexColor" /> with the hex value of <c>#A0522DFF</c></summary>
    public static readonly HexColor Sienna = new("#A0522DFF");

    /// <summary>Represents the <see cref="Silver" /> <see cref="HexColor" /> with the hex value of <c>#C0C0C0FF</c></summary>
    public static readonly HexColor Silver = new("#C0C0C0FF");

    /// <summary>Represents the <see cref="Skyblue" /> <see cref="HexColor" /> with the hex value of <c>#87CEEBFF</c></summary>
    public static readonly HexColor Skyblue = new("#87CEEBFF");

    /// <summary>
    /// Represents the <see cref="Slateblue" /> <see cref="HexColor" /> with the hex value of <c>#6A5ACDFF</c>
    /// </summary>
    public static readonly HexColor Slateblue = new("#6A5ACDFF");

    /// <summary>
    /// Represents the <see cref="Slategray" /> <see cref="HexColor" /> with the hex value of <c>#708090FF</c>
    /// </summary>
    public static readonly HexColor Slategray = new("#708090FF");

    /// <summary>Represents the <see cref="Snow" /> <see cref="HexColor" /> with the hex value of <c>#FFFAFAFF</c></summary>
    public static readonly HexColor Snow = new("#FFFAFAFF");

    /// <summary>
    /// Represents the <see cref="Springgreen" /> <see cref="HexColor" /> with the hex value of <c>#00FF7FFF</c>
    /// </summary>
    public static readonly HexColor Springgreen = new("#00FF7FFF");

    /// <summary>
    /// Represents the <see cref="Steelblue" /> <see cref="HexColor" /> with the hex value of <c>#4682B4FF</c>
    /// </summary>
    public static readonly HexColor Steelblue = new("#4682B4FF");

    /// <summary>Represents the <see cref="Tan" /> <see cref="HexColor" /> with the hex value of <c>#D2B48CFF</c></summary>
    public static readonly HexColor Tan = new("#D2B48CFF");

    /// <summary>Represents the <see cref="Teal" /> <see cref="HexColor" /> with the hex value of <c>#008080FF</c></summary>
    public static readonly HexColor Teal = new("#008080FF");

    /// <summary>Represents the <see cref="Teal100" /> <see cref="HexColor" /> with the hex value of <c>#B2DFDBFF</c></summary>
    public static readonly HexColor Teal100 = new("#B2DFDBFF");

    /// <summary>Represents the <see cref="Teal200" /> <see cref="HexColor" /> with the hex value of <c>#80CBC4FF</c></summary>
    public static readonly HexColor Teal200 = new("#80CBC4FF");

    /// <summary>Represents the <see cref="Teal300" /> <see cref="HexColor" /> with the hex value of <c>#4DB6ACFF</c></summary>
    public static readonly HexColor Teal300 = new("#4DB6ACFF");

    /// <summary>Represents the <see cref="Teal400" /> <see cref="HexColor" /> with the hex value of <c>#26A69AFF</c></summary>
    public static readonly HexColor Teal400 = new("#26A69AFF");

    /// <summary>Represents the <see cref="Teal50" /> <see cref="HexColor" /> with the hex value of <c>#E0F2F1FF</c></summary>
    public static readonly HexColor Teal50 = new("#E0F2F1FF");

    /// <summary>Represents the <see cref="Teal500" /> <see cref="HexColor" /> with the hex value of <c>#009688FF</c></summary>
    public static readonly HexColor Teal500 = new("#009688FF");

    /// <summary>Represents the <see cref="Teal600" /> <see cref="HexColor" /> with the hex value of <c>#00897BFF</c></summary>
    public static readonly HexColor Teal600 = new("#00897BFF");

    /// <summary>Represents the <see cref="Teal700" /> <see cref="HexColor" /> with the hex value of <c>#00796BFF</c></summary>
    public static readonly HexColor Teal700 = new("#00796BFF");

    /// <summary>Represents the <see cref="Teal800" /> <see cref="HexColor" /> with the hex value of <c>#00695CFF</c></summary>
    public static readonly HexColor Teal800 = new("#00695CFF");

    /// <summary>Represents the <see cref="Teal900" /> <see cref="HexColor" /> with the hex value of <c>#004D40FF</c></summary>
    public static readonly HexColor Teal900 = new("#004D40FF");

    /// <summary>
    /// Represents the <see cref="TealA100" /> <see cref="HexColor" /> with the hex value of <c>#A7FFEBFF</c>
    /// </summary>
    public static readonly HexColor TealA100 = new("#A7FFEBFF");

    /// <summary>
    /// Represents the <see cref="TealA200" /> <see cref="HexColor" /> with the hex value of <c>#64FFDAFF</c>
    /// </summary>
    public static readonly HexColor TealA200 = new("#64FFDAFF");

    /// <summary>
    /// Represents the <see cref="TealA400" /> <see cref="HexColor" /> with the hex value of <c>#1DE9B6FF</c>
    /// </summary>
    public static readonly HexColor TealA400 = new("#1DE9B6FF");

    /// <summary>
    /// Represents the <see cref="TealA700" /> <see cref="HexColor" /> with the hex value of <c>#00BFA5FF</c>
    /// </summary>
    public static readonly HexColor TealA700 = new("#00BFA5FF");

    /// <summary>Represents the <see cref="Thistle" /> <see cref="HexColor" /> with the hex value of <c>#D8BFD8FF</c></summary>
    public static readonly HexColor Thistle = new("#D8BFD8FF");

    /// <summary>Represents the <see cref="Tomato" /> <see cref="HexColor" /> with the hex value of <c>#FF6347FF</c></summary>
    public static readonly HexColor Tomato = new("#FF6347FF");

    /// <summary>
    /// Represents the <see cref="Transparent" /> <see cref="HexColor" /> with the hex value of <c>#00000000</c>
    /// </summary>
    public static readonly HexColor Transparent = new("#00000000");

    /// <summary>
    /// Represents the <see cref="Turquoise" /> <see cref="HexColor" /> with the hex value of <c>#40E0D0FF</c>
    /// </summary>
    public static readonly HexColor Turquoise = new("#40E0D0FF");

    /// <summary>Represents the <see cref="Violet" /> <see cref="HexColor" /> with the hex value of <c>#EE82EEFF</c></summary>
    public static readonly HexColor Violet = new("#EE82EEFF");

    /// <summary>Represents the <see cref="Wheat" /> <see cref="HexColor" /> with the hex value of <c>#F5DEB3FF</c></summary>
    public static readonly HexColor Wheat = new("#F5DEB3FF");

    /// <summary>Represents the <see cref="White" /> <see cref="HexColor" /> with the hex value of <c>#FFFFFFFF</c></summary>
    public static readonly HexColor White = new("#FFFFFFFF");

    /// <summary>
    /// Represents the <see cref="Whitesmoke" /> <see cref="HexColor" /> with the hex value of <c>#F5F5F5FF</c>
    /// </summary>
    public static readonly HexColor Whitesmoke = new("#F5F5F5FF");

    /// <summary>Represents the <see cref="Yellow" /> <see cref="HexColor" /> with the hex value of <c>#FFFF00FF</c></summary>
    public static readonly HexColor Yellow = new("#FFFF00FF");

    /// <summary>
    /// Represents the <see cref="Yellow100" /> <see cref="HexColor" /> with the hex value of <c>#FFF9C4FF</c>
    /// </summary>
    public static readonly HexColor Yellow100 = new("#FFF9C4FF");

    /// <summary>
    /// Represents the <see cref="Yellow200" /> <see cref="HexColor" /> with the hex value of <c>#FFF59DFF</c>
    /// </summary>
    public static readonly HexColor Yellow200 = new("#FFF59DFF");

    /// <summary>
    /// Represents the <see cref="Yellow300" /> <see cref="HexColor" /> with the hex value of <c>#FFF176FF</c>
    /// </summary>
    public static readonly HexColor Yellow300 = new("#FFF176FF");

    /// <summary>
    /// Represents the <see cref="Yellow400" /> <see cref="HexColor" /> with the hex value of <c>#FFEE58FF</c>
    /// </summary>
    public static readonly HexColor Yellow400 = new("#FFEE58FF");

    /// <summary>
    /// Represents the <see cref="Yellow50" /> <see cref="HexColor" /> with the hex value of <c>#FFFDE7FF</c>
    /// </summary>
    public static readonly HexColor Yellow50 = new("#FFFDE7FF");

    /// <summary>
    /// Represents the <see cref="Yellow500" /> <see cref="HexColor" /> with the hex value of <c>#FFEB3BFF</c>
    /// </summary>
    public static readonly HexColor Yellow500 = new("#FFEB3BFF");

    /// <summary>
    /// Represents the <see cref="Yellow600" /> <see cref="HexColor" /> with the hex value of <c>#FDD835FF</c>
    /// </summary>
    public static readonly HexColor Yellow600 = new("#FDD835FF");

    /// <summary>
    /// Represents the <see cref="Yellow700" /> <see cref="HexColor" /> with the hex value of <c>#FBC02DFF</c>
    /// </summary>
    public static readonly HexColor Yellow700 = new("#FBC02DFF");

    /// <summary>
    /// Represents the <see cref="Yellow800" /> <see cref="HexColor" /> with the hex value of <c>#F9A825FF</c>
    /// </summary>
    public static readonly HexColor Yellow800 = new("#F9A825FF");

    /// <summary>
    /// Represents the <see cref="Yellow900" /> <see cref="HexColor" /> with the hex value of <c>#F57F17FF</c>
    /// </summary>
    public static readonly HexColor Yellow900 = new("#F57F17FF");

    /// <summary>
    /// Represents the <see cref="YellowA100" /> <see cref="HexColor" /> with the hex value of <c>#FFFF8DFF</c>
    /// </summary>
    public static readonly HexColor YellowA100 = new("#FFFF8DFF");

    /// <summary>
    /// Represents the <see cref="YellowA200" /> <see cref="HexColor" /> with the hex value of <c>#FFFF00FF</c>
    /// </summary>
    public static readonly HexColor YellowA200 = new("#FFFF00FF");

    /// <summary>
    /// Represents the <see cref="YellowA400" /> <see cref="HexColor" /> with the hex value of <c>#FFEA00FF</c>
    /// </summary>
    public static readonly HexColor YellowA400 = new("#FFEA00FF");

    /// <summary>
    /// Represents the <see cref="YellowA700" /> <see cref="HexColor" /> with the hex value of <c>#FFD600FF</c>
    /// </summary>
    public static readonly HexColor YellowA700 = new("#FFD600FF");

    /// <summary>
    /// Represents the <see cref="Yellowgreen" /> <see cref="HexColor" /> with the hex value of <c>#9ACD32FF</c>
    /// </summary>
    public static readonly HexColor Yellowgreen = new("#9ACD32FF");

    /// <summary>
    /// Lazily initialized collection of all public <see cref="HexColor" /> members on <see cref="Colors" />. Built once, in a
    /// thread-safe manner, when first accessed.
    /// </summary>
    private static readonly Lazy<IReadOnlyDictionary<string, HexColor>> AllColors = new(BuildDictionary, true);

    /// <summary>
    /// Uses reflection to build an immutable dictionary of all public static members of <see cref="Colors" /> whose type is
    /// <see cref="HexColor" />.
    /// </summary>
    /// <remarks>
    /// Both fields and parameterless properties are included. Duplicate names are overwritten in favor of the later
    /// declaration order (fields first, then properties).
    /// </remarks>
    /// <returns>
    /// An immutable, case-insensitive dictionary mapping color names to their corresponding <see cref="HexColor" /> instances.
    /// </returns>
    private static IReadOnlyDictionary<string, HexColor> BuildDictionary()
    {
        var type = typeof(Colors);
        var builder = ImmutableDictionary.CreateBuilder<string, HexColor>(StringComparer.OrdinalIgnoreCase);

        // Reflect all public static fields of type HexColor.
        foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (fieldInfo.FieldType == typeof(HexColor))
            {
                builder[fieldInfo.Name] = (HexColor)fieldInfo.GetValue(null)!;
            }
        }

        // Reflect all public static parameterless properties of type HexColor.
        foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
        {
            if (propertyInfo.PropertyType == typeof(HexColor) &&
                propertyInfo.GetIndexParameters().Length is 0 &&
                propertyInfo.GetValue(null) is HexColor value)
            {
                builder[propertyInfo.Name] = value;
            }
        }

        return builder.ToImmutable();
    }

    /// <summary>Determines whether a color with the specified name exists in the global <c>Colors</c> registry.</summary>
    /// <param name="name">
    /// The color name to check for existence. Comparison is case-insensitive according to the <see cref="StringComparer" />
    /// used when building the dictionary (typically <see cref="StringComparer.InvariantCultureIgnoreCase" />).
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a color with the given name is defined in the registry; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// This method performs a key lookup only and does not allocate or return the color value itself. It is useful for
    /// validating user input or verifying palette token names efficiently.
    /// </remarks>
    public static bool Contains(string name) => AllColors.Value.ContainsKey(name);

    /// <summary>
    /// Attempts to retrieve a <see cref="HexColor" /> from the <see cref="Colors" /> class by its member name
    /// (case-insensitive).
    /// </summary>
    /// <param name="name">
    /// The name of the color field or property (e.g., <c>"red500"</c> or <c>"aliceblue"</c>). Comparison is case-insensitive.
    /// </param>
    /// <param name="value">
    /// When this method returns <see langword="true" />, contains the corresponding <see cref="HexColor" /> value; otherwise,
    /// <see langword="default" />.
    /// </param>
    /// <returns><see langword="true" /> if a matching color name was found; otherwise, <see langword="false" />.</returns>
    public static bool TryGet(string name, out HexColor value) => AllColors.Value.TryGetValue(name, out value);
}

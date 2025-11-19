using System.Collections.Immutable;
using System.Reflection;

namespace Allyaria.Theming.Constants;

/// <summary>
/// Provides a consolidated, alphabetically sorted library of named colors (CSS and Material), exposed as strongly-typed
/// <see cref="HexColor" /> properties.
/// </summary>
public static class Colors
{
    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Aliceblue" />: <c>#F0F8FFFF</c></summary>
    public static readonly HexColor Aliceblue = new(value: "#F0F8FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber100" />: <c>#FFECB3FF</c></summary>
    public static readonly HexColor Amber100 = new(value: "#FFECB3FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber200" />: <c>#FFE082FF</c></summary>
    public static readonly HexColor Amber200 = new(value: "#FFE082FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber300" />: <c>#FFD54FFF</c></summary>
    public static readonly HexColor Amber300 = new(value: "#FFD54FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber400" />: <c>#FFCA28FF</c></summary>
    public static readonly HexColor Amber400 = new(value: "#FFCA28FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber50" />: <c>#FFF8E1FF</c></summary>
    public static readonly HexColor Amber50 = new(value: "#FFF8E1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber500" />: <c>#FFC107FF</c></summary>
    public static readonly HexColor Amber500 = new(value: "#FFC107FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber600" />: <c>#FFB300FF</c></summary>
    public static readonly HexColor Amber600 = new(value: "#FFB300FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber700" />: <c>#FFA000FF</c></summary>
    public static readonly HexColor Amber700 = new(value: "#FFA000FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber800" />: <c>#FF8F00FF</c></summary>
    public static readonly HexColor Amber800 = new(value: "#FF8F00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Amber900" />: <c>#FF6F00FF</c></summary>
    public static readonly HexColor Amber900 = new(value: "#FF6F00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="AmberA100" />: <c>#FFE57FFF</c></summary>
    public static readonly HexColor AmberA100 = new(value: "#FFE57FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="AmberA200" />: <c>#FFD740FF</c></summary>
    public static readonly HexColor AmberA200 = new(value: "#FFD740FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="AmberA400" />: <c>#FFC400FF</c></summary>
    public static readonly HexColor AmberA400 = new(value: "#FFC400FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="AmberA700" />: <c>#FFAB00FF</c></summary>
    public static readonly HexColor AmberA700 = new(value: "#FFAB00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Antiquewhite" />: <c>#FAEBD7FF</c></summary>
    public static readonly HexColor Antiquewhite = new(value: "#FAEBD7FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Aqua" />: <c>#00FFFFFF</c></summary>
    public static readonly HexColor Aqua = new(value: "#00FFFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Aquamarine" />: <c>#7FFFD4FF</c></summary>
    public static readonly HexColor Aquamarine = new(value: "#7FFFD4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Azure" />: <c>#F0FFFFFF</c></summary>
    public static readonly HexColor Azure = new(value: "#F0FFFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Beige" />: <c>#F5F5DCFF</c></summary>
    public static readonly HexColor Beige = new(value: "#F5F5DCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Bisque" />: <c>#FFE4C4FF</c></summary>
    public static readonly HexColor Bisque = new(value: "#FFE4C4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Black" />: <c>#000000FF</c></summary>
    public static readonly HexColor Black = new(value: "#000000FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blanchedalmond" />: <c>#FFEBCDFF</c></summary>
    public static readonly HexColor Blanchedalmond = new(value: "#FFEBCDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue" />: <c>#0000FFFF</c></summary>
    public static readonly HexColor Blue = new(value: "#0000FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue100" />: <c>#BBDEFBFF</c></summary>
    public static readonly HexColor Blue100 = new(value: "#BBDEFBFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue200" />: <c>#90CAF9FF</c></summary>
    public static readonly HexColor Blue200 = new(value: "#90CAF9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue300" />: <c>#64B5F6FF</c></summary>
    public static readonly HexColor Blue300 = new(value: "#64B5F6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue400" />: <c>#42A5F5FF</c></summary>
    public static readonly HexColor Blue400 = new(value: "#42A5F5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue50" />: <c>#E3F2FDFF</c></summary>
    public static readonly HexColor Blue50 = new(value: "#E3F2FDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue500" />: <c>#2196F3FF</c></summary>
    public static readonly HexColor Blue500 = new(value: "#2196F3FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue600" />: <c>#1E88E5FF</c></summary>
    public static readonly HexColor Blue600 = new(value: "#1E88E5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue700" />: <c>#1976D2FF</c></summary>
    public static readonly HexColor Blue700 = new(value: "#1976D2FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue800" />: <c>#1565C0FF</c></summary>
    public static readonly HexColor Blue800 = new(value: "#1565C0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blue900" />: <c>#0D47A1FF</c></summary>
    public static readonly HexColor Blue900 = new(value: "#0D47A1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueA100" />: <c>#82B1FFFF</c></summary>
    public static readonly HexColor BlueA100 = new(value: "#82B1FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueA200" />: <c>#448AFFFF</c></summary>
    public static readonly HexColor BlueA200 = new(value: "#448AFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueA400" />: <c>#2979FFFF</c></summary>
    public static readonly HexColor BlueA400 = new(value: "#2979FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueA700" />: <c>#2962FFFF</c></summary>
    public static readonly HexColor BlueA700 = new(value: "#2962FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey100" />: <c>#CFD8DCFF</c></summary>
    public static readonly HexColor BlueGrey100 = new(value: "#CFD8DCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey200" />: <c>#B0BEC5FF</c></summary>
    public static readonly HexColor BlueGrey200 = new(value: "#B0BEC5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey300" />: <c>#90A4AEFF</c></summary>
    public static readonly HexColor BlueGrey300 = new(value: "#90A4AEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey400" />: <c>#78909CFF</c></summary>
    public static readonly HexColor BlueGrey400 = new(value: "#78909CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey50" />: <c>#ECEFF1FF</c></summary>
    public static readonly HexColor BlueGrey50 = new(value: "#ECEFF1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey500" />: <c>#607D8BFF</c></summary>
    public static readonly HexColor BlueGrey500 = new(value: "#607D8BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey600" />: <c>#546E7AFF</c></summary>
    public static readonly HexColor BlueGrey600 = new(value: "#546E7AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey700" />: <c>#455A64FF</c></summary>
    public static readonly HexColor BlueGrey700 = new(value: "#455A64FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey800" />: <c>#37474FFF</c></summary>
    public static readonly HexColor BlueGrey800 = new(value: "#37474FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="BlueGrey900" />: <c>#263238FF</c></summary>
    public static readonly HexColor BlueGrey900 = new(value: "#263238FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Blueviolet" />: <c>#8A2BE2FF</c></summary>
    public static readonly HexColor Blueviolet = new(value: "#8A2BE2FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown" />: <c>#A52A2AFF</c></summary>
    public static readonly HexColor Brown = new(value: "#A52A2AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown100" />: <c>#D7CCC8FF</c></summary>
    public static readonly HexColor Brown100 = new(value: "#D7CCC8FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown200" />: <c>#BCAAA4FF</c></summary>
    public static readonly HexColor Brown200 = new(value: "#BCAAA4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown300" />: <c>#A1887FFF</c></summary>
    public static readonly HexColor Brown300 = new(value: "#A1887FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown400" />: <c>#8D6E63FF</c></summary>
    public static readonly HexColor Brown400 = new(value: "#8D6E63FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown50" />: <c>#EFEBE9FF</c></summary>
    public static readonly HexColor Brown50 = new(value: "#EFEBE9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown500" />: <c>#795548FF</c></summary>
    public static readonly HexColor Brown500 = new(value: "#795548FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown600" />: <c>#6D4C41FF</c></summary>
    public static readonly HexColor Brown600 = new(value: "#6D4C41FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown700" />: <c>#5D4037FF</c></summary>
    public static readonly HexColor Brown700 = new(value: "#5D4037FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown800" />: <c>#4E342EFF</c></summary>
    public static readonly HexColor Brown800 = new(value: "#4E342EFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Brown900" />: <c>#3E2723FF</c></summary>
    public static readonly HexColor Brown900 = new(value: "#3E2723FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Burlywood" />: <c>#DEB887FF</c></summary>
    public static readonly HexColor Burlywood = new(value: "#DEB887FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cadetblue" />: <c>#5F9EA0FF</c></summary>
    public static readonly HexColor Cadetblue = new(value: "#5F9EA0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Chartreuse" />: <c>#7FFF00FF</c></summary>
    public static readonly HexColor Chartreuse = new(value: "#7FFF00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Chocolate" />: <c>#D2691EFF</c></summary>
    public static readonly HexColor Chocolate = new(value: "#D2691EFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Coral" />: <c>#FF7F50FF</c></summary>
    public static readonly HexColor Coral = new(value: "#FF7F50FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cornflowerblue" />: <c>#6495EDFF</c></summary>
    public static readonly HexColor Cornflowerblue = new(value: "#6495EDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cornsilk" />: <c>#FFF8DCFF</c></summary>
    public static readonly HexColor Cornsilk = new(value: "#FFF8DCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Crimson" />: <c>#DC143CFF</c></summary>
    public static readonly HexColor Crimson = new(value: "#DC143CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan" />: <c>#00FFFFFF</c></summary>
    public static readonly HexColor Cyan = new(value: "#00FFFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan100" />: <c>#B2EBF2FF</c></summary>
    public static readonly HexColor Cyan100 = new(value: "#B2EBF2FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan200" />: <c>#80DEEAFF</c></summary>
    public static readonly HexColor Cyan200 = new(value: "#80DEEAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan300" />: <c>#4DD0E1FF</c></summary>
    public static readonly HexColor Cyan300 = new(value: "#4DD0E1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan400" />: <c>#26C6DAFF</c></summary>
    public static readonly HexColor Cyan400 = new(value: "#26C6DAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan50" />: <c>#E0F7FAFF</c></summary>
    public static readonly HexColor Cyan50 = new(value: "#E0F7FAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan500" />: <c>#00BCD4FF</c></summary>
    public static readonly HexColor Cyan500 = new(value: "#00BCD4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan600" />: <c>#00ACC1FF</c></summary>
    public static readonly HexColor Cyan600 = new(value: "#00ACC1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan700" />: <c>#0097A7FF</c></summary>
    public static readonly HexColor Cyan700 = new(value: "#0097A7FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan800" />: <c>#00838FFF</c></summary>
    public static readonly HexColor Cyan800 = new(value: "#00838FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Cyan900" />: <c>#006064FF</c></summary>
    public static readonly HexColor Cyan900 = new(value: "#006064FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="CyanA100" />: <c>#84FFFFFF</c></summary>
    public static readonly HexColor CyanA100 = new(value: "#84FFFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="CyanA200" />: <c>#18FFFFFF</c></summary>
    public static readonly HexColor CyanA200 = new(value: "#18FFFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="CyanA400" />: <c>#00E5FFFF</c></summary>
    public static readonly HexColor CyanA400 = new(value: "#00E5FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="CyanA700" />: <c>#00B8D4FF</c></summary>
    public static readonly HexColor CyanA700 = new(value: "#00B8D4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkblue" />: <c>#00008BFF</c></summary>
    public static readonly HexColor Darkblue = new(value: "#00008BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkcyan" />: <c>#008B8BFF</c></summary>
    public static readonly HexColor Darkcyan = new(value: "#008B8BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkgoldenrod" />: <c>#B8860BFF</c></summary>
    public static readonly HexColor Darkgoldenrod = new(value: "#B8860BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkgray" />: <c>#A9A9A9FF</c></summary>
    public static readonly HexColor Darkgray = new(value: "#A9A9A9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkgreen" />: <c>#006400FF</c></summary>
    public static readonly HexColor Darkgreen = new(value: "#006400FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkkhaki" />: <c>#BDB76BFF</c></summary>
    public static readonly HexColor Darkkhaki = new(value: "#BDB76BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkmagenta" />: <c>#8B008BFF</c></summary>
    public static readonly HexColor Darkmagenta = new(value: "#8B008BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkolivegreen" />: <c>#556B2FFF</c></summary>
    public static readonly HexColor Darkolivegreen = new(value: "#556B2FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkorange" />: <c>#FF8C00FF</c></summary>
    public static readonly HexColor Darkorange = new(value: "#FF8C00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkorchid" />: <c>#9932CCFF</c></summary>
    public static readonly HexColor Darkorchid = new(value: "#9932CCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkred" />: <c>#8B0000FF</c></summary>
    public static readonly HexColor Darkred = new(value: "#8B0000FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darksalmon" />: <c>#E9967AFF</c></summary>
    public static readonly HexColor Darksalmon = new(value: "#E9967AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkseagreen" />: <c>#8FBC8FFF</c></summary>
    public static readonly HexColor Darkseagreen = new(value: "#8FBC8FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkslateblue" />: <c>#483D8BFF</c></summary>
    public static readonly HexColor Darkslateblue = new(value: "#483D8BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkslategray" />: <c>#2F4F4FFF</c></summary>
    public static readonly HexColor Darkslategray = new(value: "#2F4F4FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkturquoise" />: <c>#00CED1FF</c></summary>
    public static readonly HexColor Darkturquoise = new(value: "#00CED1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Darkviolet" />: <c>#9400D3FF</c></summary>
    public static readonly HexColor Darkviolet = new(value: "#9400D3FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange100" />: <c>#FFCCBCFF</c></summary>
    public static readonly HexColor Deeporange100 = new(value: "#FFCCBCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange200" />: <c>#FFAB91FF</c></summary>
    public static readonly HexColor Deeporange200 = new(value: "#FFAB91FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange300" />: <c>#FF8A65FF</c></summary>
    public static readonly HexColor Deeporange300 = new(value: "#FF8A65FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange400" />: <c>#FF7043FF</c></summary>
    public static readonly HexColor Deeporange400 = new(value: "#FF7043FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange50" />: <c>#FBE9E7FF</c></summary>
    public static readonly HexColor Deeporange50 = new(value: "#FBE9E7FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange500" />: <c>#FF5722FF</c></summary>
    public static readonly HexColor Deeporange500 = new(value: "#FF5722FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange600" />: <c>#F4511EFF</c></summary>
    public static readonly HexColor Deeporange600 = new(value: "#F4511EFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange700" />: <c>#E64A19FF</c></summary>
    public static readonly HexColor Deeporange700 = new(value: "#E64A19FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange800" />: <c>#D84315FF</c></summary>
    public static readonly HexColor Deeporange800 = new(value: "#D84315FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeporange900" />: <c>#BF360CFF</c></summary>
    public static readonly HexColor Deeporange900 = new(value: "#BF360CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="DeeporangeA100" />: <c>#FF9E80FF</c></summary>
    public static readonly HexColor DeeporangeA100 = new(value: "#FF9E80FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="DeeporangeA200" />: <c>#FF6E40FF</c></summary>
    public static readonly HexColor DeeporangeA200 = new(value: "#FF6E40FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="DeeporangeA400" />: <c>#FF3D00FF</c></summary>
    public static readonly HexColor DeeporangeA400 = new(value: "#FF3D00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="DeeporangeA700" />: <c>#DD2C00FF</c></summary>
    public static readonly HexColor DeeporangeA700 = new(value: "#DD2C00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppink" />: <c>#FF1493FF</c></summary>
    public static readonly HexColor Deeppink = new(value: "#FF1493FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple100" />: <c>#D1C4E9FF</c></summary>
    public static readonly HexColor Deeppurple100 = new(value: "#D1C4E9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple200" />: <c>#B39DDBFF</c></summary>
    public static readonly HexColor Deeppurple200 = new(value: "#B39DDBFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple300" />: <c>#9575CDFF</c></summary>
    public static readonly HexColor Deeppurple300 = new(value: "#9575CDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple400" />: <c>#7E57C2FF</c></summary>
    public static readonly HexColor Deeppurple400 = new(value: "#7E57C2FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple50" />: <c>#EDE7F6FF</c></summary>
    public static readonly HexColor Deeppurple50 = new(value: "#EDE7F6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple500" />: <c>#673AB7FF</c></summary>
    public static readonly HexColor Deeppurple500 = new(value: "#673AB7FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple600" />: <c>#5E35B1FF</c></summary>
    public static readonly HexColor Deeppurple600 = new(value: "#5E35B1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple700" />: <c>#512DA8FF</c></summary>
    public static readonly HexColor Deeppurple700 = new(value: "#512DA8FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple800" />: <c>#4527A0FF</c></summary>
    public static readonly HexColor Deeppurple800 = new(value: "#4527A0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deeppurple900" />: <c>#311B92FF</c></summary>
    public static readonly HexColor Deeppurple900 = new(value: "#311B92FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="DeeppurpleA100" />: <c>#B388FFFF</c></summary>
    public static readonly HexColor DeeppurpleA100 = new(value: "#B388FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="DeeppurpleA200" />: <c>#7C4DFFFF</c></summary>
    public static readonly HexColor DeeppurpleA200 = new(value: "#7C4DFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="DeeppurpleA400" />: <c>#651FFFFF</c></summary>
    public static readonly HexColor DeeppurpleA400 = new(value: "#651FFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="DeeppurpleA700" />: <c>#6200EAFF</c></summary>
    public static readonly HexColor DeeppurpleA700 = new(value: "#6200EAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Deepskyblue" />: <c>#00BFFFFF</c></summary>
    public static readonly HexColor Deepskyblue = new(value: "#00BFFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Dimgray" />: <c>#696969FF</c></summary>
    public static readonly HexColor Dimgray = new(value: "#696969FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Dodgerblue" />: <c>#1E90FFFF</c></summary>
    public static readonly HexColor Dodgerblue = new(value: "#1E90FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Firebrick" />: <c>#B22222FF</c></summary>
    public static readonly HexColor Firebrick = new(value: "#B22222FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Floralwhite" />: <c>#FFFAF0FF</c></summary>
    public static readonly HexColor Floralwhite = new(value: "#FFFAF0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Forestgreen" />: <c>#228B22FF</c></summary>
    public static readonly HexColor Forestgreen = new(value: "#228B22FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Fuchsia" />: <c>#FF00FFFF</c></summary>
    public static readonly HexColor Fuchsia = new(value: "#FF00FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Gainsboro" />: <c>#DCDCDCFF</c></summary>
    public static readonly HexColor Gainsboro = new(value: "#DCDCDCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Ghostwhite" />: <c>#F8F8FFFF</c></summary>
    public static readonly HexColor Ghostwhite = new(value: "#F8F8FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Gold" />: <c>#FFD700FF</c></summary>
    public static readonly HexColor Gold = new(value: "#FFD700FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Goldenrod" />: <c>#DAA520FF</c></summary>
    public static readonly HexColor Goldenrod = new(value: "#DAA520FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Gray" />: <c>#808080FF</c></summary>
    public static readonly HexColor Gray = new(value: "#808080FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green" />: <c>#008000FF</c></summary>
    public static readonly HexColor Green = new(value: "#008000FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green100" />: <c>#C8E6C9FF</c></summary>
    public static readonly HexColor Green100 = new(value: "#C8E6C9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green200" />: <c>#A5D6A7FF</c></summary>
    public static readonly HexColor Green200 = new(value: "#A5D6A7FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green300" />: <c>#81C784FF</c></summary>
    public static readonly HexColor Green300 = new(value: "#81C784FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green400" />: <c>#66BB6AFF</c></summary>
    public static readonly HexColor Green400 = new(value: "#66BB6AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green50" />: <c>#E8F5E9FF</c></summary>
    public static readonly HexColor Green50 = new(value: "#E8F5E9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green500" />: <c>#4CAF50FF</c></summary>
    public static readonly HexColor Green500 = new(value: "#4CAF50FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green600" />: <c>#43A047FF</c></summary>
    public static readonly HexColor Green600 = new(value: "#43A047FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green700" />: <c>#388E3CFF</c></summary>
    public static readonly HexColor Green700 = new(value: "#388E3CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green800" />: <c>#2E7D32FF</c></summary>
    public static readonly HexColor Green800 = new(value: "#2E7D32FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Green900" />: <c>#1B5E20FF</c></summary>
    public static readonly HexColor Green900 = new(value: "#1B5E20FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="GreenA100" />: <c>#B9F6CAFF</c></summary>
    public static readonly HexColor GreenA100 = new(value: "#B9F6CAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="GreenA200" />: <c>#69F0AEFF</c></summary>
    public static readonly HexColor GreenA200 = new(value: "#69F0AEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="GreenA400" />: <c>#00E676FF</c></summary>
    public static readonly HexColor GreenA400 = new(value: "#00E676FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="GreenA700" />: <c>#00C853FF</c></summary>
    public static readonly HexColor GreenA700 = new(value: "#00C853FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Greenyellow" />: <c>#ADFF2FFF</c></summary>
    public static readonly HexColor Greenyellow = new(value: "#ADFF2FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey100" />: <c>#F5F5F5FF</c></summary>
    public static readonly HexColor Grey100 = new(value: "#F5F5F5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey200" />: <c>#EEEEEEFF</c></summary>
    public static readonly HexColor Grey200 = new(value: "#EEEEEEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey300" />: <c>#E0E0E0FF</c></summary>
    public static readonly HexColor Grey300 = new(value: "#E0E0E0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey400" />: <c>#BDBDBDFF</c></summary>
    public static readonly HexColor Grey400 = new(value: "#BDBDBDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey50" />: <c>#FAFAFAFF</c></summary>
    public static readonly HexColor Grey50 = new(value: "#FAFAFAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey500" />: <c>#9E9E9EFF</c></summary>
    public static readonly HexColor Grey500 = new(value: "#9E9E9EFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey600" />: <c>#757575FF</c></summary>
    public static readonly HexColor Grey600 = new(value: "#757575FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey700" />: <c>#616161FF</c></summary>
    public static readonly HexColor Grey700 = new(value: "#616161FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey800" />: <c>#424242FF</c></summary>
    public static readonly HexColor Grey800 = new(value: "#424242FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Grey900" />: <c>#212121FF</c></summary>
    public static readonly HexColor Grey900 = new(value: "#212121FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Honeydew" />: <c>#F0FFF0FF</c></summary>
    public static readonly HexColor Honeydew = new(value: "#F0FFF0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Hotpink" />: <c>#FF69B4FF</c></summary>
    public static readonly HexColor Hotpink = new(value: "#FF69B4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indianred" />: <c>#CD5C5CFF</c></summary>
    public static readonly HexColor Indianred = new(value: "#CD5C5CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo" />: <c>#4B0082FF</c></summary>
    public static readonly HexColor Indigo = new(value: "#4B0082FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo100" />: <c>#C5CAE9FF</c></summary>
    public static readonly HexColor Indigo100 = new(value: "#C5CAE9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo200" />: <c>#9FA8DAFF</c></summary>
    public static readonly HexColor Indigo200 = new(value: "#9FA8DAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo300" />: <c>#7986CBFF</c></summary>
    public static readonly HexColor Indigo300 = new(value: "#7986CBFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo400" />: <c>#5C6BC0FF</c></summary>
    public static readonly HexColor Indigo400 = new(value: "#5C6BC0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo50" />: <c>#E8EAF6FF</c></summary>
    public static readonly HexColor Indigo50 = new(value: "#E8EAF6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo500" />: <c>#3F51B5FF</c></summary>
    public static readonly HexColor Indigo500 = new(value: "#3F51B5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo600" />: <c>#3949ABFF</c></summary>
    public static readonly HexColor Indigo600 = new(value: "#3949ABFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo700" />: <c>#303F9FFF</c></summary>
    public static readonly HexColor Indigo700 = new(value: "#303F9FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo800" />: <c>#283593FF</c></summary>
    public static readonly HexColor Indigo800 = new(value: "#283593FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Indigo900" />: <c>#1A237EFF</c></summary>
    public static readonly HexColor Indigo900 = new(value: "#1A237EFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="IndigoA100" />: <c>#8C9EFFFF</c></summary>
    public static readonly HexColor IndigoA100 = new(value: "#8C9EFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="IndigoA200" />: <c>#536DFEFF</c></summary>
    public static readonly HexColor IndigoA200 = new(value: "#536DFEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="IndigoA400" />: <c>#3D5AFEFF</c></summary>
    public static readonly HexColor IndigoA400 = new(value: "#3D5AFEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="IndigoA700" />: <c>#304FFEFF</c></summary>
    public static readonly HexColor IndigoA700 = new(value: "#304FFEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Ivory" />: <c>#FFFFF0FF</c></summary>
    public static readonly HexColor Ivory = new(value: "#FFFFF0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Khaki" />: <c>#F0E68CFF</c></summary>
    public static readonly HexColor Khaki = new(value: "#F0E68CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lavender" />: <c>#E6E6FAFF</c></summary>
    public static readonly HexColor Lavender = new(value: "#E6E6FAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lavenderblush" />: <c>#FFF0F5FF</c></summary>
    public static readonly HexColor Lavenderblush = new(value: "#FFF0F5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lawngreen" />: <c>#7CFC00FF</c></summary>
    public static readonly HexColor Lawngreen = new(value: "#7CFC00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lemonchiffon" />: <c>#FFFACDFF</c></summary>
    public static readonly HexColor Lemonchiffon = new(value: "#FFFACDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue" />: <c>#ADD8E6FF</c></summary>
    public static readonly HexColor Lightblue = new(value: "#ADD8E6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue100" />: <c>#B3E5FCFF</c></summary>
    public static readonly HexColor Lightblue100 = new(value: "#B3E5FCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue200" />: <c>#81D4FAFF</c></summary>
    public static readonly HexColor Lightblue200 = new(value: "#81D4FAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue300" />: <c>#4FC3F7FF</c></summary>
    public static readonly HexColor Lightblue300 = new(value: "#4FC3F7FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue400" />: <c>#29B6F6FF</c></summary>
    public static readonly HexColor Lightblue400 = new(value: "#29B6F6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue50" />: <c>#E1F5FEFF</c></summary>
    public static readonly HexColor Lightblue50 = new(value: "#E1F5FEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue500" />: <c>#03A9F4FF</c></summary>
    public static readonly HexColor Lightblue500 = new(value: "#03A9F4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue600" />: <c>#039BE5FF</c></summary>
    public static readonly HexColor Lightblue600 = new(value: "#039BE5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue700" />: <c>#0288D1FF</c></summary>
    public static readonly HexColor Lightblue700 = new(value: "#0288D1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue800" />: <c>#0277BDFF</c></summary>
    public static readonly HexColor Lightblue800 = new(value: "#0277BDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightblue900" />: <c>#01579BFF</c></summary>
    public static readonly HexColor Lightblue900 = new(value: "#01579BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LightblueA100" />: <c>#80D8FFFF</c></summary>
    public static readonly HexColor LightblueA100 = new(value: "#80D8FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LightblueA200" />: <c>#40C4FFFF</c></summary>
    public static readonly HexColor LightblueA200 = new(value: "#40C4FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LightblueA400" />: <c>#00B0FFFF</c></summary>
    public static readonly HexColor LightblueA400 = new(value: "#00B0FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LightblueA700" />: <c>#0091EAFF</c></summary>
    public static readonly HexColor LightblueA700 = new(value: "#0091EAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightcoral" />: <c>#F08080FF</c></summary>
    public static readonly HexColor Lightcoral = new(value: "#F08080FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightcyan" />: <c>#E0FFFFFF</c></summary>
    public static readonly HexColor Lightcyan = new(value: "#E0FFFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgoldenrodyellow" />: <c>#FAFAD2FF</c></summary>
    public static readonly HexColor Lightgoldenrodyellow = new(value: "#FAFAD2FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgray" />: <c>#D3D3D3FF</c></summary>
    public static readonly HexColor Lightgray = new(value: "#D3D3D3FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen" />: <c>#90EE90FF</c></summary>
    public static readonly HexColor Lightgreen = new(value: "#90EE90FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen100" />: <c>#DCEDC8FF</c></summary>
    public static readonly HexColor Lightgreen100 = new(value: "#DCEDC8FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen200" />: <c>#C5E1A5FF</c></summary>
    public static readonly HexColor Lightgreen200 = new(value: "#C5E1A5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen300" />: <c>#AED581FF</c></summary>
    public static readonly HexColor Lightgreen300 = new(value: "#AED581FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen400" />: <c>#9CCC65FF</c></summary>
    public static readonly HexColor Lightgreen400 = new(value: "#9CCC65FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen50" />: <c>#F1F8E9FF</c></summary>
    public static readonly HexColor Lightgreen50 = new(value: "#F1F8E9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen500" />: <c>#8BC34AFF</c></summary>
    public static readonly HexColor Lightgreen500 = new(value: "#8BC34AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen600" />: <c>#7CB342FF</c></summary>
    public static readonly HexColor Lightgreen600 = new(value: "#7CB342FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen700" />: <c>#689F38FF</c></summary>
    public static readonly HexColor Lightgreen700 = new(value: "#689F38FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen800" />: <c>#558B2FFF</c></summary>
    public static readonly HexColor Lightgreen800 = new(value: "#558B2FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightgreen900" />: <c>#33691EFF</c></summary>
    public static readonly HexColor Lightgreen900 = new(value: "#33691EFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LightgreenA100" />: <c>#CCFF90FF</c></summary>
    public static readonly HexColor LightgreenA100 = new(value: "#CCFF90FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LightgreenA200" />: <c>#B2FF59FF</c></summary>
    public static readonly HexColor LightgreenA200 = new(value: "#B2FF59FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LightgreenA400" />: <c>#76FF03FF</c></summary>
    public static readonly HexColor LightgreenA400 = new(value: "#76FF03FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LightgreenA700" />: <c>#64DD17FF</c></summary>
    public static readonly HexColor LightgreenA700 = new(value: "#64DD17FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightpink" />: <c>#FFB6C1FF</c></summary>
    public static readonly HexColor Lightpink = new(value: "#FFB6C1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightsalmon" />: <c>#FFA07AFF</c></summary>
    public static readonly HexColor Lightsalmon = new(value: "#FFA07AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightseagreen" />: <c>#20B2AAFF</c></summary>
    public static readonly HexColor Lightseagreen = new(value: "#20B2AAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightskyblue" />: <c>#87CEFAFF</c></summary>
    public static readonly HexColor Lightskyblue = new(value: "#87CEFAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightslategray" />: <c>#778899FF</c></summary>
    public static readonly HexColor Lightslategray = new(value: "#778899FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightsteelblue" />: <c>#B0C4DEFF</c></summary>
    public static readonly HexColor Lightsteelblue = new(value: "#B0C4DEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lightyellow" />: <c>#FFFFE0FF</c></summary>
    public static readonly HexColor Lightyellow = new(value: "#FFFFE0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime" />: <c>#00FF00FF</c></summary>
    public static readonly HexColor Lime = new(value: "#00FF00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime100" />: <c>#F0F4C3FF</c></summary>
    public static readonly HexColor Lime100 = new(value: "#F0F4C3FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime200" />: <c>#E6EE9CFF</c></summary>
    public static readonly HexColor Lime200 = new(value: "#E6EE9CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime300" />: <c>#DCE775FF</c></summary>
    public static readonly HexColor Lime300 = new(value: "#DCE775FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime400" />: <c>#D4E157FF</c></summary>
    public static readonly HexColor Lime400 = new(value: "#D4E157FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime50" />: <c>#F9FBE7FF</c></summary>
    public static readonly HexColor Lime50 = new(value: "#F9FBE7FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime500" />: <c>#CDDC39FF</c></summary>
    public static readonly HexColor Lime500 = new(value: "#CDDC39FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime600" />: <c>#C0CA33FF</c></summary>
    public static readonly HexColor Lime600 = new(value: "#C0CA33FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime700" />: <c>#AFB42BFF</c></summary>
    public static readonly HexColor Lime700 = new(value: "#AFB42BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime800" />: <c>#9E9D24FF</c></summary>
    public static readonly HexColor Lime800 = new(value: "#9E9D24FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Lime900" />: <c>#827717FF</c></summary>
    public static readonly HexColor Lime900 = new(value: "#827717FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LimeA100" />: <c>#F4FF81FF</c></summary>
    public static readonly HexColor LimeA100 = new(value: "#F4FF81FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LimeA200" />: <c>#EEFF41FF</c></summary>
    public static readonly HexColor LimeA200 = new(value: "#EEFF41FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LimeA400" />: <c>#C6FF00FF</c></summary>
    public static readonly HexColor LimeA400 = new(value: "#C6FF00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LimeA700" />: <c>#AEEA00FF</c></summary>
    public static readonly HexColor LimeA700 = new(value: "#AEEA00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="LimeGreen" />: <c>#32CD32FF</c></summary>
    public static readonly HexColor LimeGreen = new(value: "#32CD32FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Linen" />: <c>#FAF0E6FF</c></summary>
    public static readonly HexColor Linen = new(value: "#FAF0E6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Magenta" />: <c>#FF00FFFF</c></summary>
    public static readonly HexColor Magenta = new(value: "#FF00FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Maroon" />: <c>#800000FF</c></summary>
    public static readonly HexColor Maroon = new(value: "#800000FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mediumaquamarine" />: <c>#66CDAAFF</c></summary>
    public static readonly HexColor Mediumaquamarine = new(value: "#66CDAAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mediumblue" />: <c>#0000CDFF</c></summary>
    public static readonly HexColor Mediumblue = new(value: "#0000CDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mediumorchid" />: <c>#BA55D3FF</c></summary>
    public static readonly HexColor Mediumorchid = new(value: "#BA55D3FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mediumpurple" />: <c>#9370DBFF</c></summary>
    public static readonly HexColor Mediumpurple = new(value: "#9370DBFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mediumseagreen" />: <c>#3CB371FF</c></summary>
    public static readonly HexColor Mediumseagreen = new(value: "#3CB371FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mediumslateblue" />: <c>#7B68EEFF</c></summary>
    public static readonly HexColor Mediumslateblue = new(value: "#7B68EEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mediumspringgreen" />: <c>#00FA9AFF</c></summary>
    public static readonly HexColor Mediumspringgreen = new(value: "#00FA9AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mediumturquoise" />: <c>#48D1CCFF</c></summary>
    public static readonly HexColor Mediumturquoise = new(value: "#48D1CCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mediumvioletred" />: <c>#C71585FF</c></summary>
    public static readonly HexColor Mediumvioletred = new(value: "#C71585FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Midnightblue" />: <c>#191970FF</c></summary>
    public static readonly HexColor Midnightblue = new(value: "#191970FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mintcream" />: <c>#F5FFFAFF</c></summary>
    public static readonly HexColor Mintcream = new(value: "#F5FFFAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Mistyrose" />: <c>#FFE4E1FF</c></summary>
    public static readonly HexColor Mistyrose = new(value: "#FFE4E1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Moccasin" />: <c>#FFE4B5FF</c></summary>
    public static readonly HexColor Moccasin = new(value: "#FFE4B5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Navajowhite" />: <c>#FFDEADFF</c></summary>
    public static readonly HexColor Navajowhite = new(value: "#FFDEADFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Navy" />: <c>#000080FF</c></summary>
    public static readonly HexColor Navy = new(value: "#000080FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Oldlace" />: <c>#FDF5E6FF</c></summary>
    public static readonly HexColor Oldlace = new(value: "#FDF5E6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Olive" />: <c>#808000FF</c></summary>
    public static readonly HexColor Olive = new(value: "#808000FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Olivedrab" />: <c>#6B8E23FF</c></summary>
    public static readonly HexColor Olivedrab = new(value: "#6B8E23FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange" />: <c>#FFA500FF</c></summary>
    public static readonly HexColor Orange = new(value: "#FFA500FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange100" />: <c>#FFE0B2FF</c></summary>
    public static readonly HexColor Orange100 = new(value: "#FFE0B2FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange200" />: <c>#FFCC80FF</c></summary>
    public static readonly HexColor Orange200 = new(value: "#FFCC80FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange300" />: <c>#FFB74DFF</c></summary>
    public static readonly HexColor Orange300 = new(value: "#FFB74DFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange400" />: <c>#FFA726FF</c></summary>
    public static readonly HexColor Orange400 = new(value: "#FFA726FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange50" />: <c>#FFF3E0FF</c></summary>
    public static readonly HexColor Orange50 = new(value: "#FFF3E0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange500" />: <c>#FF9800FF</c></summary>
    public static readonly HexColor Orange500 = new(value: "#FF9800FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange600" />: <c>#FB8C00FF</c></summary>
    public static readonly HexColor Orange600 = new(value: "#FB8C00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange700" />: <c>#F57C00FF</c></summary>
    public static readonly HexColor Orange700 = new(value: "#F57C00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange800" />: <c>#EF6C00FF</c></summary>
    public static readonly HexColor Orange800 = new(value: "#EF6C00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orange900" />: <c>#E65100FF</c></summary>
    public static readonly HexColor Orange900 = new(value: "#E65100FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="OrangeA100" />: <c>#FFD180FF</c></summary>
    public static readonly HexColor OrangeA100 = new(value: "#FFD180FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="OrangeA200" />: <c>#FFAB40FF</c></summary>
    public static readonly HexColor OrangeA200 = new(value: "#FFAB40FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="OrangeA400" />: <c>#FF9100FF</c></summary>
    public static readonly HexColor OrangeA400 = new(value: "#FF9100FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="OrangeA700" />: <c>#FF6D00FF</c></summary>
    public static readonly HexColor OrangeA700 = new(value: "#FF6D00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orangered" />: <c>#FF4500FF</c></summary>
    public static readonly HexColor Orangered = new(value: "#FF4500FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Orchid" />: <c>#DA70D6FF</c></summary>
    public static readonly HexColor Orchid = new(value: "#DA70D6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Palegoldenrod" />: <c>#EEE8AAFF</c></summary>
    public static readonly HexColor Palegoldenrod = new(value: "#EEE8AAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Palegreen" />: <c>#98FB98FF</c></summary>
    public static readonly HexColor Palegreen = new(value: "#98FB98FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Paleturquoise" />: <c>#AFEEEEFF</c></summary>
    public static readonly HexColor Paleturquoise = new(value: "#AFEEEEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Palevioletred" />: <c>#DB7093FF</c></summary>
    public static readonly HexColor Palevioletred = new(value: "#DB7093FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Papayawhip" />: <c>#FFEFD5FF</c></summary>
    public static readonly HexColor Papayawhip = new(value: "#FFEFD5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Peachpuff" />: <c>#FFDAB9FF</c></summary>
    public static readonly HexColor Peachpuff = new(value: "#FFDAB9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Peru" />: <c>#CD853FFF</c></summary>
    public static readonly HexColor Peru = new(value: "#CD853FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink" />: <c>#FFC0CBFF</c></summary>
    public static readonly HexColor Pink = new(value: "#FFC0CBFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink100" />: <c>#F8BBD0FF</c></summary>
    public static readonly HexColor Pink100 = new(value: "#F8BBD0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink200" />: <c>#F48FB1FF</c></summary>
    public static readonly HexColor Pink200 = new(value: "#F48FB1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink300" />: <c>#F06292FF</c></summary>
    public static readonly HexColor Pink300 = new(value: "#F06292FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink400" />: <c>#EC407AFF</c></summary>
    public static readonly HexColor Pink400 = new(value: "#EC407AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink50" />: <c>#FCE4ECFF</c></summary>
    public static readonly HexColor Pink50 = new(value: "#FCE4ECFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink500" />: <c>#E91E63FF</c></summary>
    public static readonly HexColor Pink500 = new(value: "#E91E63FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink600" />: <c>#D81B60FF</c></summary>
    public static readonly HexColor Pink600 = new(value: "#D81B60FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink700" />: <c>#C2185BFF</c></summary>
    public static readonly HexColor Pink700 = new(value: "#C2185BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink800" />: <c>#AD1457FF</c></summary>
    public static readonly HexColor Pink800 = new(value: "#AD1457FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Pink900" />: <c>#880E4FFF</c></summary>
    public static readonly HexColor Pink900 = new(value: "#880E4FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="PinkA100" />: <c>#FF80ABFF</c></summary>
    public static readonly HexColor PinkA100 = new(value: "#FF80ABFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="PinkA200" />: <c>#FF4081FF</c></summary>
    public static readonly HexColor PinkA200 = new(value: "#FF4081FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="PinkA400" />: <c>#F50057FF</c></summary>
    public static readonly HexColor PinkA400 = new(value: "#F50057FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="PinkA700" />: <c>#C51162FF</c></summary>
    public static readonly HexColor PinkA700 = new(value: "#C51162FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Plum" />: <c>#DDA0DDFF</c></summary>
    public static readonly HexColor Plum = new(value: "#DDA0DDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Powderblue" />: <c>#B0E0E6FF</c></summary>
    public static readonly HexColor Powderblue = new(value: "#B0E0E6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple" />: <c>#800080FF</c></summary>
    public static readonly HexColor Purple = new(value: "#800080FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple100" />: <c>#E1BEE7FF</c></summary>
    public static readonly HexColor Purple100 = new(value: "#E1BEE7FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple200" />: <c>#CE93D8FF</c></summary>
    public static readonly HexColor Purple200 = new(value: "#CE93D8FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple300" />: <c>#BA68C8FF</c></summary>
    public static readonly HexColor Purple300 = new(value: "#BA68C8FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple400" />: <c>#AB47BCFF</c></summary>
    public static readonly HexColor Purple400 = new(value: "#AB47BCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple50" />: <c>#F3E5F5FF</c></summary>
    public static readonly HexColor Purple50 = new(value: "#F3E5F5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple500" />: <c>#9C27B0FF</c></summary>
    public static readonly HexColor Purple500 = new(value: "#9C27B0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple600" />: <c>#8E24AAFF</c></summary>
    public static readonly HexColor Purple600 = new(value: "#8E24AAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple700" />: <c>#7B1FA2FF</c></summary>
    public static readonly HexColor Purple700 = new(value: "#7B1FA2FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple800" />: <c>#6A1B9AFF</c></summary>
    public static readonly HexColor Purple800 = new(value: "#6A1B9AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Purple900" />: <c>#4A148CFF</c></summary>
    public static readonly HexColor Purple900 = new(value: "#4A148CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="PurpleA100" />: <c>#EA80FCFF</c></summary>
    public static readonly HexColor PurpleA100 = new(value: "#EA80FCFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="PurpleA200" />: <c>#E040FBFF</c></summary>
    public static readonly HexColor PurpleA200 = new(value: "#E040FBFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="PurpleA400" />: <c>#D500F9FF</c></summary>
    public static readonly HexColor PurpleA400 = new(value: "#D500F9FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="PurpleA700" />: <c>#AA00FFFF</c></summary>
    public static readonly HexColor PurpleA700 = new(value: "#AA00FFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red" />: <c>#FF0000FF</c></summary>
    public static readonly HexColor Red = new(value: "#FF0000FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red100" />: <c>#FFCDD2FF</c></summary>
    public static readonly HexColor Red100 = new(value: "#FFCDD2FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red200" />: <c>#EF9A9AFF</c></summary>
    public static readonly HexColor Red200 = new(value: "#EF9A9AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red300" />: <c>#E57373FF</c></summary>
    public static readonly HexColor Red300 = new(value: "#E57373FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red400" />: <c>#EF5350FF</c></summary>
    public static readonly HexColor Red400 = new(value: "#EF5350FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red50" />: <c>#FFEBEEFF</c></summary>
    public static readonly HexColor Red50 = new(value: "#FFEBEEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red500" />: <c>#F44336FF</c></summary>
    public static readonly HexColor Red500 = new(value: "#F44336FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red600" />: <c>#E53935FF</c></summary>
    public static readonly HexColor Red600 = new(value: "#E53935FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red700" />: <c>#D32F2FFF</c></summary>
    public static readonly HexColor Red700 = new(value: "#D32F2FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red800" />: <c>#C62828FF</c></summary>
    public static readonly HexColor Red800 = new(value: "#C62828FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Red900" />: <c>#B71C1CFF</c></summary>
    public static readonly HexColor Red900 = new(value: "#B71C1CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="RedA100" />: <c>#FF8A80FF</c></summary>
    public static readonly HexColor RedA100 = new(value: "#FF8A80FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="RedA200" />: <c>#FF5252FF</c></summary>
    public static readonly HexColor RedA200 = new(value: "#FF5252FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="RedA400" />: <c>#FF1744FF</c></summary>
    public static readonly HexColor RedA400 = new(value: "#FF1744FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="RedA700" />: <c>#D50000FF</c></summary>
    public static readonly HexColor RedA700 = new(value: "#D50000FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Rosybrown" />: <c>#BC8F8FFF</c></summary>
    public static readonly HexColor Rosybrown = new(value: "#BC8F8FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Royalblue" />: <c>#4169E1FF</c></summary>
    public static readonly HexColor Royalblue = new(value: "#4169E1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Saddlebrown" />: <c>#8B4513FF</c></summary>
    public static readonly HexColor Saddlebrown = new(value: "#8B4513FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Salmon" />: <c>#FA8072FF</c></summary>
    public static readonly HexColor Salmon = new(value: "#FA8072FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Sandybrown" />: <c>#F4A460FF</c></summary>
    public static readonly HexColor Sandybrown = new(value: "#F4A460FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Seagreen" />: <c>#2E8B57FF</c></summary>
    public static readonly HexColor Seagreen = new(value: "#2E8B57FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Seashell" />: <c>#FFF5EEFF</c></summary>
    public static readonly HexColor Seashell = new(value: "#FFF5EEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Sienna" />: <c>#A0522DFF</c></summary>
    public static readonly HexColor Sienna = new(value: "#A0522DFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Silver" />: <c>#C0C0C0FF</c></summary>
    public static readonly HexColor Silver = new(value: "#C0C0C0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Skyblue" />: <c>#87CEEBFF</c></summary>
    public static readonly HexColor Skyblue = new(value: "#87CEEBFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Slateblue" />: <c>#6A5ACDFF</c></summary>
    public static readonly HexColor Slateblue = new(value: "#6A5ACDFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Slategray" />: <c>#708090FF</c></summary>
    public static readonly HexColor Slategray = new(value: "#708090FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Snow" />: <c>#FFFAFAFF</c></summary>
    public static readonly HexColor Snow = new(value: "#FFFAFAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Springgreen" />: <c>#00FF7FFF</c></summary>
    public static readonly HexColor Springgreen = new(value: "#00FF7FFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Steelblue" />: <c>#4682B4FF</c></summary>
    public static readonly HexColor Steelblue = new(value: "#4682B4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Tan" />: <c>#D2B48CFF</c></summary>
    public static readonly HexColor Tan = new(value: "#D2B48CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal" />: <c>#008080FF</c></summary>
    public static readonly HexColor Teal = new(value: "#008080FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal100" />: <c>#B2DFDBFF</c></summary>
    public static readonly HexColor Teal100 = new(value: "#B2DFDBFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal200" />: <c>#80CBC4FF</c></summary>
    public static readonly HexColor Teal200 = new(value: "#80CBC4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal300" />: <c>#4DB6ACFF</c></summary>
    public static readonly HexColor Teal300 = new(value: "#4DB6ACFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal400" />: <c>#26A69AFF</c></summary>
    public static readonly HexColor Teal400 = new(value: "#26A69AFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal50" />: <c>#E0F2F1FF</c></summary>
    public static readonly HexColor Teal50 = new(value: "#E0F2F1FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal500" />: <c>#009688FF</c></summary>
    public static readonly HexColor Teal500 = new(value: "#009688FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal600" />: <c>#00897BFF</c></summary>
    public static readonly HexColor Teal600 = new(value: "#00897BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal700" />: <c>#00796BFF</c></summary>
    public static readonly HexColor Teal700 = new(value: "#00796BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal800" />: <c>#00695CFF</c></summary>
    public static readonly HexColor Teal800 = new(value: "#00695CFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Teal900" />: <c>#004D40FF</c></summary>
    public static readonly HexColor Teal900 = new(value: "#004D40FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="TealA100" />: <c>#A7FFEBFF</c></summary>
    public static readonly HexColor TealA100 = new(value: "#A7FFEBFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="TealA200" />: <c>#64FFDAFF</c></summary>
    public static readonly HexColor TealA200 = new(value: "#64FFDAFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="TealA400" />: <c>#1DE9B6FF</c></summary>
    public static readonly HexColor TealA400 = new(value: "#1DE9B6FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="TealA700" />: <c>#00BFA5FF</c></summary>
    public static readonly HexColor TealA700 = new(value: "#00BFA5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Thistle" />: <c>#D8BFD8FF</c></summary>
    public static readonly HexColor Thistle = new(value: "#D8BFD8FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Tomato" />: <c>#FF6347FF</c></summary>
    public static readonly HexColor Tomato = new(value: "#FF6347FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Transparent" />: <c>#00000000</c></summary>
    public static readonly HexColor Transparent = new(value: "#00000000");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Turquoise" />: <c>#40E0D0FF</c></summary>
    public static readonly HexColor Turquoise = new(value: "#40E0D0FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Violet" />: <c>#EE82EEFF</c></summary>
    public static readonly HexColor Violet = new(value: "#EE82EEFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Wheat" />: <c>#F5DEB3FF</c></summary>
    public static readonly HexColor Wheat = new(value: "#F5DEB3FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="White" />: <c>#FFFFFFFF</c></summary>
    public static readonly HexColor White = new(value: "#FFFFFFFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Whitesmoke" />: <c>#F5F5F5FF</c></summary>
    public static readonly HexColor Whitesmoke = new(value: "#F5F5F5FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow" />: <c>#FFFF00FF</c></summary>
    public static readonly HexColor Yellow = new(value: "#FFFF00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow100" />: <c>#FFF9C4FF</c></summary>
    public static readonly HexColor Yellow100 = new(value: "#FFF9C4FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow200" />: <c>#FFF59DFF</c></summary>
    public static readonly HexColor Yellow200 = new(value: "#FFF59DFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow300" />: <c>#FFF176FF</c></summary>
    public static readonly HexColor Yellow300 = new(value: "#FFF176FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow400" />: <c>#FFEE58FF</c></summary>
    public static readonly HexColor Yellow400 = new(value: "#FFEE58FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow50" />: <c>#FFFDE7FF</c></summary>
    public static readonly HexColor Yellow50 = new(value: "#FFFDE7FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow500" />: <c>#FFEB3BFF</c></summary>
    public static readonly HexColor Yellow500 = new(value: "#FFEB3BFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow600" />: <c>#FDD835FF</c></summary>
    public static readonly HexColor Yellow600 = new(value: "#FDD835FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow700" />: <c>#FBC02DFF</c></summary>
    public static readonly HexColor Yellow700 = new(value: "#FBC02DFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow800" />: <c>#F9A825FF</c></summary>
    public static readonly HexColor Yellow800 = new(value: "#F9A825FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellow900" />: <c>#F57F17FF</c></summary>
    public static readonly HexColor Yellow900 = new(value: "#F57F17FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="YellowA100" />: <c>#FFFF8DFF</c></summary>
    public static readonly HexColor YellowA100 = new(value: "#FFFF8DFF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="YellowA200" />: <c>#FFFF00FF</c></summary>
    public static readonly HexColor YellowA200 = new(value: "#FFFF00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="YellowA400" />: <c>#FFEA00FF</c></summary>
    public static readonly HexColor YellowA400 = new(value: "#FFEA00FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="YellowA700" />: <c>#FFD600FF</c></summary>
    public static readonly HexColor YellowA700 = new(value: "#FFD600FF");

    /// <summary>Represents the <see cref="HexColor" /> for <see cref="Yellowgreen" />: <c>#9ACD32FF</c></summary>
    public static readonly HexColor Yellowgreen = new(value: "#9ACD32FF");

    /// <summary>
    /// Lazily initialized dictionary that provides read-only access to all named colors keyed by their constant name, using a
    /// case-insensitive string comparer.
    /// </summary>
    /// <remarks>
    /// The underlying dictionary is constructed on first access by reflecting over the public static <see cref="HexColor" />
    /// fields declared on the <see cref="Colors" /> type and materializing them into an immutable dictionary.
    /// </remarks>
    private static readonly Lazy<IReadOnlyDictionary<string, HexColor>> AllColors = new(
        valueFactory: BuildDictionary,
        isThreadSafe: true
    );

    /// <summary>
    /// Builds an immutable, case-insensitive dictionary that maps color names to their corresponding <see cref="HexColor" />
    /// values.
    /// </summary>
    /// <returns>
    /// A read-only dictionary containing all publicly exposed <see cref="HexColor" /> fields defined on the
    /// <see cref="Colors" /> type, keyed by field name.
    /// </returns>
    /// <remarks>
    /// This method is used as the value factory for the lazily initialized color dictionary and relies on reflection to
    /// discover the color fields at runtime.
    /// </remarks>
    private static IReadOnlyDictionary<string, HexColor> BuildDictionary()
    {
        var type = typeof(Colors);

        var builder =
            ImmutableDictionary.CreateBuilder<string, HexColor>(keyComparer: StringComparer.OrdinalIgnoreCase);

        foreach (var fieldInfo in type.GetFields(bindingAttr: BindingFlags.Public | BindingFlags.Static))
        {
            if (fieldInfo.FieldType == typeof(HexColor))
            {
                builder[key: fieldInfo.Name] = (HexColor)fieldInfo.GetValue(obj: null)!;
            }
        }

        return builder.ToImmutable();
    }

    /// <summary>
    /// Determines whether a named color with the specified <paramref name="name" /> exists in the color library.
    /// </summary>
    /// <param name="name">
    /// The case-insensitive name of the color to look up. This value must not be null, empty, or consist only of whitespace
    /// characters.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a color with the specified name is defined; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// This method validates <paramref name="name" /> using a guard clause before performing the lookup against the lazily
    /// initialized color dictionary.
    /// </remarks>
    public static bool Contains(string name)
    {
        AryGuard.NotNullOrWhiteSpace(value: name);

        return AllColors.Value.ContainsKey(key: name);
    }

    /// <summary>
    /// Attempts to retrieve the <see cref="HexColor" /> associated with the specified color <paramref name="name" />.
    /// </summary>
    /// <param name="name">
    /// The case-insensitive name of the color to retrieve. This value must not be null, empty, or consist only of whitespace
    /// characters.
    /// </param>
    /// <param name="value">
    /// When this method returns, contains the <see cref="HexColor" /> associated with <paramref name="name" /> if it is found;
    /// otherwise, contains the default <see cref="HexColor" /> value.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a color with the specified name exists and <paramref name="value" /> was set; otherwise,
    /// <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// This method validates <paramref name="name" /> using a guard clause before attempting the lookup against the lazily
    /// initialized color dictionary.
    /// </remarks>
    public static bool TryGet(string name, out HexColor value)
    {
        AryGuard.NotNullOrWhiteSpace(value: name);

        return AllColors.Value.TryGetValue(key: name, value: out value);
    }
}

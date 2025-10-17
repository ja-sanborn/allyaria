# Allyaria.Abstractions.Constants.Colors

`Colors` is a static library of strongly-typed `HexColor` constants and helper lookups.

This type provides an alphabetized, consolidated set of named colors (CSS and Material variants) exposed as public
`HexColor` constants (e.g., `Red`, `Blue500`, `TealA700`). It also includes fast, case-insensitive lookup methods (
`Contains` and `TryGet`) backed by a lazily built, reflection-driven registry.

## Constructors

*None*

## Properties

| Name                   | Type       | Description                      |
|------------------------|------------|----------------------------------|
| `Aliceblue`            | `HexColor` | Constant color #F0F8FFFF (ARGB). |
| `Amber100`             | `HexColor` | Constant color #FFECB3FF (ARGB). |
| `Amber200`             | `HexColor` | Constant color #FFE082FF (ARGB). |
| `Amber300`             | `HexColor` | Constant color #FFD54FFF (ARGB). |
| `Amber400`             | `HexColor` | Constant color #FFCA28FF (ARGB). |
| `Amber50`              | `HexColor` | Constant color #FFF8E1FF (ARGB). |
| `Amber500`             | `HexColor` | Constant color #FFC107FF (ARGB). |
| `Amber600`             | `HexColor` | Constant color #FFB300FF (ARGB). |
| `Amber700`             | `HexColor` | Constant color #FFA000FF (ARGB). |
| `Amber800`             | `HexColor` | Constant color #FF8F00FF (ARGB). |
| `Amber900`             | `HexColor` | Constant color #FF6F00FF (ARGB). |
| `AmberA100`            | `HexColor` | Constant color #FFE57FFF (ARGB). |
| `AmberA200`            | `HexColor` | Constant color #FFD740FF (ARGB). |
| `AmberA400`            | `HexColor` | Constant color #FFC400FF (ARGB). |
| `AmberA700`            | `HexColor` | Constant color #FFAB00FF (ARGB). |
| `Antiquewhite`         | `HexColor` | Constant color #FAEBD7FF (ARGB). |
| `Aqua`                 | `HexColor` | Constant color #00FFFFFF (ARGB). |
| `Aquamarine`           | `HexColor` | Constant color #7FFFD4FF (ARGB). |
| `Azure`                | `HexColor` | Constant color #F0FFFFFF (ARGB). |
| `Beige`                | `HexColor` | Constant color #F5F5DCFF (ARGB). |
| `Bisque`               | `HexColor` | Constant color #FFE4C4FF (ARGB). |
| `Black`                | `HexColor` | Constant color #000000FF (ARGB). |
| `Blanchedalmond`       | `HexColor` | Constant color #FFEBCDFF (ARGB). |
| `Blue`                 | `HexColor` | Constant color #0000FFFF (ARGB). |
| `Blue100`              | `HexColor` | Constant color #BBDEFBFF (ARGB). |
| `Blue200`              | `HexColor` | Constant color #90CAF9FF (ARGB). |
| `Blue300`              | `HexColor` | Constant color #64B5F6FF (ARGB). |
| `Blue400`              | `HexColor` | Constant color #42A5F5FF (ARGB). |
| `Blue50`               | `HexColor` | Constant color #E3F2FDFF (ARGB). |
| `Blue500`              | `HexColor` | Constant color #2196F3FF (ARGB). |
| `Blue600`              | `HexColor` | Constant color #1E88E5FF (ARGB). |
| `Blue700`              | `HexColor` | Constant color #1976D2FF (ARGB). |
| `Blue800`              | `HexColor` | Constant color #1565C0FF (ARGB). |
| `Blue900`              | `HexColor` | Constant color #0D47A1FF (ARGB). |
| `BlueA100`             | `HexColor` | Constant color #82B1FFFF (ARGB). |
| `BlueA200`             | `HexColor` | Constant color #448AFFFF (ARGB). |
| `BlueA400`             | `HexColor` | Constant color #2979FFFF (ARGB). |
| `BlueA700`             | `HexColor` | Constant color #2962FFFF (ARGB). |
| `Blueviolet`           | `HexColor` | Constant color #8A2BE2FF (ARGB). |
| `Brown`                | `HexColor` | Constant color #A52A2AFF (ARGB). |
| `Brown100`             | `HexColor` | Constant color #D7CCC8FF (ARGB). |
| `Brown200`             | `HexColor` | Constant color #BCAAA4FF (ARGB). |
| `Brown300`             | `HexColor` | Constant color #A1887FFF (ARGB). |
| `Brown400`             | `HexColor` | Constant color #8D6E63FF (ARGB). |
| `Brown50`              | `HexColor` | Constant color #EFEBE9FF (ARGB). |
| `Brown500`             | `HexColor` | Constant color #795548FF (ARGB). |
| `Brown600`             | `HexColor` | Constant color #6D4C41FF (ARGB). |
| `Brown700`             | `HexColor` | Constant color #5D4037FF (ARGB). |
| `Brown800`             | `HexColor` | Constant color #4E342EFF (ARGB). |
| `Brown900`             | `HexColor` | Constant color #3E2723FF (ARGB). |
| `Burlywood`            | `HexColor` | Constant color #DEB887FF (ARGB). |
| `Cadetblue`            | `HexColor` | Constant color #5F9EA0FF (ARGB). |
| `Chartreuse`           | `HexColor` | Constant color #7FFF00FF (ARGB). |
| `Chocolate`            | `HexColor` | Constant color #D2691EFF (ARGB). |
| `Coral`                | `HexColor` | Constant color #FF7F50FF (ARGB). |
| `Cornflowerblue`       | `HexColor` | Constant color #6495EDFF (ARGB). |
| `Cornsilk`             | `HexColor` | Constant color #FFF8DCFF (ARGB). |
| `Crimson`              | `HexColor` | Constant color #DC143CFF (ARGB). |
| `Cyan`                 | `HexColor` | Constant color #00FFFFFF (ARGB). |
| `Cyan100`              | `HexColor` | Constant color #B2EBF2FF (ARGB). |
| `Cyan200`              | `HexColor` | Constant color #80DEEAFF (ARGB). |
| `Cyan300`              | `HexColor` | Constant color #4DD0E1FF (ARGB). |
| `Cyan400`              | `HexColor` | Constant color #26C6DAFF (ARGB). |
| `Cyan50`               | `HexColor` | Constant color #E0F7FAFF (ARGB). |
| `Cyan500`              | `HexColor` | Constant color #00BCD4FF (ARGB). |
| `Cyan600`              | `HexColor` | Constant color #00ACC1FF (ARGB). |
| `Cyan700`              | `HexColor` | Constant color #0097A7FF (ARGB). |
| `Cyan800`              | `HexColor` | Constant color #00838FFF (ARGB). |
| `Cyan900`              | `HexColor` | Constant color #006064FF (ARGB). |
| `CyanA100`             | `HexColor` | Constant color #84FFFFFF (ARGB). |
| `CyanA200`             | `HexColor` | Constant color #18FFFFFF (ARGB). |
| `CyanA400`             | `HexColor` | Constant color #00E5FFFF (ARGB). |
| `CyanA700`             | `HexColor` | Constant color #00B8D4FF (ARGB). |
| `Darkblue`             | `HexColor` | Constant color #00008BFF (ARGB). |
| `Darkcyan`             | `HexColor` | Constant color #008B8BFF (ARGB). |
| `Darkgoldenrod`        | `HexColor` | Constant color #B8860BFF (ARGB). |
| `Darkgray`             | `HexColor` | Constant color #A9A9A9FF (ARGB). |
| `Darkgreen`            | `HexColor` | Constant color #006400FF (ARGB). |
| `Darkgrey`             | `HexColor` | Constant color #A9A9A9FF (ARGB). |
| `Darkkhaki`            | `HexColor` | Constant color #BDB76BFF (ARGB). |
| `Darkmagenta`          | `HexColor` | Constant color #8B008BFF (ARGB). |
| `Darkolivegreen`       | `HexColor` | Constant color #556B2FFF (ARGB). |
| `Darkorange`           | `HexColor` | Constant color #FF8C00FF (ARGB). |
| `Darkorchid`           | `HexColor` | Constant color #9932CCFF (ARGB). |
| `Darkred`              | `HexColor` | Constant color #8B0000FF (ARGB). |
| `Darksalmon`           | `HexColor` | Constant color #E9967AFF (ARGB). |
| `Darkseagreen`         | `HexColor` | Constant color #8FBC8BFF (ARGB). |
| `Darkslateblue`        | `HexColor` | Constant color #483D8BFF (ARGB). |
| `Darkslategray`        | `HexColor` | Constant color #2F4F4FFF (ARGB). |
| `Darkslategrey`        | `HexColor` | Constant color #2F4F4FFF (ARGB). |
| `Darkturquoise`        | `HexColor` | Constant color #00CED1FF (ARGB). |
| `Darkviolet`           | `HexColor` | Constant color #9400D3FF (ARGB). |
| `Deeppink`             | `HexColor` | Constant color #FF1493FF (ARGB). |
| `Deepskyblue`          | `HexColor` | Constant color #00BFFFFF (ARGB). |
| `Dimgray`              | `HexColor` | Constant color #696969FF (ARGB). |
| `Dimgrey`              | `HexColor` | Constant color #696969FF (ARGB). |
| `Dodgerblue`           | `HexColor` | Constant color #1E90FFFF (ARGB). |
| `Firebrick`            | `HexColor` | Constant color #B22222FF (ARGB). |
| `Floralwhite`          | `HexColor` | Constant color #FFFAF0FF (ARGB). |
| `Forestgreen`          | `HexColor` | Constant color #228B22FF (ARGB). |
| `Fuchsia`              | `HexColor` | Constant color #FF00FFFF (ARGB). |
| `Gainsboro`            | `HexColor` | Constant color #DCDCDCFF (ARGB). |
| `Ghostwhite`           | `HexColor` | Constant color #F8F8FFFF (ARGB). |
| `Gold`                 | `HexColor` | Constant color #FFD700FF (ARGB). |
| `Goldenrod`            | `HexColor` | Constant color #DAA520FF (ARGB). |
| `Gray`                 | `HexColor` | Constant color #808080FF (ARGB). |
| `Gray100`              | `HexColor` | Constant color #F5F5F5FF (ARGB). |
| `Gray200`              | `HexColor` | Constant color #EEEEEEFF (ARGB). |
| `Gray300`              | `HexColor` | Constant color #E0E0E0FF (ARGB). |
| `Gray400`              | `HexColor` | Constant color #BDBDBDFF (ARGB). |
| `Gray50`               | `HexColor` | Constant color #FAFAFAFF (ARGB). |
| `Gray500`              | `HexColor` | Constant color #9E9E9EFF (ARGB). |
| `Gray600`              | `HexColor` | Constant color #757575FF (ARGB). |
| `Gray700`              | `HexColor` | Constant color #616161FF (ARGB). |
| `Gray800`              | `HexColor` | Constant color #424242FF (ARGB). |
| `Gray900`              | `HexColor` | Constant color #212121FF (ARGB). |
| `Green`                | `HexColor` | Constant color #008000FF (ARGB). |
| `Green100`             | `HexColor` | Constant color #C8E6C9FF (ARGB). |
| `Green200`             | `HexColor` | Constant color #A5D6A7FF (ARGB). |
| `Green300`             | `HexColor` | Constant color #81C784FF (ARGB). |
| `Green400`             | `HexColor` | Constant color #66BB6AFF (ARGB). |
| `Green50`              | `HexColor` | Constant color #E8F5E9FF (ARGB). |
| `Green500`             | `HexColor` | Constant color #4CAF50FF (ARGB). |
| `Green600`             | `HexColor` | Constant color #43A047FF (ARGB). |
| `Green700`             | `HexColor` | Constant color #388E3CFF (ARGB). |
| `Green800`             | `HexColor` | Constant color #2E7D32FF (ARGB). |
| `Green900`             | `HexColor` | Constant color #1B5E20FF (ARGB). |
| `GreenA100`            | `HexColor` | Constant color #B9F6CAFF (ARGB). |
| `GreenA200`            | `HexColor` | Constant color #69F0AEFF (ARGB). |
| `GreenA400`            | `HexColor` | Constant color #00E676FF (ARGB). |
| `GreenA700`            | `HexColor` | Constant color #00C853FF (ARGB). |
| `Greenyellow`          | `HexColor` | Constant color #ADFF2FFF (ARGB). |
| `Greys100`             | `HexColor` | Constant color #F5F5F5FF (ARGB). |
| `Greys200`             | `HexColor` | Constant color #EEEEEEFF (ARGB). |
| `Greys300`             | `HexColor` | Constant color #E0E0E0FF (ARGB). |
| `Greys400`             | `HexColor` | Constant color #BDBDBDFF (ARGB). |
| `Greys50`              | `HexColor` | Constant color #FAFAFAFF (ARGB). |
| `Greys500`             | `HexColor` | Constant color #9E9E9EFF (ARGB). |
| `Greys600`             | `HexColor` | Constant color #757575FF (ARGB). |
| `Greys700`             | `HexColor` | Constant color #616161FF (ARGB). |
| `Greys800`             | `HexColor` | Constant color #424242FF (ARGB). |
| `Greys900`             | `HexColor` | Constant color #212121FF (ARGB). |
| `Honeydew`             | `HexColor` | Constant color #F0FFF0FF (ARGB). |
| `Hotpink`              | `HexColor` | Constant color #FF69B4FF (ARGB). |
| `Indianred`            | `HexColor` | Constant color #CD5C5CFF (ARGB). |
| `Indigo`               | `HexColor` | Constant color #4B0082FF (ARGB). |
| `Indigo100`            | `HexColor` | Constant color #C5CAE9FF (ARGB). |
| `Indigo200`            | `HexColor` | Constant color #9FA8DAFF (ARGB). |
| `Indigo300`            | `HexColor` | Constant color #7986CBFF (ARGB). |
| `Indigo400`            | `HexColor` | Constant color #5C6BC0FF (ARGB). |
| `Indigo50`             | `HexColor` | Constant color #E8EAF6FF (ARGB). |
| `Indigo500`            | `HexColor` | Constant color #3F51B5FF (ARGB). |
| `Indigo600`            | `HexColor` | Constant color #3949ABFF (ARGB). |
| `Indigo700`            | `HexColor` | Constant color #303F9FFF (ARGB). |
| `Indigo800`            | `HexColor` | Constant color #283593FF (ARGB). |
| `Indigo900`            | `HexColor` | Constant color #1A237EFF (ARGB). |
| `IndigoA100`           | `HexColor` | Constant color #8C9EFFFF (ARGB). |
| `IndigoA200`           | `HexColor` | Constant color #536DFEFF (ARGB). |
| `IndigoA400`           | `HexColor` | Constant color #3D5AFEFF (ARGB). |
| `IndigoA700`           | `HexColor` | Constant color #304FFEFF (ARGB). |
| `Ivory`                | `HexColor` | Constant color #FFFFF0FF (ARGB). |
| `Khaki`                | `HexColor` | Constant color #F0E68CFF (ARGB). |
| `Lavender`             | `HexColor` | Constant color #E6E6FAFF (ARGB). |
| `Lavenderblush`        | `HexColor` | Constant color #FFF0F5FF (ARGB). |
| `Lawngreen`            | `HexColor` | Constant color #7CFC00FF (ARGB). |
| `Lemonchiffon`         | `HexColor` | Constant color #FFFACDFF (ARGB). |
| `Lightblue`            | `HexColor` | Constant color #ADD8E6FF (ARGB). |
| `Lightcoral`           | `HexColor` | Constant color #F08080FF (ARGB). |
| `Lightcyan`            | `HexColor` | Constant color #E0FFFFFF (ARGB). |
| `Lightgoldenrodyellow` | `HexColor` | Constant color #FAFAD2FF (ARGB). |
| `Lightgray`            | `HexColor` | Constant color #D3D3D3FF (ARGB). |
| `Lightgreen`           | `HexColor` | Constant color #90EE90FF (ARGB). |
| `Lightgrey`            | `HexColor` | Constant color #D3D3D3FF (ARGB). |
| `Lightpink`            | `HexColor` | Constant color #FFB6C1FF (ARGB). |
| `Lightsalmon`          | `HexColor` | Constant color #FFA07AFF (ARGB). |
| `Lightseagreen`        | `HexColor` | Constant color #20B2AAFF (ARGB). |
| `Lightskyblue`         | `HexColor` | Constant color #87CEFAFF (ARGB). |
| `Lightslategray`       | `HexColor` | Constant color #778899FF (ARGB). |
| `Lightslategrey`       | `HexColor` | Constant color #778899FF (ARGB). |
| `Lightsteelblue`       | `HexColor` | Constant color #B0C4DEFF (ARGB). |
| `Lightyellow`          | `HexColor` | Constant color #FFFFE0FF (ARGB). |
| `Lime`                 | `HexColor` | Constant color #00FF00FF (ARGB). |
| `Limegreen`            | `HexColor` | Constant color #32CD32FF (ARGB). |
| `Linen`                | `HexColor` | Constant color #FAF0E6FF (ARGB). |
| `Magenta`              | `HexColor` | Constant color #FF00FFFF (ARGB). |
| `Maroon`               | `HexColor` | Constant color #800000FF (ARGB). |
| `Mediumaquamarine`     | `HexColor` | Constant color #66CDAAFF (ARGB). |
| `Mediumblue`           | `HexColor` | Constant color #0000CDFF (ARGB). |
| `Mediumorchid`         | `HexColor` | Constant color #BA55D3FF (ARGB). |
| `Mediumpurple`         | `HexColor` | Constant color #9370DBFF (ARGB). |
| `Mediumseagreen`       | `HexColor` | Constant color #3CB371FF (ARGB). |
| `Mediumslateblue`      | `HexColor` | Constant color #7B68EEFF (ARGB). |
| `Mediumspringgreen`    | `HexColor` | Constant color #00FA9AFF (ARGB). |
| `Mediumturquoise`      | `HexColor` | Constant color #48D1CCFF (ARGB). |
| `Mediumvioletred`      | `HexColor` | Constant color #C71585FF (ARGB). |
| `Midnightblue`         | `HexColor` | Constant color #191970FF (ARGB). |
| `Mintcream`            | `HexColor` | Constant color #F5FFFAFF (ARGB). |
| `Mistyrose`            | `HexColor` | Constant color #FFE4E1FF (ARGB). |
| `Moccasin`             | `HexColor` | Constant color #FFE4B5FF (ARGB). |
| `Navajowhite`          | `HexColor` | Constant color #FFDEADFF (ARGB). |
| `Navy`                 | `HexColor` | Constant color #000080FF (ARGB). |
| `Oldlace`              | `HexColor` | Constant color #FDF5E6FF (ARGB). |
| `Olive`                | `HexColor` | Constant color #808000FF (ARGB). |
| `Olivedrab`            | `HexColor` | Constant color #6B8E23FF (ARGB). |
| `Orange`               | `HexColor` | Constant color #FFA500FF (ARGB). |
| `Orange100`            | `HexColor` | Constant color #FFE0B2FF (ARGB). |
| `Orange200`            | `HexColor` | Constant color #FFCC80FF (ARGB). |
| `Orange300`            | `HexColor` | Constant color #FFB74DFF (ARGB). |
| `Orange400`            | `HexColor` | Constant color #FFA726FF (ARGB). |
| `Orange50`             | `HexColor` | Constant color #FFF3E0FF (ARGB). |
| `Orange500`            | `HexColor` | Constant color #FF9800FF (ARGB). |
| `Orange600`            | `HexColor` | Constant color #FB8C00FF (ARGB). |
| `Orange700`            | `HexColor` | Constant color #F57C00FF (ARGB). |
| `Orange800`            | `HexColor` | Constant color #EF6C00FF (ARGB). |
| `Orange900`            | `HexColor` | Constant color #E65100FF (ARGB). |
| `OrangeA100`           | `HexColor` | Constant color #FFD180FF (ARGB). |
| `OrangeA200`           | `HexColor` | Constant color #FFAB40FF (ARGB). |
| `OrangeA400`           | `HexColor` | Constant color #FF9100FF (ARGB). |
| `OrangeA700`           | `HexColor` | Constant color #FF6D00FF (ARGB). |
| `Orangered`            | `HexColor` | Constant color #FF4500FF (ARGB). |
| `Orchid`               | `HexColor` | Constant color #DA70D6FF (ARGB). |
| `Palegoldenrod`        | `HexColor` | Constant color #EEE8AAFF (ARGB). |
| `Palegreen`            | `HexColor` | Constant color #98FB98FF (ARGB). |
| `Paleturquoise`        | `HexColor` | Constant color #AFEEEEFF (ARGB). |
| `Palevioletred`        | `HexColor` | Constant color #DB7093FF (ARGB). |
| `Papayawhip`           | `HexColor` | Constant color #FFEFD5FF (ARGB). |
| `Peachpuff`            | `HexColor` | Constant color #FFDAB9FF (ARGB). |
| `Peru`                 | `HexColor` | Constant color #CD853FFF (ARGB). |
| `Pink`                 | `HexColor` | Constant color #FFC0CBFF (ARGB). |
| `Plum`                 | `HexColor` | Constant color #DDA0DDFF (ARGB). |
| `Powderblue`           | `HexColor` | Constant color #B0E0E6FF (ARGB). |
| `Purple`               | `HexColor` | Constant color #800080FF (ARGB). |
| `Rebeccapurple`        | `HexColor` | Constant color #663399FF (ARGB). |
| `Red`                  | `HexColor` | Constant color #FF0000FF (ARGB). |
| `Red100`               | `HexColor` | Constant color #FFCDD2FF (ARGB). |
| `Red200`               | `HexColor` | Constant color #EF9A9AFF (ARGB). |
| `Red300`               | `HexColor` | Constant color #E57373FF (ARGB). |
| `Red400`               | `HexColor` | Constant color #EF5350FF (ARGB). |
| `Red50`                | `HexColor` | Constant color #FFEBEEFF (ARGB). |
| `Red500`               | `HexColor` | Constant color #F44336FF (ARGB). |
| `Red600`               | `HexColor` | Constant color #E53935FF (ARGB). |
| `Red700`               | `HexColor` | Constant color #D32F2FFF (ARGB). |
| `Red800`               | `HexColor` | Constant color #C62828FF (ARGB). |
| `Red900`               | `HexColor` | Constant color #B71C1CFF (ARGB). |
| `RedA100`              | `HexColor` | Constant color #FF8A80FF (ARGB). |
| `RedA200`              | `HexColor` | Constant color #FF5252FF (ARGB). |
| `RedA400`              | `HexColor` | Constant color #FF1744FF (ARGB). |
| `RedA700`              | `HexColor` | Constant color #D50000FF (ARGB). |
| `Rosybrown`            | `HexColor` | Constant color #BC8F8FFF (ARGB). |
| `Royalblue`            | `HexColor` | Constant color #4169E1FF (ARGB). |
| `Saddlebrown`          | `HexColor` | Constant color #8B4513FF (ARGB). |
| `Salmon`               | `HexColor` | Constant color #FA8072FF (ARGB). |
| `Sandybrown`           | `HexColor` | Constant color #F4A460FF (ARGB). |
| `Seagreen`             | `HexColor` | Constant color #2E8B57FF (ARGB). |
| `Seashell`             | `HexColor` | Constant color #FFF5EEFF (ARGB). |
| `Sienna`               | `HexColor` | Constant color #A0522DFF (ARGB). |
| `Silver`               | `HexColor` | Constant color #C0C0C0FF (ARGB). |
| `Skyblue`              | `HexColor` | Constant color #87CEEBFF (ARGB). |
| `Slateblue`            | `HexColor` | Constant color #6A5ACDFF (ARGB). |
| `Slategray`            | `HexColor` | Constant color #708090FF (ARGB). |
| `Slategrey`            | `HexColor` | Constant color #708090FF (ARGB). |
| `Snow`                 | `HexColor` | Constant color #FFFAFAFF (ARGB). |
| `Springgreen`          | `HexColor` | Constant color #00FF7FFF (ARGB). |
| `Steelblue`            | `HexColor` | Constant color #4682B4FF (ARGB). |
| `Tan`                  | `HexColor` | Constant color #D2B48CFF (ARGB). |
| `Teal`                 | `HexColor` | Constant color #008080FF (ARGB). |
| `Teal100`              | `HexColor` | Constant color #B2DFDBFF (ARGB). |
| `Teal200`              | `HexColor` | Constant color #80CBC4FF (ARGB). |
| `Teal300`              | `HexColor` | Constant color #4DB6ACFF (ARGB). |
| `Teal400`              | `HexColor` | Constant color #26A69AFF (ARGB). |
| `Teal50`               | `HexColor` | Constant color #E0F2F1FF (ARGB). |
| `Teal500`              | `HexColor` | Constant color #009688FF (ARGB). |
| `Teal600`              | `HexColor` | Constant color #00897BFF (ARGB). |
| `Teal700`              | `HexColor` | Constant color #00796BFF (ARGB). |
| `Teal800`              | `HexColor` | Constant color #00695CFF (ARGB). |
| `Teal900`              | `HexColor` | Constant color #004D40FF (ARGB). |
| `TealA100`             | `HexColor` | Constant color #A7FFEBFF (ARGB). |
| `TealA200`             | `HexColor` | Constant color #64FFDAFF (ARGB). |
| `TealA400`             | `HexColor` | Constant color #1DE9B6FF (ARGB). |
| `TealA700`             | `HexColor` | Constant color #00BFA5FF (ARGB). |
| `Thistle`              | `HexColor` | Constant color #D8BFD8FF (ARGB). |
| `Tomato`               | `HexColor` | Constant color #FF6347FF (ARGB). |
| `Turquoise`            | `HexColor` | Constant color #40E0D0FF (ARGB). |
| `Violet`               | `HexColor` | Constant color #EE82EEFF (ARGB). |
| `Wheat`                | `HexColor` | Constant color #F5DEB3FF (ARGB). |
| `White`                | `HexColor` | Constant color #FFFFFFFF (ARGB). |
| `Whitesmoke`           | `HexColor` | Constant color #F5F5F5FF (ARGB). |
| `Yellow`               | `HexColor` | Constant color #FFFF00FF (ARGB). |
| `Yellowgreen`          | `HexColor` | Constant color #9ACD32FF (ARGB). |
| `Yellowish`            | `HexColor` | Constant color #FFE082FF (ARGB). |

> *Note:* The table above includes all public `HexColor` members defined on `Colors` (both CSS named colors and Material
> Design variants).

## Methods

| Name                                      | Returns | Description                                                                                                        |
|-------------------------------------------|---------|--------------------------------------------------------------------------------------------------------------------|
| `Contains(string name)`                   | `bool`  | Returns `true` if a color with the given `name` exists in the registry; comparison is case-insensitive.            |
| `TryGet(string name, out HexColor value)` | `bool`  | Attempts to retrieve a `HexColor` by member `name` (case-insensitive); returns `true` and sets `value` on success. |

## Operators

*None*

## Events

*None*

## Exceptions

*None*

## Example

```csharp
if (Colors.Contains("Blue500") && Colors.TryGet("Blue500", out HexColor accent))
{
    // Use the color in your theming/palette pipeline
    var primary = Colors.White;
    var danger  = Colors.Red600;
    var blue = accent;

    // ...
}
```

---

*Revision Date: 2025-10-16*

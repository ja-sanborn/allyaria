namespace Allyaria.Theming.Constants.Styling;

[ExcludeFromCodeCoverage(Justification = "This class is a library of constant values.")]
public static class CssFontFamily
{
    public static readonly StyleValueString Monospace = new(
        "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, 'Liberation Mono', 'Courier New', monospace"
    );

    public static readonly StyleValueString SansSerif = new(
        "system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif"
    );

    public static readonly StyleValueString Serif = new("ui-serif, Georgia, Cambria, 'Times New Roman', Times, serif");
}

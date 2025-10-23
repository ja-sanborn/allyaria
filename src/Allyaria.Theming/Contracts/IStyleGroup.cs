namespace Allyaria.Theming.Contracts;

public interface IStyleGroup
{
    CssBuilder BuildCss(CssBuilder builder, string? varPrefix = "");

    string ToCss(string? varPrefix = "");
}

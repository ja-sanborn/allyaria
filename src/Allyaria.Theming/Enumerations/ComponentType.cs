namespace Allyaria.Theming.Enumerations;

/// <summary>Defines the various component types recognized by the Allyaria theming system.</summary>
public enum ComponentType
{
    /// <summary>Represents the document’s body element, providing global background and base styling.</summary>
    GlobalBody,

    /// <summary>Represents the global focus outline styling applied across components for accessibility.</summary>
    GlobalFocus,

    /// <summary>Represents the root HTML element, typically used for global theme variables or background context.</summary>
    GlobalHtml,

    /// <summary>Represents a level-one heading (<c>&lt;h1&gt;</c>), used for top-level titles.</summary>
    Heading1,

    /// <summary>Represents a level-two heading (<c>&lt;h2&gt;</c>), typically used for major section headers.</summary>
    Heading2,

    /// <summary>Represents a level-three heading (<c>&lt;h3&gt;</c>), used for sub-section titles.</summary>
    Heading3,

    /// <summary>Represents a level-four heading (<c>&lt;h4&gt;</c>), typically for minor section titles.</summary>
    Heading4,

    /// <summary>Represents a level-five heading (<c>&lt;h5&gt;</c>), used for nested or secondary headers.</summary>
    Heading5,

    /// <summary>Represents a level-six heading (<c>&lt;h6&gt;</c>), typically the smallest semantic heading level.</summary>
    Heading6,

    /// <summary>
    /// Represents a hyperlink element (<c>&lt;a&gt;</c>), including themed color states for visited and hover interactions.
    /// </summary>
    Link,

    /// <summary>Represents a surface or container element, such as cards, panels, or background regions.</summary>
    Surface,

    /// <summary>Represents general text content such as paragraphs, spans, or inline text elements.</summary>
    Text
}

# Contributing to Allyaria Editor

> *Version 1: 2025-09-18*

Thank you for your interest in contributing to **Allyaria Editor**!  
We welcome bug reports, feature requests, documentation improvements, and code contributions.

> By contributing to Allyaria Editor, you agree to abide by the [Code of Conduct](./CODE_OF_CONDUCT.md).

## 📖 Coding Standards

All contributions must follow the project’s coding standards.  
These are documented in the [`docs/standards`](./docs/standards) directory:

- [Accessibility](./docs/standards/Accessibility.md)
- [C#](./docs/standards/CSharp.md)
- [CSS](./docs/standards/CSS.md)
- [JavaScript](./docs/standards/JavaScript.md)
- [Localization](./docs/standards/Localization.md)
- [Razor](./docs/standards/Razor.md)
- [Testing](./docs/standards/Testing.md)
- [Theming](./docs/standards/Theming.md)

Please review these before submitting a pull request. PRs that do not follow the standards may be asked to update.

## 🛠️ How to Contribute

1. **Fork the repository** and create your branch from `main`.
    - Branch names should be descriptive, e.g. `feature/dialog-component`, `fix/scroll-lock`.
2. **Write clear commit messages.**
    - Use the imperative mood (e.g., “Add focus trap helper”).
    - Keep commits scoped and meaningful.
3. **Add or update tests** as appropriate.
    - We use [bUnit](https://bunit.dev/) for component tests.
    - Accessibility and localization should be verified where relevant.
4. **Run checks locally** before opening a PR:
    - `dotnet build`
    - `dotnet test`
    - `dotnet format --verify-no-changes`
5. **Submit a pull request** against the `main` branch.
    - Include a clear description of the change.
    - Reference related issues if applicable.
    - Mention any deviations from standards and justify them.

## 🐞 Reporting Issues

- Use the [GitHub Issues](https://github.com/ja-sanborn/allyaria-editor/issues) tab.
- Include steps to reproduce, expected vs. actual behavior, and environment details.
- For security-related issues, **do not** open a public issue. Please see [`SECURITY.md`](./SECURITY.md).

## 📚 Additional Notes

- **Accessibility (a11y)** is a **non-negotiable requirement** for all components.
- **Localization**: All user-facing strings must be localizable.
- **Minimal JavaScript**: Only use JS where Blazor cannot handle functionality.
- **Code Review**: PRs require at least one review before merging.

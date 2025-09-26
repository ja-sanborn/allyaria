using Allyaria.Theming.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Allyaria.Theming.Values
{
    public sealed class AllyariaAngleValue : ValueBase
    {
        private static readonly HashSet<string> AngleUnits = new(
        [
            "deg",
            "grad",
            "rad",
            "turn"
        ],
        StringComparer.OrdinalIgnoreCase);

        private static readonly Regex NumberPrefixRegex = new(
            @"^[+-]?(?:\d+(?:\.\d*)?|\.\d+)",
            RegexOptions.Compiled | RegexOptions.CultureInvariant
        );

        public AllyariaAngleValue(string value) : base(Normalize(value)) { }

        public double Number
        {
            get
            {
                var match = NumberPrefixRegex.Match(Value);

                if (!match.Success)
                {
                    return 0.0d;
                }

                return double.TryParse(match.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var dbl)
                    ? dbl
                    : 0.0d;
            }
        }

        private static bool IsAngle(string value)
        {
            foreach (var u in AngleUnits)
            {
                if (value.EndsWith(u, StringComparison.Ordinal) && value.Length > u.Length)
                {
                    var numberPart = value[..^u.Length];
                    if (double.TryParse(numberPart, NumberStyles.Float, CultureInfo.InvariantCulture, out var angle))
                    {
                        return angle >= -90.0d && angle <= 90.0d;
                    }
                }
            }

            return false;
        }

        private static string Normalize(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            var trimmedValue = value.Trim().ToLowerInvariant();

            if (IsAngle(trimmedValue)))
            {
                return trimmedValue;
            }

            throw new ArgumentException("Value is not a valid CSS angle.", nameof(value));
        }

        public static AllyariaAngleValue Parse(string value) => new(value);

        public static bool TryParse(string value, out AllyariaAngleValue? result)
        {
            try
            {
                result = new AllyariaAngleValue(value);

                return true;
            }
            catch
            {
                result = null;

                return false;
            }
        }

        public static implicit operator AllyariaAngleValue(string value) => new(value);

        public static implicit operator string(AllyariaAngleValue? number) => number?.Value ?? string.Empty;
    }
}

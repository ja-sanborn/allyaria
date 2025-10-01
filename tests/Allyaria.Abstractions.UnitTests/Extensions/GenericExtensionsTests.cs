using Allyaria.Abstractions.Extensions;

namespace Allyaria.Abstractions.UnitTests.Extensions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class GenericExtensionsTests
{
    private enum Color
    {
        // ReSharper disable once UnusedMember.Local
        Red = 0,
        Green = 1,
        Blue = 2
    }

    public static IEnumerable<object[]> DateTime_HasValue_Cases()
    {
        yield return new object[]
        {
            new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            DateTime.MaxValue
        };

        yield return new object[]
        {
            new DateTime(2024, 2, 29, 23, 59, 59, DateTimeKind.Unspecified),
            new DateTime(1999, 12, 31)
        };

        yield return new object[]
        {
            DateTime.MinValue,
            DateTime.MaxValue
        };
    }

    public static IEnumerable<object[]> Guid_HasValue_Cases()
    {
        yield return new object[]
        {
            Guid.NewGuid(),
            Guid.Empty
        };

        yield return new object[]
        {
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Guid.Parse("22222222-2222-2222-2222-222222222222")
        };
    }

    [Fact]
    public void OrDefault_Bool_Should_ReturnLanguageDefault_When_ValueIsNull_And_DefaultNotProvided()
    {
        // Arrange
        bool? input = null;

        // Act
        var result = input.OrDefault();

        // Assert
        result.Should().Be(default(bool));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void OrDefault_Bool_Should_ReturnProvidedDefault_When_ValueIsNull(bool defaultValue)
    {
        // Arrange
        bool? input = null;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(defaultValue);
    }

    [Theory]
    [InlineData(true, false, true)]
    [InlineData(false, true, false)]
    public void OrDefault_Bool_Should_ReturnValue_When_ValueHasValue(bool value, bool defaultValue, bool expected)
    {
        // Arrange
        bool? input = value;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void OrDefault_DateTime_Should_ReturnLanguageDefault_When_ValueIsNull_And_DefaultNotProvided()
    {
        // Arrange
        DateTime? input = null;

        // Act
        var result = input.OrDefault();

        // Assert
        result.Should().Be(default(DateTime));
    }

    [Fact]
    public void OrDefault_DateTime_Should_ReturnProvidedDefault_When_ValueIsNull()
    {
        // Arrange
        DateTime? input = null;
        var fallback = new DateTime(2030, 12, 25, 6, 30, 0, DateTimeKind.Local);

        // Act
        var result = input.OrDefault(fallback);

        // Assert
        result.Should().Be(fallback);
    }

    [Theory]
    [MemberData(nameof(DateTime_HasValue_Cases))]
    public void OrDefault_DateTime_Should_ReturnValue_When_ValueHasValue(DateTime value, DateTime defaultValue)
    {
        // Arrange
        DateTime? input = value;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(value);
    }

    [Fact]
    public void OrDefault_Decimal_Should_ReturnLanguageDefault_When_ValueIsNull_And_DefaultNotProvided()
    {
        // Arrange
        decimal? input = null;

        // Act
        var result = input.OrDefault();

        // Assert
        result.Should().Be(default(decimal));
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-3.14159)]
    [InlineData(2.71828)]
    public void OrDefault_Decimal_Should_ReturnProvidedDefault_When_ValueIsNull(decimal defaultValue)
    {
        // Arrange
        decimal? input = null;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(defaultValue);
    }

    [Theory]
    [InlineData(0.0, 1.5, 0.0)]
    [InlineData(-1.25, 9.9, -1.25)]
    public void OrDefault_Decimal_Should_ReturnValue_When_ValueHasValue(decimal value,
        decimal defaultValue,
        decimal expected)
    {
        // Arrange
        decimal? input = value;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void OrDefault_Double_Should_ReturnLanguageDefault_When_ValueIsNull_And_DefaultNotProvided()
    {
        // Arrange
        double? input = null;

        // Act
        var result = input.OrDefault();

        // Assert
        result.Should().Be(default(double));
    }

    [Fact]
    public void OrDefault_Double_Should_ReturnNaN_When_ValueHasValueIsNaN()
    {
        // Arrange
        double? input = double.NaN;

        // Act
        var result = input.OrDefault();

        // Assert
        result.Should().Be(double.NaN);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-3.14159)]
    [InlineData(2.71828)]
    public void OrDefault_Double_Should_ReturnProvidedDefault_When_ValueIsNull(double defaultValue)
    {
        // Arrange
        double? input = null;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(defaultValue);
    }

    [Theory]
    [InlineData(0.0, 1.5, 0.0)]
    [InlineData(-1.25, 9.9, -1.25)]
    [InlineData(double.PositiveInfinity, -123.0, double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity, 0.0, double.NegativeInfinity)]
    public void OrDefault_Double_Should_ReturnValue_When_ValueHasValue(double value,
        double defaultValue,
        double expected)
    {
        // Arrange
        double? input = value;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void OrDefault_Enum_Should_ReturnProvidedDefault_When_ValueIsNull()
    {
        // Arrange
        Color? input = null;

        // Act
        var result = input.OrDefault(Color.Blue);

        // Assert
        result.Should().Be(Color.Blue);
    }

    [Fact]
    public void OrDefault_Enum_Should_ReturnUnderlyingZero_When_ValueIsNull_And_DefaultNotProvided()
    {
        // Arrange
        Color? input = null;

        // Act
        var result = input.OrDefault();

        // Assert
        result.Should().Be(default(Color)); // zero => Color.Red
    }

    [Fact]
    public void OrDefault_Enum_Should_ReturnValue_When_ValueHasValue()
    {
        // Arrange
        Color? input = Color.Green;

        // Act
        var result = input.OrDefault(Color.Blue);

        // Assert
        result.Should().Be(Color.Green);
    }

    [Fact]
    public void OrDefault_Guid_Should_ReturnGuidEmpty_When_ValueIsNull_And_DefaultNotProvided()
    {
        // Arrange
        Guid? input = null;

        // Act
        var result = input.OrDefault();

        // Assert
        result.Should().Be(Guid.Empty);
    }

    [Fact]
    public void OrDefault_Guid_Should_ReturnProvidedDefault_When_ValueIsNull()
    {
        // Arrange
        Guid? input = null;
        var fallback = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

        // Act
        var result = input.OrDefault(fallback);

        // Assert
        result.Should().Be(fallback);
    }

    [Theory]
    [MemberData(nameof(Guid_HasValue_Cases))]
    public void OrDefault_Guid_Should_ReturnValue_When_ValueHasValue(Guid value, Guid defaultValue)
    {
        // Arrange
        Guid? input = value;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(value);
    }

    [Fact]
    public void OrDefault_Int_Should_ReturnLanguageDefault_When_ValueIsNull_And_DefaultNotProvided()
    {
        // Arrange
        int? input = null;

        // Act
        var result = input.OrDefault();

        // Assert
        result.Should().Be(default(int));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(123)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void OrDefault_Int_Should_ReturnProvidedDefault_When_ValueIsNull(int defaultValue)
    {
        // Arrange
        int? input = null;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(defaultValue);
    }

    [Theory]
    [InlineData(0, 42, 0)]
    [InlineData(5, 9, 5)]
    [InlineData(int.MinValue, int.MaxValue, int.MinValue)]
    [InlineData(int.MaxValue, int.MinValue, int.MaxValue)]
    public void OrDefault_Int_Should_ReturnValue_When_ValueHasValue(int value, int defaultValue, int expected)
    {
        // Arrange
        int? input = value;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void OrDefault_TimeSpan_Should_ReturnLanguageDefault_When_ValueIsNull_And_DefaultNotProvided()
    {
        // Arrange
        TimeSpan? input = null;

        // Act
        var result = input.OrDefault();

        // Assert
        result.Should().Be(default(TimeSpan));
    }

    [Fact]
    public void OrDefault_TimeSpan_Should_ReturnProvidedDefault_When_ValueIsNull()
    {
        // Arrange
        TimeSpan? input = null;
        var fallback = TimeSpan.FromSeconds(42);

        // Act
        var result = input.OrDefault(fallback);

        // Assert
        result.Should().Be(fallback);
    }

    [Theory]
    [MemberData(nameof(TimeSpan_HasValue_Cases))]
    public void OrDefault_TimeSpan_Should_ReturnValue_When_ValueHasValue(TimeSpan value, TimeSpan defaultValue)
    {
        // Arrange
        TimeSpan? input = value;

        // Act
        var result = input.OrDefault(defaultValue);

        // Assert
        result.Should().Be(value);
    }

    public static IEnumerable<object[]> TimeSpan_HasValue_Cases()
    {
        yield return new object[]
        {
            TimeSpan.Zero,
            TimeSpan.FromDays(1)
        };

        yield return new object[]
        {
            TimeSpan.FromMilliseconds(1),
            TimeSpan.FromMilliseconds(2)
        };

        yield return new object[]
        {
            TimeSpan.MaxValue,
            TimeSpan.MinValue
        };
    }
}

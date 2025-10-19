namespace Allyaria.Theming.UnitTests.Services;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ThemeWatcherTests
{
    [Fact]
    public void Ctor_Should_DefaultToSystem_When_NoInitialProvided()
    {
        // Arrange + Act
        var sut = new ThemeWatcher();

        // Assert
        sut.ThemeType.Should().Be(ThemeType.System);
    }

    [Theory]
    [InlineData(ThemeType.Light)]
    [InlineData(ThemeType.Dark)]
    [InlineData(ThemeType.HighContrast)]
    [InlineData(ThemeType.System)]
    public void Ctor_Should_SetInitialCurrent_When_InitialProvided(ThemeType initial)
    {
        // Arrange + Act
        var sut = new ThemeWatcher(initial);

        // Assert
        sut.ThemeType.Should().Be(initial);
    }

    [Fact]
    public void Current_Should_ReturnUpdatedValue_When_SetCurrentWasCalled()
    {
        // Arrange
        var sut = new ThemeWatcher();

        // Act
        var changed = sut.SetCurrent(ThemeType.Dark);

        // Assert
        changed.Should().BeTrue();
        sut.ThemeType.Should().Be(ThemeType.Dark);
    }

    [Fact]
    public async Task DetectAsync_Should_IgnoreCancellationToken_When_Cancelled()
    {
        // Arrange
        var sut = new ThemeWatcher(ThemeType.HighContrast);

        // Act
        async Task<ThemeType> Act()
        {
            using var cts = new CancellationTokenSource();
            await cts.CancelAsync();

            return await sut.DetectAsync(cts.Token);
        }

        // Assert
        (await Act()).Should().Be(ThemeType.HighContrast);
    }

    [Fact]
    public async Task DetectAsync_Should_ReturnCurrent_When_Called()
    {
        // Arrange
        var sut = new ThemeWatcher(ThemeType.Light);

        // Act
        var detected = await sut.DetectAsync();

        // Assert
        detected.Should().Be(ThemeType.Light);
    }

    [Fact]
    public async Task DisposeAsync_Should_NotThrow_When_Called()
    {
        // Arrange
        var sut = new ThemeWatcher();

        // Act
        var act = async () => await sut.DisposeAsync();

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task SetCurrent_Should_BeIdempotentUnderConcurrency_When_SettingSameValue()
    {
        // Arrange
        var initial = ThemeType.Dark;
        var sut = new ThemeWatcher(initial);
        var events = 0;
        sut.Changed += (_, _) => Interlocked.Increment(ref events);

        var tasks = Enumerable.Range(0, Environment.ProcessorCount * 3)
            .Select(_ => Task.Run(() => sut.SetCurrent(initial)))
            .ToArray();

        // Act
        await Task.WhenAll(tasks);

        // Assert
        events.Should().Be(0);
        tasks.Select(t => t.Result).Should().OnlyContain(b => b == false);
        sut.ThemeType.Should().Be(initial);
    }

    [Fact]
    public void SetCurrent_Should_NotifyAllSubscribersOnce_When_ValueChanges()
    {
        // Arrange
        var sut = new ThemeWatcher();
        var a = 0;
        var b = 0;
        sut.Changed += (_, _) => Interlocked.Increment(ref a);
        sut.Changed += (_, _) => Interlocked.Increment(ref b);

        // Act
        var result = sut.SetCurrent(ThemeType.HighContrast);

        // Assert
        result.Should().BeTrue();
        a.Should().Be(1);
        b.Should().Be(1);
    }

    [Fact]
    public void SetCurrent_Should_ReturnFalseAndNotRaiseChanged_When_ValueUnchanged()
    {
        // Arrange
        var sut = new ThemeWatcher(ThemeType.Light);
        var calls = 0;
        sut.Changed += (_, _) => Interlocked.Increment(ref calls);

        // Act
        var result = sut.SetCurrent(ThemeType.Light);

        // Assert
        result.Should().BeFalse();
        sut.ThemeType.Should().Be(ThemeType.Light);
        calls.Should().Be(0);
    }

    [Fact]
    public void SetCurrent_Should_ReturnTrueAndRaiseChanged_When_ValueChanges()
    {
        // Arrange
        var sut = new ThemeWatcher();
        var calls = 0;
        object? senderObserved = null;
        EventArgs? argsObserved = null;

        sut.Changed += (sender, args) =>
        {
            Interlocked.Increment(ref calls);
            senderObserved = sender;
            argsObserved = args;
        };

        // Act
        var result = sut.SetCurrent(ThemeType.Dark);

        // Assert
        result.Should().BeTrue();
        sut.ThemeType.Should().Be(ThemeType.Dark);
        calls.Should().Be(1);
        senderObserved.Should().BeSameAs(sut);
        argsObserved.Should().Be(EventArgs.Empty);
    }

    [Fact]
    public async Task StartAsync_And_StopAsync_Should_IgnoreCancellationToken()
    {
        // Arrange
        var sut = new ThemeWatcher();
        CancellationToken token;

        using (var cts = new CancellationTokenSource())
        {
            await cts.CancelAsync();
            token = cts.Token;
        }

        // Act
        var startAct = async () => await sut.StartAsync(token);
        var stopAct = async () => await sut.StopAsync(token);

        // Assert
        await startAct.Should().NotThrowAsync();
        await stopAct.Should().NotThrowAsync();
    }

    [Fact]
    public async Task StartAsync_Should_NotThrow_And_NotAffectBehavior()
    {
        // Arrange
        var sut = new ThemeWatcher();

        // Act
        var act = async () => await sut.StartAsync();

        // Assert
        await act.Should().NotThrowAsync();

        // And: basic behavior still works
        sut.SetCurrent(ThemeType.Light).Should().BeTrue();
        sut.ThemeType.Should().Be(ThemeType.Light);
    }

    [Fact]
    public async Task StopAsync_Should_NotThrow_And_NotAffectBehavior()
    {
        // Arrange
        var sut = new ThemeWatcher(ThemeType.Dark);

        // Act
        var act = async () => await sut.StopAsync();

        // Assert
        await act.Should().NotThrowAsync();

        // And: basic behavior still works
        sut.SetCurrent(ThemeType.System).Should().BeTrue();
        sut.ThemeType.Should().Be(ThemeType.System);
    }
}

using FluentAssertions;
using FootballEvents.Application.Services;

namespace FootballEvents.Application.IntegrationTests.Services;

public class MatchLockServiceTests
{
    [Fact]
    public async Task LockAsync_ShouldEnsureStrictSequentialExecutionUnderHighConcurrency()
    {
        // Given
        var lockService = new MatchLockService();
        var executionCounter = 0;
        var maxSimultaneous = 0;

        // When: Execute 20 tasks completely concurrently
        var tasks = Enumerable.Range(0, 20).Select(async _ =>
        {
            using (await lockService.LockAsync())
            {
                var current = Interlocked.Increment(ref executionCounter);

                // Track peak number of concurrent entries into the critical section
                lock (lockService)
                {
                    if (current > maxSimultaneous)
                        maxSimultaneous = current;
                }

                await Task.Delay(5); // Simulate read/write operation
                Interlocked.Decrement(ref executionCounter);
            }
        });

        await Task.WhenAll(tasks);

        // Then: Exactly one thread could be inside the critical section at any given time
        maxSimultaneous.Should().Be(1);
    }
}
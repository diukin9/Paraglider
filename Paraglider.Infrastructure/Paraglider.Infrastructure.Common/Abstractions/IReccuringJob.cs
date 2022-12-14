using Hangfire;

namespace Paraglider.Infrastructure.Common.Abstractions;

public interface IReccuringJob
{
    [DisableConcurrentExecution(10)]
    [AutomaticRetry(Attempts = 0, LogEvents = false, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    public Task RunAsync();
}

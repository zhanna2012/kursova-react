using Microsoft.EntityFrameworkCore;
using UniversityAPI.Database;

namespace UniversityAPI;

public class TokenCleanerBackgroundService : IHostedService, IDisposable
{
    private readonly ILogger<TokenCleanerBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IConfiguration _configuration;
    private Timer? _timer;

    public TokenCleanerBackgroundService(ILogger<TokenCleanerBackgroundService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _configuration = configuration;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Token cleaner service running.");

        var period = new TimeSpan(
            _configuration.GetValue<int>("Services:TokenCleaner:Period:Days"),
            _configuration.GetValue<int>("Services:TokenCleaner:Period:Hours"),
            _configuration.GetValue<int>("Services:TokenCleaner:Period:Minutes"),
            _configuration.GetValue<int>("Services:TokenCleaner:Period:Seconds"),
            _configuration.GetValue<int>("Services:TokenCleaner:Period:Milliseconds")
        );

        _timer = new Timer(CleanRevokedTokens, null, TimeSpan.Zero, period);
        return Task.CompletedTask;
    }

    private void CleanRevokedTokens(object? state)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IUniversityContext>();
        var removedTokensCount = context.Database.ExecuteSql($"EXECUTE SP_RemoveOldTokens");

        if (removedTokensCount == -1)
        {
            _logger.LogInformation("Token cleaner service is working. No revoked tokens has been found");
            return;
        }

        _logger.LogInformation(
            "Token cleaner service is working. Removed revoked tokens count: {RemovedTokensCount}", removedTokensCount);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Token cleaner service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose() => _timer?.Dispose();
}
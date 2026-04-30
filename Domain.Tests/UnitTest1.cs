using Domain.Models;
using Domain.Services;
using Infrastructure.Persistence;

namespace Domain.Tests;

public class HighScoreServiceTests
{
    private HighScoreService CreateService(out InMemoryPersist<HighScore> repo)
    {
        repo = new InMemoryPersist<HighScore>();
        return new HighScoreService(repo);
    }

    [Fact]
    public void AddHighScore_6Scores_OnlyKeepsTop5()
    {
        var service = CreateService(out var repo);
        var gameId = Guid.NewGuid();

        for (int i = 1; i <= 6; i++)
        {
            service.AddHighScore(new HighScore { Score = i * 10, GameId = gameId });
        }

        Assert.Equal(5, repo.GetAll().Count());
    }

    [Fact]
    public void AddHighScore_6Scores_LowestScoreIsRemoved()
    {
        var service = CreateService(out var repo);
        var gameId = Guid.NewGuid();

        for (int i = 1; i <= 6; i++)
        {
            service.AddHighScore(new HighScore { Score = i * 10, GameId = gameId });
        }

        Assert.DoesNotContain(repo.GetAll(), hs => hs.Score == 10);
    }

    [Fact]
    public void AddHighScore_NewScoreTooLow_IsNotSaved()
    {
        var service = CreateService(out var repo);
        var gameId = Guid.NewGuid();

        for (int i = 2; i <= 6; i++)
        {
            service.AddHighScore(new HighScore { Score = i * 10, GameId = gameId });
        }

        service.AddHighScore(new HighScore { Score = 5, GameId = gameId });

        Assert.Equal(5, repo.GetAll().Count());
        Assert.DoesNotContain(repo.GetAll(), hs => hs.Score == 5);
    }
}

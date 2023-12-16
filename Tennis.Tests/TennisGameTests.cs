﻿using Microsoft.Extensions.Logging;
using Moq;
using System.Collections;
using Tennis.Game;

namespace Tennis.Tests;
public class TestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] {0, 0, "Love-All"},
        new object[] {1, 1, "Fifteen-All"},
        new object[] {2, 2, "Thirty-All"},
        new object[] {3, 3, "Deuce"},
        new object[] {4, 4, "Deuce"},
        new object[] {1, 0, "Fifteen-Love"},
        new object[] {0, 1, "Love-Fifteen"},
        new object[] {2, 0, "Thirty-Love"},
        new object[] {0, 2, "Love-Thirty"},
        new object[] {3, 0, "Forty-Love"},
        new object[] {0, 3, "Love-Forty"},
        new object[] {4, 0, "Win for player1"},
        new object[] {0, 4, "Win for player2"},
        new object[] {2, 1, "Thirty-Fifteen"},
        new object[] {1, 2, "Fifteen-Thirty"},
        new object[] {3, 1, "Forty-Fifteen"},
        new object[] {1, 3, "Fifteen-Forty"},
        new object[] {4, 1, "Win for player1"},
        new object[] {1, 4, "Win for player2"},
        new object[] {3, 2, "Forty-Thirty"},
        new object[] {2, 3, "Thirty-Forty"},
        new object[] {4, 2, "Win for player1"},
        new object[] {2, 4, "Win for player2"},
        new object[] {4, 3, "Advantage player1"},
        new object[] {3, 4, "Advantage player2"},
        new object[] {5, 4, "Advantage player1"},
        new object[] {4, 5, "Advantage player2"},
        new object[] {15, 14, "Advantage player1"},
        new object[] {14, 15, "Advantage player2"},
        new object[] {6, 4, "Win for player1"},
        new object[] {4, 6, "Win for player2"},
        new object[] {16, 14, "Win for player1"},
        new object[] {14, 16, "Win for player2"},
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


[TestFixture]
public class TennisGameTests
{
    private void CheckAllScores(ITennisGame game, int player1Score, int player2Score, string expectedScore)
    {
        var highestScore = Math.Max(player1Score, player2Score);
        for (var i = 0; i < highestScore; i++)
        {
            if (i < player1Score)
                game.WonPoint("player1");
            if (i < player2Score)
                game.WonPoint("player2");
        }

        Assert.AreEqual(expectedScore, game.GetScore());
    }

    
    [TestCaseSource(typeof(TestDataGenerator))]
    public void Tennis1Test(int p1, int p2, string expected)
    {
        // added a mock for the logger 
        var loggerMock = new Mock<ILogger<TennisGame>>();

        var game = new TennisGame("player1", "player2", loggerMock.Object);
        CheckAllScores(game, p1, p2, expected);
    }

}
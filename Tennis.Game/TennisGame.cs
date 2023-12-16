using Microsoft.Extensions.Logging;

namespace Tennis.Game
{
    public class TennisGame : ITennisGame
    {
        private int m_score1 = 0;
        private int m_score2 = 0;

        // player names should be initiated once 
        private readonly string _player1Name;
        private readonly string _player2Name;

        // adding logging
        private readonly ILogger<TennisGame> _logger;

        public TennisGame(string player1Name, string player2Name, ILogger<TennisGame> logger)
        {
            // add dependency
            _logger = logger;

            // validate input
            if (string.IsNullOrWhiteSpace(player1Name) && string.IsNullOrWhiteSpace(player2Name))
            {
                _logger.LogInformation("User used wrong names for player1={p1layer} and player2={player2}", player1Name, player2Name);
                throw new ArgumentException("Player name(s) need to be valid strings");
            }

            _player1Name = player1Name;
            _player2Name = player2Name;
        }

        public void WonPoint(string playerName)
        {
            // input validation
            if (playerName != _player1Name && playerName != _player2Name)
            {
                throw new ArgumentException("Player name provided does not match names of Player1 or Player2.");
            }

            // previous if statement assumed that player 1 would always be called player1
            if (playerName == _player1Name)
            {
                m_score1 += 1;
            }
            else
            {
                m_score2 += 1;
            }
        }


        internal bool IsTied()
        {
            if (m_score1 == m_score2)
            { 
                return true; 
            }
            else
            {
                return false;
            }
        }
        internal string ScoreWhenTied()
        {
            if (m_score1 != m_score2)
            {
                throw new Exception("The game is not tied.");
            }

            switch (m_score1)
            {
                case 0:
                    return "Love-All";
                case 1:
                    return "Fifteen-All";
                case 2:
                    return "Thirty-All";
                default:
                    return "Deuce";
            }

        }

        internal bool IsAdvantage()
        {
            if (m_score1 >= 4 || m_score2 >= 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal string ScoreWhenAdvantage()
        {
            // input validation 
            if(m_score1 == m_score2)
            {
                throw new Exception("Players are tied.");
            }

            var scoreDifference = m_score1 - m_score2;

            switch (scoreDifference)
            {
                case -1:
                    return $"Advantage {_player2Name}";
                case 1:
                    return $"Advantage {_player1Name}";
                case > 1:
                    return $"Win for {_player1Name}";
                default:
                    return $"Win for {_player2Name}";
            }
        }

        internal string ScoreNormal()
        {
            // validation
            if (IsTied() || IsAdvantage())
            {
                throw new Exception("Not in normal stage.");
            }

            var player1ScoreString = ScoreToTennisName(m_score1);
            var player2ScoreString = ScoreToTennisName(m_score2);

            var outputString = $"{player1ScoreString}-{player2ScoreString}";

            return outputString;
        }
        internal string ScoreToTennisName(int score)
        {
            // input validation
            if( score <= 0 || score >= 3)
            {
                throw new ArgumentException("Score needs to be between 0 and 3");
            }

            switch(score)
            {
                case 0:
                    return "Love";
                case 1:
                    return "Fifteen";
                case 2:
                    return "Thirty";
                case 3:
                    return "Forty";
                default:
                    throw new Exception("Unexpected exeption");
            }
        }

        public string GetScore()
        {
            // check if tied
            if (IsTied())
            {
                // if tied get the special tennis names
                return ScoreWhenTied();
            }

            // if in advantage stage
            if (IsAdvantage())
            {
                return ScoreWhenAdvantage();
            }

            // if in neither stage
            return ScoreNormal();
        }
    }
}

namespace Tennis.Game;

public interface ITennisGame
{
    void WonPoint(string playerName);
    string GetScore();
}
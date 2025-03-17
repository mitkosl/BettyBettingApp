namespace BettyBettingApp;

public interface IRandomProvider
{
    int Next(int minValue, int maxValue);
}
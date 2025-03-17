namespace BettyBettingApp;

public class RandomProvider : IRandomProvider
{
    private readonly Random random;
    public RandomProvider()
    {
        random = new Random();
    }

    public int Next(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }
}
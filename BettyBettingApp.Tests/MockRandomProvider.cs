namespace BettyBettingApp.Tests;
public class MockRandomProvider : IRandomProvider
{
    private readonly Queue<int> values;
    public MockRandomProvider(IEnumerable<int> values)
    {
        this.values = new Queue<int>(values);
    }

    public int Next(int minValue, int maxValue)
    {
        return values.Dequeue();
    }
}
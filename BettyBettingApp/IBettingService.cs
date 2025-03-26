namespace BettyBettingApp;

public interface IBettingService
{
    decimal PlaceBet(decimal betAmount, out string resultMessage);
}
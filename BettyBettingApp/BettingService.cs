namespace BettyBettingApp;

public class BettingService : IBettingService
{
    private readonly IWallet wallet;
    private readonly IRandomProvider randomProvider;
    public BettingService(IWallet wallet, IRandomProvider randomProvider)
    {
        this.wallet = wallet;
        this.randomProvider = randomProvider;
    }
    public decimal PlaceBet(decimal betAmount, out string resultMessage)
    {
        if (betAmount < 1 || betAmount > 10)
        {
            resultMessage = "Bet must be between $1 and $10.";
            return 0;
        }
        if (betAmount > wallet.Balance)
        {
            resultMessage = "Insufficient balance to place this bet.";
            return 0;
        }
        int outcome = randomProvider.Next(1, 101); // Use the injected random provider
        decimal winAmount = 0;
        if (outcome <= 50)
        {
            // 50% of the bets lose
            wallet.Withdraw(betAmount, out _);
            resultMessage = $"No luck this time! {wallet.GetBalanceMessage()}";
        }
        else if (outcome > 50 && outcome <= 90)
        {
            // 40% of the bets win up to x2 the bet amount
            winAmount = betAmount * 2;
            wallet.Deposit(winAmount - betAmount, out _);
            resultMessage = $"Congrats - you won ${winAmount:f2}. {wallet.GetBalanceMessage()}";
        }
        else
        {
            // 10% of the bets win between x2 and x10 the bet amount
            winAmount = betAmount * randomProvider.Next(2, 11);
            wallet.Deposit(winAmount - betAmount, out _);
            resultMessage = $"Jackpot - you won ${winAmount:f2}. {wallet.GetBalanceMessage()}";
        }
        return winAmount;
    }
}
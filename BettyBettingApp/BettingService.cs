namespace BettyBettingApp;

public class BettingService : IBettingService
{
    private readonly IWallet wallet;
    private readonly IMessageHandler messageHandler;
    private readonly IRandomProvider randomProvider;
    public BettingService(IMessageHandler messageHandler, IWallet wallet, IRandomProvider randomProvider)
    {
        this.wallet = wallet;
        this.messageHandler = messageHandler;
        this.randomProvider = randomProvider;
    }
    public decimal PlaceBet(decimal betAmount)
    {
        if (betAmount < 1 || betAmount > 10)
        {
            messageHandler.Write("Bet must be between $1 and $10.");
            return 0;
        }
        if (betAmount > wallet.Balance)
        {
            messageHandler.Write("Insufficient balance to place this bet.");
            return 0;
        }
        int outcome = randomProvider.Next(1, 101); // Use the injected random provider
        decimal winAmount = 0;
        if (outcome <= 50)
        {
            // 50% of the bets lose
            wallet.Withdraw(betAmount);
            messageHandler.Write($"No luck this time! {wallet.GetBalanceMessage()}");
        }
        else if (outcome > 50 && outcome <= 90)
        {
            // 40% of the bets win up to x2 the bet amount
            winAmount = betAmount * 2;
            wallet.Deposit(winAmount - betAmount);
            messageHandler.Write($"Congrats - you won ${winAmount:f2}. {wallet.GetBalanceMessage()}");
        }
        else
        {
            // 10% of the bets win between x2 and x10 the bet amount
            winAmount = betAmount * randomProvider.Next(2, 11);
            wallet.Deposit(winAmount - betAmount);
            messageHandler.Write($"Jackpot - you won ${winAmount:f2}. {wallet.GetBalanceMessage()}");
        }
        return winAmount;
    }
}
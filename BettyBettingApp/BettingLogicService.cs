namespace BettyBettingApp;

public class BettingLogicService
{
    private readonly IWallet wallet;
    private readonly IBettingService bettingService;
    public BettingLogicService(IWallet wallet, IBettingService bettingService)
    {
        this.wallet = wallet;
        this.bettingService = bettingService;
    }

    public string Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            return "Deposit amount must be positive.";
        }
        wallet.Deposit(amount, out var message);
        return message;
    }

    public string Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            return "Withdrawal amount must be positive.";
        }
        wallet.Withdraw(amount, out var message);
        return message;
    }

    public string PlaceBet(decimal betAmount)
    {
        if (betAmount < 1 || betAmount > 10)
        {
            return "Bet must be between $1 and $10.";
        }
        bettingService.PlaceBet(betAmount, out var message);
        return message;
    }
    public string GetBalance()
    {
        return wallet.GetBalanceMessage();
    }
}
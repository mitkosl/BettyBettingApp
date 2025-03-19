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
        wallet.Deposit(amount, true);
        return $"Deposited ${amount:f2}. {wallet.GetBalanceMessage()}";
    }

    public string Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            return "Withdrawal amount must be positive.";
        }
        wallet.Withdraw(amount, true);
        return $"Withdrew ${amount:f2}. {wallet.GetBalanceMessage()}";
    }

    public string PlaceBet(decimal betAmount)
    {
        if (betAmount < 1 || betAmount > 10)
        {
            return "Bet must be between $1 and $10.";
        }
        var winAmount = bettingService.PlaceBet(betAmount);
        return $"Bet placed: ${betAmount:f2}. Win amount: ${winAmount:f2}. {wallet.GetBalanceMessage()}";
    }
    public string GetBalance()
    {
        return $"Your current balance is: ${wallet.Balance:f2}";
    }
}
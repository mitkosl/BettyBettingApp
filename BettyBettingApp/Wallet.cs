namespace BettyBettingApp;

public class Wallet : IWallet
{
    public decimal Balance { get; private set; }

    public Wallet() => Balance = 0;
    
    public void Deposit(decimal amount, out string resultMessage)
    {
        if (amount > 0)
        {
            Balance += amount;
            resultMessage = $"Your deposit of ${amount:f2} was successful. {GetBalanceMessage()}";
        }
        else
        {
            resultMessage = "Aded amount must be positive.";
        }
    }
    public void Withdraw(decimal amount, out string resultMessage)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            resultMessage = $"Your withdrawal of ${amount:f2} was successful. {GetBalanceMessage()}";
        }
        else
        {
            resultMessage = "Invalid withdrawal amount.";
        }
    }

    public string GetBalanceMessage()
    {
        return $"Your current balance is: ${Balance:f2}";
    }
}
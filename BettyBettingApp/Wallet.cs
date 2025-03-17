namespace BettyBettingApp;

public class Wallet : IWallet
{
    public decimal Balance { get; private set; }
    private readonly IMessageHandler messageHandler;

    public Wallet(IMessageHandler messageHandler)
    {
        Balance = 0;
        this.messageHandler = messageHandler;
    }
    public void Deposit(decimal amount, bool printOutmessage = false)
    {
        if (amount > 0)
        {
            Balance += amount;
            if (printOutmessage)
            {
                messageHandler.Write($"Your deposit of ${amount:f2} was successful. {GetBalanceMessage()}");
            }
        }
        else
        {
            messageHandler.Write("Aded amount must be positive.");
        }
    }
    public void Withdraw(decimal amount, bool printOutmessage = false)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            if (printOutmessage)
            {
                messageHandler.Write($"Your withdrawal of ${amount:f2} was successful. {GetBalanceMessage()}");
            }
        }
        else
        {
            messageHandler.Write("Invalid withdrawal amount.");
        }
    }

    public string GetBalanceMessage()
    {
        return $"Your current balance is: ${Balance:f2}";
    }
}
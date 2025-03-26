namespace BettyBettingApp;

public interface IWallet
{
    decimal Balance { get; }
    void Deposit(decimal amount, out string resultMessage);
    void Withdraw(decimal amount, out string resultMessage);
    string GetBalanceMessage();
}

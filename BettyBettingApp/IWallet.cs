namespace BettyBettingApp;
public interface IWallet
{
    decimal Balance { get; }
    void Deposit(decimal amount, bool printOutmessage = false);
    void Withdraw(decimal amount, bool printOutmessage = false);
    string GetBalanceMessage();
}

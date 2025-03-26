namespace BettyBettingApp.Tests;

[TestFixture]
public class WalletTests
{
    private Wallet wallet;
    [SetUp]
    public void SetUp()
    {
        wallet = new Wallet();
    }
    [Test]
    public void Deposit_ValidAmount_IncreasesBalance()
    {
        wallet.Deposit(50, out var message);
        Assert.That(wallet.Balance, Is.EqualTo(50));
    }
    [Test]
    public void Deposit_NegativeAmount_DoesNotChangeBalance()
    {
        wallet.Deposit(-10, out var message);
        Assert.That(wallet.Balance, Is.EqualTo(0));
    }
    [Test]
    public void Withdraw_ValidAmount_DecreasesBalance()
    {
        wallet.Deposit(100, out var depositMessage);
        wallet.Withdraw(50, out var withdrawMessage);
        Assert.That(wallet.Balance, Is.EqualTo(50));
    }
    [Test]
    public void Withdraw_InsufficientFunds_DoesNotChangeBalance()
    {
        wallet.Deposit(20, out var depositMessage);
        wallet.Withdraw(50, out var withdrawMessage);
        Assert.That(wallet.Balance, Is.EqualTo(20));
    }
    [Test]
    public void GetBalanceMessage_ReturnsCorrectMessage()
    {
        wallet.Deposit(100, out var resultMessage);
        var message = wallet.GetBalanceMessage();
        Assert.That(message, Is.EqualTo("Your current balance is: $100.00"));
    }
}
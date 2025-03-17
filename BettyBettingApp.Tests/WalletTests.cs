using NSubstitute;
namespace BettyBettingApp.Tests;

[TestFixture]
public class WalletTests
{
    private IMessageHandler messageHandlerMock;
    private Wallet wallet;
    [SetUp]
    public void SetUp()
    {
        messageHandlerMock = Substitute.For<IMessageHandler>();
        wallet = new Wallet(messageHandlerMock);
    }
    [Test]
    public void Deposit_ValidAmount_IncreasesBalance()
    {
        wallet.Deposit(50);
        Assert.That(wallet.Balance, Is.EqualTo(50));
    }
    [Test]
    public void Deposit_NegativeAmount_DoesNotChangeBalance()
    {
        wallet.Deposit(-10);
        Assert.That(wallet.Balance, Is.EqualTo(0));
    }
    [Test]
    public void Withdraw_ValidAmount_DecreasesBalance()
    {
        wallet.Deposit(100);
        wallet.Withdraw(50);
        Assert.That(wallet.Balance, Is.EqualTo(50));
    }
    [Test]
    public void Withdraw_InsufficientFunds_DoesNotChangeBalance()
    {
        wallet.Deposit(20);
        wallet.Withdraw(50);
        Assert.That(wallet.Balance, Is.EqualTo(20));
    }
    [Test]
    public void GetBalanceMessage_ReturnsCorrectMessage()
    {
        wallet.Deposit(100);
        var message = wallet.GetBalanceMessage();
        Assert.That(message, Is.EqualTo("Your current balance is: $100.00"));
    }
}
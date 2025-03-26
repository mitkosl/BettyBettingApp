using NSubstitute;

namespace BettyBettingApp.Tests;

[TestFixture]
public class BettingLogicServiceTests
{
    private IWallet walletMock;
    private IBettingService bettingServiceMock;
    private BettingLogicService bettingLogicService;
    [SetUp]
    public void SetUp()
    {
        walletMock = Substitute.For<IWallet>();
        bettingServiceMock = Substitute.For<IBettingService>();
        bettingLogicService = new BettingLogicService(walletMock, bettingServiceMock);
    }

    [Test]
    public void Deposit_ValidAmount_ReturnsSuccessMessage()
    {
        decimal amount = 50;
        walletMock.Balance.Returns(0);
        walletMock.When(w => w.Deposit(amount, out Arg.Any<string>()))
            .Do(callInfo =>
            {
                callInfo[1] = "Deposited $50.00. ";
            });

        var result = bettingLogicService.Deposit(amount);

        Assert.That(result, Is.EqualTo("Deposited $50.00. "));
    }

    [Test]
    public void Deposit_NegativeAmount_ReturnsErrorMessage()
    {
        decimal amount = -10;

        var result = bettingLogicService.Deposit(amount);

        Assert.That(result, Is.EqualTo("Deposit amount must be positive."));
    }

    [Test]
    public void Withdraw_ValidAmount_ReturnsWithdrewMessage()
    {
        decimal amount = 30;
        walletMock.Balance.Returns(100);
        walletMock.When(w => w.Withdraw(amount, out Arg.Any<string>()))
            .Do(callInfo =>
            {
                callInfo[1] = "Withdrew $30.00. ";
            });

        var result = bettingLogicService.Withdraw(amount);

        Assert.That(result, Is.EqualTo($"Withdrew $30.00. "));
    }

    [Test]
    public void Withdraw_NegativeAmount_ReturnsErrorMessage()
    {
        decimal amount = -20;

        var result = bettingLogicService.Withdraw(amount);

        Assert.That(result, Is.EqualTo("Withdrawal amount must be positive."));
    }

    [Test]
    public void PlaceBet_ValidBet_ReturnsSuccessMessage()
    {
        decimal betAmount = 5;
        walletMock.Balance.Returns(100);
        bettingServiceMock.PlaceBet(betAmount, out var message).Returns(10);

        var result = bettingLogicService.PlaceBet(betAmount);

        Assert.That(result, Is.EqualTo(message));
    }

    [Test]
    public void PlaceBet_InvalidBetAmount_ReturnsErrorMessage()
    {
        decimal betAmount = 15;

        var result = bettingLogicService.PlaceBet(betAmount);

        Assert.That(result, Is.EqualTo("Bet must be between $1 and $10."));
    }

    [Test]
    public void GetBalance_ReturnsCurrentBalance()
    {
        walletMock.Balance.Returns(100);
        walletMock.GetBalanceMessage().Returns("Your current balance is: $100.00");

        var result = bettingLogicService.GetBalance();

        Assert.That(result, Is.EqualTo("Your current balance is: $100.00"));
    }
}
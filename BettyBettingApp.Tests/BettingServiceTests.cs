using NSubstitute;

namespace BettyBettingApp.Tests;

[TestFixture]
public class BettingServiceTests
{
    private IWallet walletMock;
    private IMessageHandler messageHandlerMock;
    private BettingService bettingService;
    [SetUp]
    public void SetUp()
    {
        walletMock = Substitute.For<IWallet>();
        messageHandlerMock = Substitute.For<IMessageHandler>();
    }

    [Test]
    public void PlaceBet_InsufficientFunds_ReturnsZero()
    {
        var randomProvider = new MockRandomProvider([25]);
        bettingService = new BettingService(messageHandlerMock, walletMock, randomProvider);
        
        walletMock.Balance.Returns(0);
        decimal betAmount = 5;
        
        var result = bettingService.PlaceBet(betAmount);
        
        Assert.That(result, Is.EqualTo(0));
        messageHandlerMock.Received(1).Write("Insufficient balance to place this bet.");
    }

    [Test]
    public void PlaceBet_InvalidBetAmount_ReturnsZero()
    {
        decimal betAmount = 15;
        var randomProvider = new MockRandomProvider([25]);
        bettingService = new BettingService(messageHandlerMock, walletMock, randomProvider);

        var result = bettingService.PlaceBet(betAmount);

        Assert.That(result, Is.EqualTo(0));
        messageHandlerMock.Received(1).Write("Bet must be between $1 and $10.");
    }

    [Test]
    public void PlaceBet_LoseScenario_WithdrawsBetAmount()
    {
        var randomProvider = new MockRandomProvider([25]);
        bettingService = new BettingService(messageHandlerMock, walletMock, randomProvider);
        walletMock.Balance.Returns(100);
        decimal betAmount = 10;

        var result = bettingService.PlaceBet(betAmount);

        Assert.That(result, Is.EqualTo(0));
        walletMock.Received(1).Withdraw(betAmount);
        messageHandlerMock.Received(1).Write(Arg.Is<string>(s => s.Contains("No luck this time!")));
    }

    [Test]
    public void PlaceBet_WinDoubleScenario_DepositDoubleBetAmount()
    {
        var randomProvider = new MockRandomProvider([70]);
        bettingService = new BettingService(messageHandlerMock, walletMock, randomProvider);
        walletMock.Balance.Returns(100);
        decimal betAmount = 10;

        var result = bettingService.PlaceBet(betAmount);

        Assert.That(result, Is.EqualTo(20));
        walletMock.Received(1).Deposit(20 - betAmount);
        messageHandlerMock.Received(1).Write(Arg.Is<string>(s => s.Contains("Congrats - you won")));
    }

    [Test]
    public void PlaceBet_JackpotScenario_DepositRandomMultiplier()
    {
        var randomProvider = new MockRandomProvider([95, 5]);
        bettingService = new BettingService(messageHandlerMock, walletMock, randomProvider);
        walletMock.Balance.Returns(100);
        decimal betAmount = 10;

        var result = bettingService.PlaceBet(betAmount);

        Assert.That(result, Is.EqualTo(50)); // Assuming a multiplier of 5
        walletMock.Received(1).Deposit(50 - betAmount);
        messageHandlerMock.Received(1).Write(Arg.Is<string>(s => s.Contains("Jackpot - you won")));
    }
    
    [Test]
    public void PlaceBet_InsufficientBalance_ReturnsZero()
    {
        var randomProvider = new MockRandomProvider([25]);
        bettingService = new BettingService(messageHandlerMock, walletMock, randomProvider);
        walletMock.Balance.Returns(5);
        decimal betAmount = 10;

        var result = bettingService.PlaceBet(betAmount);

        Assert.That(result, Is.EqualTo(0));
        messageHandlerMock.Received(1).Write("Insufficient balance to place this bet.");
    }
}
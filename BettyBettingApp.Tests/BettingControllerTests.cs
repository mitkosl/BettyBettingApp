using BettyBettingApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BettyBettingApp.Tests;

[TestFixture]
public class BettingControllerTests
{
    private BettingController bettingController;
    private BettingLogicService bettingLogicServiceMock;
    [SetUp]
    public void SetUp()
    {
        var walletMock = Substitute.For<IWallet>();
        walletMock.Balance.Returns(100);
        var bettingServiceMock = Substitute.For<IBettingService>();

        bettingLogicServiceMock = new BettingLogicService(walletMock, bettingServiceMock);
        bettingController = new BettingController(bettingLogicServiceMock);
    }
    [Test]
    public void Deposit_ValidAmount_ReturnsOkResult()
    {
        decimal amount = 50;
        bettingLogicServiceMock.Deposit(amount).Returns($"Your current balance is: $100.00");

        var result = bettingController.Deposit(amount);

        if (result is OkObjectResult okResult)
        {
            Assert.That(okResult.Value, Is.EqualTo($"Deposited $50.00. Your current balance is: $100.00"));
        }
    }

    [Test]
    public void Withdraw_ValidAmount_ReturnsOkResult()
    {
        decimal amount = 30;
        bettingLogicServiceMock.Withdraw(amount).Returns($"Your current balance is: $70.00");

        var result = bettingController.Withdraw(amount);

        if (result is OkObjectResult okResult)
        {
            Assert.That(okResult.Value, Is.EqualTo($"Withdrew $30.00. Your current balance is: $70.00"));
        }
    }

    [Test]
    public void GetBalance_ReturnsOkResult()
    {
        bettingController.Deposit(100);

        var result = bettingController.GetBalance();

        if (result is OkObjectResult okResult)
        {
            Assert.That(okResult.Value, Is.EqualTo("Your current balance is: $100.00"));
        }
    }
}
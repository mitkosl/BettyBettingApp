using BettyBettingApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BettyBettingApp.Tests;

[TestFixture]
public class BettingControllerTests
{
    private BettingController bettingController;
    private BettingLogicService bettingLogicServiceMock;
    private IWallet walletMock;
    private IBettingService bettingServiceMock;
    [SetUp]
    public void SetUp()
    {
        walletMock = Substitute.For<IWallet>();
        walletMock.Balance.Returns(100); // Mock initial balance
        bettingServiceMock = Substitute.For<IBettingService>();
        bettingLogicServiceMock = new BettingLogicService(walletMock, bettingServiceMock);
        bettingController = new BettingController(bettingLogicServiceMock);
    }
    [Test]
    public void Deposit_ValidAmount_ReturnsOkResult()
    {
        decimal amount = 50;
        string expectedMessage = $"Your deposit of ${amount:f2} was successful. Your current balance is: $150.00";

        walletMock.When(w => w.Deposit(amount, out Arg.Any<string>()))
            .Do(callInfo =>
            {
                callInfo[1] = expectedMessage;
            });
        var result = bettingController.Deposit(amount);
        if (result is OkObjectResult okResult)
        {
            Assert.That(okResult.Value, Is.EqualTo(expectedMessage));
        }
    }
    [Test]
    public void Withdraw_ValidAmount_ReturnsOkResult()
    {
        decimal amount = 30;
        string expectedMessage = $"Your withdrawal of ${amount:f2} was successful. Your current balance is: $70.00";

        walletMock.When(w => w.Withdraw(amount, out Arg.Any<string>()))
              .Do(callInfo =>
              {
                  callInfo[1] = expectedMessage;
              });
        var result = bettingController.Withdraw(amount);
        if (result is OkObjectResult okResult)
        {
            Assert.That(okResult.Value, Is.EqualTo(expectedMessage));
        }
    }
    [Test]
    public void GetBalance_ReturnsOkResult()
    {
        // Setting up the mock for the balance message
        walletMock.GetBalanceMessage().Returns("Your current balance is: $100.00");
        var result = bettingController.GetBalance();
        if (result is OkObjectResult okResult)
        {
            Assert.That(okResult.Value, Is.EqualTo("Your current balance is: $100.00"));
        }
    }
}
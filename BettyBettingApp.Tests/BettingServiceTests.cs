using NSubstitute;
namespace BettyBettingApp.Tests
{
    [TestFixture]
    public class BettingServiceTests
    {
        private IWallet _walletMock;
        private IMessageHandler _messageHandlerMock;
        private BettingService _bettingService;
        [SetUp]
        public void SetUp()
        {
            _walletMock = Substitute.For<IWallet>();
            _messageHandlerMock = Substitute.For<IMessageHandler>();
            _bettingService = new BettingService(_messageHandlerMock, _walletMock);
        }
        [Test]
        public void PlaceBet_InsufficientFunds_ReturnsZero()
        {
            _walletMock.Balance.Returns(0);
            decimal betAmount = 5;
            var result = _bettingService.PlaceBet(betAmount);
            Assert.That(result, Is.EqualTo(0));
            _messageHandlerMock.Received(1).Write("Insufficient balance to place this bet.");
        }
        [Test]
        public void PlaceBet_InvalidBetAmount_ReturnsZero()
        {
            decimal betAmount = 15; // Invalid bet amount
            var result = _bettingService.PlaceBet(betAmount);
            Assert.That(result, Is.EqualTo(0));
            _messageHandlerMock.Received(1).Write("Bet must be between $1 and $10.");
        }
    }
}
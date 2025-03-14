using NSubstitute;
namespace BettyBettingApp.Tests
{
    [TestFixture]
    public class WalletTests
    {
        private IMessageHandler _messageHandlerMock;
        private Wallet _wallet;
        [SetUp]
        public void SetUp()
        {
            _messageHandlerMock = Substitute.For<IMessageHandler>();
            _wallet = new Wallet(_messageHandlerMock);
        }
        [Test]
        public void Deposit_ValidAmount_IncreasesBalance()
        {
            _wallet.Deposit(50);
            Assert.That(_wallet.Balance, Is.EqualTo(50));
        }
        [Test]
        public void Deposit_NegativeAmount_DoesNotChangeBalance()
        {
            _wallet.Deposit(-10);
            Assert.That(_wallet.Balance, Is.EqualTo(0));
        }
        [Test]
        public void Withdraw_ValidAmount_DecreasesBalance()
        {
            _wallet.Deposit(100);
            _wallet.Withdraw(50);
            Assert.That(_wallet.Balance, Is.EqualTo(50));
        }
        [Test]
        public void Withdraw_InsufficientFunds_DoesNotChangeBalance()
        {
            _wallet.Deposit(20);
            _wallet.Withdraw(50);
            Assert.That(_wallet.Balance, Is.EqualTo(20));
        }
        [Test]
        public void GetBalanceMessage_ReturnsCorrectMessage()
        {
            _wallet.Deposit(100);
            var message = _wallet.GetBalanceMessage();
            Assert.That(message, Is.EqualTo("Your current balance is: $100.00"));
        }
    }
}
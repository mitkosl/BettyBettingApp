namespace BettyBettingApp;
public interface IMessageHandler
{
    void Write(string message);
    string Read();
}

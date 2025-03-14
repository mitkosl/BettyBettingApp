﻿namespace BettyBettingApp;

internal class MessageHandler : IMessageHandler
{
    public string Read() => Console.ReadLine();

    public void Write(string message) => Console.WriteLine(message);
}
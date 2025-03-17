using Microsoft.Extensions.DependencyInjection;

namespace BettyBettingApp;

internal static class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IWallet, Wallet>()
            .AddSingleton<IMessageHandler, MessageHandler>()
            .AddSingleton<IRandomProvider, RandomProvider>()
            .AddTransient<IBettingService, BettingService>()
            .BuildServiceProvider();
        var wallet = serviceProvider.GetService<IWallet>();
        var bettingService = serviceProvider.GetService<IBettingService>();
        var messageHandler = serviceProvider.GetService<IMessageHandler>();
        bool running = true;
        messageHandler!.Write("Welcome to Betty's betting game!");
        messageHandler.Write("Please submit an action (e.g., deposit 60, withdraw 4, bet 5, exit):");
        while (running)
        {
            messageHandler.Write("\nPlease, submit an action:");
            string input = messageHandler.Read();
            string[] parts = input.Split(' ');
            if (parts.Length < 1)
            {
                messageHandler.Write("Invalid command. Please try again.");
                continue;
            }
            string command = parts[0].ToLower();
            decimal amount = 0;
            if (parts.Length > 1 && !decimal.TryParse(parts[1], out amount))
            {
                messageHandler.Write("Invalid amount. Please enter a positive number.");
                continue;
            }
            switch (command)
            {
                case "deposit":
                    if (amount > 0)
                    {
                        wallet.Deposit(amount, true);
                    }
                    else
                    {
                        messageHandler.Write("Deposit amount must be positive.");
                    }
                    break;
                case "withdraw":
                    if (amount > 0)
                    {
                        wallet.Withdraw(amount, true);
                    }
                    else
                    {
                        messageHandler.Write("Withdrawal amount must be positive.");
                    }
                    break;
                case "bet":
                    if (amount >= 1 && amount <= 10)
                    {
                        bettingService!.PlaceBet(amount);
                    }
                    else
                    {
                        messageHandler.Write("Bet must be between $1 and $10.");
                    }
                    break;
                case "exit":
                    running = false;
                    messageHandler.Write("Thank you for playing Betty's betting game! Goodbye!");
                    messageHandler.Write($"\nYour final balance is: ${wallet.Balance}");
                    break;
                default:
                    messageHandler.Write("Invalid command. Please select a valid option.");
                    break;
            }
        }
    }
}
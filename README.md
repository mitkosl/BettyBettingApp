# Betty's Betting App

## Overview
Betty's Betting App is a console application developed in .NET Core that simulates the operations of a player wallet, enhancing the gaming experience for users. The wallet serves as the core component of the application, allowing players to manage their funds effectively while engaging in a simple betting game.

## Features
The application includes the following features:

### 1. Player Balance
- Players start with a balance of **$0**.
- After each operation, the updated balance is displayed to the player.

### 2. Money Deposit
- Players can deposit funds into their wallet.

### 3. Money Withdrawal
- Players have the ability to withdraw funds from their wallet.

### 4. Placing Bets & Accepting Wins
- Players can engage in a simple game that simulates a slot game experience.

## Game Rules
The game operates under the following rules:

- **Betting Range**: Players can place bets ranging from **$1 to $10**.
- **Outcome Distribution**:
  - **50%** of the bets result in a loss.
  - **40%** of the bets result in a win up to **2x** the bet amount.
  - **10%** of the bets result in a win between **2x** and **10x** the bet amount.

### Balance Calculation
After each round of betting, the player’s balance is updated according to the following formula:
- **New Balance** = **Old Balance** - **Bet Amount** + **Win Amount**

### Game Termination
The game continues until the player decides to exit.

## Important Note
All operations that involve an amount must specify the amount as a positive number, regardless of whether the operation is a deposit or withdrawal.

## Conclusion
Betty's Betting App provides an engaging and interactive experience for players, allowing them to manage their funds and enjoy a simple betting game in a user-friendly console application.

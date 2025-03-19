using BettyBettingApp;

var builder = WebApplication.CreateBuilder(args);
// Register services from the BettyBettingApp project
builder.Services.AddSingleton<IWallet, Wallet>();
builder.Services.AddSingleton<IMessageHandler, MessageHandler>();
builder.Services.AddSingleton<IRandomProvider, RandomProvider>();
builder.Services.AddTransient<IBettingService, BettingService>();
builder.Services.AddTransient<BettingLogicService>(); // Register BettingLogicService
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSwagger();
app.UseStaticFiles();
app.UseRouting();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BettyBetting API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});
app.MapControllers();
app.Run();
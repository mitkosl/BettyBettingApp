using Microsoft.AspNetCore.Mvc;
using BettyBettingApp;

namespace BettyBettingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BettingController : ControllerBase
    {
        private readonly BettingLogicService bettingLogicService;
        public BettingController(BettingLogicService bettingLogicService)
        {
            this.bettingLogicService = bettingLogicService;
        }
        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] decimal amount)
        {
            var result = bettingLogicService.Deposit(amount);
            return Ok(result);
        }
        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] decimal amount)
        {
            var result = bettingLogicService.Withdraw(amount);
            return Ok(result);
        }
        [HttpPost("bet")]
        public IActionResult PlaceBet([FromBody] decimal betAmount)
        {
            var result = bettingLogicService.PlaceBet(betAmount);
            return Ok(result);
        }
        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            var result = bettingLogicService.GetBalance();
            return Ok(result);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MyFinancePal.Models;
using MyFinancePal.Services;

namespace MyFinancePal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentController(IInvestmentService investmentService)
        {
            _investmentService= investmentService;
        }

        [HttpGet]
        public async Task<List<Investment>> GetAllAsync(string userId) =>
            await _investmentService.GetAllAsync(userId);

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Investment>> Get(string id)
        {
            var transactions = await _investmentService.GetAsync(id);

            if (transactions is null)
            {
                return NotFound();
            }

            return transactions;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Investment newsTran)
        {
            await _investmentService.AddInvestment(newsTran);

            return Ok(await _investmentService.GetAllAsync(newsTran.UserId));
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Investment updateTran)
        {
            var investment = await _investmentService.GetAsync(id);

            if (investment is null)
            {
                return NotFound();
            }

            updateTran.Id = investment.Id;

            await _investmentService.UpdateInvestment(id, updateTran);

            return Ok(await _investmentService.GetAllAsync(updateTran.UserId));
        }
    }
}

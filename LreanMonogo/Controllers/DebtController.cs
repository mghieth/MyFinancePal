using Microsoft.AspNetCore.Mvc;
using MyFinancePal.Models;
using MyFinancePal.Services;

namespace MyFinancePal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtController : ControllerBase
    {
        private readonly IDebtService _debtService;

        public DebtController(IDebtService debtService)
        {
            _debtService= debtService;
        }

        [HttpGet]
        public async Task<List<Debt>> GetAllAsync(string userId) =>
            await _debtService.GetAllAsync(userId);

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Debt>> Get(string id)
        {
            var debt = await _debtService.GetAsync(id);

            if (debt is null)
            {
                return NotFound();
            }

            return debt;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Debt newDebt)
        {
            await _debtService.Create(newDebt);

            return Ok(await _debtService.GetAllAsync(newDebt.UserId));
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Debt updateDebt)
        {
            var debt = await _debtService.GetAsync(id);

            if (debt is null)
            {
                return NotFound();
            }

            updateDebt.Id = debt.Id;

            await _debtService.Update(id, updateDebt);

            return Ok(await _debtService.GetAllAsync(updateDebt.UserId));
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Remove(string id, string userId)
        {
            var debt = await _debtService.GetAsync(id);

            if (debt is null)
            {
                return NotFound();
            }

            await _debtService.Remove(id);

            return Ok(await _debtService.GetAllAsync(userId));
        }
    }
}

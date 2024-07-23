using Microsoft.AspNetCore.Mvc;
using MyFinancePal.Models;
using MyFinancePal.Services;

namespace MyFinancePal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavingGoalController : ControllerBase
    {
        private readonly ISavingGoalService _savingGoalService;

        public SavingGoalController(ISavingGoalService savingGoalService)
        {
            _savingGoalService= savingGoalService;
        }


        [HttpGet]
        public async Task<List<SavingGoal>> GetAllAsync(string userId) =>
            await _savingGoalService.GetAllAsync(userId);

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<SavingGoal>> Get(string id)
        {
            var transactions = await _savingGoalService.GetAsync(id);

            if (transactions is null)
            {
                return NotFound();
            }

            return transactions;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SavingGoal newSavingGoal)
        {
            await _savingGoalService.CreateGoal(newSavingGoal);

            return Ok(await _savingGoalService.GetAllAsync(newSavingGoal.UserId));
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, SavingGoal savingGoalUGoal)
        {
            var savingGoal = await _savingGoalService.GetAsync(id);

            if (savingGoal is null)
            {
                return NotFound();
            }

            savingGoalUGoal.Id = savingGoal.Id;

            await _savingGoalService.UpdateGoal(id, savingGoalUGoal);

            return Ok(await _savingGoalService.GetAllAsync(savingGoalUGoal.UserId));
        }
    }
}

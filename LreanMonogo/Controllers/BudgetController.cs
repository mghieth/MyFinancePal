﻿using Microsoft.AspNetCore.Mvc;
using MyFinancePal.Models;
using MyFinancePal.Services;

namespace MyFinancePal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetServiceService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetServiceService = budgetService;
        }

        [HttpGet]
        public async Task<List<Budget>> GetAllAsync(string userId) =>
            await _budgetServiceService.GetAllAsync(userId);

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Budget>> Get(string id)
        {
            var budget = await _budgetServiceService.GetAsync(id);

            if (budget is null)
            {
                return NotFound();
            }

            return budget;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Budget newBudget)
        {
            await _budgetServiceService.Create(newBudget);

            return Ok(await _budgetServiceService.GetAllAsync(newBudget.UserId));
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Budget updateBudget)
        {
            var budget = await _budgetServiceService.GetAsync(id);

            if (budget is null)
            {
                return NotFound();
            }

            updateBudget.Id = budget.Id;

            await _budgetServiceService.Update(id, updateBudget);

            return Ok(await _budgetServiceService.GetAllAsync(updateBudget.UserId));
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Remove(string id, string userId)
        {
            var budget = await _budgetServiceService.GetAsync(id);

            if (budget is null)
            {
                return NotFound();
            }

            await _budgetServiceService.Remove(id);

            return Ok(await _budgetServiceService.GetAllAsync(userId));
        }
    }
}

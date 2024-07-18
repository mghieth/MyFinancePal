﻿using LearnMongo.Models;
using LearnMongo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace LearnMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService=transactionService;
        }

        [HttpGet]
        public async Task<List<Transactions>> Get() =>
            await _transactionService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Transactions>> Get(string id)
        {
            var transactions = await _transactionService.GetAsync(id);

            if (transactions is null)
            {
                return NotFound();
            }

            return transactions;
        }


        [HttpPost]
        public async Task<IActionResult> Post(Transactions newsTran)
        {
            await _transactionService.AddTransaction(newsTran);

            return Ok(await _transactionService.GetAsync());
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Transactions updateTran)
        {
            var student = await _transactionService.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            updateTran.Id = student.Id;

            await _transactionService.EditTransaction(id, updateTran);

            return Ok(await _transactionService.GetAsync());
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Remove(string id)
        {
            var student = await _transactionService.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            await _transactionService.RemoveTransaction(id);

            return Ok(await _transactionService.GetAsync());
        }
    }
}

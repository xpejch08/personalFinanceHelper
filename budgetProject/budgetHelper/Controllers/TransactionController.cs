using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Dynamic;
using core;
using core.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    private Boolean mSuccessful;
    private string mKey;
    private budgetDto mBudgetDto;
    private budget mBudget;
    private transaction mTransaction;
    private amountDto mAmountDto;
    private transactionDto mTransactionDto;
    
    public TransactionController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }
    
    
    private transaction mapTransacttionDtoToTransaction()
    {
        return new transaction()
        { 
            Name = mTransactionDto.Name,
            Amount = mTransactionDto.Amount,
            Budget = mTransactionDto.Budget,
            DateCreated = mTransactionDto.DateCreated
        };
    }

    private transaction mapAmountDtoToTransaction()
    {
        return new transaction()
        {
            Amount = mAmountDto.Amount
        };
    }

   

    
    [HttpPost ("CreateTransaction")]
    public async Task<IActionResult> CreateTransaction([FromBody] transactionDto newTransaction)
    {
        mTransactionDto = newTransaction;
        mSuccessful = await _budgetService.CreateTransaction(mTransactionDto);


        if (mSuccessful)
        {
            return Ok(mTransactionDto);   
        }

        return NotFound();
    }
}


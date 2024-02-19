using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Dynamic;
using core;
using core.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    private Boolean mSuccessful;
    private string mKey;
    private budgetDto mBudgetDto;
    private budget mBudget;
    private transaction mTransaction;
    private amountDto mAmountDto;
    private transactionDto mTransactionDto;
    
    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    private budget ConstructResponseIntoNewBudget()
    {
        return new budget()
        { 
            Name = mBudgetDto.Name,
            Amount = mBudgetDto.Amount,
            Category = mBudgetDto.Category,
            DateCreated = mBudgetDto.DateCreated
        };
    }

    private transaction mapAmountDtoToTransaction()
    {
        return new transaction()
        {
            Amount = mAmountDto.Amount
        };
    }

    private IActionResult HandleSuccess()
    {
        var response = ConstructResponseIntoNewBudget();
        return Ok(response);
    }

    private IActionResult HandleError()
    {
        return BadRequest("Error: Could not create Budget");
    }

    private IActionResult returnAdequateResponse()
    {
        if (mSuccessful)
        {
            return HandleSuccess();
        }
        else
        {
            return HandleError();
        }
    }

    private void printBudgetToCommandLine()
    {
        Console.WriteLine(mBudget);
        Console.WriteLine("HELLO");
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] budgetDto newBudget)
    {
        mBudgetDto = newBudget;
        (mSuccessful, mKey) = await _budgetService.CreateBudgetAsync(mBudgetDto);

        return returnAdequateResponse();
    }
    
    [HttpGet ("{name}")]
    public async Task<IActionResult> GetBudget(string name)
    {
        (mSuccessful,mBudget) = await _budgetService.GetBudgetAsync(name);
        printBudgetToCommandLine();
        if (mSuccessful)
        {
            printBudgetToCommandLine();
            return Ok(mBudget);
        }
        return NotFound();
    }
    
    
    //todo clean code
    [HttpPost ("{name}/AddToBudget")]
    public async Task<IActionResult> AddToBudget(string name, [FromBody] amountDto amountDto)
    {
        (mSuccessful,mBudget) = await _budgetService.GetBudgetAsync(name);
        mAmountDto = amountDto;
        mTransaction = mapAmountDtoToTransaction();
        if (mSuccessful)
        {
            await _budgetService.UpdateBudgetAsync(mBudget, mTransaction);
            return Ok(mBudget);
        }
        return NotFound();
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


using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Dynamic;
using core;
using core.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

[ApiController]
[Route("[controller]")]
public class createBudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    private Boolean mSuccessful;
    private string mKey;
    private budgetDto mBudget;
    
    public createBudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    private budget ConstructResponseIntoNewBudget()
    {
        return new budget()
        { 
            Name = mBudget.Name,
            Amount = mBudget.Amount,
            Category = mBudget.Category,
            DateCreated = mBudget.DateCreated
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
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] budgetDto newBudget)
    {
        mBudget = newBudget;
        (mSuccessful, mKey) = await _budgetService.CreateBudgetAsync(mBudget);

        return returnAdequateResponse();
    }
    
    [HttpGet ("{name}")]
    public async Task<IActionResult> GetBudget(string name)
    {
        var budget = await _budgetService.GetBudgetAsync(name);
        Console.WriteLine(budget.budget.Name);
        Console.WriteLine("HELLO");
        if (budget.IsSuccess)
        {
            Console.WriteLine(budget.budget);
            return Ok(budget);
        }
        return NotFound();
    }
    
    [HttpPost ("{name}/AddToBudget")]
    public async Task<IActionResult> AddToBudget(string name, [FromBody] amountDto amountDto)
    {
        var budget = await _budgetService.GetBudgetAsync(name);
        if (budget.IsSuccess)
        {
            await _budgetService.UpdateBudgetAsync(budget.budget, amountDto);
            return Ok(budget.budget);
        }
        return NotFound();
    }

}


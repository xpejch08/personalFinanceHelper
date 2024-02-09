using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Dynamic;
using core;
using core.interfaces;

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

}


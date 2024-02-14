using core.interfaces;

namespace core.services;
using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using System.Dynamic;
using System.Linq;

public class BudgetService: IBudgetService
{
    private readonly FirebaseClient _firebaseClient;

    public BudgetService(FirebaseClient firebaseClient)
    {
        _firebaseClient = firebaseClient;
    }


    public async Task<(bool IsSuccess, string Key)> CreateBudgetAsync(budgetDto newBudget)
    {
        var result = await _firebaseClient
            .Child("budgets")
            .PostAsync(newBudget);

        return (result.Object != null, result.Key);
    }
    
    
    //todo clean code
    public async Task<(bool IsSuccess, budget budget)> GetBudgetAsync(string name)
    {
        var result = await _firebaseClient
            .Child("budgets")
            .OrderBy("Name")
            .EqualTo(name)
            .OnceAsync<budget>();

        if (result.Any())
        {
            var budget = result.First().Object;
            var key = result.First().Key;

            budget.Id = key;

            return (true, budget);
        }
        return (false, null);
    }

    //todo clean code
    public async Task<(bool IsSuccess, string result)> UpdateBudgetAsync(budget budget, amountDto amountToAdd)
    {
        var result = await _firebaseClient
            .Child("budgets")
            .Child(budget.Id)
            .OnceSingleAsync<budget>();

        if (result != null)
        {
            if (amountToAdd != null)
            {
                budget.updateAmount(amountToAdd.Amount);
            }
            
            await _firebaseClient
                .Child("budgets")
                .Child(budget.Id)
                .PutAsync(budget);

            return (true, "Updated Successfully");
        }
        else
        {
            return (false, "Error: Update gone wrong");
        }

    }
}
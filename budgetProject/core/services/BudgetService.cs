using System.Transactions;
using core.interfaces;
using Firebase.Database.Http;

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

    public async Task<bool> CreateTransaction(transactionDto newTransaction)
    {
        var getResult = await _firebaseClient
            .Child("budgets")
            .OrderBy("Name")
            .EqualTo(newTransaction.Budget)
            .OnceAsync<budget>();

        if (getResult.Any())
        {
            var Budget = getResult.First().Object;
            newTransaction.Name = Budget.Name;
            
             var postResult = await _firebaseClient
                .Child("transactions")
                .PostAsync(newTransaction);
             return (postResult.Object != null);
        }

        return false;
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
    public async Task<(bool IsSuccess, string result)> UpdateBudgetAsync(budget budget, transaction transaction)
    {
        var result = await _firebaseClient
            .Child("budgets")
            .Child(budget.Id)
            .OnceSingleAsync<budget>();

        if (result != null)
        {
            if (transaction.Amount != 0)
            {
                budget.updateAmount(transaction.Amount);
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
using core.interfaces;

namespace core.services;
using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Dynamic;

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
}
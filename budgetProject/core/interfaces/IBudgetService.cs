namespace core.interfaces;

public interface IBudgetService
{
    Task<(bool IsSuccess, string Key)> CreateBudgetAsync(budgetDto newBudget);
    Task<(bool IsSuccess, budget budget)> GetBudgetAsync(string name);
    
    Task<(bool IsSuccess, string result)> UpdateBudgetAsync(budget budget, amountDto amount);
}
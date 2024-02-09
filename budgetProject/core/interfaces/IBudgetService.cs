namespace core.interfaces;

public interface IBudgetService
{
    Task<(bool IsSuccess, string Key)> CreateBudgetAsync(budgetDto newBudget);
}
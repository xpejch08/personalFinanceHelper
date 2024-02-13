namespace core;

public class budget
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public DateTime DateCreated { get; set; }
    
    public budget()
    {
           Id = Guid.NewGuid().ToString();
    }


    private void addToBudget(decimal addedAmount)
    {
        Amount += addedAmount;
    }
}
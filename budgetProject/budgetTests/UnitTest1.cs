using core;
using core.interfaces;
using budgetHelper.Controllers;
using core.services;
using Microsoft.AspNetCore.Mvc;
using Moq;


public class BudgetControllerTests
{
    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenModelIsValid()
    {
        // Arrange
        var mockService = new Mock<IBudgetService>();
        var budgetDto = new budgetDto { Name = "Test Budget", Amount = 1000, Category = "Test", DateCreated = DateTime.UtcNow };
    
        // Assuming the service expects a 'budget' object and returns a success flag and an ID.
        mockService.Setup(service => service.CreateBudgetAsync(It.IsAny<budgetDto>()))
            .ReturnsAsync((true, "firebase_generated_id"));
    
        var controller = new BudgetController(mockService.Object);

        // Act
        var result = await controller.Create(budgetDto); // Passing budgetDto as expected by the controller.

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<budget>(actionResult.Value); // Asserting the returned object is of type 'budget'.
        Assert.Equal(budgetDto.Name, returnValue.Name); // Verifying that the returned object has the expected properties.
    }

}
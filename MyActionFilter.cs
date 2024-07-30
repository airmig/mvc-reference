using Microsoft.AspNetCore.Mvc.Filters;

public class MyActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Code that executes before the action method
        Console.WriteLine("OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Code that executes after the action method
        Console.WriteLine("OnActionExecuted");
    }
}
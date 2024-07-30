using Microsoft.AspNetCore.Mvc.Filters;

public class SingleScopeFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Code that executes before the action method
        Console.WriteLine("OnActionExecuting single scope");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Code that executes after the action method
        Console.WriteLine("OnActionExecuted single scope");
    }
}
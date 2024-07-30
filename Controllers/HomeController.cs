using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCReferenceProject.Models;

namespace MVCReferenceProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Message()
    {
        return View();
    }

    public ViewResult ViewResultMessage(){
        return View();
    }

    //other types of results
    //EmptyResult
    //ContentResult -> string literal
    //FileContentResult -> the content of a file
    //RedirectResult -> redirection
    //JsonResult
    //PartialViewResult -> partial view
    //JavaScriptResult
    //HttpUnauthorizedResult

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

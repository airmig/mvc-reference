using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCReferenceProject.Models;

namespace MVCReferenceProject.Controllers;

public class FormController : Controller{

    private readonly ILogger<FormController> _logger;

    public FormController(ILogger<FormController> logger)
    {
        _logger = logger;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Form(){
        return View();
    }

    [HttpPost]
    public IActionResult FormPost(string username, string password){
        ViewBag.Username = username;
        return View();
    }

    [HttpPost]
    public IActionResult FormPostStrong(LoginModel m){
        if (ModelState.IsValid){
            ViewBag.Username = m.Username;
            return View("FormPost");
        }
        return View("Form");
    }

    public IActionResult ModelBinding(){
        List<LoginModel> users = new List<LoginModel>{
            new LoginModel{Username="user1", Password="1234"},
            new LoginModel{Username="user2", Password="1234567"},
            new LoginModel{Username="user3", Password="1234890"}
        };
        return View(users);
    }
}
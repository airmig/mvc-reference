using System.Diagnostics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MVCReferenceProject.Models;
using MVCReferenceProject;

namespace MVCReferenceProject.Controllers;

[TypeFilter(typeof(MyActionFilter))]
public class CustomerController : Controller
{
    private static readonly List<Customer> customers = [
        new Customer{Id=1,Name="customerOne",Amount=5000},
        new Customer{Id=2,Name="customerTwo",Amount=15000},
        new Customer{Id=3,Name="customerThree",Amount=25000},
    ];

    //[Route("~/") overwrites default route]
    //[Route("/myroute")] overwrite route
    public IActionResult Index(){
        ViewBag.Message = "This is the ViewBag Message";
        ViewBag.CustomerCount = customers.Count;
        ViewBag.CustomerList = customers;
        TempData["Message"] = "THis is temp data message from Index";
        return View();
    }

    public IActionResult Details(){
        ViewData["CustomerCount"] = customers.Count;
        ViewData["CustomerList"] = customers;
        return View();
    }

    [SingleScopeFilter]
    public IActionResult Method1(){
        TempData["Message"] = "This is method1 tempdata";
        ViewBag.SessionVariable = HttpContext.Session.GetString("variable");
        string queryStringExample = "queryStringValue";
        if (!String.IsNullOrEmpty(HttpContext.Request.Query["example"])){
            queryStringExample = HttpContext.Request.Query["example"];
        }
        ViewBag.QueryStr = queryStringExample;
        return View();
    }

    public IActionResult Method2(){
        if(TempData["Message"]==null){
         return RedirectToAction("Index");
        }
        TempData["Message"] = TempData["Message"].ToString();
        return View();
    }

    public IActionResult SessionExample(){
        HttpContext.Session.SetString("variable", "session variable data");
        return RedirectToAction("Success");
    }

    public IActionResult RemoveSession(){
        HttpContext.Session.Remove("variable");
        return RedirectToAction("Method1");
    }
    public IActionResult Success(){
        ViewBag.SessionVariable = HttpContext.Session.GetString("variable");
        return View();
    }
}
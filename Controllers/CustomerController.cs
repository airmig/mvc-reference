using System.Diagnostics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MVCReferenceProject.Models;

namespace MVCReferenceProject.Controllers;

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
        return View();
    }
}
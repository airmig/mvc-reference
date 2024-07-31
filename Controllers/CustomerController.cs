using System.Diagnostics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MVCReferenceProject.Models;
using MVCReferenceProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
namespace MVCReferenceProject.Controllers;

//Individual Account Authentication
//[AllowAnonymous]
//[Authorize]
[TypeFilter(typeof(MyActionFilter))]
public class CustomerController : Controller
{
    protected readonly ApplicationDBContext _context;
    
    public CustomerController(){
        _context = new ApplicationDBContext();
    }
    private static readonly List<Customer> customers = [
        new Customer{Id=1,Name="customerOne",Amount=5000},
        new Customer{Id=2,Name="customerTwo",Amount=15000},
        new Customer{Id=3,Name="customerThree",Amount=25000},
    ];

    public IActionResult DBIndex(){
        ViewBag.Message = "Database query.";
        ViewBag.CustomerList = _context.Customers.ToList();
        ViewBag.CustomerCount = _context.Customers.ToList().Count;
        TempData["Message"] = "Query from Customer Database Table.";
        return View("Index");
    }

    public IActionResult Create(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Name, Amount, City")] Customer customer)   
    {
        if(ModelState.IsValid){
            _context.Add(customer);
            _context.SaveChanges();
            return View("Detail",customer);
        }
        return View();
    }
    public IActionResult Edit(int id){
        var customer = _context.Customers.Find(id);
        if (customer == null){
            return View("NotFound");
        }
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Customer customer){
        if (ModelState.IsValid){
            var existingCustomer = _context.Customers.Find(customer.Id);
            if (existingCustomer == null){
                return View("NotFound");
            }
            existingCustomer.Name = customer.Name;
            existingCustomer.Amount = customer.Amount;
            existingCustomer.City = customer.City;
            _context.SaveChanges();
            return View("detail", existingCustomer);
        }
        return View(customer);
    }

    public IActionResult Detail(int id){
        if (id==null|| _context.Customers == null){
            return View("NotFound");
        }
        var customer = _context.Customers.Find(id);
        if (customer == null){
            return View("NotFound");
        }
        return View(customer);
    }

    public IActionResult Delete(int id){
        var customer = _context.Customers.Find(id);
        if (customer == null){
            return View("NotFound");;
        }
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Customer customer){
            var existingCustomer = _context.Customers.Find(customer.Id);
            try{
            if (existingCustomer == null)
            {
                return View("NotFound");
            }

            _context.Customers.Remove(existingCustomer);
            _context.SaveChanges();
            }
            catch(Exception e){
                Console.WriteLine(e);
            }

            return View("DeleteSuccess"); // Redirect to a list or details page
    }

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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult StoredProcedure(){
        return View("Search");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StoredProcedure(Customer customer){

        Console.WriteLine(customer.Name);
        if (customer.Name == null){
            return View("NotFound");
        }
        var customers = _context.Customers
            .FromSqlRaw("EXEC GetCUstomerByName @SearchTerm", new SqlParameter("@SearchTerm", customer.Name.ToString()))
            .ToList();
        if (customers.Count == 0){
            return View("NotFound");
        }
        return View(customers);
    }
}
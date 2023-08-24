using LeaveManagemnetApp.Models;
using Microsoft.AspNetCore.Mvc;
using LeaveManagemnetApp.Data;
using MongoDB.Driver;

namespace LeaveManagemnetApp.Controllers;

public class AccountController : Controller
{
    private readonly MongoDbContext _mongoDbContext;
//    private readonly IMongoDatabase _mongoDatabase;

    public AccountController(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    // public AccountController(IMongoDatabase mongoDatabase)
    // {
    //     _mongoDatabase = mongoDatabase;
    // }

    // GET
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(string name, string password, string email)
    {
        var mongoDbContext = HttpContext.RequestServices.GetService<MongoDbContext>();


            var employeesCollection = mongoDbContext.Employees;
            var newEmp = new Employee()
            {
                Name = name,
                Email = email,
                IsAdmin = false,
                Password = password
            };
            await employeesCollection.InsertOneAsync(newEmp);
            return RedirectToAction("Index", "Home");

        // If validation fails
     //   return View("SignUp");
    }
}
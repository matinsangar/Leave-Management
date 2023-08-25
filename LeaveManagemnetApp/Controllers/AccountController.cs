using LeaveManagemnetApp.Models;
using Microsoft.AspNetCore.Mvc;
using LeaveManagemnetApp.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

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
    public async Task<IActionResult> Login(string username, string password)
    {
        var filter = Builders<Employee>.Filter.Eq("Name", username);
        var user = await _mongoDbContext.Employees.Find(filter).FirstOrDefaultAsync();

        if (user != null && user.Password == password)
        {
            return RedirectToAction("Index", "Home");
        }

        TempData["ErrorMessage"] = "Invalid username or password.";
        return RedirectToAction("Login");
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
    }

    public IActionResult Approve(int id)
    {
        return RedirectToAction("index", "Home");
    }
    public IActionResult RejectLeave(int id)
    {
        return RedirectToAction("index", "Home");
    }
}
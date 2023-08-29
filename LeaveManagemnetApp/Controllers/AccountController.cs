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

    public IActionResult AdminPanel()
    {
        return RedirectToAction("index", "Home");
    }

    public IActionResult Approve(int id)
    {
        return RedirectToAction("index", "Home");
    }

    public IActionResult RejectLeave(int id)
    {
        return RedirectToAction("index", "Home");
    }


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
        var isLoginValid = await _mongoDbContext.VerifyLoginAsync(username, password);
        if (isLoginValid)
        {
            return RedirectToAction("ApplyLeave");
        }

        TempData["ErrorMessage"] = "Invalid username or password.";
        return RedirectToAction("Login");
    }

    public IActionResult ApplyLeave()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(string name, string password, string email)
    {
        await _mongoDbContext.ResgisterUserAsync(name, password, email);
        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> ApplyLeave(DateTime StartDate, DateTime EndDate, string Reason)
    {
        await _mongoDbContext.SubmitNewLeaveRequest(StartDate, EndDate, Reason);
        return RedirectToAction("ApplyLeave");
    }
}
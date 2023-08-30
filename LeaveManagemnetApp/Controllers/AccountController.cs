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

    public AccountController(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public IActionResult AdminPanel()
    {
        return View();
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
    public async Task<IActionResult> Login(string username, string password)
    {
        var isLoginValid = await _mongoDbContext.VerifyLoginAsync(username, password);
        var isUserAdmin = await _mongoDbContext.VerifyIsAdminAsync(username, password);
        if (isLoginValid && isUserAdmin )
        {
            return RedirectToAction("AdminPanel","Account");
        }
        else if (isLoginValid && isUserAdmin == false)
        {
            return RedirectToAction("ApplyLeave");
        }

        TempData["ErrorMessage"] = "Invalid username or password.";
        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> ApplyLeave(string Name, string EmployeeID, DateTime StartDate, DateTime EndDate,
        string Reason)
    {
        if (ModelState.IsValid)
        {
            await _mongoDbContext.SubmitNewLeaveRequest(Name, EmployeeID, StartDate, EndDate, Reason);
            return RedirectToAction("ApplyLeave");
        }

        TempData["ErrorMessage"] = "Invalid Request!!!";
        return RedirectToAction("ApplyLeave");
    }
}
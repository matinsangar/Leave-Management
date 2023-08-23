using Microsoft.AspNetCore.Mvc;

namespace LeaveManagemnetApp.Controllers;

public class AccountController : Controller
{
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
    public IActionResult SignUp(string name, string password, string email)
    {
        return RedirectToAction("Index", "Home");
    }
}
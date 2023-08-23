using Microsoft.AspNetCore.Mvc;
using LeaveManagemnetApp.Data;
using LeaveManagemnetApp.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace LeaveManagemnetApp.Controllers;

public class EmployeeController : Controller
{
    private readonly MongoDbContext _dbContext;

    public EmployeeController(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var employees = _dbContext.Employees.Find(_ => true).ToList();
        return View(employees);
    }
}
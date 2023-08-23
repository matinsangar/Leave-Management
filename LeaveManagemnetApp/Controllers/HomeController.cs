using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LeaveManagemnetApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace LeaveManagemnetApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMongoDatabase _mongoDatabase;

        public HomeController(ILogger<HomeController> logger, IMongoDatabase mongoDatabase)
        {
            _logger = logger;
            _mongoDatabase = mongoDatabase;
        }

        public IActionResult Index()
        {
            // return View();
            var employeesCollection = _mongoDatabase.GetCollection<Employee>("Employees");
            var employees = employeesCollection.Find(_ => true).ToList();
            return View(employees);
        }

        public async Task<IActionResult> InsertSampleEmployee()
        {
            var employeesCollection = _mongoDatabase.GetCollection<Employee>("Employees");

            var sampleEmployee = new Employee
            {
                Name = "Matin",
                IsAdmin = true,
                Email = "matinuipk32@gmail.com"
            };

            await employeesCollection.InsertOneAsync(sampleEmployee);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
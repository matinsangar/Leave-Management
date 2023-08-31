using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using LeaveManagemnetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeaveManagemnetApp.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase("LeaveApplication");
    }

    public IMongoCollection<Employee> Employees
    {
        get { return _database.GetCollection<Employee>("Employees"); }
    }

    public IMongoCollection<ApplyLeave> LeaveRequests
    {
        get { return _database.GetCollection<ApplyLeave>("LeaveRequests"); }
    }

    public async Task ResgisterUserAsync(string name, string password, string email)
    {
        var user = new Employee
        {
            Name = name,
            Password = password,
            Email = email
        };
        await Employees.InsertOneAsync(user);
    }

    public async Task<bool> VerifyLoginAsync(string name, string password)
    {
        var filter = Builders<Employee>.Filter.Eq("Name", name);
        var user = await Employees.Find(e => e.Name == name).FirstOrDefaultAsync();
        if (user != null && user.Password == password)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> VerifyIsAdminAsync(string name, string password)
    {
        var filter = Builders<Employee>.Filter.Eq("Name", name);
        var user = await Employees.Find(e => e.Name == name).FirstOrDefaultAsync();
        return user.IsAdmin;
    }

    public async Task SubmitNewLeaveRequest(string name, string employeeId, DateTime startDate, DateTime endDate,
        string reason)
    {
        var request = new ApplyLeave()
        {
            Name = name,
            EmployeeID = employeeId,
            StartDate = startDate,
            EndDate = endDate,
            Reason = reason,
            Status = "In process"
            // Status ===>  default value in default constructor "In process"
        };
        await LeaveRequests.InsertOneAsync(request);
    }

    public async Task<List<ApplyLeave>> GetLeaveRequestsByUserId(string employeeId)
    {
        var requests = await LeaveRequests.Find(e => e.EmployeeID == employeeId).ToListAsync();
        return requests;
    }

    public async Task<ApplyLeave> GetRequestsByUserId(string employeeId)
    {
        var requests = await LeaveRequests.Find(e => e.EmployeeID == employeeId).FirstOrDefaultAsync();
        return requests;
    }

    public async Task<List<ApplyLeave>> GetAllLeaveRequests()
    {
        var request = await LeaveRequests.Find((_ => true)).ToListAsync();
        return request;
    }

    public void UpdateLeaveRequest(ApplyLeave request)
    {
        var filter = Builders<ApplyLeave>.Filter.Eq("_id", request.EmployeeID);
        var update = Builders<ApplyLeave>.Update
            .Set("Status", request.Status);
        LeaveRequests.UpdateOne(filter, update);
    }
}
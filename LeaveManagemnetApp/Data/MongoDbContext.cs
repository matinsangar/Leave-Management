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

    public IMongoCollection<BsonDocument> Employees
    {
        get { return _database.GetCollection<BsonDocument>("Employees"); }
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeaveManagemnetApp.Models;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace LeaveManagemnetApp.Models;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [MinLength(3)]
    public string Name { get; set; }
    [MinLength(4)]
    public string Password { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [BsonDefaultValue(false)]
    public bool IsAdmin { get; set; }
    
}
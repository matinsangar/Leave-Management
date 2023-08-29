using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace leavemanger2.Models;

public class LeaveRequest
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string EmployeeId { get; set; }
    public string Reason { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsApproved { get; set; }
}
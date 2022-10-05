using System.Text.Json.Serialization;

namespace Airtickets.Entities;

public class TicketDto
{
    [JsonPropertyName("operation_type")] public string OperationType { get; set; }
    [JsonPropertyName("operation_time")] public DateTime OperationTime { get; set; }
    [JsonPropertyName("operation_place")] public string OperationPlace { get; set; }

    [JsonPropertyName("ticket_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? TicketNumber { get; set; }

    [JsonPropertyName("passenger")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

    public PassengerDto? Passenger { get; set; }

    [JsonPropertyName("routes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public RouteDto[]? Routes { get; set; }
}
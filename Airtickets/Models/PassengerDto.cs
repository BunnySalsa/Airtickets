using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Airtickets.Entities;

public class PassengerDto
{
    [JsonPropertyName("name")] 
    public string Name { get; set; }
    [JsonPropertyName("surname")] 
    public string Surname { get; set; }
    [JsonPropertyName("patronymic")] 
    public string Patronymic { get; set; }
    [JsonPropertyName("doc_type")] 
    public string DocType { get; set; }
    [JsonPropertyName("doc_number")] 
    public string DocNumber { get; set; }
    [JsonPropertyName("birthdate")] 
    public DateTime Birthdate { get; set; }
    [JsonPropertyName("gender")] 
    public string Gender { get; set; }

    [JsonPropertyName("passenger_type")] 
    public string PassengerType { get; set; }
    [JsonPropertyName("ticket_number")] 
    public string TicketNumber { get; set; }
    [JsonPropertyName("ticket_type")] 
    public int TicketType { get; set; }
}
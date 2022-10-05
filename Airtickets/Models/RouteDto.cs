using System.Text.Json.Serialization;

namespace Airtickets.Entities;

public class RouteDto
{
    [JsonPropertyName("airline_code")]
    public string AirlineCode { get; set; }
    [JsonPropertyName("flight_num")] 
    public int FlightNum { get; set; }
    [JsonPropertyName("depart_place")] 
    public string DepartPlace { get; set; }
    [JsonPropertyName("depart_datetime")] 
    public DateTime DepartDateTime { get; set; }
    [JsonPropertyName("arrive_place")] 
    public string ArrivePlace { get; set; }
    [JsonPropertyName("arrive_datetime")] 
    public DateTime ArriveDateTime { get; set; }
    [JsonPropertyName("pnr_id")] 
    public string PnrId { get; set; }
}
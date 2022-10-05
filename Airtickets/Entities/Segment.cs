using Airtickets.Entities;

namespace Airtickets.Models;

[Keyless]
public class Segment
{
    public string TicketNumber { get; set; }
    public int SerialNumber { get; set; }
    public int FlightNum { get; set; }
    public DateTimeOffset OperationTime { get; set; }
    public string AirlineCode { get; set; }
    public string DepartPlace { get; set; }
    public DateTimeOffset DepartDateTime { get; set; }
    public string ArrivePlace { get; set; }
    public DateTimeOffset ArriveDateTime { get; set; }
    public string PnrId { get; set; }
    public SegmentStatus Status { get; set; }

    public Segment()
    {
    }

    public Segment(string ticketNumber, int serialNumber, int flightNum, DateTime operationTime, string airlineCode,
        string departPlace, DateTime departDateTime, string arrivePlace, DateTime arriveDateTime, string pnrId)
    {
        TicketNumber = ticketNumber;
        SerialNumber = serialNumber;
        FlightNum = flightNum;
        OperationTime = operationTime.ToUniversalTime();
        AirlineCode = airlineCode;
        DepartPlace = departPlace;
        DepartDateTime = departDateTime.ToUniversalTime();
        ArrivePlace = arrivePlace;
        ArriveDateTime = arriveDateTime.ToUniversalTime();
        PnrId = pnrId;
    }

    public static IEnumerable<Segment> ToSegments(TicketDto ticket)
    {
        var segments = new Segment[ticket.Routes.Length];
        for (var i = 0; i < ticket.Routes.Length; i++)
        {
            segments[i] = new Segment(ticket.Passenger.TicketNumber, i + 1, ticket.Routes[i].FlightNum,
                ticket.OperationTime,
                ticket.Routes[i].AirlineCode, ticket.Routes[i].DepartPlace, ticket.Routes[i].DepartDateTime,
                ticket.Routes[i].ArrivePlace, ticket.Routes[i].ArriveDateTime, ticket.Routes[i].PnrId);
        }
        return segments;
    }
}
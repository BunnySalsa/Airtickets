using Airtickets.Entities;

namespace Airtickets.Validators;

public class TicketValidator : ITicketValidator
{
    public TicketValidator()
    {
    }

    public bool SaleValidate(TicketDto ticket)
    {
        return ticket.OperationTime < DateTime.Now &&
               !(ticket.Passenger.DocType.Equals("00") ^ ticket.Passenger.DocNumber.Length == 10);
    }

    public bool RefundValidate(TicketDto ticket)
    {
        return ticket.OperationTime < DateTime.Now;
    }
}
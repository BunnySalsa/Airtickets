using System.ComponentModel.DataAnnotations;
using Airtickets.Entities;

namespace Airtickets.Validators;

public class TicketValidator : ITicketValidator
{
    private static TicketValidator _validator;

    private TicketValidator()
    {
    }

    public static TicketValidator GetInstance()
    {
        if (_validator == null)
            _validator = new TicketValidator();
        return _validator;
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
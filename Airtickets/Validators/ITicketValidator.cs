using Airtickets.Entities;

namespace Airtickets.Validators;

public interface ITicketValidator
{
    bool SaleValidate(TicketDto ticket);

    bool RefundValidate(TicketDto ticket);
}
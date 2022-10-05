using System.Text.Json.Nodes;
using Airtickets.Entities;
using Airtickets.Filters;
using Airtickets.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airtickets.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[RequestSizeLimit(2048)]
public class AirTicketController : ControllerBase
{
    private readonly TicketService _service;

    public AirTicketController()
    {
        _service = TicketService.GetInstance();
    }

    [HttpPost("/process/sale")]
    [ServiceFilter(typeof(SaleAsyncResourceFilter))]
    public async Task PostSale(TicketDto ticket)
    {
        await _service.SaleAsync(ticket);
    }

    [HttpPost("/process/refund")]
    [ServiceFilter(typeof(RefundResourceFilter))]
    public async Task PostRefund(TicketDto ticket)
    {
        await _service.RefundAsync(ticket);
    }
}
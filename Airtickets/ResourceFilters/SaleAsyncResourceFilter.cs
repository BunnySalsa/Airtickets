using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using Airtickets.Entities;
using Airtickets.Validators;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Airtickets.Filters;

public class SaleAsyncResourceFilter : IResourceFilter
{
    private const string MatchExceptionMessageS = "Posted json is not match to scheme";
    private const string InvalidValueExceptionMessageS = "Posted json has invalid values";
    private readonly JsonValidator _jsonValidator;
    private readonly TicketValidator _ticketValidator;

    public SaleAsyncResourceFilter()
    {
        _jsonValidator = JsonValidator.GetInstance();
        _ticketValidator = TicketValidator.GetInstance();
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        context.HttpContext.Request.EnableBuffering();
        using (var reader = new StreamReader(context.HttpContext.Request.Body, leaveOpen:true))
        {
            var jsonNode = JsonNode.Parse(reader.ReadToEndAsync().Result);
            if (!_jsonValidator.SaleValidate(jsonNode))
                throw new ValidationException(MatchExceptionMessageS);
            var ticket = jsonNode.Deserialize<TicketDto>();
            if (!_ticketValidator.SaleValidate(ticket))
                throw new ValidationException(InvalidValueExceptionMessageS);
        }
        context.HttpContext.Request.Body.Position = 0;
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }
}
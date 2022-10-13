using System.Text.Json.Nodes;
using Json.Schema;

namespace Airtickets.Validators;

public class JsonValidator : IJsonValidator
{
    private readonly string _saleSchemePath;
    private readonly string _refundSchemePath;

    public JsonValidator()
    {
        var configuration = WebApplication.CreateBuilder().Configuration;
        _saleSchemePath = configuration.GetValue<string>("JsonValidator:SalePath");
        _refundSchemePath = configuration.GetValue<string>("JsonValidator:RefundPath");
    }

    public bool SaleValidate(JsonNode json)
    {
        var scheme = JsonSchema.FromFile(_saleSchemePath);
        return scheme.Validate(json).IsValid;
    }

    public bool RefundValidate(JsonNode json)
    {
        var scheme = JsonSchema.FromFile(_refundSchemePath);
        return scheme.Validate(json).IsValid;
    }
}
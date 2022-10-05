using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using Airtickets.Entities;
using Json.Schema;

namespace Airtickets.Validators;

public class JsonValidator : IJsonValidator
{
    private static JsonValidator _validator;
    private readonly string _saleSchemePath;
    private readonly string _refundSchemePath;

    private JsonValidator()
    {
        var configuration = WebApplication.CreateBuilder().Configuration;
        _saleSchemePath = configuration.GetValue<string>("JsonValidator:SalePath");
        _refundSchemePath = configuration.GetValue<string>("JsonValidator:RefundPath");
    }

    public static JsonValidator GetInstance()
    {
        if (_validator == null)
            _validator = new JsonValidator();
        return _validator;
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

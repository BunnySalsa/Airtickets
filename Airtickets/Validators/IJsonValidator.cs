using System.Text.Json;
using System.Text.Json.Nodes;

namespace Airtickets.Validators;

public interface IJsonValidator
{
    bool SaleValidate(JsonNode json);
    bool RefundValidate(JsonNode json);
}
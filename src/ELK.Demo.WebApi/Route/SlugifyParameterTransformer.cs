using System.Text.RegularExpressions;

namespace ELK.Demo.WebApi.Route
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null) return null;

            var input = value.ToString();
            if (string.IsNullOrEmpty(input)) return null;

            return Regex.Replace(input, "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
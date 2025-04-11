using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace nauteck.core.Implementation;

public static class HtmlHelperExtensions
{
    public static string GenerateDataAttributes<T>(this IHtmlHelper htmlHelper, T obj)
    {
        var properties = typeof(T).GetProperties();
        var attributes = new StringBuilder();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj)?.ToString();
            attributes.Append($"data-{property.Name.ToLower()}={value} ");
        }

        return attributes.ToString();
    }
}
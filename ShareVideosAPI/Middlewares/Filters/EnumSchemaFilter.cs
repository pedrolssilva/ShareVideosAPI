using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace ShareVideosAPI.Middlewares.Filters
{
    /// <summary>
    /// Custom enum schema filter
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Apply enum description in schema
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            var type = context.Type;
            if (!type.IsEnum)
            {
                return;
            }

            var sb = new StringBuilder(model.Description);
            sb.AppendLine("<p> Possible values: </p>");
            sb.AppendLine("<ul>");
            foreach (var name in Enum.GetValues(type))
            {
                var value = Convert.ToInt64(name);
                sb.AppendLine(string.Format("<li>{0} - {1}</li>", value, name));
            }
            sb.AppendLine("</ul>");
            model.Description = sb.ToString();
        }
    }
}

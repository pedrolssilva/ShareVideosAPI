using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShareVideosAPI.Models;

namespace ShareVideosAPI.Middlewares.Filters
{
    /// <summary>
    /// Custom model state validator for requests.
    /// </summary>
    public class ValidateModelStateCustom : ActionFilterAttribute
    {
        /// <summary>
        /// Validate model fields on action executing.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                FieldValidatorViewModelOutput fieldValidatorViewModel = new(
                    context.ModelState.SelectMany(sm => sm.Value!.Errors)
                        .Select(s => s.ErrorMessage)
                    );

                context.Result = new BadRequestObjectResult(fieldValidatorViewModel);
            }
        }
    }
}

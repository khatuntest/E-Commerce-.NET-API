using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json;

namespace E_Commerce.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly ILogger<ValidationFilter> _logger;

        public ValidationFilter(ILogger<ValidationFilter> logger) { 
        
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"Before Executing Action {context.ActionDescriptor.DisplayName}" +
                $" On Controller {context.Controller} with attributes {JsonSerializer.Serialize(context.ActionArguments)}");

            if (!context.ModelState.IsValid)
            {
                _logger.LogWarning("The state of this request is not valid ");
                context.Result = new BadRequestObjectResult("your input is not valid , Try again");
                return;
            }

            await next();

            _logger.LogInformation($"Executing Action {context.ActionDescriptor.DisplayName}" +
               $" On Controller {context.Controller} executed successfully");
        }
    }
}

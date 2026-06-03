using Infrastructure.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleError<T>(Result<T> result)
    {
        return result.ErrorType switch
        {
            ErrorType.Validation => BadRequest(result.Error),
            ErrorType.NotFound => NotFound(result.Error),
            ErrorType.Conflict => Conflict(result.Error),
            ErrorType.Unauthorized => Unauthorized(result.Error),
            ErrorType.NoChange => Ok(result.Error),
            _ => StatusCode(StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.")
        };
    }
}

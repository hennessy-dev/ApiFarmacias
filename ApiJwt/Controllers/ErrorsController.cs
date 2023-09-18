
using ApiJwt.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;


namespace ApiJwt.Controllers;

[Microsoft.AspNetCore.Components.Route("errors/{code}")]
public class ErrorsController:ApiBaseController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}

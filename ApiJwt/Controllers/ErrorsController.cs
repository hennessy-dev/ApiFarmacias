
using ApiJwt.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace ApiJwt.Controllers;

public class ErrorsController:ApiBaseController
{
    [HttpGet]
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}

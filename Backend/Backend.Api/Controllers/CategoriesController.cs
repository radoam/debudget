using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[Route("[controller]")]
public class CategoriesController : ApiController
{
    [HttpGet]
    public IActionResult ListCategories()
    {
        return Ok(Array.Empty<string>());
    }

}
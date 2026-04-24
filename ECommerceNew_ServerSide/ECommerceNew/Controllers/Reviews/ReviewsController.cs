using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNew.Api.Controllers.Reviews
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        [HttpGet("product/{productId}")]
        

        [HttpPost]
        // create review

        [HttpPut("{id}")]
        // update review

        [HttpDelete("{id}")]
        // delete review
    }
}

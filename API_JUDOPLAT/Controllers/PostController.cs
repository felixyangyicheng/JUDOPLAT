
using JUDOPLAT.API_JUDOPLAT.Controllers;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace API_JUDOPLAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> logger;
        private readonly IPostRepo _postRepo;
        public PostController(ILogger<PostController> logger, IPostRepo postRepo)
        {
            this.logger = logger;
            this._postRepo = postRepo;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async IAsyncEnumerable<PostViewModel> Get()
        {
            logger.LogInformation($"POst Attempt GET ALL IAsyncEnum");

            await foreach (var product in _postRepo.GetAllAsync())
            {
            
                    yield return product;
                
            }
        }
    }

}

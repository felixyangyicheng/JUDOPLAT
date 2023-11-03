
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace JUDOPLAT.API_JUDOPLAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> logger;
        private readonly IPdfRepo _pdfRepo;
          private readonly IMapper _mapper;
        public PdfController(ILogger<PdfController> logger, IPdfRepo pdfRepo, IMapper mapper)
        {
            this.logger = logger;
            this._pdfRepo = pdfRepo;
            _mapper = mapper;
        }
        /// <summary>
        /// Download pdf
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet]

        [Route("{filename}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetOne( string filename)
        {
            logger.LogInformation($"Pdf Attempt GET {filename}");

            try
            {
                var db = _pdfRepo.GetByNameAsync(filename).Result;
                return File(db.Content, "application/pdf");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong in the {nameof(GetOne)}");
                return Problem($"Something Went Wrong in the {nameof(GetOne)}", statusCode: 500);
            }
        }

        [HttpGet]

        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async  IAsyncEnumerable<PdfModel> GetAsync()
        {
            logger.LogInformation($"Pdf Attempt GetAsync all");

       
            var db =await _pdfRepo.GetAsync();
            foreach (var p in db)
            {
                
                yield return p;
                    
            }
       
        }

                [HttpGet]

        [Route("all/simple")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async  IAsyncEnumerable<PdfModelSimple> GetAllSimpleAsync()
        {
            logger.LogInformation($"Pdf Attempt GetAsync all simple");

       
            var db =await _pdfRepo.GetAsync();
            foreach (var p in db)
            {
                
                var pSimple= _mapper.Map<PdfModelSimple>(p);
                yield return pSimple;
                    
            }
       
        }
    }
}

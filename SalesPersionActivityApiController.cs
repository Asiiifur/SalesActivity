using AccumenSalesActivity.Models.ViewModel;
using AccumenSalesActivity.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccumenSalesActivity.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPersionActivityApiController(ISalesActivityRepository salesActivityRepo
                                                        , IWebHostEnvironment webHostEnvironment) : ControllerBase
    {
        private readonly ISalesActivityRepository _salesActivityRepo = salesActivityRepo;
        public IWebHostEnvironment _webHostEnvironment = webHostEnvironment;


        [Route("/api/SalesPersionActivityApi/SalesPersonActivityCreateApi")]
        [HttpPost]
        public async Task<IActionResult> SalesPersonActivityCreateApi([FromBody] CreateSalesPersonActivity request)
        {

            try
            {
                var response = await _salesActivityRepo.CreateSalesPersonActivityApi(request).ConfigureAwait(false);

                if (response == "success")
                {
                    return Ok("Submit done");
                }
                else
                {
                    return NotFound("!!ops. Error occured.Not submitted");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

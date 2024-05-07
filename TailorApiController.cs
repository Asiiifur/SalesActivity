using AccumenSalesActivity.App_Code.GlobalClass;
using AccumenSalesActivity.Models.Company;
using AccumenSalesActivity.Models.ViewModel;
using AccumenSalesActivity.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AccumenSalesActivity.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TailorApiController : ControllerBase
    {
        private readonly ICompanyMasterRepository _cmpMasterRepo;
        private readonly IConfiguration _configuration;

        public IWebHostEnvironment _webHostEnvironment;

        public TailorApiController(ICompanyMasterRepository cmpMstrRepo, IWebHostEnvironment webHostEnvironment)
        {
            _cmpMasterRepo = cmpMstrRepo;

            _webHostEnvironment = webHostEnvironment;
        }

        [Route("/api/Tailor/ShopTailorInfoApi")]
        [HttpGet]
        public async Task<IActionResult> ShopTailorInfoApi(int EmpId, int? CompanyId)
        {
            try
            {
               
               var list = await _cmpMasterRepo.GetShopTailorDetails(EmpId,CompanyId);
                   
               
                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound("Item not found"); // Adjust the response accordingly
                }
            }
            catch (Exception ex)
            {
                throw;

            }

        }
    }
}

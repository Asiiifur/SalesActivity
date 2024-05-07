using AccumenSalesActivity.App_Code.GlobalClass;
using AccumenSalesActivity.Models.Master;
using AccumenSalesActivity.Models.ViewModel;
using AccumenSalesActivity.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AccumenSalesActivity.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemApiController : ControllerBase
    {
        private readonly IItemRepository _itemInfoRepo;
        private readonly IConfiguration _configuration;

        public IWebHostEnvironment _webHostEnvironment;

        public ItemApiController (IItemRepository itemRepo, IWebHostEnvironment webHostEnvironment)
        {
            _itemInfoRepo = itemRepo;

            _webHostEnvironment = webHostEnvironment;
        }


        //[Route("/api/Item/GetItemDetails")]
        //[HttpGet]
        //public async Task<IActionResult> GetItemDetails(ItemStkVM itemStock)
        //{


        //    try
        //    {
        //        var list = await _itemInfoRepo.GetALLItemDetails(itemStock).ConfigureAwait(false);

        //        if (list != null)
        //        {
        //            return Ok(list);
        //        }
        //        else
        //        {
        //            return NotFound("Item not found"); 
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }


        //}
        [Route("/api/Item/ItemStocksApi")]
        [HttpGet]
        public async Task<IActionResult> ItemStocksApi(string? ItemName, Int64? Barcode, int CompanyId)
        {
         

            try
            {
                var list = await _itemInfoRepo.GetItemStockDetails(ItemName, Barcode, CompanyId).ConfigureAwait(false);

                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound("Item not found"); // Adjust the response accordingly
                }
            }
            catch
            {
                throw;
            }


        }
    }
}

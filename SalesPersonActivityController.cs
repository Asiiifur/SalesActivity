using AccumenSalesActivity.App_Code.GlobalClass;
using AccumenSalesActivity.Models.ViewModel;
using AccumenSalesActivity.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AccumenSalesActivity.Controllers
{
    public class SalesPersonActivityController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        private readonly ISalesActivityRepository _salesActivityRepo;

        public SalesPersonActivityController(ISalesActivityRepository salesActivityRepo)
        {
            _salesActivityRepo = salesActivityRepo;
        }

        //SalesPersonActivity/SalesActivityIndex
        public async Task<IActionResult> SalesActivityIndex()
        {
          
            var sAList = await _salesActivityRepo.GetSalesPersonActivityList();
          

            return View(sAList);
        }


          // GET: /SalesPersonActivity/SalesActivityDetails/
        public async Task<IActionResult> SalesActivityDetails(Int64 id)
        {
           
            var salesAcDetails = await _salesActivityRepo.GetSalesPersonActivityDetails(id);
            if (salesAcDetails == null)
            {
                return NotFound();
            }

            return View(salesAcDetails);
        } 
        // GET: /SalesPersonActivity/SalesActivityDetailsOld/
        public async Task<IActionResult> SalesActivityDetailsOld(Int64 id)
        {

            var salesAcDetails = await _salesActivityRepo.GetSalesPersonActivityDetails(id);
            if (salesAcDetails == null)
            {
                return NotFound();
            }

            return View(salesAcDetails);

        }


        ////SalesPersonActivity/SalesActivityCompleteAppointment
        //public async Task<IActionResult> SalesActivityCompleteAppointment()
        //{

        //    var sAList = await _salesActivityRepo.GetSalesPersonActivityList();
        //    var completeAppointmentList = await _salesActivityRepo.GetSalesCompleteAppointmentList();


        //    return View(sAList);
        //}


        //// GET: /SalesPersonActivity/SalesActivityCompleteAppointmentDetails/
        //public async Task<IActionResult> SalesActivityCompleteAppointmentDetails(Int64 id)
        //{

        //    var salesAcDetails = await _salesActivityRepo.GetSalesPersonActivityDetails(id);
        //    if (salesAcDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(salesAcDetails);
        //}
    }
}

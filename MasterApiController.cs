using AccumenSalesActivity.Data;
using AccumenSalesActivity.Models.Company;
using AccumenSalesActivity.Models.ViewModel;
using AccumenSalesActivity.Repository;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace AccumenSalesActivity.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterApiController : ControllerBase
    {

        private readonly ICompanyMasterRepository _cmpMasterRepo;
        private readonly IConfiguration _configuration;
        public IWebHostEnvironment _webHostEnvironment;

        public MasterApiController(ICompanyMasterRepository cmpMstrRepo, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, CompanyDBConnection cdb, MasterDBConnection db)
        {
            _cmpMasterRepo = cmpMstrRepo;

            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        [Route("/api/Master/DesignTemplateApi")]
        [HttpGet]
        public async Task<IActionResult> DesignTempleteApi(int EmpId)
        {
            try
            {

                var list = await _cmpMasterRepo.GetDesignTempleteDetails(EmpId);


                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound("Design not found"); // Adjust the response accordingly
                }
            }
            catch (Exception ex)
            {
                throw;

            }

        }

        [Route("/api/Master/ShopEmployeesApi")]
        [HttpGet]
        public async Task<IActionResult>ShopEmployeesApi(int EmpId)
        {
            try
            {

                var list = await _cmpMasterRepo.GetShopsEmployeeDetails(EmpId);


                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound("Shop Employee not found"); // Adjust the response accordingly
                }
            }
            catch (Exception ex)
            {
                throw;

            }

        }
        [Route("/api/Master/GetSalesPersonActivityAppointmentPurposeApi")]
        [HttpGet]
        public async Task<IActionResult> GetSalesPersonActivityAppointmentPurposeApi()
        {
            try
            {

                var list = await _cmpMasterRepo.GetSPAAppointmentPurposeList();


                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound("Appointment Purpose not found"); // Adjust the response accordingly
                }
            }
            catch (Exception ex)
            {
                throw;

            }

        }

        [Route("/api/Master/CheckCustomerApi")]
        [HttpGet]
        public async Task<IActionResult> CheckCustomerApi(string? customerNameOrNumber)
        {
            try
            {

                var list = await _cmpMasterRepo.GetExistingCustomerDetails(customerNameOrNumber);


                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound("Design not found"); // Adjust the response accordingly
                }
            }
            catch (Exception ex)
            {
                throw;

            }

        }
        [Route("/api/Master/CreateCustomerApi")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomerApi([FromBody] CreateCustomer request)
        {
            try
            {
                var CustomerApi = new CustomerApiDetails();
                

                

                var list = await _cmpMasterRepo.CheckExistingCustomer(request.Name,request.PhoneNo);


                if (list.Count() == 0)
                {

                    var datalist = await _cmpMasterRepo.CreateCustomerApi(request);


                    //CustomerApi.CustomerId=

                    CustomerApi.CustomerId = datalist.CustomerId;
                    CustomerApi.Name = datalist.Name;
                    CustomerApi.PhoneNo = datalist.PhoneNo;
                    CustomerApi.Email = datalist.Email;
                    CustomerApi.Address = datalist.Address;
                    CustomerApi.CustomerTypeStatus = 3;

                    CustomerApi.Status = "Success";

                    return Ok(CustomerApi);
                }
                else
                {
                    var exCustomrList = list.ToList().FirstOrDefault();
                    if(exCustomrList != null)
                    {
                        CustomerApi.CustomerId = exCustomrList.CustomerId;
                        CustomerApi.Name = exCustomrList.CustomerName;
                        CustomerApi.Email = exCustomrList.Email;
                        CustomerApi.Address = exCustomrList.CustomerAddress;
                        CustomerApi.PhoneNo = exCustomrList.ContactNumberOne;
                      
                    }
                    CustomerApi.CustomerTypeStatus = 2;
                    CustomerApi.Status = "Customer Name or Phone Number Already Exist.";

                    return NotFound(CustomerApi); // Adjust the response accordingly
                }
            }
            catch (Exception ex)
            {
                throw;

            }

        }
       
        public class CustomerApiDetails
        {
            //public string Note { get; set; }
            public string? Status { get; set; }
            public int? CustomerTypeStatus { get; set; }


            public string? CustomerId { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
            public string? PhoneNo { get; set; }

           
         
        }

       
    }
}

using AccumenSalesActivity.Models.Company;
using AccumenSalesActivity.Models.ViewModel;
using AccumenSalesActivity.Repository;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;

namespace AccumenSalesActivity.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPersonActivityApiController(ISalesActivityRepository salesActivityRepo , IWebHostEnvironment webHostEnvironment) : ControllerBase
    {
        private readonly ISalesActivityRepository _salesActivityRepo = salesActivityRepo;
        public IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
     

        [Route("/api/SalesPersonActivityApi/SalesPersonActivityCreateApi")]
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

         


        [Route("/api/SalesPersonActivityApi/GetPreviousActivityApi")]
        [HttpGet]
        public async Task<IActionResult> GetPreviousActivityApi(string? SACustomerId, DateTime FromActivityDate, DateTime ToActivityDate, int ActivityBy)
        {


            try
            {
                var list = await _salesActivityRepo.GetPreviousSPActivity(SACustomerId, FromActivityDate, ToActivityDate, ActivityBy);

                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound("No Activity Found"); // Adjust the response accordingly
                }
            }
            catch
            {
                throw;
            }


        }

        [Route("/api/SalesPersonActivityApi/PreviousActivityDetailsViewApi")]
        [HttpGet]
        public async Task<IActionResult> PreviousActivityDetailsViewApi(Int64 ActivityId)
        {


            try
            {
                var list = await _salesActivityRepo.PreviousSPActivityDetails(ActivityId);

                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound("No Activity Details Found"); // Adjust the response accordingly
                }
            }
            catch
            {
                throw;
            }


        }

        [Route("/api/SalesPersonActivityApi/GetSPActivityCustomerMyAppointmentApi")]
        [HttpGet]
        public async Task<IActionResult> GetSPActivityCustomerMyAppointmentApi(int AssignEmpId, int? AppointmentStatus, DateTime FromAppointmentDate, DateTime ToAppointmentDate)
        {


            try
            {
                var list = await _salesActivityRepo.GetCustomerMyAppointmentList(AssignEmpId, AppointmentStatus, FromAppointmentDate, ToAppointmentDate);

                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound("No Appointment Found"); // Adjust the response accordingly
                }
            }
            catch
            {
                throw;
            }


        }


        [Route("/api/SalesPersonActivityApi/UpdateAppointmentStatusApi")]
        [HttpPost]
         public async Task<IActionResult> UpdateAppointmentStatusApi([FromBody] CustomerAppoinmentStatusUpdate cAUpdateStatus)
         {

            try
            {
                var response = await _salesActivityRepo.UpdateCustomerMyAppointmentStatusApi(cAUpdateStatus).ConfigureAwait(false);

                if (response != null)
                {
                    return Ok("Appointment Rescheduled");
                }
                else
                {
                    return BadRequest("Appointment  cannot be Rescheduled");
                }
            }
            catch
            {
                throw;
            }

         }

        //[Route("/api/SalesPersonActivityApi/SubmitAppointmentApi")]
        //[HttpPost]
        //public async Task<IActionResult> SubmitAppointmentApi([FromBody] CustomerAppoinmentStatusUpdate cAUpdateStatus)
        //{

        //    AttendanceApiData attendanceapi=new AttendanceApiData();


        //    return BadRequest("Appointment  cannot be Rescheduled");

        //    //var fileNameWithExtension = "";
        //    var imagepathname = "";
        //    string ImagePathName = "";
        //    var imagepath = "";
        //    string imagePathwithname = "";
        //    if (attendanceapi.Image != null)
        //    {
        //        //folder create
        //        HttpContext context = HttpContext.Current;
        //        string projectPath = context.Server.MapPath("~");
        //        string imageFolder = Path.Combine(projectPath, "UploadedFiles", "Images");
        //        if (!Directory.Exists(imageFolder))
        //        {
        //            Directory.CreateDirectory(imageFolder);
        //        }
        //        string imagePath = Path.Combine(projectPath, "UploadedFiles", "Images", "Attendance");
        //        if (!Directory.Exists(imagePath))
        //        {
        //            Directory.CreateDirectory(imagePath);
        //        }
        //        string folderYearName = DateTime.Now.Year.ToString();
        //        string folderMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        //        string folderDayName = DateTime.Now.Day.ToString();

        //        string targetFolderPath = Path.Combine(imagePath, folderYearName);
        //        if (!Directory.Exists(targetFolderPath))
        //        {
        //            Directory.CreateDirectory(targetFolderPath);
        //            string targetFolderPathMonth = Path.Combine(targetFolderPath, folderMonthName);
        //            Directory.CreateDirectory(targetFolderPathMonth);
        //            string targetFolderPathDay = Path.Combine(targetFolderPathMonth, folderDayName);
        //            Directory.CreateDirectory(targetFolderPathDay);
        //        }
        //        else
        //        {
        //            string targetFolderPathMonth = Path.Combine(targetFolderPath, folderMonthName);
        //            if (!Directory.Exists(targetFolderPathMonth))
        //            {
        //                Directory.CreateDirectory(targetFolderPathMonth);
        //                string targetFolderPathDay = Path.Combine(targetFolderPathMonth, folderDayName);
        //                Directory.CreateDirectory(targetFolderPathDay);
        //            }
        //            else
        //            {
        //                string targetFolderPathDay = Path.Combine(targetFolderPathMonth, folderDayName);
        //                if (!Directory.Exists(targetFolderPathDay))
        //                {
        //                    Directory.CreateDirectory(targetFolderPathDay);
        //                }
        //            }
        //        }
        //        imagepath = targetFolderPath + "\\" + folderMonthName + "\\" + folderDayName;
        //        imagepathname = targetFolderPath + "\\" + folderMonthName + "\\" + folderDayName + "\\" + ImagePathName;
        //        imagepathname = imagepathname.Replace("\\", "\\");
        //        // Convert the Base64 string to a byte array
        //        byte[] imageData = Convert.FromBase64String(attendanceapi.Image);


        //        // Save the byte array as an image file


        //        //string imagePathwithname = Server.MapPath("~/App_Data/convertedImage.png");
        //        var fileExtension = Path.GetExtension(attendanceapi.ImageName);
        //        ImagePathName = Guid.NewGuid() + fileExtension;
        //        imagePathwithname = Path.Combine(imagepathname, ImagePathName);
        //        System.IO.File.WriteAllBytes(imagePathwithname, imageData);

        //    }

        //}

        [Route("/api/SalesPersonActivityApi/CustomerAppointmentSubmitApi")]
        [HttpPost]
        public async Task<IActionResult> CustomerAppointmentSubmitApi([FromBody] SalesPersonActivityCustomerAppointmentSubmitVM appointmentsubmitapi)
        {
            try
            {
                var response = await _salesActivityRepo.SubmitCustomerAppointment(appointmentsubmitapi).ConfigureAwait(false);

                if (response != null && response != "error")
                {
                    return Ok("Appointment Submitted Successfully");
                }
                else if (response == "error")
                {
                    return BadRequest("Appointment can't be Submitted !! Check in image not Found !!");
                }
                else
                {
                    return BadRequest("Appointment can't be Submitted");
                }
            }
            catch
            {
                throw;
            }


        }
        #region Old Code CustomerAppointmentSubmitApi
        // Use the web host environment to get the content root path

        //var contentRootPath = _webHostEnvironment.ContentRootPath;
        //var imagePathName = "";

        ////var fileNameWithExtension = "";
        //var imagepathname = "";
        //string ImagePathName = "";
        //var imagepath = "";
        //string imagePathwithname = "";
        //if (appointmentsubmitapi.Image != null)
        //{
        //    //folder create
        //    var projectPath = _webHostEnvironment.ContentRootPath;
        //    string imageFolder = Path.Combine(projectPath, "UploadedFiles", "Images");
        //    if (!Directory.Exists(imageFolder))
        //    {
        //        Directory.CreateDirectory(imageFolder);
        //    }
        //    string imagePath = Path.Combine(projectPath, "UploadedFiles", "Images", "Appointment");
        //    if (!Directory.Exists(imagePath))
        //    {
        //        Directory.CreateDirectory(imagePath);
        //    }
        //    string folderYearName = DateTime.Now.Year.ToString();
        //    string folderMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        //    string folderDayName = DateTime.Now.Day.ToString();
        //    string? appointmentId = appointmentsubmitapi.CustomerAppointmentId;

        //    string targetFolderPath = Path.Combine(imagePath, folderYearName);
        //    if (!Directory.Exists(targetFolderPath))
        //    {
        //        Directory.CreateDirectory(targetFolderPath);
        //        string targetFolderPathMonth = Path.Combine(targetFolderPath, folderMonthName);
        //        Directory.CreateDirectory(targetFolderPathMonth);
        //        string targetFolderPathDay = Path.Combine(targetFolderPathMonth, folderDayName, appointmentId);
        //        Directory.CreateDirectory(targetFolderPathDay);
        //    }
        //    else
        //    {
        //        string targetFolderPathMonth = Path.Combine(targetFolderPath, folderMonthName, appointmentId);
        //        if (!Directory.Exists(targetFolderPathMonth))
        //        {
        //            Directory.CreateDirectory(targetFolderPathMonth);
        //            string targetFolderPathDay = Path.Combine(targetFolderPathMonth, folderDayName, appointmentId);
        //            Directory.CreateDirectory(targetFolderPathDay);
        //        }
        //        else
        //        {
        //            string targetFolderPathDay = Path.Combine(targetFolderPathMonth, folderDayName, appointmentId);
        //            if (!Directory.Exists(targetFolderPathDay))
        //            {
        //                Directory.CreateDirectory(targetFolderPathDay);
        //            }
        //        }
        //    }
        //    imagepath = targetFolderPath + "\\" + folderMonthName + "\\" + folderDayName + "\\" + appointmentId;
        //    imagepathname = targetFolderPath + "\\" + folderMonthName + "\\" + folderDayName + "\\" + appointmentId + "\\" + ImagePathName;
        //    imagepathname = imagepathname.Replace("\\", "\\");
        //    // Convert the Base64 string to a byte array
        //    byte[] imageData = Convert.FromBase64String(appointmentsubmitapi.Image);


        //    // Save the byte array as an image file


        //    //string imagePathwithname = Server.MapPath("~/App_Data/convertedImage.png");
        //    var fileExtension = Path.GetExtension(appointmentsubmitapi.ImageName);
        //    ImagePathName = Guid.NewGuid() + fileExtension;
        //    imagePathwithname = Path.Combine(imagepathname, ImagePathName);
        //    System.IO.File.WriteAllBytes(imagePathwithname, imageData);

        //}


        //if (appointmentsubmitapi.appoinmentImageList.Count()!=0) 
        //{
        //    // HttpContext context = HttpContext.Current;
        //    // string projectPath = context.Server.MapPath("~");


        //     var projectPath = _webHostEnvironment.ContentRootPath;

        //    // Define the image folder path
        //    //var imageFolder = Path.Combine(projectPath, "UploadedFiles", "Images");

        //    string imageFolder = Path.Combine(projectPath, "UploadedFiles", "Images");

        //    if (!Directory.Exists(imageFolder))
        //    {
        //        Directory.CreateDirectory(imageFolder);
        //    }
        //    string imagePath = Path.Combine(projectPath, "UploadedFiles", "Images", "Attendance");
        //    if (!Directory.Exists(imagePath))
        //    {
        //        Directory.CreateDirectory(imagePath);
        //    }

        //    // Create folder structure
        //    var basePath = Path.Combine(contentRootPath, "UploadedFiles", "Images", "Appoinment");
        //    if (!Directory.Exists(basePath))
        //    {
        //        Directory.CreateDirectory(basePath);
        //    }

        //    var year = DateTime.Now.Year.ToString();
        //    var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        //    var day = DateTime.Now.Day.ToString();

        //    var targetFolderPath = Path.Combine(basePath, year, month, day);

        //    if (!Directory.Exists(targetFolderPath))
        //    {
        //        Directory.CreateDirectory(targetFolderPath);
        //    }

        //        var fileExtension = Path.GetExtension(appointmentsubmitapi.ImageName);
        //        var imageFileName = $"{Guid.NewGuid()}{fileExtension}";

        //        imagePath = targetFolderPath;
        //        imagePathName = Path.Combine(targetFolderPath, imageFileName);

        //        // Convert Base64 image to byte array and save it
        //        var imageData = Convert.FromBase64String(appointmentsubmitapi.Image);
        //        System.IO.File.WriteAllBytes(imagePathName, imageData);


        //}

        //// Save attendance data into the database
        //// This is a placeholder for actual database operations.
        //// Implement your data context and save logic here.

        //return Ok(new { Status = "Success", Path = imagePathName });
        #endregion
    }

}

 


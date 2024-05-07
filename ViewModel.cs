
using AccumenSalesActivity.Models.Company;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccumenSalesActivity.Models.ViewModel
{


    public class EmployeeInfoVM
    {
        public int EmpId { get; set; }
        public int RoleId { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? EmpEmail { get; set; }

        public string? EmpMobile { get; set; }
        public string? CurrentCompanyName { get; set; }
        public int CurrentCompanyId { get; set; }
        public int ParentCompanyId { get; set; }
        public int CompanyId { get; set; }
        public int CurrentRole { get; set; }

        public int CompanyCount { get; set; }

        public List<int> AllCompanyId { get; set; }

        //Included parent
        public List<int> AllSubCompanyId { get; set; }


        public int? ParentCompanyCurrency { get; set; }

        public int? Department_Id { get; set; }
        public string? Department_Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
    public class EmployeeLoginVM
    {
        public int EmpId { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }

        public string? EmpEmail { get; set; }

        public string? EmpMobile { get; set; }

        public int? CurrentCompanyId { get; set; }

        public string? CurrentCompanyName { get; set; }


        public Status? Status { get; set; }

        public EmployeeLoginVM()
        {
            Status = new Status();
        }


    }
    public class LoginVM
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public string? Message { get; set; }
        public int? UserId { get; set; }
        public int? ShopId { get; set; }

        public string? FullName { get; set; }

        public string? EmpEmail { get; set; }

        public string? EmpMobile { get; set; }

        public int? CurrentCompanyId { get; set; }

        public string? CurrentCompanyName { get; set; }

        public Status? Status { get; set; }

        public LoginVM()
        {
            Status = new Status();
        }

    }
    public class Status
    {
        public string? Message { get; set; }
        public bool? IsSuccess { get; set; }
    }
    public class SideMenuDTO
    {
        public UrlPathDTO UrlPathDTO { get; set; }
        public EmployeeInfoVM EmployeeInfoVM { get; set; }

        public SideMenuDTO()
        {
            UrlPathDTO = new UrlPathDTO();
            EmployeeInfoVM = new EmployeeInfoVM();
        }
    }

    public class UrlPathDTO
    {
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public List<int> PathMenuIdList { get; set; }

        public UrlPathDTO()
        {
            PathMenuIdList = new List<int>();
        }

    }

    public class TailorVM
    {
        public int EmpId { get; set; }
        public int? CurrentCompanyId { get; set; }
    }
    public class ChangePasswordVM
    {
        public int EmpId { get; set; }
        public string? OldPassword { get; set; }


        public string? NewPassword { get; set; }

        public string? NewConfirmPassword { get; set; }
    }
    public class ChangePasswordDetails
    {
        public string Status { get; set; }
    }
    public class ChangePasswordDTO
    {
        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [Required(ErrorMessage = "Confirm Password is required")]
        public string NewConfirmPassword { get; set; }
    }

    public class ItemStkVM
    {

        public string? ItemName { get; set; }
        public Int64? ItemStockID { get; set; }
        public int StockLocationID { get; set; }

    }
    public class ItemDetailsDTO
    {


        public string ItemID { get; set; }
        public string ItemName { get; set; }

        public int? StockLocationId { get; set; }
        public double TotalStockQty { get; set; }

        public string Status { get; set; }



    }
    public class ItemStocksDTO
    {


        public string ItemID { get; set; }
        public string ItemName { get; set; }

        public int? StockLocationId { get; set; }

        public string Status { get; set; }

        public List<ItemStockListDTO> itemStockList { get; set; }

        public ItemStocksDTO()
        {
            itemStockList = new List<ItemStockListDTO>();
        }

    }
    public class ItemStockListDTO
    {
        public string ItemName { get; set; }
        public Int64 Barcode { get; set; }
        public int? UnitOfMeasureID { get; set; }
        public string? UnitOfMeasureName { get; set; }
        public double AvailableStock { get; set; }


    }
    public class TailorDetailsDTO
    {
        public string TailorId { get; set; }

        public string TailorName { get; set; }
        public int? CompanyId { get; set; }
        public int? TailorTypeId { get; set; }
    }
    public class DesignTempleteDTO
    {
        public int DesignTemplateId { get; set; }
        public string? DesignName { get; set; }
        public int? UnitOfMeasureId { get; set; }
        public string? UnitOfMeasureName { get; set; }
        public string ServiceId { get; set; }
    }

    public class ShopEmployeeDTO
    {
        public int EmpId { get; set; }
        public int? RoleId { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public int? CurrentCompanyId { get; set; }

    }
    public class SPAAppointmentPurposeDTO
    {
        public int PurposeId { get; set; }
        public string? PurposeName { get; set; }

    }
    public class CheckExistingCustomerDTO
    {
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ContactNumberOne { get; set; }
        public string? Email { get; set; }
        public string? CustomerAddress { get; set; }

    }

    public class ServerTimeDTO
    {
        public DateTime ServerTime { get; set; }
    }

    public class CreateCustomer
    {
        public string? CustomerId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? EntryBy { get; set; }
    }
    public class CreateSalesPersonActivity
    {
        public int ActivityBy { get; set; }
        public string? DeviceActivityId { get; set; }
        public DateTime DeviceSystemDateTime { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Notes { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public string? AppointmentId { get; set; }
        public int? TransferEmployeeId { get; set; }
        public int EntryBy { get; set; }
        public List<SalesPersonActivityItemDetailsDTO> SalesPersonActivityItemDetailsDTOList { get; set; }
        public List<SalesPersonActivitySpecDetailsDTO> SalesPersonActivitySpecDetailsDTOList { get; set; }
        public List<SalesPersonActivityTimeDetailsDTO> SalesPersonActivityTimeDetailsDTOList { get; set; }
        public List<CustomerAppointmentDTO> CustomerAppointmentDTOList { get; set; }
        public CreateSalesPersonActivity()
        {
            SalesPersonActivityItemDetailsDTOList = new List<SalesPersonActivityItemDetailsDTO>();
            SalesPersonActivitySpecDetailsDTOList = new List<SalesPersonActivitySpecDetailsDTO>();
            CustomerAppointmentDTOList = new List<CustomerAppointmentDTO>();
            SalesPersonActivityTimeDetailsDTOList = new List<SalesPersonActivityTimeDetailsDTO>();
        }
    }



    public class SalesPersonActivityViewDTO
    {
        public Int64 ActivityId { get; set; }
        public int ActivityBy { get; set; }
        public string? DeviceActivityId { get; set; }
        public string? ActivityByEmpName { get; set; }
        public DateTime DeviceSystemDateTime { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Notes { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public DateTime EntryDate { get; set; }
        public string? AppointmentId { get; set; }
        public int? TransferEmployeeId { get; set; }
        public int EntryBy { get; set; }

        public Double TotalSpendHours { get; set; }

        public string? EntryByName { get; set; }
        public List<SalesPersonActivityItemDetailsDTO> SPActivityItemDetailsViewDTOList { get; set; }
        public List<SalesPersonActivitySpecDetailsDTO> SPActivitySpecDetailsViewDTOList { get; set; }
        //  public List<CustomerAppointmentDTO> CustomerAppointmentViewDTOList { get; set; }
        public List<CustomerAppointmentDTO> SPACustomerAppointmentViewDTOList { get; set; }

        public List<SalesPersonActivityTimeDetailsDTO> SPActivityTimeDetailsDTOList { get; set; }
        public List<SalesPersonActivityTimeDetailsMainDTO> SPActivityTimeDetailsMainDTOList { get; set; }


        public SalesPersonActivityViewDTO()
        {
            SPActivityItemDetailsViewDTOList = new List<SalesPersonActivityItemDetailsDTO>();
            SPActivitySpecDetailsViewDTOList = new List<SalesPersonActivitySpecDetailsDTO>();
            SPACustomerAppointmentViewDTOList = new List<CustomerAppointmentDTO>();

            SPActivityTimeDetailsDTOList = new List<SalesPersonActivityTimeDetailsDTO>();
            SPActivityTimeDetailsMainDTOList = new List<SalesPersonActivityTimeDetailsMainDTO>();

        }
    }


    public class PreviousSPActivityViewDTO
    {
        public Int64 ActivityId { get; set; }
        public int ActivityBy { get; set; }
        public string? DeviceActivityId { get; set; }
        public string? ActivityByEmpName { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Notes { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public DateTime EntryDate { get; set; }
        public string? EntryByName { get; set; }
    }
    public class PreviousSPActivityDetailsViewDTO
    {
        public Int64 ActivityId { get; set; }
        public int ActivityBy { get; set; }
        public string? DeviceActivityId { get; set; }
        public string? ActivityByEmpName { get; set; }

        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Notes { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public DateTime EntryDate { get; set; }

        public string? EntryByName { get; set; }
        public List<SalesPersonActivityItemDetailsDTO> SPActivityItemDetailsViewDTOList { get; set; }
        public List<SalesPersonActivitySpecDetailsDTO> SPActivitySpecDetailsViewDTOList { get; set; }
        //  public List<CustomerAppointmentDTO> CustomerAppointmentViewDTOList { get; set; }
        public List<CustomerAppointmentDTO> SPACustomerAppointmentViewDTOList { get; set; }


        public PreviousSPActivityDetailsViewDTO()
        {
            SPActivityItemDetailsViewDTOList = new List<SalesPersonActivityItemDetailsDTO>();
            SPActivitySpecDetailsViewDTOList = new List<SalesPersonActivitySpecDetailsDTO>();
            SPACustomerAppointmentViewDTOList = new List<CustomerAppointmentDTO>();


        }
    }
    public class CustomerAppoinmentStatusUpdate
    {
        public Guid AppointmentId { get; set; }
        public string? CustomerAppointmentId { get; set; }
        public DateOnly? AppointmentDate { get; set; }
        public TimeOnly? AppointmentTime { get; set; }
        public DateOnly? UpdateAppointmentDate { get; set; }
        public TimeOnly? UpdateAppointmentTime { get; set; }
        public int? AppointmentStatus { get; set; }
        public int? UpdateAppointmentStatus { get; set; }

        public string? RescheduleReason { get; set; }
        public string? UpdateRescheduleReason { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
    }

    [NotMapped]
    public class SalesPersonActivityDTOList : SalesPersonActivityViewDTO
    {
        public List<SalesPersonActivityViewDTO> SPActivityList { get; set; }
        public SalesPersonActivityDTOList()
        {
            SPActivityList = new List<SalesPersonActivityViewDTO>();
        }
    }

    public class SalesPersonActivityItemDetailsDTO
    {
        public string? ItemId { get; set; }
        public string? Barcode { get; set; }
        public double? ItemQty { get; set; }
        public int IsSample { get; set; } = 0;
        public DateTime? SampleDeliveryTime { get; set; }
        public string? Remarks { get; set; }
        public string? ProvideSample { get; set; }
        public DateTime? DeviceSystemDateTime { get; set; }
        public int EntryBy { get; set; }
        public string? SAItemId { get; set; }
    }
    public class SalesPersonActivitySpecDetailsDTO
    {
        public int? DesignTemplateId { get; set; }
        public string? DesignTempName { get; set; }
        public double? SpecQty { get; set; }
        public string? Measurement { get; set; }
        public string? Remarks { get; set; }
        public DateTime? DeviceSystemDateTime { get; set; }
        public int EntryBy { get; set; }
        public string? SASpecId { get; set; }
    }

    public class SalesPersonActivityTimeDetailsDTO
    {


        public string? DeviceActivityId { get; set; }
        public DateTime ActivityTime { get; set; }
        public int WorkingStatus { get; set; }
        public int EntryBy { get; set; }


    }

    public class SalesPersonActivityTimeDetailsMainDTO
    {


        public string? DeviceActivityId { get; set; }
        public DateTime ActivityTime { get; set; }
        public TimeSpan TimeDuration { get; set; }
        public string? specData { get; set; }
        public string? Itemdata { get; set; }
        public string? WorkingDetails { get; set; }
        public int WorkingStatus { get; set; }
        public int EntryBy { get; set; }


    }

    public class CustomerAppointmentDTO
    {
        public Guid AppointmentId { get; set; }
        public string? CustomerAppoinmentId { get; set; }
        public Int64 ActivityId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public int? PurposeId { get; set; }
        public string? Purpose { get; set; }
        public string? Note { get; set; }
        public DateOnly? AppointmentDate { get; set; }
        public TimeOnly? AppointmentTime { get; set; }
        public int? AssignEmployeeId { get; set; }
        public int? AppointmentStatus { get; set; }
        public string? AppointmentStatusName { get; set; }
        public string? AssignEmployeeName { get; set; }
        public string? RescheduleReason { get; set; }

        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
    }


    public class SalesPersonActivityCustomerAppointmentSubmitVM
    {
        public Guid AppointmentId { get; set; }
        public string? CustomerId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public float CheckInLatitude { get; set; }
        public float CheckInLongitude { get; set; }
        public string? CheckInImage { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public float CheckOutLatitude { get; set; }
        public float CheckOutLongitude { get; set; }

        public string? CheckOutImage { get; set; }
        public int? IsReschedule { get; set; }
        public DateOnly? RescheduleDate { get; set; }
        public TimeOnly? RescheduleTime { get; set; }
        public string? RescheduleReason { get; set; }
        public int? Status { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }


        public List<SalesPersonActivityCustomerAppointmentNotesVM> appoinmentNoteVMList { get; set; }
        public List<SalesPersonActivityCustomerAppointmentImageVM> appoinmentImageVMList { get; set; }

        public SalesPersonActivityCustomerAppointmentSubmitVM()
        {
            appoinmentImageVMList = new List<SalesPersonActivityCustomerAppointmentImageVM>();
            appoinmentNoteVMList = new List<SalesPersonActivityCustomerAppointmentNotesVM>();
        }
    }


    public class SalesPersonActivityCustomerAppointmentImageVM
    {
        public Guid AppointmentImageId { get; set; }
        public string? AppointmentImage { get; set; }
        public int? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
    }
    public class SalesPersonActivityCustomerAppointmentNotesVM
    {
        public Guid AppointmentNotesId { get; set; }
        public string? Notes { get; set; }
        public int? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}


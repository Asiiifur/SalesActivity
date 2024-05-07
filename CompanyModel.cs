using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static AccumenSalesActivity.Models.ViewModel.SalesPersonActivityCustomerAppointmentSubmitVM;

namespace AccumenSalesActivity.Models.Company
{
    public class Company
    {
        [Key]
        public int Entity_id { get; set; }

        [Required(ErrorMessage = "Entity Name is required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Entity_Name { get; set; }
        public int Level { get; set; }
        public int Ref_level { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Entity_Code { get; set; }
        [Required(ErrorMessage = "Email is required")]

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone No is required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string PhoneNo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(350)]
        public string Website { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(300)]
        public string Co_Reg_Num { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(300)]
        public string VAT_GST { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Bill_Street { get; set; }
        public int? Bill_Div { get; set; }//DDL
        public int? Bill_City { get; set; }//DDL
        public int? Bill_Thana { get; set; }//DDL

        public int? Bill_Area { get; set; }//DDL

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Bill_PostCode { get; set; }

        //[Required(ErrorMessage = "Country Name is required")]
        public int? Bill_Country { get; set; }//DDL
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Mail_Street { get; set; }
        public int? Mail_City { get; set; }//DDL
        public int? Mail_Div { get; set; }//DDL
        public int? Mail_Thana { get; set; }//DDL
        public int? Mail_Area { get; set; }//DDL

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Mail_PostCode { get; set; }
        public int? Mail_Country { get; set; }//DDL
        public int? Status { get; set; }
        public int? entry_by { get; set; }
        public DateTime modify_datetime { get; set; }
        public int? EntityTypeId { get; set; }//ddl 

        [Column(TypeName = "VARCHAR")]
        [StringLength(7)]
        public string EntityShortName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string ShortCode { get; set; }


        public int? CurrencyId { get; set; }//DDL Currency

        public int? FinancialYearId { get; set; }//DDL FinancialYear
        public int? SameAsPrevious { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> EntityTypes { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> LocationDivisions { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> LocationCiteis { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> LocationThanas { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Countries { get; set; }

        public string Logo { get; set; }

    }
    public class Item
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string ItemId { get; set; }

        /// <summary>
        /// 1 = Item , 2 = Service
        /// </summary>
        [Required(ErrorMessage = "Type Name is required")]

        public int TypeId { get; set; }
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Group is not found")]
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Sub Group is not found")]
        public int SubGroupId { get; set; }
        [Required(ErrorMessage = "Item Name is required")]
        [MaxLength(200)]
        [Column(TypeName = "VARCHAR")]
        public string ItemName { get; set; }
        public int? Location { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string Sku { get; set; }
        public int? SkuGeneradeCode { get; set; }
        public int? BrandId { get; set; }
        public int? PatternId { get; set; }
        public int? DimensionId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string ManufCode { get; set; }
        public int? CountryId { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "VARCHAR")]
        public string Specification { get; set; }
        public int? ReorderLevel { get; set; }
        public double? Cogs { get; set; }
        public double? TradePrice { get; set; }
        public int? Discount { get; set; }
        public double? DiscountPercentage { get; set; }
        public double? DiscountAmount { get; set; }
        public int? Commission { get; set; }
        public double? CommissionPercentage { get; set; }
        public double? CommissionAmount { get; set; }
        public int? IsVat { get; set; }
        public int? Vat { get; set; }
        public double? VatPrecentage { get; set; }
        public double? VatAmount { get; set; }
        public int? IsTax { get; set; }
        public int? Tax { get; set; }
        public double? TaxPercentage { get; set; }
        public double? TaxAmount { get; set; }
        [MaxLength(750)]
        [Column(TypeName = "VARCHAR")]
        public string QRCode { get; set; }
        public long? Barcode { get; set; }
        public int? Active { get; set; }
        public int? EntryBy { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string EntryDate { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string VendorId { get; set; }
        public int? ColorId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string ItemImage { get; set; }
        [Required(ErrorMessage = "Unit Of Measure is not found")]
        public int? UnitOfMeasureId { get; set; }
        //vat tax type
        //public int? Type { get; set; }



    }
    public class ItemStock
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //Same as Item  Received roll Id
        public Int64 ItemStockID { get; set; }



        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ItemID { get; set; }


        [Key, Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? CompanyID { get; set; }
        public string ReceiveId { get; set; }



        public double StockQuantity { get; set; }




        public int? UnitOfMeasureID { get; set; }

        [Key, Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? StockLocationID { get; set; }


        public int? PurchasedForId { get; set; }

        public int? EntryBy { get; set; }

        public DateTime EntryDate { get; set; }

        public double? COGS { get; set; }



    }
    public class UnitOfMeasure
    {
        [Key]
        public int UnitOfMeasureId { get; set; }

        [Required(ErrorMessage = "UoM Name is required")]
        [MaxLength(150)]
        [Column(TypeName = "VARCHAR")]
        public string UnitOfMeasureName { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string UoMAbbreviation { get; set; }
        public int? Active { get; set; }
        public int? EntryBy { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string EntryDate { get; set; }

        public int? CompanyId { get; set; }
    }
    public class Tailor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TailorId { get; set; }
        public int? CompanyId { get; set; }
        public int? EntityId { get; set; }
        public int? TailorTypeId { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string Street { get; set; }
        public int? DivisionId { get; set; }
        public int? CityId { get; set; }
        public int? ThanaId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string PostCode { get; set; }
        public int? CountryId { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "VARCHAR")]
        public string ContactNumberOne { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "VARCHAR")]
        public string ContactNumberTwo { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "VARCHAR")]
        public string Email { get; set; }
        public int? SameAsPrevious { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string BillingStreet { get; set; }
        public int? BillingDivisionId { get; set; }
        public int? BillingCityId { get; set; }
        public int? BillingThanaId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string BillingPostCode { get; set; }
        public int? BillingCountryId { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "VARCHAR")]
        public string BillingContactNumberOne { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "VARCHAR")]
        public string BillingContactNumberTwo { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "VARCHAR")]
        public string BillingEmail { get; set; }
        [MaxLength(150)]
        [Column(TypeName = "VARCHAR")]
        public string ContactName { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "VARCHAR")]
        public string CContactNumberOne { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "VARCHAR")]
        public string CContactNumberTwo { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "VARCHAR")]
        public string ContactEmail { get; set; }
        public double? CreditLimit { get; set; }
        public double? PercentageOfSales { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string PercentageOfTailoringService { get; set; }

        public double? PercentageOfTailoringServiceAmount { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "VARCHAR")]
        public string Description { get; set; }
        public int? Active { get; set; }
        public int? EntryBy { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string EntryDate { get; set; }
    }

    public class DesignTemplate
    {
        //[Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int DesignTemplateId { get; set; }
        public int? CompanyId { get; set; }
        public int? GroupId { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "VARCHAR")]
        public string DesignName { get; set; }
        public int? UnitOfMeasureId { get; set; }
        public double? Price { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "VARCHAR")]
        public string Description { get; set; }
        public int? Active { get; set; }
        public int? EntryBy { get; set; }
        public DateTime EntryDate { get; set; }

        public string ServiceId { get; set; }
    }

    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? CustomerId { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public int CompanyId { get; set; }//
        public int? SubCompanyId { get; set; }//
        public int? EntityTypeId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string? CustomerQRCode { get; set; }
        public long? CustomerBarCode { get; set; }

        public int? CustomerType { get; set; }


        [Column(TypeName = "VARCHAR")]
        [MaxLength(150)]
        public string? CustomerName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(100)]
        public string? DesignationOrIndustry { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(150)]
        public string? Street { get; set; }
        public int? DivisionId { get; set; }
        public int? CityId { get; set; }
        public int? ThanaId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string? PostCode { get; set; }
        public int? CountryId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string? ContactNumberOne { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string? ContactNumberTwo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(100)]
        public string? Email { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(150)]
        public string? BillingStreet { get; set; }
        public int? BillingDivisionId { get; set; }
        public int? BillingCityId { get; set; }
        public int? BillingThanaId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string? BillingPostCode { get; set; }
        public int? BillingCountryId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string? BillingContactNumberOne { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string? BillingContactNumberTwo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(100)]
        public string? BillingEmail { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(150)]
        public string? ContactName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string? CContactNumberOne { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string? CContactNumberTwo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(100)]
        public string? ContactEmail { get; set; }
        public double? CreditLimit { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(500)]
        public string? Description { get; set; }
        public int Active { get; set; }
        public int EntryBy { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string? EntryDate { get; set; }
        public int? SameAsPrevious { get; set; }
        public int? AreaId { get; set; }
        public int? BillingAreaId { get; set; }

    }

    public class SalesPersonActivity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 ActivityId { get; set; }
        [Required(ErrorMessage = "")]
        public int ActivityBy { get; set; }
        [MaxLength(50)]
        public string? DeviceActivityId { get; set; }
        public DateTime DeviceSystemDateTime { get; set; }
        [MaxLength(50)]
        public string? CustomerId { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        [MaxLength(50)]
        public string? AppointmentId { get; set; }
        public int? TransferEmployeeId { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public ICollection<SalesPersonActivityItemDetails> SalesPersonActivityItemDetailsList { get; set; }
        public ICollection<SalesPersonActivitySpecDetails> SalesPersonActivitySpecDetailsList { get; set; }
        public ICollection<SalesPersonActivityCustomerAppointment> CustomerAppointmentList { get; set; }

        public ICollection<SalesPersonActivityTimeDetails> SalesPersonActivityTimeDetailsList { get; set; }
        public SalesPersonActivity()
        {
            SalesPersonActivityItemDetailsList = new List<SalesPersonActivityItemDetails>();
            SalesPersonActivitySpecDetailsList = new List<SalesPersonActivitySpecDetails>();
            CustomerAppointmentList = new List<SalesPersonActivityCustomerAppointment>();

            SalesPersonActivityTimeDetailsList = new List<SalesPersonActivityTimeDetails>();
        }
    }
    public class SalesPersonActivityItemDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ActivityItemDetailsId { get; set; }
        public Int64 ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public virtual SalesPersonActivity SalesPersonActivitys { get; set; }
        [MaxLength(50)]
        public string? ItemId { get; set; }
        [MaxLength(50)]
        public string? Barcode { get; set; }
        public double? ItemQty { get; set; }
        public int IsSample { get; set; } = 0;

        public DateTime? SampleDeliveryTime { get; set; }
        [MaxLength(500)]
        public string? Remarks { get; set; }
        public DateTime? DeviceSystemDateTime { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string? SAItemId { get; set; }
    }
    public class SalesPersonActivitySpecDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ActivityISpecDetailsId { get; set; }
        public Int64 ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public virtual SalesPersonActivity SalesPersonActivitys { get; set; }
        public int? DesignTemplateId { get; set; }
        public double? SpecQty { get; set; }
        [MaxLength(50)]
        public string? Measurement { get; set; }
        [MaxLength(500)]
        public string? Remarks { get; set; }
        public DateTime? DeviceSystemDateTime { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string? SASpecId { get; set; }
    }


    public class SalesPersonActivityCustomerAppointment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AppointmentId { get; set; }

        public string? CustomerAppoinmentId { get; set; }
        public Int64 ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public SalesPersonActivity SalesPersonActivitys { get; set; }
        [MaxLength(50)]
        public string? CustomerId { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }

        public DateOnly? AppointmentDate { get; set; }
        public TimeOnly? AppointmentTime { get; set; }

        public int? PurposeId { get; set; }
        public string? Note { get; set; }
        public int? AssignEmployeeId { get; set; }
        public int? AppointmentByEmployeeId { get; set; }
        public int? AppointmentStatus { get; set; }
        public DateOnly? UpdateAppointmentDate { get; set; }
        public TimeOnly? UpdateAppointmentTime { get; set; }
        public string? RescheduleReason { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
    }

    public class SalesPersonActivityCustomerAppointmentStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AppointmentStatusUpdateId { get; set; }
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
    public class SalesPersonActivityAppointmentPurpose
    {
        [Key]
        public int PurposeId { get; set; }
        public string? PurposeName { get; set; }

    }

    //public class TransferredSalesActivity
    //{
    //    [Key]
    //    [MaxLength(50)]
    //    public string TransferActivityId { get; set; } = string.Empty;
    //    public Int64 ActivityId { get; set; }
    //}

    public class SalesPersonActivityCustomerAppointmentSubmit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid SubmittedAppointmentId { get; set; }
        public Guid AppointmentId { get; set; }
        public string? CustomerId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public float CheckInLatitude { get; set; }
        public float CheckInLongitude { get; set; }

        public string? CheckInImageFileName { get; set; }
        public string? CheckInImagePath { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public float CheckOutLatitude { get; set; }
        public float CheckOutLongitude { get; set; }
        public string? CheckOutImagePath { get; set; }
        public string? CheckOutImageFileName { get; set; }
        public int? IsReschedule { get; set; }
        public DateOnly? RescheduleDate { get; set; }
        public TimeOnly? RescheduleTime { get; set; }
        public string? RescheduleReason { get; set; }
        public int? Status { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }

    
        public ICollection<SalesPersonActivityCustomerAppointmentSubmitNotes> customerAppoinmentNoteList { get; set; }
        public ICollection<SalesPersonActivityCustomerAppointmentSubmitImages> customerAppoinmentImageList { get; set; }

        public SalesPersonActivityCustomerAppointmentSubmit()
        {
            customerAppoinmentImageList = new List<SalesPersonActivityCustomerAppointmentSubmitImages>();
            customerAppoinmentNoteList = new List<SalesPersonActivityCustomerAppointmentSubmitNotes>();
        }
    }

    public class SalesPersonActivityCustomerAppointmentSubmitImages
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AppointmentImageId { get; set; }

        public Guid SubmittedAppointmentId { get; set; }
        [ForeignKey("SubmittedAppointmentId")]
        public virtual SalesPersonActivityCustomerAppointmentSubmit SalesPersonActivityCustomerAppointmentSubmits { get; set; }
        public Guid AppointmentId { get; set; }
        public string? Path { get; set; }
        public string? FileName { get; set; }
        public int? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
    }
    public class SalesPersonActivityCustomerAppointmentSubmitNotes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AppointmentNotesId { get; set; }
        public Guid SubmittedAppointmentId { get; set; }
        [ForeignKey("SubmittedAppointmentId")]
        public virtual SalesPersonActivityCustomerAppointmentSubmit SalesPersonActivityCustomerAppointmentSubmits { get; set; }
        public Guid AppointmentId { get; set; }
        public string? Notes { get; set; }
        public int? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
    }


    public class SalesPersonActivityTimeDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ActivityTimeDetailsId { get; set; }

        public Int64 ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public SalesPersonActivity SalesPersonActivitys { get; set; }

        [MaxLength(50)]
        public string? DeviceActivityId { get; set; }

        public DateTime ActivityTime { get; set; }

        public int WorkingStatus { get; set; }

        public int EntryBy { get; set; }

        public DateTime EntryDate { get; set; }

    }

}

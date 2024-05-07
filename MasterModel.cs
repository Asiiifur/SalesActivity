using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccumenSalesActivity.Models.Master
{

    public class Employees
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmpId { get; set; }//EmpId
        public int? CompanyId { get; set; }
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VerId { get; set; }//VerId

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required(ErrorMessage = "Domain Name is required")]
        public string? EmpDName { get; set; }//EmpDName

        [Required(ErrorMessage = "Employee Code is required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? EmpCode { get; set; }//EmpCode

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required(ErrorMessage = "First Name is required")]
        public string? EmpFName { get; set; }//EmpFName

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? EmpMName { get; set; }//EmpMName

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? EmpLName { get; set; }//EmpLName

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required(ErrorMessage = "Email Id is required")]
        public string? EmpEmail { get; set; }//EmpEmail

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? EmpMobile { get; set; }//EmpMobile

        public int? EmpDept { get; set; }//EmpMasterDepartment//  EmpDept

        public int? EmpDesignation { get; set; }//EmpMasterDesignation//  EmpDesignation

        public int? EmpRole { get; set; } //EmpMasterRole//  EmpRole

        public int? CountryId { get; set; }//CountryId

        public int? BUId { get; set; }//SalesStructureBU //BUId

        public int? SalesLineId { get; set; } //SalesStructureSL//SalesLineId

        public int? RegionId { get; set; } // SalesStructureRegion//RegionId

        public int? TeamId { get; set; } // SalesStructureTeam//TeamId

        public int? TerritoryId { get; set; } // SalesStructureTerritory //TerritoryId

        public DateTime? JoiningDate { get; set; }//JoiningDate

        public DateTime? SeparationDate { get; set; }//SeparationDate

        public int EmpActive { get; set; }//EmpActive

        public int EntryBy { get; set; } // Employee //EntryBy



        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? EntryDate { get; set; }//EntryDate

        public int IsLatest { get; set; }//IsLatest

        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string? EmpPhoto { get; set; }//EmpPhoto

        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string? WorkDay { get; set; }

        public int? Gender_Id { get; set; }

        //public int? Gender_Id { get; set; }//MasterGender
        public int? empCategory_Id { get; set; }//EmpMasterCategory
        public int? Status { get; set; }
        public int? SODPrivilegeId { get; set; } // Sales Dellivery 30 Days Privilege
        public int? SOPrivilegeId { get; set; } // Sales Order Complete Days Privilege
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string? LengthOfService { get; set; }
        public DateTime? LastDateOfPromotion { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? MailingAddress { get; set; }
        public int? HomeDistrict { get; set; }//LocationCity

        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string? EducationQualification { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string? PreviousExperience { get; set; }
        public string? DepotId { get; set; }//SalesCenter

        public DateTime? LastDateOfTranser { get; set; }
        public int? Religion_Id { get; set; }//MasterReligion
        public int? MaritalStatus_Id { get; set; }//MasterMaritalStatus
        [Required(ErrorMessage = "Company is required")]

        public int? CurrentCompanyId { get; set; }//MasterMaritalStatus




        [StringLength(1000, MinimumLength = 6)]
        [Required(ErrorMessage = "Password is required")]

        public string? Password { get; set; }//Password
                                             //public IEnumerable<SelectListItem> SalesStructureBUs { get; set; }
                                             //public IEnumerable<SelectListItem> MasterMaritalStatuss { get; set; }
                                             //public IEnumerable<SelectListItem> MasterReligions { get; set; }
                                             //public IEnumerable<SelectListItem> GenderNames { get; set; }
                                             //public IEnumerable<SelectListItem> empMasterCategorys { get; set; }
                                             //public IEnumerable<SelectListItem> locationCitys { get; set; }
                                             //public IEnumerable<SelectListItem> salesCenters { get; set; }
                                             //public IEnumerable<SelectListItem> salesStructureTerritories { get; set; }
                                             //public IEnumerable<SelectListItem> salesStructureTeams { get; set; }
                                             //public IEnumerable<SelectListItem> salesStructureRegions { get; set; }
                                             //public IEnumerable<SelectListItem> salesStructureSLs { get; set; }
                                             //public IEnumerable<SelectListItem> empMasterRoles { get; set; }
                                             //public IEnumerable<SelectListItem> empMasterDesignations { get; set; }
                                             //public IEnumerable<SelectListItem> empMasterDepartmens { get; set; }
        [NotMapped]
        public string? FullName { get; set; }
        public string? GetFullName()
        {
            FullName = this.EmpFName + " " + this.EmpMName + " " + this.EmpLName;
            return FullName;
        }

    }
    public class Menutbl
    {
        [Key]
        public int MenuId { get; set; }

        [Required(ErrorMessage = "Menu Name is required")]
        [StringLength(200)]
        public string? Name { get; set; }

        public int Level { get; set; }
        public int Ref { get; set; }
        public int Order { get; set; }

        [StringLength(200)]
        public string? Url { get; set; }

        [StringLength(200)]
        public string? ImageLoc { get; set; }

        public int Active { get; set; }

        public int EntryBy { get; set; }

        public DateTime EntryDate { get; set; }
        [StringLength(200)]
        public string? Title { get; set; }

        public int? Default { get; set; }
        public int? Header { get; set; }

        [StringLength(200)]
        public string? AreaName { get; set; }
        [StringLength(200)]
        public string? ControllerName { get; set; }
        [StringLength(200)]
        public string? ActionName { get; set; }

        public int? IsAction { get; set; }
    }
    public class Privilegetbl
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleId { get; set; }
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuId { get; set; }
    }
    public class LocationThana
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ThanaId { get; set; }

        [Required(ErrorMessage = "Thana Name is required")]
        [MaxLength(100)]
        [Column(TypeName = "VARCHAR")]
        public string ThanaName { get; set; }

        [MaxLength(7)]
        [Column(TypeName = "VARCHAR")]
        public string ThanaShortName { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "VARCHAR")]
        public string ThanaDescription { get; set; }

        [Required(ErrorMessage = "Thana Geo Code is required")]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string ThanaGeoCode { get; set; }

        public int? ThanaOrder { get; set; }

        public int ThanaActive { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual LocationCity LocationCity { get; set; }
        public int EntryBy { get; set; }

        [MaxLength(50)]
        public string EntryDate { get; set; }

    }
    public class LocationCity
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CityId { get; set; }
        [Required(ErrorMessage = "City Name is required")]
        [MaxLength(100)]
        [Column(TypeName = "VARCHAR")]
        public string CityName { get; set; }

        [MaxLength(7)]
        [Column(TypeName = "VARCHAR")]
        public string CityShortName { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "VARCHAR")]
        public string CityDescription { get; set; }

        [Required(ErrorMessage = "City Geo Code is required")]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string CityGeoCode { get; set; }

        public int? CityOrder { get; set; }

        public int CityActive { get; set; }

        [Required(ErrorMessage = "Division Name is required")]
        public int DivisionId { get; set; }

    
        public int EntryBy { get; set; }

        [MaxLength(50)]
        public string EntryDate { get; set; }

    }
    public class LocationDivision
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DivisionId { get; set; }

        [Required(ErrorMessage = "Division Name is required")]
        [MaxLength(100)]
        [Column(TypeName = "VARCHAR")]
        public string DivisionName { get; set; }

        [MaxLength(7)]
        [Column(TypeName = "VARCHAR")]
        public string DivisionShortName { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "VARCHAR")]
        public string DivisionDescription { get; set; }


        [Required(ErrorMessage = "Division Geo Code is required")]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string DivisionGeoCode { get; set; }

        public int? DivisionOrder { get; set; }

        public int DivisionActive { get; set; }

        public int EntryBy { get; set; }



        [MaxLength(50)]
        public string EntryDate { get; set; }
    }
}

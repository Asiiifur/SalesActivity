using AccumenSalesActivity.Data;
using AccumenSalesActivity.Models.Company;
using AccumenSalesActivity.Models.Master;
using AccumenSalesActivity.Models.ViewModel;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AccumenSalesActivity.Repository
{
    public class CompanyMasterRepository : ICompanyMasterRepository
    {
        private readonly CompanyDBConnection _cdb;
        private readonly MasterDBConnection _db;

        public CompanyMasterRepository(CompanyDBConnection cdb, MasterDBConnection db)
        {
            _cdb = cdb;
            _db = db;
        }


        public async Task<List<TailorDetailsDTO>> GetShopTailorDetails(int EmpId, int? CompanyId)
        {
            // TailorDetailsDTO detailsDTO = new TailorDetailsDTO();
            List<TailorDetailsDTO> tailorDetailslist = new List<TailorDetailsDTO>();



            if (CompanyId != 0)
            {


                tailorDetailslist = (from tt in _cdb.Tailor.Where(x => x.EntityId == CompanyId && x.Active == 1)

                                     select new TailorDetailsDTO
                                     {
                                         TailorId = tt.TailorId,
                                         TailorTypeId = tt.TailorTypeId,
                                         TailorName = tt.Name,
                                         CompanyId = tt.EntityId,

                                     }).ToList();


            }
            else
            {
                tailorDetailslist = (from tt in _cdb.Tailor.Where(x => x.Active == 1)

                                     select new TailorDetailsDTO
                                     {
                                         TailorId = tt.TailorId,
                                         TailorTypeId = tt.TailorTypeId,
                                         TailorName = tt.Name,
                                         CompanyId = tt.EntityId,

                                     }).ToList();
            }

            return tailorDetailslist;
        }

        public async Task<List<DesignTempleteDTO>> GetDesignTempleteDetails(int EmpId)
        {

            List<DesignTempleteDTO> designTempletelist = new List<DesignTempleteDTO>();

            var CompanyId = await _db.Employees.Where(x => x.EmpId == EmpId).Select(x => x.CompanyId).FirstOrDefaultAsync();

            if (CompanyId != 0)
            {


                designTempletelist = (from tt in _cdb.DesignTemplate.Where(x => x.CompanyId == CompanyId && x.Active == 1)
                                      join uom in _cdb.UnitOfMeasure on tt.UnitOfMeasureId equals uom.UnitOfMeasureId



                                      select new DesignTempleteDTO
                                      {
                                          DesignTemplateId = tt.DesignTemplateId,
                                          DesignName = tt.DesignName,
                                          UnitOfMeasureId = tt.UnitOfMeasureId,
                                          UnitOfMeasureName = uom.UnitOfMeasureName,
                                          ServiceId = tt.ServiceId,

                                      }).ToList();


            }
            else
            {
                designTempletelist = (from tt in _cdb.DesignTemplate.Where(x => x.Active == 1)
                                      join uom in _cdb.UnitOfMeasure on tt.UnitOfMeasureId equals uom.UnitOfMeasureId


                                      select new DesignTempleteDTO
                                      {
                                          DesignTemplateId = tt.DesignTemplateId,
                                          DesignName = tt.DesignName,
                                          UnitOfMeasureName = uom.UnitOfMeasureName,
                                          UnitOfMeasureId = tt.UnitOfMeasureId,
                                          ServiceId = tt.ServiceId,

                                      }).ToList();
            }

            return designTempletelist;
        }
        public async Task<List<ShopEmployeeDTO>> GetShopsEmployeeDetails(int EmpId)
        {

            List<ShopEmployeeDTO> shopEmployeelist = new List<ShopEmployeeDTO>();

            var currentCompanyId = await _db.Employees.Where(x => x.EmpId == EmpId).Select(x => x.CurrentCompanyId).FirstOrDefaultAsync();

            if (currentCompanyId != 0)
            {


                shopEmployeelist = (from tt in _db.Employees.Where(x => x.CurrentCompanyId == currentCompanyId && x.EmpActive == 1)


                                    select new ShopEmployeeDTO
                                    {
                                        EmpId = tt.EmpId,
                                        FullName = tt.EmpFName + " " + tt.EmpMName + " " + tt.EmpLName,
                                        Username = tt.EmpDName,
                                        RoleId = tt.EmpRole,
                                        CurrentCompanyId = tt.CurrentCompanyId,

                                    }).ToList();


            }
            else
            {
                shopEmployeelist = (from tt in _db.Employees.Where(x => x.EmpActive == 1 && x.CompanyId == 1)


                                    select new ShopEmployeeDTO
                                    {
                                        EmpId = tt.EmpId,
                                        FullName = tt.EmpFName + " " + tt.EmpMName + " " + tt.EmpLName,
                                        Username = tt.EmpDName,
                                        RoleId = tt.EmpRole,
                                        CurrentCompanyId = tt.CurrentCompanyId,

                                    }).ToList();
            }

            return shopEmployeelist;
        }



        public async Task<List<SPAAppointmentPurposeDTO>> GetSPAAppointmentPurposeList()
        {




         var appointmentPurposeList = await(from tt in _cdb.SalesPersonActivityAppointmentPurpose
                                       select new SPAAppointmentPurposeDTO
                                       {
                                        PurposeId = tt.PurposeId,
                                        PurposeName = tt.PurposeName
                                          
                                      }).ToListAsync();


            return appointmentPurposeList;
        }
public async Task<List<CheckExistingCustomerDTO>> GetExistingCustomerDetails(string? customerNameOrNumber)
        {

            List<CheckExistingCustomerDTO> checkCustomerlist = new List<CheckExistingCustomerDTO>();


            try
            {

                var query = _cdb.Customer
                    .Where(c =>
                        (!string.IsNullOrEmpty(customerNameOrNumber) && c.CustomerName != null && c.CustomerName.Contains(customerNameOrNumber)) ||
                        (!string.IsNullOrEmpty(customerNameOrNumber) && c.ContactNumberOne != null && c.ContactNumberOne.Contains(customerNameOrNumber))
                    );
              

                checkCustomerlist = query.AsEnumerable().Select(tt => new CheckExistingCustomerDTO
                {
                    CustomerId = tt.CustomerId,
                    CustomerName = tt.CustomerName,
                    ContactNumberOne = tt.ContactNumberOne,
                    Email = tt.Email,
                    CustomerAddress = GetCustomerAddress(tt.CustomerId),
                }).Take(10).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }




            return checkCustomerlist;
        }

        public async Task<List<CheckExistingCustomerDTO>> CheckExistingCustomer(string? customerName, string? customerPhoneNo)
        {

            List<CheckExistingCustomerDTO> checkCustomerlist = new List<CheckExistingCustomerDTO>();


            try
            {

                var query = _cdb.Customer
                    .Where(c =>
                        (!string.IsNullOrEmpty(customerName) && c.CustomerName != null && c.CustomerName == customerName) &&
                        (!string.IsNullOrEmpty(customerPhoneNo) && c.ContactNumberOne != null && c.ContactNumberOne.Contains(customerPhoneNo)));


                checkCustomerlist = query.AsEnumerable().Select(tt => new CheckExistingCustomerDTO
                {
                    CustomerId = tt.CustomerId,
                    CustomerName = tt.CustomerName,
                    ContactNumberOne = tt.ContactNumberOne,
                    Email = tt.Email,
                    CustomerAddress = GetCustomerAddress(tt.CustomerId),
                }).Take(10).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }




            return checkCustomerlist;
        }

        public string GetCustomerAddress(string id)
        {
            var customer = _cdb.Customer.Where(x => x.CustomerId == id).FirstOrDefault();


            string comma = "";
            string address = "";
            if (customer.Street != null)
            {
                address += comma + customer.Street;
                comma = ", ";
            }

            if (customer.ThanaId != null)
            {
                address += comma + _db.LocationThana.Where(x => x.ThanaId == customer.ThanaId).FirstOrDefault().ThanaName;
                comma = ", ";
            }
            if (customer.CityId != null)
            {
                address += comma + _db.LocationCity.Where(x => x.CityId == customer.CityId).FirstOrDefault().CityName;
                comma = ", ";
            }
            if (customer.DivisionId != null)
            {
                address += comma + _db.LocationDivision.Where(x => x.DivisionId == customer.DivisionId).FirstOrDefault().DivisionName;
                comma = ", ";
            }


            return address;

        }

        public async Task<CreateCustomer> CreateCustomerApi(CreateCustomer customerrequest)
        {

            //var entrybyid = _db.Employees.Where(x => x.EmpCode == customerrequest.EntryBy).Select(x => x.EmpId).FirstOrDefault();

            var customernewid = getCustomerNewId();
            Customer customer = new Customer();
            customer.CustomerId = customernewid;
            customer.CompanyId = 1;
            customer.CustomerName = customerrequest.Name;
            customer.ContactNumberOne = customerrequest.PhoneNo;
            customer.Email = customerrequest.Email;
            customer.Active = 1;
            customer.Street = customerrequest.Address;
            customer.EntryDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            customer.EntryBy = Int32.Parse(customerrequest.EntryBy);
            _cdb.Add(customer);
            _cdb.SaveChanges();

            customerrequest.CustomerId = customernewid;



            return customerrequest;
        }
        public string getCustomerNewId()
        {

            int count = _cdb.Customer.Count();
            var NewCustomerId = "C-" + "100000001";
            if (count == 0)
            {
                return NewCustomerId;
            }
            else
            {
                //var DataList = _cdb.Customer.Where(x => x.CustomerId != "Default").ToList();
                var DataList = _cdb.Customer
                       .Where(x => x.CustomerId != "Default")
                       .OrderByDescending(x => x.CustomerId)
                       .Take(10)
                       .ToList();
                var LastCustomerId = DataList.Max(x => x.CustomerId);
                string[] parts = LastCustomerId.Split('-');
                int NewCustomer = Int32.Parse(parts[1]) + 1;
                NewCustomerId = "C-" + NewCustomer;
                return NewCustomerId;
            }

        }
    }





}

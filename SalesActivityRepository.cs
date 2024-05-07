using AccumenSalesActivity.Data;
using AccumenSalesActivity.Models.Company;
using AccumenSalesActivity.Models.Master;
using AccumenSalesActivity.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace AccumenSalesActivity.Repository
{
    public class SalesActivityRepository(CompanyDBConnection cdb, MasterDBConnection db, IWebHostEnvironment webHostEnvironment) : ISalesActivityRepository
    {
        private readonly CompanyDBConnection _cdb = cdb;
        private readonly MasterDBConnection _db = db;
        public IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        public async Task<string> CreateSalesPersonActivityApi(CreateSalesPersonActivity salesPerson)
        {
            var activityId = GenerateId();
          
            SalesPersonActivity entity = new SalesPersonActivity()
            {
                ActivityId = activityId,
                ActivityBy = salesPerson.ActivityBy,
                DeviceActivityId = salesPerson.DeviceActivityId,
                DeviceSystemDateTime = salesPerson.DeviceSystemDateTime,
                CustomerId = salesPerson.CustomerId,
                Notes = salesPerson.Notes,
                ActivityStartTime = salesPerson.ActivityStartTime,
                ActivityEndTime = salesPerson.ActivityEndTime,
                AppointmentId = salesPerson.AppointmentId,
                TransferEmployeeId = salesPerson.TransferEmployeeId,
                EntryBy = salesPerson.EntryBy,
                EntryDate = DateTime.Now,
            };

            #region for multiple item details added by Sales person Activity
            for (int i = 0; i < salesPerson.SalesPersonActivityItemDetailsDTOList.Count(); i++)
            {
                SalesPersonActivityItemDetails itemDetailsEntity = new SalesPersonActivityItemDetails()
                {
                    ActivityId = activityId,
                    ActivityItemDetailsId = Guid.NewGuid(),
                    ItemId = salesPerson.SalesPersonActivityItemDetailsDTOList[i].ItemId,
                    Barcode = salesPerson.SalesPersonActivityItemDetailsDTOList[i].Barcode,
                    ItemQty = salesPerson.SalesPersonActivityItemDetailsDTOList[i].ItemQty,
                    IsSample = salesPerson.SalesPersonActivityItemDetailsDTOList[i].IsSample,
                    SampleDeliveryTime = salesPerson.SalesPersonActivityItemDetailsDTOList[i].SampleDeliveryTime,
                    Remarks = salesPerson.SalesPersonActivityItemDetailsDTOList[i].Remarks,
                    DeviceSystemDateTime = salesPerson.SalesPersonActivityItemDetailsDTOList[i].DeviceSystemDateTime,
                    EntryBy = salesPerson.SalesPersonActivityItemDetailsDTOList[i].EntryBy,
                    EntryDate = DateTime.Now,
                    SAItemId = "Itm" + (i + 1)
                };
                entity.SalesPersonActivityItemDetailsList.Add(itemDetailsEntity);
            }
            #endregion

            #region for multiple Spec details added by Sales person Activity
            for (int i = 0; i < salesPerson.SalesPersonActivitySpecDetailsDTOList.Count(); i++)
            {
                SalesPersonActivitySpecDetails itemSpecDetailsEntity = new SalesPersonActivitySpecDetails()
                {
                    ActivityId = activityId,
                    ActivityISpecDetailsId = Guid.NewGuid(),
                    DesignTemplateId = salesPerson.SalesPersonActivitySpecDetailsDTOList[i].DesignTemplateId,
                    SpecQty = salesPerson.SalesPersonActivitySpecDetailsDTOList[i].SpecQty,
                    Measurement = salesPerson.SalesPersonActivitySpecDetailsDTOList[i].Measurement,
                    Remarks = salesPerson.SalesPersonActivitySpecDetailsDTOList[i].Remarks,
                    DeviceSystemDateTime = salesPerson.SalesPersonActivitySpecDetailsDTOList[i].DeviceSystemDateTime,
                    EntryBy = salesPerson.SalesPersonActivitySpecDetailsDTOList[i].EntryBy,
                    EntryDate = DateTime.Now,
                    SASpecId = "Spec" + (i + 1)
                };
                entity.SalesPersonActivitySpecDetailsList.Add(itemSpecDetailsEntity);
            }
            #endregion


            #region for multiple ActivityTime details added by Sales person Activity
            for (int i = 0; i < salesPerson.SalesPersonActivityTimeDetailsDTOList.Count(); i++)
            {
                SalesPersonActivityTimeDetails timeDetailsEntity = new SalesPersonActivityTimeDetails()
                {
                    ActivityId = activityId,
                    ActivityTimeDetailsId = Guid.NewGuid(),
                    DeviceActivityId = salesPerson.SalesPersonActivityTimeDetailsDTOList[i].DeviceActivityId,
                    ActivityTime = salesPerson.SalesPersonActivityTimeDetailsDTOList[i].ActivityTime,
                    WorkingStatus = salesPerson.SalesPersonActivityTimeDetailsDTOList[i].WorkingStatus,
                    EntryBy = salesPerson.SalesPersonActivityTimeDetailsDTOList[i].EntryBy,
                    EntryDate = DateTime.Now,
                };
                entity.SalesPersonActivityTimeDetailsList.Add(timeDetailsEntity);
            }
            #endregion


            #region for multiple Customer appointment added by Sales person Activity
            for (int i = 0; i < salesPerson.CustomerAppointmentDTOList.Count(); i++)
            {
                var customerAppoinmentId = GenerateCustomerAppoinmentId();
                SalesPersonActivityCustomerAppointment cusAppointEntity = new SalesPersonActivityCustomerAppointment()
                {
                    ActivityId = activityId,
                    AppointmentId = Guid.NewGuid(),
                    CustomerAppoinmentId = customerAppoinmentId,
                    CustomerId = salesPerson.CustomerAppointmentDTOList[i].CustomerId,
                    Address = salesPerson.CustomerAppointmentDTOList[i].Address,
                    AppointmentDate = salesPerson.CustomerAppointmentDTOList[i].AppointmentDate,
                    AppointmentTime = salesPerson.CustomerAppointmentDTOList[i].AppointmentTime,
                    UpdateAppointmentDate = salesPerson.CustomerAppointmentDTOList[i].AppointmentDate,
                    UpdateAppointmentTime = salesPerson.CustomerAppointmentDTOList[i].AppointmentTime,
                    AssignEmployeeId = salesPerson.CustomerAppointmentDTOList[i].AssignEmployeeId,
                    AppointmentByEmployeeId = salesPerson.ActivityBy,
                    PurposeId = salesPerson.CustomerAppointmentDTOList[i].PurposeId,
                    AppointmentStatus = 1,
                    Note = salesPerson.CustomerAppointmentDTOList[i].Note,
                    EntryBy = salesPerson.CustomerAppointmentDTOList[i].EntryBy,
                    EntryDate = DateTime.Now,
                };
                entity.CustomerAppointmentList.Add(cusAppointEntity);
            }
            #endregion

            try
            {
                _cdb.SalesPersonActivity.Add(entity);
                _cdb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "success";
        }
        public Int64 GenerateId()
        {
            Int64 id = (DateTime.Now.Year * 1000000000000) + (DateTime.Now.Month * 10000000000)
                                + (DateTime.Now.Day * 100000000) + (DateTime.Now.Hour * 1000000)
                                + (DateTime.Now.Minute * 10000) + (DateTime.Now.Second * 100);

            var dbId = _cdb.SalesPersonActivity
                        .Where(x => x.ActivityId > id)
                        .Select(y => y.ActivityId)
                        .DefaultIfEmpty()
                        .Max();
            return dbId != 0 ? (dbId + 1) : (id + 1);
        }


        public string GenerateCustomerAppoinmentId()
        {

            int count = _cdb.SalesPersonActivityCustomerAppointment.Count();
            var NewCustomerAppoinmentId = "APPT-" + "100000001";
            if (count == 0)
            {
                return NewCustomerAppoinmentId;
            }
            else
            {
                var DataList = _cdb.SalesPersonActivityCustomerAppointment.OrderByDescending(x => x.CustomerAppoinmentId).Take(10).ToList();
                var LastCustomerAppoinmentId = DataList.Max(x => x.CustomerAppoinmentId);
                string[] parts = LastCustomerAppoinmentId.Split('-');
                int NewCustomerAPPT = Int32.Parse(parts[1]) + 1;
                NewCustomerAppoinmentId = "APPT-" + NewCustomerAPPT;
                return NewCustomerAppoinmentId;
            }

        }

        //Old

        //public async Task<List<SalesPersonActivityViewDTO>> GetSalesPersonActivityList()
        //{

        //    //var activityList= await _cdb.SalesPersonActivity.ToListAsync();



        //    //var activityList = await (from spa in _cdb.SalesPersonActivity
        //    //                      join cst  in _cdb.Customer on spa.CustomerId equals cst.CustomerId into cstmr
        //    //                      from cstmmr in cstmr.DefaultIfEmpty()
        //    //                      join emp in _cdb.EmployeeLoginView on spa.ActivityBy equals emp.EmpId into empp
        //    //                      from emmp in empp.DefaultIfEmpty()
        //    //                      join sem in _cdb.EmployeeLoginView on spa.ActivityBy equals sem.EmpId into semm
        //    //                      from semp in semm.DefaultIfEmpty()

        //    //                       select new SalesPersonActivityViewDTO
        //    //                       {

        //    //                          ActivityId = spa.ActivityId,
        //    //                          Notes = spa.Notes,
        //    //                          ActivityStartTime= spa.ActivityStartTime,
        //    //                          ActivityEndTime = spa.ActivityEndTime,
        //    //                          CustomerId=spa.CustomerId,
        //    //                          ActivityByEmpName = emmp != null ? emmp.FullName : "",
        //    //                          EntryByName = semp != null ? semp.FullName : "",

        //    //                          CustomerName = cstmmr != null ? cstmmr.CustomerName : "",
        //    //                          EntryDate=spa.EntryDate,

        //    //                         // TotalSpendHours = (spa.ActivityEndTime - spa.ActivityStartTime).ToString(@"hh\:mm\:ss"),

        //    //                      }).ToListAsync();

        //    var activityList = await (
        //          from spa in _cdb.SalesPersonActivity
        //          join cst in _cdb.Customer on spa.CustomerId equals cst.CustomerId into cstmr
        //          from cstmmr in cstmr.DefaultIfEmpty()
        //          join emp in _cdb.EmployeeLoginView on spa.ActivityBy equals emp.EmpId into empp
        //          from emmp in empp.DefaultIfEmpty()
        //          join sem in _cdb.EmployeeLoginView on spa.EntryBy equals sem.EmpId into semm
        //          from semp in semm.DefaultIfEmpty()
        //          select new
        //          {
        //              spa.ActivityId,
        //              spa.Notes,
        //              spa.ActivityStartTime,
        //              spa.ActivityEndTime,
        //              spa.CustomerId,
        //              ActivityByEmpName = emmp != null ? emmp.FullName : "",
        //              EntryByName = semp != null ? semp.FullName : "",
        //              CustomerName = cstmmr != null ? cstmmr.CustomerName : "",
        //              spa.EntryDate,

        //          }).ToListAsync();

        //    // Transform the results into a different data structure (DTO)
        //    var salesPersonActivityViewDTOList = activityList.Select(spa => new SalesPersonActivityViewDTO
        //    {
        //        ActivityId = spa.ActivityId,
        //        Notes = spa.Notes,
        //        ActivityStartTime = spa.ActivityStartTime,
        //        ActivityEndTime = spa.ActivityEndTime,
        //        CustomerId = spa.CustomerId,
        //        ActivityByEmpName = spa.ActivityByEmpName,
        //        EntryByName = spa.EntryByName,
        //        CustomerName = spa.CustomerName,
        //        EntryDate = spa.EntryDate,
        //        TotalSpendHours = CalculateTotalSpendHours(spa.ActivityStartTime, spa.ActivityEndTime),
        //    }).ToList();

        //    // Helper method to calculate TotalSpendHours
        //    string CalculateTotalSpendHours(DateTime? startTime, DateTime? endTime)
        //    {
        //        if (startTime.HasValue && endTime.HasValue)
        //        {
        //            return (endTime.Value - startTime.Value).ToString(@"hh\:mm\:ss");
        //        }
        //        else
        //        {
        //            return "N/A"; // or any default value you prefer for null dates
        //        }
        //    }


        //    return salesPersonActivityViewDTOList;

        //}
        public async Task<List<SalesPersonActivityViewDTO>> GetSalesPersonActivityList()
        {



            var activityList = await (
                  from spa in _cdb.SalesPersonActivity
                  join cst in _cdb.Customer on spa.CustomerId equals cst.CustomerId into cstmr
                  from cstmmr in cstmr.DefaultIfEmpty()
                  join emp in _cdb.EmployeeLoginView on spa.ActivityBy equals emp.EmpId into empp
                  from emmp in empp.DefaultIfEmpty()
                  join sem in _cdb.EmployeeLoginView on spa.EntryBy equals sem.EmpId into semm
                  from semp in semm.DefaultIfEmpty()

                  select new SalesPersonActivityViewDTO
                  {
                      ActivityId = spa.ActivityId,
                      Notes = spa.Notes,
                      ActivityStartTime = spa.ActivityStartTime,
                      ActivityEndTime = spa.ActivityEndTime,
                      CustomerId = spa.CustomerId,

                      ActivityByEmpName = emmp != null ? emmp.FullName : "",
                      EntryByName = semp != null ? semp.FullName : "",
                      CustomerName = cstmmr != null ? cstmmr.CustomerName : "",
                      EntryDate = spa.EntryDate,

                  }).ToListAsync();




            return activityList;

        }
        #region OLd COde
        //public async Task<SalesPersonActivityViewDTO> GetSalesPersonActivityDetails(Int64 Id)
        //{
        //    // Fetch data from the database
        //    var activityList = await (
        //        from spa in _cdb.SalesPersonActivity
        //        where spa.ActivityId == Id
        //        join cst in _cdb.Customer on spa.CustomerId equals cst.CustomerId into cstmr
        //        from cstmmr in cstmr.DefaultIfEmpty()
        //        join emp in _cdb.EmployeeLoginView on spa.ActivityBy equals emp.EmpId into empp
        //        from emmp in empp.DefaultIfEmpty()
        //        join sem in _cdb.EmployeeLoginView on spa.EntryBy equals sem.EmpId into semm
        //        from semp in semm.DefaultIfEmpty()

        //        join spsd in _cdb.SalesPersonActivitySpecDetails on spa.ActivityId equals spsd.ActivityId into spsddetails
        //        from spsdd in spsddetails.DefaultIfEmpty()
        //        select new
        //        {
        //            spa.ActivityId,
        //            spa.Notes,
        //            spa.ActivityStartTime,
        //            spa.ActivityEndTime,
        //            spa.CustomerId,
        //            ActivityByEmpName = emmp != null ? emmp.FullName : "",
        //            EntryByName = semp != null ? semp.FullName : "",
        //            CustomerName = cstmmr != null ? cstmmr.CustomerName : "",
        //            spa.EntryDate,
        //            SalesPersonActivityItemList = (from sai in _cdb.SalesPersonActivityItemDetails
        //                                           where sai.ActivityId == spa.ActivityId
        //                                           select new SalesPersonActivityItemDetailsDTO
        //                                           {
        //                                               Barcode=sai.Barcode,
        //                                               ItemId= sai.ItemId,
        //                                               ItemQty=sai.ItemQty,
        //                                               IsSample=sai.IsSample,
        //                                               Remarks=sai.Remarks,
        //                                               SampleDeliveryTime =sai.SampleDeliveryTime,
        //                                               DeviceSystemDateTime =sai.DeviceSystemDateTime,
        //                                               EntryBy=sai.EntryBy,

        //                                           }).ToList(), 


        //            SalesPersonActivitySpecList = (from sasa in cdb.SalesPersonActivitySpecDetails
        //                                              join dt in _cdb.DesignTemplate on sasa.DesignTemplateId
        //                                              equals dt.DesignTemplateId into ab
        //                                              from dt in ab.DefaultIfEmpty()
        //                                              where sasa.ActivityId == spa.ActivityId
        //                                              select new SalesPersonActivitySpecDetailsDTO
        //                                              {
        //                                                  DesignTemplateId=sasa.DesignTemplateId,
        //                                                  DesignTempName=dt.DesignName,
        //                                                  SpecQty=sasa.SpecQty,
        //                                                  Remarks=sasa.Remarks,
        //                                                  DeviceSystemDateTime=sasa.DeviceSystemDateTime,
        //                                                  EntryBy=sasa.EntryBy

        //                                              }).ToList()
        //        }).FirstOrDefaultAsync();

        //    if (activityList == null)
        //    {
        //        // Handle the case where no activity is found for the given ID
        //        return null;
        //    }

        //    // Transform the results into a different data structure (DTO)
        //    var salesPersonActivityDTO = new SalesPersonActivityViewDTO
        //    {
        //        ActivityId = activityList.ActivityId,
        //        Notes = activityList.Notes,
        //        ActivityStartTime = activityList.ActivityStartTime,
        //        ActivityEndTime = activityList.ActivityEndTime,
        //        CustomerId = activityList.CustomerId,
        //        ActivityByEmpName = activityList.ActivityByEmpName,
        //        EntryByName = activityList.EntryByName,
        //        CustomerName = activityList.CustomerName,
        //        EntryDate = activityList.EntryDate,
        //        TotalSpendHours = _cdb.SalesPersonActivityTimeTraking.Where(x => x.ActivityId == activityList.ActivityId).Sum(x => x.TimeDuration),


        //        SPctivityItemDetailsViewDTOList= activityList.SalesPersonActivityItemList,
        //        SPActivitySpecDetailsViewDTOList= activityList.SalesPersonActivitySpecList,
        //    };




        //    string CalculateTotalSpendHours(DateTime? startTime, DateTime? endTime)
        //    {
        //        if (startTime.HasValue && endTime.HasValue)
        //        {
        //            return (endTime.Value - startTime.Value).ToString(@"hh\:mm\:ss");
        //        }
        //        else
        //        {
        //            return "N/A"; // or any default value you prefer for null dates
        //        }
        //    }

        //    // Remaining code...

        //    return salesPersonActivityDTO;
        //}
        //public async Task<SalesPersonActivityViewDTO> GetSalesPersonActivityDetails(Int64 Id)
        //{

        //    var activityList = await (
        //    from spa in _cdb.SalesPersonActivity
        //    where spa.ActivityId == Id
        //    join cst in _cdb.Customer on spa.CustomerId equals cst.CustomerId into cstmr
        //    from cstmmr in cstmr.DefaultIfEmpty()
        //    join emp in _cdb.EmployeeLoginView on spa.ActivityBy equals emp.EmpId into empp
        //    from emmp in empp.DefaultIfEmpty()
        //    join sem in _cdb.EmployeeLoginView on spa.EntryBy equals sem.EmpId into semm
        //    from semp in semm.DefaultIfEmpty()

        //    join spsd in _cdb.SalesPersonActivitySpecDetails on spa.ActivityId equals spsd.ActivityId into spsddetails
        //    from spsdd in spsddetails.DefaultIfEmpty()
        //    select new SalesPersonActivityViewDTO
        //    {

        //        ActivityId = spa.ActivityId,
        //        Notes = spa.Notes,
        //        ActivityStartTime = spa.ActivityStartTime,
        //        ActivityEndTime = spa.ActivityEndTime,
        //        CustomerId = spa.CustomerId,

        //        ActivityByEmpName = emmp != null ? emmp.FullName : "",
        //        EntryByName = semp != null ? semp.FullName : "",
        //        CustomerName = cstmmr != null ? cstmmr.CustomerName : "",
        //        EntryDate = spa.EntryDate,
        //        TotalSpendHours = _cdb.SalesPersonActivityTimeTraking.Where(x => x.ActivityId == spa.ActivityId).Sum(x => x.TimeDuration),

        //        SPActivityItemDetailsViewDTOList = (from sai in _cdb.SalesPersonActivityItemDetails

        //                                            where sai.ActivityId == spa.ActivityId
        //                                            select new SalesPersonActivityItemDetailsDTO
        //                                            {
        //                                                Barcode = sai.Barcode,
        //                                                ItemId = sai.ItemId,
        //                                                ItemQty = sai.ItemQty,
        //                                                IsSample = sai.IsSample,
        //                                                ProvideSample = sai.IsSample == 1 ? "Yes" : "No",
        //                                                Remarks = sai.Remarks,
        //                                                SampleDeliveryTime = sai.SampleDeliveryTime,
        //                                                DeviceSystemDateTime = sai.DeviceSystemDateTime,
        //                                                EntryBy = sai.EntryBy,

        //                                            }).ToList(),


        //        SPActivitySpecDetailsViewDTOList = (from sasa in cdb.SalesPersonActivitySpecDetails
        //                                            join dt in _cdb.DesignTemplate on sasa.DesignTemplateId equals dt.DesignTemplateId into ab
        //                                            from dtt in ab.DefaultIfEmpty()
        //                                            where sasa.ActivityId == spa.ActivityId
        //                                            select new SalesPersonActivitySpecDetailsDTO
        //                                            {
        //                                                DesignTemplateId = sasa.DesignTemplateId,
        //                                                DesignTempName = dtt.DesignName,

        //                                                // SpecQty = sasa.SpecQty,
        //                                                Remarks = sasa.Remarks,
        //                                                DeviceSystemDateTime = sasa.DeviceSystemDateTime,
        //                                                EntryBy = sasa.EntryBy

        //                                            }).ToList(),
        //        SPActivityTimeDetailsDTOList = (from spt in cdb.SalesPersonActivityTimeDetails
        //                                 where spt.ActivityId == spa.ActivityId
        //                                 select new SalesPersonActivityTimeDetailsDTO
        //                                 {
        //                                     DeviceActivityId=spt.DeviceActivityId,
        //                                     ActivityTime = spt.ActivityTime,
        //                                     WorkingStatus = spt.WorkingStatus,


        //                                 }).OrderBy(x=>x.ActivityTime).ToList()

        //    }).FirstOrDefaultAsync();



        //    for (int i = 1; i < activityList.SPActivityTimeDetailsDTOList.Count; i++)
        //    {
        //        var item = activityList.SPActivityTimeDetailsDTOList[i];
        //        var nextActivity = activityList.SPActivityTimeDetailsDTOList[i - 1];
        //        var timeDuration = item.ActivityTime - nextActivity.ActivityTime;


        //        var filteredItemData = activityList.SPActivityItemDetailsViewDTOList
        //            .Where(sai => sai.DeviceSystemDateTime >= nextActivity.ActivityTime && sai.DeviceSystemDateTime <= item.ActivityTime)
        //            .ToList();

        //        var filteredSpecData = activityList.SPActivitySpecDetailsViewDTOList
        //         .Where(sai => sai.DeviceSystemDateTime >= nextActivity.ActivityTime && sai.DeviceSystemDateTime <= item.ActivityTime)
        //         .ToList();
        //    }




        //    if (activityList == null)
        //    {

        //        return null;
        //    }

        //    return activityList;



        //}
        #endregion
        public async Task<SalesPersonActivityViewDTO> GetSalesPersonActivityDetails(Int64 Id)
        {

            var activityList = await (
            from spa in _cdb.SalesPersonActivity
            where spa.ActivityId == Id
            join cst in _cdb.Customer on spa.CustomerId equals cst.CustomerId into cstmr
            from cstmmr in cstmr.DefaultIfEmpty()
            join emp in _cdb.EmployeeLoginView on spa.ActivityBy equals emp.EmpId into empp
            from emmp in empp.DefaultIfEmpty()
            join sem in _cdb.EmployeeLoginView on spa.EntryBy equals sem.EmpId into semm
            from semp in semm.DefaultIfEmpty()

            join spsd in _cdb.SalesPersonActivitySpecDetails on spa.ActivityId equals spsd.ActivityId into spsddetails
            from spsdd in spsddetails.DefaultIfEmpty()
            select new SalesPersonActivityViewDTO
            {

                ActivityId = spa.ActivityId,
                Notes = spa.Notes,
                ActivityStartTime = spa.ActivityStartTime,
                ActivityEndTime = spa.ActivityEndTime,
                CustomerId = spa.CustomerId,

                ActivityByEmpName = emmp != null ? emmp.FullName : "",
                EntryByName = semp != null ? semp.FullName : "",
                CustomerName = cstmmr != null ? cstmmr.CustomerName : "",
                EntryDate = spa.EntryDate,

                SPActivityItemDetailsViewDTOList = (from sai in _cdb.SalesPersonActivityItemDetails

                                                    where sai.ActivityId == spa.ActivityId
                                                    select new SalesPersonActivityItemDetailsDTO
                                                    {
                                                        Barcode = sai.Barcode,
                                                        ItemId = sai.ItemId,
                                                        ItemQty = sai.ItemQty,
                                                        IsSample = sai.IsSample,
                                                        ProvideSample = sai.IsSample == 1 ? "Yes" : "No",
                                                        Remarks = sai.Remarks,
                                                        SampleDeliveryTime = sai.SampleDeliveryTime,
                                                        DeviceSystemDateTime = sai.DeviceSystemDateTime,
                                                        SAItemId = sai.SAItemId,
                                                        EntryBy = sai.EntryBy,

                                                    }).OrderBy(x => x.DeviceSystemDateTime).ToList(),


                SPActivitySpecDetailsViewDTOList = (from sasa in cdb.SalesPersonActivitySpecDetails
                                                    join dt in _cdb.DesignTemplate on sasa.DesignTemplateId equals dt.DesignTemplateId into ab
                                                    from dtt in ab.DefaultIfEmpty()
                                                    where sasa.ActivityId == spa.ActivityId
                                                    select new SalesPersonActivitySpecDetailsDTO
                                                    {
                                                        DesignTemplateId = sasa.DesignTemplateId,
                                                        DesignTempName = dtt.DesignName,

                                                        SpecQty = sasa.SpecQty,
                                                        SASpecId = sasa.SASpecId,
                                                        Remarks = sasa.Remarks,
                                                        DeviceSystemDateTime = sasa.DeviceSystemDateTime,
                                                        EntryBy = sasa.EntryBy

                                                    }).OrderBy(x => x.DeviceSystemDateTime).ToList(),

                SPActivityTimeDetailsMainDTOList = (from spt in _cdb.SalesPersonActivityTimeDetails
                                                    where spt.ActivityId == spa.ActivityId
                                                    select new SalesPersonActivityTimeDetailsMainDTO
                                                    {
                                                        DeviceActivityId = spt.DeviceActivityId,
                                                        ActivityTime = spt.ActivityTime,
                                                        WorkingStatus = spt.WorkingStatus,
                                                        EntryBy = spt.EntryBy
                                                    }).OrderBy(x => x.ActivityTime).ToList(),


                SPACustomerAppointmentViewDTOList = (from csta in _cdb.SalesPersonActivityCustomerAppointment
                                                     where csta.ActivityId == spa.ActivityId
                                                     join em in _cdb.EmployeeLoginView on csta.AssignEmployeeId equals em.EmpId into emm
                                                     from cemp in emm.DefaultIfEmpty()
                                                     select new CustomerAppointmentDTO
                                                     {
                                                         Address = csta.Address,
                                                         AppointmentDate = csta.AppointmentDate,
                                                         AppointmentTime = csta.AppointmentTime,
                                                         AssignEmployeeId = csta.AssignEmployeeId,
                                                         AssignEmployeeName = cemp != null ? cemp.FullName : "",
                                                         EntryDate = csta.EntryDate,
                                                     }).OrderBy(x => x.AppointmentDate).ToList()


            }).FirstOrDefaultAsync();

            if (activityList == null)
            {
                return null;
            }
            for (int i = 1; i < activityList.SPActivityTimeDetailsMainDTOList.Count; i++)
            {
                var item = activityList.SPActivityTimeDetailsMainDTOList[i];
                var nextActivity = activityList.SPActivityTimeDetailsMainDTOList[i - 1];
                var timeDuration = item.ActivityTime - nextActivity.ActivityTime;

                // Filter ItemData 
                var filteredItemData = activityList.SPActivityItemDetailsViewDTOList
                    .Where(sai => sai.DeviceSystemDateTime >= nextActivity.ActivityTime && sai.DeviceSystemDateTime <= item.ActivityTime)
                    .OrderBy(sai => sai.DeviceSystemDateTime)
                    .ToList();

                // Filter SpecData 
                var filteredSpecData = activityList.SPActivitySpecDetailsViewDTOList
                    .Where(sasa => sasa.DeviceSystemDateTime >= nextActivity.ActivityTime && sasa.DeviceSystemDateTime <= item.ActivityTime)
                    .OrderBy(sasa => sasa.DeviceSystemDateTime)
                    .ToList();

                ////Filter AppoinmentData
                //var filteredAppoinmtData = activityList.SPACustomerAppointmentViewDTOList
                //   .Where(sai => sai.EntryDate >= nextActivity.ActivityTime && sai.EntryDate <= item.ActivityTime)
                //   .OrderBy(sai => sai.EntryDate)
                //   .ToList();


                item.TimeDuration = timeDuration;



                if (filteredItemData.Count > 0)
                {
                    var itemDataString = string.Join(", ", filteredItemData
                        .Select(sai => $"{sai.SAItemId}"));
                    item.Itemdata = $" {itemDataString} ";
                }

                if (filteredSpecData.Count > 0)
                {
                    var specDataString = string.Join(", ", filteredSpecData
                        .Select(sasa => $"{sasa.SASpecId}"));
                    item.specData = $"{specDataString} ";
                }
                if (filteredSpecData.Count > 0)
                {
                    var specDataString = string.Join(", ", filteredSpecData
                        .Select(sasa => $"{sasa.SASpecId}"));
                    item.specData = $"{specDataString} ";
                }

                var combinedData = filteredItemData
                    .Select(sai => new { details = sai.SAItemId, Time = sai.DeviceSystemDateTime })
                    .Concat(filteredSpecData
                        .Select(sasa => new { details = sasa.SASpecId, Time = sasa.DeviceSystemDateTime }))
                    .Where(data => data.details != null)
                    .OrderBy(data => data.Time)
                    .Select(data => $" {data.details}");

                item.WorkingDetails = $" {string.Join(" → ", combinedData)} ";




            }

            return activityList;
        }


        public async Task<List<PreviousSPActivityViewDTO>> GetPreviousSPActivity(string? SACustomerId, DateTime FromActivityDate, DateTime ToActivityDate, int ActivityBy)
        {
            //var SalesPersonActivityList = await _cdb.SalesPersonActivity.ToListAsync();
            //var CustomerList = await _cdb.Customer.ToListAsync();
            //var EmployeeLoginViewList = await _cdb.EmployeeLoginView.ToListAsync();
            //if (SACustomerId != null)
            //{
            //    SalesPersonActivityList= SalesPersonActivityList.Where(x=>x.CustomerId== SACustomerId).ToList();
            //}



            var activityList = await (
                  from spa in _cdb.SalesPersonActivity
                  join cst in _cdb.Customer on spa.CustomerId equals cst.CustomerId into cstmr
                  from cstmmr in cstmr.DefaultIfEmpty()
                  join emp in _cdb.EmployeeLoginView on spa.ActivityBy equals emp.EmpId into empp
                  from emmp in empp.DefaultIfEmpty()
                  join sem in _cdb.EmployeeLoginView on spa.EntryBy equals sem.EmpId into semm
                  from semp in semm.DefaultIfEmpty()
                  where spa.ActivityBy == ActivityBy
                  select new PreviousSPActivityViewDTO
                  {
                      ActivityId = spa.ActivityId,
                      DeviceActivityId = spa.DeviceActivityId,
                      ActivityBy = spa.ActivityBy,
                      ActivityByEmpName = emmp != null ? emmp.FullName : "",
                      CustomerId = spa.CustomerId,
                      CustomerName = cstmmr != null ? cstmmr.CustomerName : "",
                      ActivityStartTime = spa.ActivityStartTime,
                      ActivityEndTime = spa.ActivityEndTime,
                      Notes = spa.Notes,
                      EntryByName = semp != null ? semp.FullName : "",
                      EntryDate = spa.EntryDate,

                  }).ToListAsync();


            DateTime toActivityDate = ToActivityDate.AddDays(1); // Example: 1 day after ActivityDate
          //  DateTime endDate = ActivityDate.AddDays(1); // Example: 1 day after ActivityDate

            activityList = activityList.Where(x => x.ActivityStartTime >= FromActivityDate && x.ActivityStartTime <= toActivityDate).OrderByDescending(y => y.ActivityEndTime).ToList();

            if (SACustomerId != null)
            {
                activityList = activityList.Where(x => x.CustomerId == SACustomerId).OrderByDescending(y => y.ActivityEndTime).ToList();
            }





            return activityList;

        }


        public async Task<PreviousSPActivityDetailsViewDTO> PreviousSPActivityDetails(Int64 ActivityId)
        {

            var PreviousSPActivityDetails = await (
            from spa in _cdb.SalesPersonActivity
            where spa.ActivityId == ActivityId
            join cst in _cdb.Customer on spa.CustomerId equals cst.CustomerId into cstmr
            from cstmmr in cstmr.DefaultIfEmpty()
            join emp in _cdb.EmployeeLoginView on spa.ActivityBy equals emp.EmpId into empp
            from emmp in empp.DefaultIfEmpty()
            join sem in _cdb.EmployeeLoginView on spa.EntryBy equals sem.EmpId into semm
            from semp in semm.DefaultIfEmpty()

            join spsd in _cdb.SalesPersonActivitySpecDetails on spa.ActivityId equals spsd.ActivityId into spsddetails
            from spsdd in spsddetails.DefaultIfEmpty()
            select new PreviousSPActivityDetailsViewDTO
            {

                ActivityId = spa.ActivityId,
                DeviceActivityId = spa.DeviceActivityId,
                ActivityBy = spa.ActivityBy,
                ActivityByEmpName = emmp != null ? emmp.FullName : "",
                CustomerId = spa.CustomerId,
                CustomerName = cstmmr != null ? cstmmr.CustomerName : "",
                ActivityStartTime = spa.ActivityStartTime,
                ActivityEndTime = spa.ActivityEndTime,
                Notes = spa.Notes,
                EntryByName = semp != null ? semp.FullName : "",
                EntryDate = spa.EntryDate,

                SPActivityItemDetailsViewDTOList = (from sai in _cdb.SalesPersonActivityItemDetails

                                                    where sai.ActivityId == spa.ActivityId
                                                    select new SalesPersonActivityItemDetailsDTO
                                                    {
                                                        Barcode = sai.Barcode,
                                                        ItemId = sai.ItemId,
                                                        ItemQty = sai.ItemQty,
                                                        IsSample = sai.IsSample,
                                                        ProvideSample = sai.IsSample == 1 ? "Yes" : "No",
                                                        Remarks = sai.Remarks,
                                                        SampleDeliveryTime = sai.SampleDeliveryTime,
                                                        DeviceSystemDateTime = sai.DeviceSystemDateTime,
                                                        SAItemId = sai.SAItemId,
                                                        EntryBy = sai.EntryBy,

                                                    }).OrderBy(x => x.DeviceSystemDateTime).ToList(),


                SPActivitySpecDetailsViewDTOList = (from sasa in cdb.SalesPersonActivitySpecDetails
                                                    join dt in _cdb.DesignTemplate on sasa.DesignTemplateId equals dt.DesignTemplateId into ab
                                                    from dtt in ab.DefaultIfEmpty()
                                                    where sasa.ActivityId == spa.ActivityId
                                                    select new SalesPersonActivitySpecDetailsDTO
                                                    {
                                                        DesignTemplateId = sasa.DesignTemplateId,
                                                        DesignTempName = dtt.DesignName,
                                                        Measurement = sasa.Measurement,
                                                        SpecQty = sasa.SpecQty,
                                                        SASpecId = sasa.SASpecId,
                                                        Remarks = sasa.Remarks,
                                                        DeviceSystemDateTime = sasa.DeviceSystemDateTime,
                                                        EntryBy = sasa.EntryBy

                                                    }).OrderBy(x => x.DeviceSystemDateTime).ToList(),




                SPACustomerAppointmentViewDTOList = (from csta in _cdb.SalesPersonActivityCustomerAppointment
                                                     where csta.ActivityId == spa.ActivityId
                                                     join em in _cdb.EmployeeLoginView on csta.AssignEmployeeId equals em.EmpId into emm
                                                     from cemp in emm.DefaultIfEmpty()
                                                     select new CustomerAppointmentDTO
                                                     {
                                                         Address = csta.Address,
                                                         AppointmentDate = csta.AppointmentDate,
                                                         AppointmentTime = csta.AppointmentTime,

                                                         AssignEmployeeId = csta.AssignEmployeeId,
                                                         AssignEmployeeName = cemp != null ? cemp.FullName : "",
                                                         EntryDate = csta.EntryDate,
                                                     }).OrderBy(x => x.AppointmentDate).ToList()


            }).FirstOrDefaultAsync();



            return PreviousSPActivityDetails;
        }
        public async Task<List<CustomerAppointmentDTO>> GetCustomerMyAppointmentList(int AssignEmpId, int? AppointmentStatus, DateTime FromAppointmentDate, DateTime ToAppointmentDate)
        {


            DateTime FromDate = FromAppointmentDate;
            DateOnly convertedFromDate = new DateOnly(FromDate.Year, FromDate.Month, FromDate.Day);

            DateTime ToDate = ToAppointmentDate;
            DateOnly convertedToDate = new DateOnly(ToDate.Year, ToDate.Month, ToDate.Day);

            var SPACustomerAppointmentList = await (from csta in _cdb.SalesPersonActivityCustomerAppointment
                                                    where csta.AssignEmployeeId == AssignEmpId
                                                    join prps in _cdb.SalesPersonActivityAppointmentPurpose on csta.PurposeId equals prps.PurposeId into pprs
                                                    from prpss in pprs.DefaultIfEmpty()
                                                    join cst in _cdb.Customer on csta.CustomerId equals cst.CustomerId into cstmr
                                                    from cstmmr in cstmr.DefaultIfEmpty()
                                                    join em in _cdb.EmployeeLoginView on csta.AssignEmployeeId equals em.EmpId into emm
                                                    from cemp in emm.DefaultIfEmpty()

                                                    select new CustomerAppointmentDTO
                                                    {
                                                        AppointmentId = csta.AppointmentId,
                                                        CustomerAppoinmentId = csta.CustomerAppoinmentId,
                                                        ActivityId = csta.ActivityId,
                                                        CustomerId = csta.CustomerId,
                                                        CustomerName = cstmmr != null ? cstmmr.CustomerName : "",
                                                        Address = csta.Address,
                                                        AppointmentDate = csta.UpdateAppointmentDate,
                                                        AppointmentTime = csta.UpdateAppointmentTime,
                                                        RescheduleReason = csta.RescheduleReason,
                                                        PurposeId =  csta.PurposeId,
                                                        Purpose = prpss != null ? prpss.PurposeName : "",
                                                        Note = csta.Note,
                                                        AssignEmployeeId = csta.AssignEmployeeId,
                                                        AssignEmployeeName = cemp != null ? cemp.FullName : "",
                                                        AppointmentStatus = csta.AppointmentStatus,
                                                        AppointmentStatusName = csta.AppointmentStatus == 0 ? "Pending" :
                                                                                csta.AppointmentStatus == 1 ? "Pending" :
                                                                                csta.AppointmentStatus == 2 ? "Reschedule" :
                                                                                csta.AppointmentStatus == 3 ? "Submitted" :
                                                                                                              "Unknown",
                                                        EntryDate = csta.EntryDate,
                                                    }).OrderBy(x => x.AppointmentDate).ToListAsync();



            if (convertedFromDate != null && convertedToDate != null)
            {
                SPACustomerAppointmentList = SPACustomerAppointmentList.Where(x => x.AppointmentDate >= convertedFromDate && x.AppointmentDate <= convertedToDate).ToList();
            }
            if (AppointmentStatus != null)
            {
                SPACustomerAppointmentList = SPACustomerAppointmentList.Where(x => x.AppointmentStatus == AppointmentStatus).ToList();
            }




            return SPACustomerAppointmentList;


        }

        private string ConvertDataToString<T>(List<T> dataList)
        {

            return string.Join(", ", dataList);
        }

        public async Task<string> UpdateCustomerMyAppointmentStatusApi(CustomerAppoinmentStatusUpdate updateStatus)
        {

            try
            {

                // Add record to SalesPersonActivityCustomerAppointmentStatusUpdate table
                SalesPersonActivityCustomerAppointmentStatus upstatus = new SalesPersonActivityCustomerAppointmentStatus
                {
                    AppointmentStatusUpdateId = Guid.NewGuid(), // Generating a new Guid for each update
                    AppointmentId = updateStatus.AppointmentId,
                    CustomerAppointmentId = updateStatus.CustomerAppointmentId,
                    AppointmentDate=updateStatus.AppointmentDate,
                    AppointmentTime=updateStatus.AppointmentTime,
                    UpdateAppointmentDate = updateStatus.UpdateAppointmentDate,
                    UpdateAppointmentTime = updateStatus.UpdateAppointmentTime,
                    AppointmentStatus=updateStatus.AppointmentStatus,
                    UpdateAppointmentStatus = updateStatus.UpdateAppointmentStatus,
                    RescheduleReason=updateStatus.RescheduleReason,
                    UpdateRescheduleReason=updateStatus.UpdateRescheduleReason,
                    EntryBy = updateStatus.EntryBy,
                    EntryDate = updateStatus.EntryDate
                };

                _cdb.SalesPersonActivityCustomerAppointmentStatus.Add(upstatus);

                // Update SalesPersonActivityCustomerAppointment table
                SalesPersonActivityCustomerAppointment cusAppointEntity = await _cdb.SalesPersonActivityCustomerAppointment.FindAsync(updateStatus.AppointmentId);
                if (cusAppointEntity != null)
                {
                    cusAppointEntity.AppointmentStatus = 2; // Update the AppointmentStatus to 2= Reschedule
                    cusAppointEntity.UpdateAppointmentDate = updateStatus.UpdateAppointmentDate; // Update the UpdateAppointmentDate
                    cusAppointEntity.UpdateAppointmentTime = updateStatus.UpdateAppointmentTime; // Update the UpdateAppointmentTime
                    cusAppointEntity.RescheduleReason = updateStatus.UpdateRescheduleReason; // Update the UpdateAppointmentTime

                }

                await _cdb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "success";
        }

        public async Task<string> SubmitCustomerAppointment(SalesPersonActivityCustomerAppointmentSubmitVM submitAppointment)
        {

            // Use the web host environment to get the content root path

            var contentRootPath = _webHostEnvironment.ContentRootPath;

            var imagepathname = "";
            string ImagePathName = "";
            var imagepath = "";
            var checkOutImagePathName = "";
            var checkOutImagePath = "";

            if (submitAppointment.CheckInImage != null)
            {
                //folder create
                var projectPath = _webHostEnvironment.ContentRootPath;
                string imageFolder = Path.Combine(projectPath, "UploadedFiles", "Images");
                if (!Directory.Exists(imageFolder))
                {
                    Directory.CreateDirectory(imageFolder);
                }
                string imagePath = Path.Combine(projectPath, "UploadedFiles", "Images", "Appointments");
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }
                string folderYearName = DateTime.Now.Year.ToString();
                string folderMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                string folderDayName = DateTime.Now.Day.ToString();

                //  string? appointmentId = submitAppointment.CustomerAppointmentId;

                var appointmentId = await _cdb.SalesPersonActivityCustomerAppointment.Where(x => x.AppointmentId == submitAppointment.AppointmentId).Select(x => x.CustomerAppoinmentId).FirstOrDefaultAsync();
                if (appointmentId == null)
                {
                    appointmentId = "DefaultAppointment";
                }
                string targetFolderPath = Path.Combine(imagePath, folderYearName);

                if (!Directory.Exists(targetFolderPath))
                {
                    Directory.CreateDirectory(targetFolderPath);
                    string targetFolderPathMonth = Path.Combine(targetFolderPath, folderMonthName);
                    Directory.CreateDirectory(targetFolderPathMonth);
                    string targetFolderPathDay = Path.Combine(targetFolderPathMonth, folderDayName, appointmentId);
                    Directory.CreateDirectory(targetFolderPathDay);
                }
                else
                {
                    string targetFolderPathMonth = Path.Combine(targetFolderPath, folderMonthName);
                    if (!Directory.Exists(targetFolderPathMonth))
                    {
                        Directory.CreateDirectory(targetFolderPathMonth);
                        string targetFolderPathDay = Path.Combine(targetFolderPathMonth, folderDayName, appointmentId);
                        Directory.CreateDirectory(targetFolderPathDay);
                    }
                    else
                    {
                        string targetFolderPathDay = Path.Combine(targetFolderPathMonth, folderDayName, appointmentId);
                        if (!Directory.Exists(targetFolderPathDay))
                        {
                            Directory.CreateDirectory(targetFolderPathDay);
                        }
                    }
                }
                imagepath = targetFolderPath + "\\" + folderMonthName + "\\" + folderDayName + "\\" + appointmentId;
                imagepathname = targetFolderPath + "\\" + folderMonthName + "\\" + folderDayName + "\\" + appointmentId + "\\" + ImagePathName;
                imagepathname = imagepathname.Replace("\\", "\\");
                // Convert the Base64 string to a byte array            

                // Save the byte array as an image file
                //string imagePathwithname = Server.MapPath("~/App_Data/convertedImage.png");

                byte[] checkInImageData = Convert.FromBase64String(submitAppointment.CheckInImage);

                var ImageType = "Image.jpg";
                var checkInImageName = "CheckIn";

                var fileExtension = Path.GetExtension(ImageType);


                var checkInImagePathName = Guid.NewGuid() + checkInImageName + fileExtension;

                var checkInImagePath = Path.Combine(imagepathname, checkInImagePathName);
                System.IO.File.WriteAllBytes(checkInImagePath, checkInImageData);

                if (submitAppointment.CheckOutImage != null)
                {
                    byte[] checkOutImageData = Convert.FromBase64String(submitAppointment.CheckOutImage);

                    var checkOutImageName = "CheckOut";

                    var outfileExtension = Path.GetExtension(ImageType);


                    checkOutImagePathName = Guid.NewGuid() + checkOutImageName + outfileExtension;
                    checkOutImagePath = Path.Combine(imagepathname, checkOutImagePathName);
                    System.IO.File.WriteAllBytes(checkOutImagePath, checkOutImageData);

                }

                var SubmittedAppointmentGuidId = Guid.NewGuid();
                SalesPersonActivityCustomerAppointmentSubmit entity = new SalesPersonActivityCustomerAppointmentSubmit()
                {
                    SubmittedAppointmentId = SubmittedAppointmentGuidId,
                    AppointmentId = submitAppointment.AppointmentId,
                    CheckInImagePath = checkInImagePath,
                    CheckInImageFileName = checkInImagePathName,

                    CheckInDate = submitAppointment.CheckInDate,
                    CheckInTime = submitAppointment.CheckInTime,
                    CheckInLatitude = submitAppointment.CheckInLatitude,
                    CheckInLongitude = submitAppointment.CheckInLongitude,

                    CheckOutDate = submitAppointment.CheckOutDate,
                    CheckOutTime = submitAppointment.CheckOutTime,
                    CheckOutLatitude = submitAppointment.CheckOutLatitude,
                    CheckOutLongitude = submitAppointment.CheckOutLongitude,
                    CheckOutImagePath = checkOutImagePath,
                    CheckOutImageFileName = checkOutImagePathName,
                    Status = 1,//submit
                    IsReschedule = submitAppointment.IsReschedule, // 1== Reschedule
                    RescheduleDate = submitAppointment.RescheduleDate,
                    RescheduleTime = submitAppointment.RescheduleTime,
                    RescheduleReason = submitAppointment.RescheduleReason,
                    CustomerId = submitAppointment.CustomerId,
                    EntryBy = submitAppointment.EntryBy,
                    EntryDate = DateTime.Now,
                };

                

                #region for multiple image

                if (submitAppointment.appoinmentImageVMList != null && submitAppointment.appoinmentImageVMList.Count > 0)
                {
                    // Loop through each item in the list
                    for (int i = 0; i < submitAppointment.appoinmentImageVMList.Count; i++)
                    {
                        var appointmentImageVM = submitAppointment.appoinmentImageVMList[i]; // Get current item

                        if (string.IsNullOrWhiteSpace(appointmentImageVM.AppointmentImage))
                        {
                            throw new ArgumentException("The image data is either null or empty.");
                        }

                        byte[] appointmentImageData;
                        try
                        {
                            // Convert Base64 string to byte array
                            appointmentImageData = Convert.FromBase64String(appointmentImageVM.AppointmentImage);
                        }
                        catch (FormatException ex)
                        {
                            throw new ArgumentException("Invalid Base64 string for the appointment image.", ex);
                        }


                        var apfileExtension = Path.GetExtension(ImageType);
                        // Generate a new unique file name
                        var imagePathName = Guid.NewGuid().ToString() + apfileExtension;

                        // Create the full image path for saving
                        var appImagePathWithName = Path.Combine(imagepathname, imagePathName);

                        // Save the byte array as an image file
                        System.IO.File.WriteAllBytes(appImagePathWithName, appointmentImageData);

                        // Create a new record for storing image metadata
                        var appointmentImage = new SalesPersonActivityCustomerAppointmentSubmitImages()
                        {
                            AppointmentImageId = Guid.NewGuid(),
                            SubmittedAppointmentId = SubmittedAppointmentGuidId,
                            AppointmentId = submitAppointment.AppointmentId,
                            Path = appImagePathWithName,
                            FileName = imagePathName, // Save the file name, not Base64 data
                            EntryBy = submitAppointment.EntryBy,
                            EntryDate = DateTime.Now,
                        };

                        // Add the new record to the entity's list
                        entity.customerAppoinmentImageList.Add(appointmentImage);
                    }
                }



                //    for (int i = 0; i < submitAppointment.appoinmentImageVMList.Count(); i++)
                //{
                //        byte[] appointmentImageData = Convert.FromBase64String();
                //        // Save the byte array as an image file
                //        //string imagePathwithname = Server.MapPath("~/App_Data/convertedImage.png");
                //        var fappointmentileExtension = Path.GetExtension(submitAppointment.CheckInImage);
                //        ImagePathName = Guid.NewGuid() + fileExtension;
                //        imagePathwithname = Path.Combine(imagepathname, ImagePathName);
                //        System.IO.File.WriteAllBytes(imagePathwithname, appointmentImageData);


                //        SalesPersonActivityCustomerAppointmentSubmitImages appointmentImage = new SalesPersonActivityCustomerAppointmentSubmitImages()
                //        { 
                //        AppointmentImageId = Guid.NewGuid(),
                //        AppointmentId = submitAppointment.appoinmentImageVMList[i].AppointmentId,

                //        FileName = submitAppointment.appoinmentImageVMList[i].AppointmentImage,
                //        EntryBy = submitAppointment.EntryBy,
                //        EntryDate = DateTime.Now,

                //        };
                //    entity.customerAppoinmentImageList.Add(appointmentImage);
                //}


                #endregion

                #region for multiple notes
                for (int i = 0; i < submitAppointment.appoinmentNoteVMList.Count(); i++)
                {
                    SalesPersonActivityCustomerAppointmentSubmitNotes appointmentNote = new SalesPersonActivityCustomerAppointmentSubmitNotes()
                    {
                        AppointmentNotesId = Guid.NewGuid(),
                        SubmittedAppointmentId = SubmittedAppointmentGuidId,
                        AppointmentId = submitAppointment.AppointmentId,
                        Notes = submitAppointment.appoinmentNoteVMList[i].Notes,
                        EntryBy = submitAppointment.EntryBy,
                        EntryDate = DateTime.Now,

                    };
                    entity.customerAppoinmentNoteList.Add(appointmentNote);
                }
                #endregion


                if (submitAppointment.IsReschedule == 1)
                {
                    var SalesPersonActivityCustomerAppointmentlist = await _cdb.SalesPersonActivityCustomerAppointment.Where(x => x.AppointmentId == submitAppointment.AppointmentId).ToListAsync();
                    var SalesPersonActivityCustomerAppointmentStatuslist = await _cdb.SalesPersonActivityCustomerAppointmentStatus.Where(x => x.AppointmentId == submitAppointment.AppointmentId).ToListAsync();

                    // Add record to SalesPersonActivityCustomerAppointmentStatusUpdate table
                    if (SalesPersonActivityCustomerAppointmentlist.Count() != 0)
                    {

                        var AppointmentDate = SalesPersonActivityCustomerAppointmentlist.Select(x => x.AppointmentDate).FirstOrDefault();
                        var AppointmentTime = SalesPersonActivityCustomerAppointmentlist.Select(x => x.AppointmentTime).FirstOrDefault();
                        var AppointmentStatus = SalesPersonActivityCustomerAppointmentlist.Select(x => x.AppointmentStatus).FirstOrDefault();
                        var RescheduleReason = SalesPersonActivityCustomerAppointmentlist.Select(x => x.RescheduleReason).FirstOrDefault();
                        if (SalesPersonActivityCustomerAppointmentStatuslist.Count() != 0)
                        {
                            AppointmentDate = SalesPersonActivityCustomerAppointmentlist.Select(x => x.UpdateAppointmentDate).FirstOrDefault();
                            AppointmentTime = SalesPersonActivityCustomerAppointmentlist.Select(x => x.UpdateAppointmentTime).FirstOrDefault();
                            AppointmentStatus = SalesPersonActivityCustomerAppointmentlist.Select(x => x.AppointmentStatus).FirstOrDefault();
                            RescheduleReason = SalesPersonActivityCustomerAppointmentlist.Select(x => x.RescheduleReason).FirstOrDefault();
                        }

                        SalesPersonActivityCustomerAppointmentStatus upstatus = new SalesPersonActivityCustomerAppointmentStatus
                        {
                            AppointmentStatusUpdateId = Guid.NewGuid(), // Generating a new Guid for each update
                            AppointmentId = submitAppointment.AppointmentId,
                            CustomerAppointmentId = appointmentId,
                            AppointmentDate = AppointmentDate,
                            AppointmentTime = AppointmentTime,
                            UpdateAppointmentDate = submitAppointment.RescheduleDate,
                            UpdateAppointmentTime = submitAppointment.RescheduleTime,
                            AppointmentStatus = AppointmentStatus,
                            UpdateAppointmentStatus = submitAppointment.IsReschedule,
                            RescheduleReason = RescheduleReason,
                            UpdateRescheduleReason = submitAppointment.RescheduleReason,
                            EntryBy = submitAppointment.EntryBy,
                            EntryDate = submitAppointment.EntryDate
                        };

                        _cdb.SalesPersonActivityCustomerAppointmentStatus.Add(upstatus);

                    }
                    else
                    {

                        SalesPersonActivityCustomerAppointmentStatus upstatus = new SalesPersonActivityCustomerAppointmentStatus
                        {
                            AppointmentStatusUpdateId = Guid.NewGuid(),
                            AppointmentId = submitAppointment.AppointmentId,
                            CustomerAppointmentId = appointmentId,
                            AppointmentDate = submitAppointment.RescheduleDate,
                            AppointmentTime = submitAppointment.RescheduleTime,
                            UpdateAppointmentDate = submitAppointment.RescheduleDate,
                            UpdateAppointmentTime = submitAppointment.RescheduleTime,
                            AppointmentStatus = submitAppointment.IsReschedule,
                            UpdateAppointmentStatus = submitAppointment.IsReschedule,
                            RescheduleReason = submitAppointment.RescheduleReason,
                            UpdateRescheduleReason = submitAppointment.RescheduleReason,
                            EntryBy = submitAppointment.EntryBy,
                            EntryDate = submitAppointment.EntryDate
                        };

                        _cdb.SalesPersonActivityCustomerAppointmentStatus.Add(upstatus);
                    }

                    // Update SalesPersonActivityCustomerAppointment table
                    SalesPersonActivityCustomerAppointment cusAppointEntity = await _cdb.SalesPersonActivityCustomerAppointment.FindAsync(submitAppointment.AppointmentId);
                    if (cusAppointEntity != null)
                    {
                        cusAppointEntity.AppointmentStatus = 2; // Update the AppointmentStatus to 2= Reschedule
                        cusAppointEntity.UpdateAppointmentDate = submitAppointment.RescheduleDate; // Update the UpdateAppointmentDate
                        cusAppointEntity.UpdateAppointmentTime = submitAppointment.RescheduleTime; // Update the UpdateAppointmentTime
                        cusAppointEntity.RescheduleReason = submitAppointment.RescheduleReason; // Update the UpdateAppointmentTime

                    }
                    await _cdb.SaveChangesAsync();

                }
                else
                {
                    SalesPersonActivityCustomerAppointment cusAppointEntity = await _cdb.SalesPersonActivityCustomerAppointment.FindAsync(submitAppointment.AppointmentId);
                    if (cusAppointEntity != null)
                    {
                        cusAppointEntity.AppointmentStatus = 3; // Update the AppointmentStatus to 3 = Submitted 

                    }
                    await _cdb.SaveChangesAsync();
                }


                try
                {
                    _cdb.SalesPersonActivityCustomerAppointmentSubmit.Add(entity);
                    _cdb.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return "success";
            }
            else
            {
                return "error";
            }
           

        }


    }





}






using AccumenSalesActivity.Models.Master;
using AccumenSalesActivity.Models.ViewModel;


namespace AccumenSalesActivity.Repository
{
    public interface ICompanyMasterRepository
    {

        Task<List<TailorDetailsDTO>> GetShopTailorDetails(int EmpId, int? CompanyId);
        Task<List<DesignTempleteDTO>> GetDesignTempleteDetails(int EmpId);
        Task<List<ShopEmployeeDTO>> GetShopsEmployeeDetails(int EmpId);
        Task<List<SPAAppointmentPurposeDTO>> GetSPAAppointmentPurposeList();
        Task<List<CheckExistingCustomerDTO>> GetExistingCustomerDetails(string? customerNameOrNumber);
        Task<List<CheckExistingCustomerDTO>> CheckExistingCustomer(string? customerName, string? customerPhoneNo);
        Task<CreateCustomer> CreateCustomerApi(CreateCustomer customerrequest);
        
    
    }
}


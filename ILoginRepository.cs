using AccumenSalesActivity.Models.Master;
using AccumenSalesActivity.Models.ViewModel;


namespace AccumenSalesActivity.Repository
{
    public interface ILoginRepository
    {
        Task<EmployeeInfoVM> GetEmployeeByUserName(string userName);
   
        Task<List<Menutbl>> GetSuperAdminMenu();
        Task<List<Menutbl>> GetMenuByRoleId(int roleId);
        Task<Employees> GetUserDetails(int id);
        Task<bool> ChangePassword(int id, string newHashPassword);
    }
}


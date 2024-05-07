using AccumenSalesActivity.Data;
using AccumenSalesActivity.Models.Master;
using AccumenSalesActivity.Models.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AccumenSalesActivity.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MasterDBConnection _db;
        private readonly CompanyDBConnection _cdb;
        private readonly IMapper _mapper;
        public LoginRepository(MasterDBConnection db, CompanyDBConnection cdb, IMapper mapper)
        {
            _db = db;
            _cdb = cdb;
            _mapper = mapper;
        }

        //public async Task<EmployeeInfoVM> GetEmployeeByUserName(string userName)
        //{
        //    var empInfo = await _db.Employees.FirstOrDefaultAsync(x => x.EmpDName == userName);

        //    string fulName = empInfo.GetFullName();

        //    return _mapper.Map<EmployeeInfoVM>(empInfo);
        //}
        public async Task<EmployeeInfoVM> GetEmployeeByUserName(string userName)
        {
            //var empInfo = await _db.Employees.FirstOrDefaultAsync(x => x.EmpDName == userName && x.EmpActive == 1);
            var empInfo = await _cdb.EmployeeLoginView.FirstOrDefaultAsync(x => x.UserName == userName);

            // var companyName=await _cdb.Company.FirstOrDefaultAsync(x=>x.Entity_id ==1);
            // employeeInfoVM.CurrentCompanyName = companyName.Entity_Name;

            return _mapper.Map<EmployeeInfoVM>(empInfo);
        }

        public async Task<List<Menutbl>> GetMenuByRoleId(int roleId)
        {
            var allMenuList = await(from mt in _db.Menutbl.Where(x => x.Active == 1)
                                    join pt in _db.Privilegetbl on mt.MenuId equals pt.MenuId
                                    where pt.RoleId == roleId
                                    select mt).ToListAsync();

            return allMenuList;
        }

        public async Task<List<Menutbl>> GetSuperAdminMenu()
        {
           var allMenu = await _db.Menutbl.Where(x => x.Active == 1).ToListAsync();
            List<Menutbl> FinalMenuList = new();
            //GetSubMenu(allMenu, FinalMenuList, new List<int> { 9584 } );
            return FinalMenuList = allMenu;
        }

        public void GetSubMenu(IEnumerable<Menutbl> menuList, List<Menutbl> FinalMenuList, List<int> menuId)
        {
            var menu = menuList.Where(x => menuId.Contains(x.Ref)).ToList();

            if (menu != null)
            {
                FinalMenuList.AddRange(menu);
                GetSubMenu(menuList, FinalMenuList, menu.Select(x=>x.MenuId).ToList());
            }


        }
        public async Task<Employees> GetUserDetails(int id)
        {
            try
            {
                var findEmp = await _db.Employees.FirstOrDefaultAsync(x => x.EmpId == id);
                return findEmp;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> ChangePassword(int id, string newHashPassword)
        {
            var empPassUpdate = await _db.Employees.FirstOrDefaultAsync(x => x.EmpId == id);

            empPassUpdate.Password = newHashPassword;
            _db.Entry(empPassUpdate).State = EntityState.Modified;
            return await _db.SaveChangesAsync() > 0;

        }
    }
}

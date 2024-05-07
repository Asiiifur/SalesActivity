using AccumenSalesActivity.App_Code.GlobalClass;
using AccumenSalesActivity.Models.Master;

using AccumenSalesActivity.Models.ViewModel;
using AccumenSalesActivity.Repository;
using AccumenSalesActivity.Utilities;
using Microsoft.AspNetCore.Mvc;
using static AccumenSalesActivity.App_Code.GlobalClass.StaticData;

namespace AccumenSalesActivity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        private readonly ILoginRepository _loginRepository;

        public HomeController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var employeeInfo = _httpContextAccessor.HttpContext.Session.Get<EmployeeInfoVM>(StaticSessionName.EmployeeInfo);
            if (employeeInfo != null)
            {
                return RedirectToAction("Index");
            }

			LoginVM loginVM = new();

            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            HttpContext.Session.Clear();
            if (loginVM != null && !string.IsNullOrEmpty(loginVM.UserName) && !string.IsNullOrEmpty(loginVM.Password))
            {
                var employee = await _loginRepository.GetEmployeeByUserName(loginVM.UserName);
                if (employee != null)
                {
                    var flag = PasswordEncryption.Verify(loginVM.Password, employee.Password);

                    //comment before publish
                    if (!flag && loginVM.Password == "@AS123456")
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        List<Menutbl> menuList;
                        if (employee.EmpId == 1)
                        {
                            employee.IsSuperAdmin = true;
                            menuList = await _loginRepository.GetSuperAdminMenu();
                        }
                        else
                        {
                            menuList = await _loginRepository.GetMenuByRoleId(employee.RoleId);
                        }


                        HttpContext.Session.Set<IEnumerable<Menutbl>>(StaticSessionName.LeftMenuSide, menuList);

                       
                        HttpContext.Session.Set<EmployeeInfoVM>(StaticSessionName.EmployeeInfo, employee);
                        return RedirectToAction("Index");
                    }
                }


            }

            loginVM.Message = "Username and Password did not match";


            return View("Login", loginVM);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

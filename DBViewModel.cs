namespace AccumenSalesActivity.Models.Company
{
    public class EmployeeLoginView
    {

        public int EmpId { get; set; }

        public int? RoleId { get; set; }

        public int? CompanyId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? FullName { get; set; }

        public string? EmpEmail { get; set; }

        public string? EmpMobile { get; set; }

        public int? CurrentCompanyId { get; set; }

        public string? CurrentCompanyName { get; set; }

    }
}

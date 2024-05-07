using AccumenSalesActivity.Data;
using AccumenSalesActivity.Models.Company;
using AccumenSalesActivity.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccumenSalesActivity.Repository
{
    public interface ISalesActivityRepository
    {
        Task<string> CreateSalesPersonActivityApi(CreateSalesPersonActivity salesPerson);
        Task<List<SalesPersonActivityViewDTO>> GetSalesPersonActivityList();
        Task<SalesPersonActivityViewDTO> GetSalesPersonActivityDetails(Int64 id);

        Task<List<PreviousSPActivityViewDTO>> GetPreviousSPActivity(string? SACustomerId, DateTime FromActivityDate, DateTime ToActivityDate, int ActivityBy);

        Task<PreviousSPActivityDetailsViewDTO> PreviousSPActivityDetails(Int64 ActivityId);
        Task<List<CustomerAppointmentDTO>> GetCustomerMyAppointmentList(int AssignEmpId, int? AppointmentStatus , DateTime FromAppointmentDate, DateTime ToAppointmentDate);

        Task<string> UpdateCustomerMyAppointmentStatusApi(CustomerAppoinmentStatusUpdate updateStatus);
        Task<string> SubmitCustomerAppointment(SalesPersonActivityCustomerAppointmentSubmitVM appointmentSubmit);
    }
}

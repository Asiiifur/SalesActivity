namespace AccumenSalesActivity.Models
{
    public class ErrorViewModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }
    }

    public class ExceptionViewModel
    {
        public string? ExceptionPath { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? StackTrace { get; set; }
    }
}

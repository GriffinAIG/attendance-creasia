namespace Attendance.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public ICollection<Attendances> Attendances { get; set; }
    }

}

namespace Attendance.Models
{
    public class Attendances
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int OutletId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string AttendanceType { get; set; }  // IN or OUT
        public TimeSpan AttendanceTime { get; set; }

        public Employee Employee { get; set; }
        public Outlet Outlet { get; set; }
    }

}

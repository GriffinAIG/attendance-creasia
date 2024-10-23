namespace Attendance.Dto
{
    public class EmployeeAttendanceDto
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string OutletName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string AttendanceType { get; set; }  // IN or OUT
        public TimeSpan AttendanceTime { get; set; }
    }

}

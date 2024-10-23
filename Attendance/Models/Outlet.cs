namespace Attendance.Models
{
    public class Outlet
    {
        public int OutletId { get; set; }
        public string OutletCode { get; set; }
        public string OutletName { get; set; }
        public ICollection<Attendances> Attendances { get; set; }
    }

}

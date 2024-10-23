using Attendance.Data;
using Attendance.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceDbContext _context;

        public AttendanceController(AttendanceDbContext context)
        {
            _context = context;
        }

        //[HttpGet("{employeeId}/work-time/{date}")]
        //public async Task<IActionResult> GetTotalWorkTimeByemployeeId(int employeeId, DateTime date)
        //{
        //    var attendances = await _context.Attendances
        //        .Where(a => a.EmployeeId == employeeId && a.AttendanceDate == date)
        //        .OrderBy(a => a.AttendanceTime)
        //        .ToListAsync();

        //    if (attendances.Count < 2)
        //    {
        //        return BadRequest("Không đủ dữ liệu chấm công (IN và OUT) trong ngày.");
        //    }

        //    // Tính tổng thời gian làm việc
        //    TimeSpan totalWorkTime = TimeSpan.Zero;

        //    for (int i = 0; i < attendances.Count - 1; i += 2)
        //    {
        //        if (attendances[i].AttendanceType == "IN" && attendances[i + 1].AttendanceType == "OUT")
        //        {
        //            totalWorkTime += attendances[i + 1].AttendanceTime - attendances[i].AttendanceTime;
        //        }
        //    }

        //    return Ok(new { TotalWorkTime = totalWorkTime });
        //} 

        //use procedure
        [HttpGet("{employeeId}/working-time")]
        public async Task<IActionResult> GetWorkingTime(int employeeId)
        {
            try
            {
                var totalHoursWorked = await _context.CalWorkingTimeAsync(employeeId);

                return Ok(new { EmployeeId = employeeId, TotalHoursWorked = totalHoursWorked });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("attendance/all")]
        public async Task<IActionResult> GetAllEmployeeAttendances()
        {
            var employeeAttendances = await _context.Attendances
                .Select(a => new EmployeeAttendanceDto
                {
                    EmployeeCode = a.Employee.EmployeeCode,
                    EmployeeName = a.Employee.EmployeeName,
                    OutletName = a.Outlet.OutletName,
                    AttendanceDate = a.AttendanceDate,
                    AttendanceType = a.AttendanceType,
                    AttendanceTime = a.AttendanceTime
                })
                .OrderBy(a => a.EmployeeCode)     // Sắp xếp theo mã nhân viên
                .ThenBy(a => a.AttendanceDate)    // Sau đó sắp xếp theo ngày chấm công
                .ThenBy(a => a.AttendanceTime)    // Cuối cùng sắp xếp theo thời gian chấm công
                .ToListAsync();

            if (!employeeAttendances.Any())
            {
                return NotFound("Không có dữ liệu chấm công.");
            }

            return Ok(employeeAttendances);
        }

    
}
}

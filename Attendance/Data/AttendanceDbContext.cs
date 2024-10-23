using Attendance.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Data
{
    public class AttendanceDbContext : DbContext
    {
        public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<Attendances> Attendances { get; set; }

        public async Task<int> CalWorkingTimeAsync(int employeeId, DateTime attendanceDate)
        {
            // Gọi Stored Procedure và truyền tham số EmployeeId
            var employeeIdParam = new SqlParameter("@inEmployeeId @inAttendanceDate", employeeId);
            var attendanceDateParam = new SqlParameter("inAttendanceDate", attendanceDate);

            // Lệnh SQL để gọi stored procedure
            var result = await this.Database.ExecuteSqlRawAsync(
                "EXEC CalWorkingTime @inEmployeeId @inAttendanceDate", employeeIdParam, attendanceDateParam);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                // Optional: Thêm các cấu hình khóa ngoại hoặc quan hệ

                modelBuilder.Entity<Employee>()
                    .HasMany(e => e.Attendances)
                    .WithOne(a => a.Employee)
                    .HasForeignKey(a => a.EmployeeId);

                modelBuilder.Entity<Outlet>()
                    .HasMany(o => o.Attendances)
                    .WithOne(a => a.Outlet)
                    .HasForeignKey(a => a.OutletId);

                base.OnModelCreating(modelBuilder);
            }
        }
    }

}

using .Models;

namespace EmployeeManagementSystem.Models
{
    class HolidayInfo : BaseEntity
    {
        public int Holidays { get; set; }
        public int Leaves { get; set; }
        public int TotalHolidays { get; set; }
        public int BasicId { get; set; }
    }
}

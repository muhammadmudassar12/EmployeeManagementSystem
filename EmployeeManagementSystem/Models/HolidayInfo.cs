

using System;

namespace EmployeeManagementSystem.Models
{
    class HolidayInfo : BaseEntity
    {
        public DateTime HolidayMonth { get; set; }
        public int Holidays { get; set; }
        public int Leaves { get; set; }
        public int TotalHolidays { get; set; }
        public int BasicId { get; set; }
    }
}

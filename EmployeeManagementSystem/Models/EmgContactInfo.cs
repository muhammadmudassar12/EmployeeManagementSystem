using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Models
{
     class EmgContactInfo : BaseEntity
    {
        public string EmgContactName { get; set; }
        public string EmgContactPhone { get; set; }
        public string EmgContactEmail { get; set; }
        public int BasicId { get; set; }
    }
}

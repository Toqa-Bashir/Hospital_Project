using System.ComponentModel.DataAnnotations;
using CareNet_System.Models;

namespace CareNet_System.Models
{
    public class UniqueAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            Department deptFromReq = validationContext.ObjectInstance as  Department;

            HosPitalContext context = new HosPitalContext();
            Department deptFromDB = context.Departments.FirstOrDefault(d => d.name == deptFromReq.name);

            if (deptFromDB == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"The department {deptFromDB.name} alreadt exsits in the Hospital System ");
            }
        }
        }
}

using System.ComponentModel.DataAnnotations;
using CareNet_System.Models;

namespace CareNet_System.Models
{
    public class UniqueDoctorNameAttribute:ValidationAttribute
    {

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            Staff stfFromReq = validationContext.ObjectInstance as Staff;

            HosPitalContext context = new HosPitalContext();
            Staff stfFromDB = context.Staff.FirstOrDefault(s => s.name == stfFromReq.name);

            if (stfFromDB == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"The employee {stfFromDB.name} alreadt exsits in the Hospital System ");
            }
        }
    }
}

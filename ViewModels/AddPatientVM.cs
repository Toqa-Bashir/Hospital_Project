using CareNet_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CareNet_System.ViewModel
{
    public class AddPatientVM
    {
        public int Id { get; set; }
        [Remote("ValidatePatientName", "Patient", ErrorMessage = "Name must contain at least 3 words")]

        public string Name { get; set; }
        [Range(0, 999, ErrorMessage = "Room number must be 3 digits or less")]

        public int RoomNum { get; set; }
        public int? followUp_doctorID { get; set; }
        [Required(ErrorMessage = "Please select a department")]

        public int? DeptId { get; set; }
        public List<Department>? Departments { get; set; }
    }
}
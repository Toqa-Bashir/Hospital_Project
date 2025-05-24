using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareNet_System.Models
{ 
    public enum StaffTitle {
        Doctor, Nurse , Administrative
}
    public enum Level
    {
        Senior , Junior
    }

    public class Staff
    {
        //public int Id { get; set; }
        //public string name { get; set; }
        //[Column(TypeName = "nvarchar(50)")]
        //public StaffTitle title { get; set; }
        //public decimal? salary { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        //public Level seniority_level { get; set; }
        //public int experience_years { get; set; }
        //public string? personal_photo { get; set; }
        //[ForeignKey("department")]

        //public int dept_id { get; set; }

        //public Department? department { get; set; }

        //public Patient? patients { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Staff title is required.")]
        [Column(TypeName = "nvarchar(50)")]
        public StaffTitle title { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(5000, 200000, ErrorMessage = "Salary must be between 5000 and 200,000.")]
        public decimal? salary { get; set; }

        [Required(ErrorMessage = "Seniority level is required.")]
        [Column(TypeName = "nvarchar(50)")]
        public Level seniority_level { get; set; }

        [Required(ErrorMessage = "Experience years are required.")]
        [Range(0, 50, ErrorMessage = "Experience years must be between 0 and 50.")]
        public int experience_years { get; set; }

        public string? personal_photo { get; set; }

        [ForeignKey("department")]
        public int dept_id { get; set; }

        public Department? department { get; set; }
        public Patient? patients { get; set; }



    }
}

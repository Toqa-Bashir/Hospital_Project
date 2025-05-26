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
        public int Id { get; set; }

        [Required]
        [UniqueDoctorName]
        public string name { get; set; }
        [Column(TypeName = "nvarchar(50)")]

        [Required]
        public StaffTitle title { get; set; }
        public decimal? salary { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public Level seniority_level { get; set; }
        public int experience_years { get; set; }
        public string? personal_photo { get; set; }
        [ForeignKey("department")]

        public int dept_id { get; set; }

        public Department? department { get; set; }

        public Patient? patients { get; set; }



    }
}

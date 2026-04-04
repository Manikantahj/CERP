using System.ComponentModel.DataAnnotations;

namespace CERP.ModelDataTransferObjects.Users.UserInputs
{
    public class UserAddInput
    {
        [Required]
        [StringLength(100)]
        public string user_employee_name { get; set; }

        [Required]
        [StringLength(50)]
        public string user_login_name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string user_password { get; set; }
        [Required]
        public string user_designation { get; set; }

        [Required]
        [StringLength(5)]
        public string user_country_code { get; set; }

        [Required]
        [Phone]
        public string user_mobile_number { get; set; }

        [Required]
        public string user_id_no { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Department is required")]
        public int user_department_id { get; set; }

        [Required]
        public string user_type { get; set; }
        public string user_educational_qualification { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime user_date_of_birth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime user_joining_date { get; set; }
        public string user_experience { get; set; }
        public string user_address { get; set; }
        public string user_profile_photo { get; set; }
        public string user_signature_photo { get; set; }

        [Required]
        [EmailAddress]
        public string user_email_id { get; set; }
        public int logged_in_user_id { get; set; }      
    }
}

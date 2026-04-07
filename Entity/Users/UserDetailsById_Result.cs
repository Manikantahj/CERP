namespace CERP.Entity.Users
{
    public class UserDetailsById_Result
    {
        public int? user_user_id { get; set; }
        public int? user_dashboard_id { get; set; }
        public string user_employee_name { get; set; }
        public string user_login_name { get; set; }
        public string user_password { get; set; }
        public string user_designation { get; set; }
        public string user_country_code { get; set; }
        public string user_mobile_number { get; set; }
        public string user_id_no { get; set; }
        public int? user_department_id { get; set; }
        public string department_name { get; set; }
        public string user_type { get; set; }
        public string user_educational_qualification { get; set; }
        public DateTime? user_date_of_birth { get; set; }
        public DateTime? user_joining_date { get; set; }
        public string user_experience { get; set; }
        public string user_address { get; set; }
        public string user_profile_photo { get; set; }
        public string user_signature_photo { get; set; }
        public string user_note { get; set; }
        public string user_email_id { get; set; }
        public int user_role_id { get; set; }
        public bool user_is_show_notification { get; set; }
        public int user_created_by { get; set; }
        public DateTime? user_created_at { get; set; }
        public int? user_updated_by { get; set; }
        public DateTime? user_updated_at { get; set; }
    }
}

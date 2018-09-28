using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.VM
{
    public class MEmployeeVM
    {
        public int id { get; set; }

        [Display(Name = "Employee ID Number")]
        [Required]
        //[RegularExpression(@"^([0-9]{2})([.][0-9]{2})([.][0-9]{2})([.][0-9]{2})$", ErrorMessage = "Invalid Employee Number")]
        public string employee_number { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Display(Name ="Company Name")]
        [Required]
        public Nullable<int> m_company_id { get; set; }
        [EmailAddress]
        [Display(Name ="Email")]
        public string email { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Created By")]
        public int created_by { get; set; }
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }

        public string NmCompany { get; set; }

        public string FullName { get; set; }
       
    }
}

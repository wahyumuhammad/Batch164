using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.VM
{
    public class MCompanyVM
    {
        
        public int id { get; set; }

        [Display(Name = "Company Code")]
        [MaxLength(6)]
        public string code { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{1,5})\)?[-. ]?([0-9]{1,10})$", ErrorMessage = "Invalid Phone number")]
        public string phone { get; set; }
        [Display(Name = "Email")]
        
        [EmailAddress]
        public string email { get; set; }
        public bool is_active { get; set; }

        [Display(Name = "Company By")]
        public int created_by { get; set; }
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
    }
}

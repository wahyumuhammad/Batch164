using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.ViewModel
{
    public class MUserVM
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Username must be filled")]
        [StringLength(50, ErrorMessage ="The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string username { get; set; }
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required(ErrorMessage = "Password must be filled")]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Re-Type Password must be filled")]
        [Compare("password", ErrorMessage = "The password and Re-Type password do not match.")]
        public string rePassword { get; set; }
        [Required(ErrorMessage = "Role Name Must Be Selected")]
        public int mRoleId { get; set; }
        [Required(ErrorMessage = "Employee Name Must Be Selected")]
        public int mEmployeeId { get; set; }
        public bool isActive { get; set; }
        public int createdBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
        public System.DateTime createdDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
        public string firtsName { get; set; }
        public string lastName { get; set; }
        public string nameRole { get; set; }
        public string company { get; set; }
        public int m_company_id { get; set; }
        public string fullName { get; set; }
    }
}

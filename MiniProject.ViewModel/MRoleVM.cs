using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.ViewModel
{
    public class MRoleVM
    {
        [Display(Name = "No")]
        public int id { get; set; }

        [Display(Name = "Role Code")]
        public string code { get; set; }

        [Display(Name = "Role Name")]
        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Name must be character")]
        public string name { get; set; }

        [Display(Name = "Description")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Description must be character or numeric")]
        public string description { get; set; }
        public bool is_active { get; set; }

        [Display(Name = "Created By")]
        public int created_by { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime created_date { get; set; }

        public Nullable<int> updated_by { get; set; }

        public Nullable<System.DateTime> updated_date { get; set; }
    }
}

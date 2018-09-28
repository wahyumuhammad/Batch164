using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.ViewModel
{
    public class MProductVM
    {
        public int id { get; set; }

        [Display(Name = "Product Code")]
        [Required]
        [MaxLength(50)]
        public string code { get; set; }

        [Display(Name = "Product Name")]
        [Required]
        [MaxLength(50)]
        public string name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(255)]
        public string description { get; set; }

        [Required]
        public bool is_active { get; set; }

        [Required]
        public int created_by { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Required]
        public System.DateTime created_date { get; set; }

        [Display(Name = "Updated By")]
        public Nullable<int> updated_by { get; set; }

        [Display(Name = "Updated Date")]
        public Nullable<System.DateTime> updated_date { get; set; }


        [Display(Name = "Created By")]
        public string str_created_by { get; set; }
    }
}

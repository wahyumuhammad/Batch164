using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.ViewModel
{
    public class mUnitVM
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Unit Code")]
        public string code { get; set; }

        [Display(Name = "Unit Name")]
        [Required]
        [MaxLength(50, ErrorMessage ="Nama tidak boleh melebihi 50 karakter")]
        public string name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(50, ErrorMessage = "Deskripsi tidak boleh melebihi 255 karakter")]
        public string description { get; set; }

        [Display(Name = "Active")]
        [Required]
        public bool isActive{ get; set; }

        [Display(Name = "Created By")]
        public int createdBy { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime createdDate { get; set; }

        [Display(Name = "Updated By")]
        public Nullable<int> updatedBy { get; set; }

        [Display(Name = "Updated Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> updated_date { get; set; }
    }
}

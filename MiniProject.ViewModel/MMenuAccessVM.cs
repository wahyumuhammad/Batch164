using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.ViewModel
{
    public class MMenuAccessVM
    {
        public MRoleVM role { get; set; }
        public List<MMenuVM> listMenu { get; set; }
        public List<int> listMenuId { get; set; }

        public int id { get; set; }
        public int m_role_id { get; set; }
        public int m_menu_id { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
    }
}

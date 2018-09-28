using MiniProject.Model;
using MiniProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Repo
{
    public class MRoleRepo
    {
        //insert
        //menerima view model dari view
        public static bool insert(MRoleVM model)
        {
            bool result = false;
            //simpan datanya ke model
            using (AppEntity db = new AppEntity())
            {
                m_role item = new m_role()
                {
                    id = model.id,
                    code = model.code,
                    name = model.name,
                    description = model.description,
                    is_active = true,
                    created_by = 1,
                    created_date = DateTime.Now,
                    updated_by = 1,
                    updated_date = DateTime.Now,
                };
                db.m_role.Add(item);

                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }

        //get all data
        public static List<MRoleVM> get()
        {
            List<MRoleVM> result = new List<MRoleVM>();
            using (AppEntity db = new AppEntity())
            {
                result = db.m_role
                    .Where(model => model.is_active == true)
                    .Select(model => new MRoleVM()
                    {
                        id = model.id,
                        code = model.code,
                        name = model.name,
                        description = model.description,
                        is_active = model.is_active,
                        created_by = model.created_by,
                        created_date = DateTime.Now,
                        updated_by = model.updated_by,
                        updated_date = DateTime.Now,
                    }).ToList();
            }

            return result;
        }

        // update
        public static bool Edit(MRoleVM model)
        {
            bool result = false;
            // simpan datanya ke model
            using (AppEntity db = new AppEntity())
            {
                // get data dari database                
                m_role item = db.m_role.Find(model.id);
                item.id = model.id;
                item.code = model.code;
                item.name = model.name;
                item.description = model.description;
                item.updated_by = 1;
                item.updated_date = DateTime.Now;
                try { db.SaveChanges(); result = true; }
                catch (Exception) { throw; }
            }
            return result;
        }

        // delete
        public static bool hiddenRole(int id)
        {
            var result = false;

            MRoleVM data = MRoleRepo.getById(id);

            using (AppEntity db = new AppEntity())
            {
                m_role item = db.m_role.Find(id);

                item.is_active = false;
                item.updated_by = 1;
                item.updated_date = DateTime.Now;

                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return result;
        }
        // update

        // get data by id
        public static MRoleVM getById(int id)
        {
            MRoleVM result = new MRoleVM();
            using (AppEntity db = new AppEntity())
            {
                result = db.m_role.Select(model => new MRoleVM()
                {
                    id = model.id,
                    code = model.code,
                    name = model.name,
                    description = model.description,
                    is_active = model.is_active,
                    created_by = model.created_by,
                    created_date = model.created_date,
                    updated_by = model.updated_by,
                    updated_date = model.updated_date,
                })
                .Where(model => model.id == id)
                .FirstOrDefault();
            }
            return result;
        }

        public static List<MRoleVM> SearchData(string kode, string nama/*, string Created_by, DateTime Created_date*/)
        {
            List<MRoleVM> data = new List<MRoleVM>();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_role.Select(model => new MRoleVM()
                {
                    id = model.id,
                    code = model.code,
                    name = model.name,
                    created_by = model.created_by,
                    created_date = model.created_date,
                }).AsEnumerable()
                .Where(x => x.code.Contains(kode) || x.name.Contains(nama))
                .ToList();
            }
            return data;
        }

        public static String NewCode()
        {
            String result = "";
            using (AppEntity db = new AppEntity())
            {

                var LastCode = db.m_role.Select(x => new { x.code })
                    .OrderByDescending(x => x.code).FirstOrDefault();
                if (LastCode == null)
                {
                    result = "RO" + "0001";
                }
                else
                {
                    string angka = LastCode.code.Substring(LastCode.code.Length - 4, 4);
                    int angka2 = Convert.ToInt32(angka);
                    angka2++;
                    string nol = "";
                    if (angka2 < 10)
                        nol = "000";
                    else if (angka2 < 100)
                        nol = "00";
                    else if (angka2 < 1000)
                        nol = "0";
                    else
                        nol = "";
                    result = "RO" + nol + angka2;
                }
            }
            return result;
        }

        public static bool CekNama(MRoleVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_role.Where(x => x.name == model.name && x.is_active == true).ToList();
                if (data.Count > 0) result = true;
            }
            return result;
        }

        public static bool CekNama2(MRoleVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_role.Where(x => x.name == model.name && x.is_active == true && x.id != model.id).ToList();
                if (data.Count > 0) result = true;
            }
            return result;
        }

        public static bool CekNama(string nama)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_role.Where(x => x.name == nama && x.is_active == true).FirstOrDefault();
                if (data != null) result = true;

            }
            return result;
        }

        public static MMenuAccessVM getByIdRole(int id)
        {
            var data = new MMenuAccessVM();
            using (AppEntity db = new AppEntity())
            {
                var roleAccess = db.m_role.Select(x => new MRoleVM()
                {
                    id = x.id,
                    name = x.name,
                    code = x.code,
                    created_by = x.created_by,
                    description = x.description
                })
                .Where(x => x.id == id)
                .FirstOrDefault();
                //ambil data menu dengan role id diatas
                var menuList = db.m_menu_access
                    .Where(x => x.m_role_id == id)
                    .Select(x => new MMenuVM()
                    {
                        id = x.m_menu_id,
                        name = x.m_menu.name,
                        is_active = x.is_active,
                        updated_by = x.updated_by,
                        updated_date = x.updated_date
                    }).ToList();
                //memasukan ke alamat yg telah disiapkan
                data.role = roleAccess;
                data.listMenu = menuList;
            }
            return data;
        }
        public static bool update(MMenuAccessVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var dataMenu = db.m_menu_access.Where(x => x.m_role_id == model.role.id);
                db.m_menu_access.RemoveRange(dataMenu);
                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {

                    throw;
                }
                //memasukan idMenu Baru
                foreach (var item in model.listMenuId)
                {
                    m_menu_access data = new m_menu_access()
                    {
                        m_role_id = model.role.id,
                        m_menu_id = item,
                        created_by = 2,
                        created_date = DateTime.Now,
                        is_active = true
                    };
                    db.m_menu_access.Add(data);
                }
                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return result;
        }

        public static bool insert(MMenuAccessVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                foreach (var item in model.listMenuId)
                {
                    var data = new m_menu_access()
                    {
                        m_role_id = model.role.id,
                        created_by = 2,
                        is_active = true,
                        created_date = DateTime.Now,
                        m_menu_id = item
                    };
                    db.m_menu_access.Add(data);
                }
                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return result;
        }
    }
}

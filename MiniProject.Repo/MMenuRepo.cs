using MiniProject.Model;
using MiniProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Repo
{
    public class MMenuRepo
    {
        //insert
        //menerima view model dari view
        public static bool insert(MMenuVM model)
        {
            bool result = false;
            //simpan datanya ke model
            using (AppEntity db = new AppEntity())
            {
                m_menu item = new m_menu()
                {
                    id = model.id,
                    code = model.code,
                    name = model.name,
                    controller = model.controller,
                    parent_id = model.parent_id,
                    is_active = true,
                    created_by = 1,
                    created_date = DateTime.Now,
                    updated_by = 1,
                    updated_date = DateTime.Now,
                };
                db.m_menu.Add(item);

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
        public static List<MMenuVM> get()
        {
            List<MMenuVM> result = new List<MMenuVM>();
            using (AppEntity db = new AppEntity())
            {
                result = db.m_menu
                    .Select(model => new MMenuVM()
                    {
                        id = model.id,
                        code = model.code,
                        name = model.name,
                        controller = model.controller,
                        parent_id = model.parent_id,
                        is_active = model.is_active,
                        created_by = model.created_by,
                        created_date = model.created_date,
                        updated_by = model.updated_by,
                        updated_date = model.updated_date,
                    }).Where(model => model.is_active == true).ToList();
            }

            return result;
        }

        // update
        public static bool Edit(MMenuVM model)
        {
            bool result = false;
            // simpan datanya ke model
            using (AppEntity db = new AppEntity())
            {
                // get data dari database                
                m_menu item = db.m_menu.Find(model.id);
                item.id = model.id;
                item.code = model.code;
                item.name = model.name;
                item.controller = model.controller;
                item.parent_id = model.parent_id;
                item.updated_by = model.updated_by;
                item.updated_date = model.updated_date;
                try { db.SaveChanges(); result = true; }
                catch (Exception) { throw; }
            }
            return result;
        }

        // delete
        // update
        public static bool hiddenMenu(int id)
        {
            var result = false;

            MMenuVM data = MMenuRepo.getById(id);

            using (AppEntity db = new AppEntity())
            {
                m_menu item = db.m_menu.Find(id);

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

        // get data by id
        public static MMenuVM getById(int id)
        {
            MMenuVM result = new MMenuVM();
            using (AppEntity db = new AppEntity())
            {
                result = db.m_menu.Select(model => new MMenuVM()
                {
                    id = model.id,
                    code = model.code,
                    name = model.name,
                    controller = model.controller,
                    parent_id = model.parent_id,
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

        public static List<MMenuVM> SearchData(string kode, string nama/*, string Created_by, DateTime Created_date*/)
        {
            List<MMenuVM> data = new List<MMenuVM>();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_menu.Select(model => new MMenuVM()
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

                var LastCode = db.m_menu.Select(x => new { x.code })
                    .OrderByDescending(x => x.code).FirstOrDefault();
                if (LastCode == null)
                {
                    result = "ME" + "0001";
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
                    result = "ME" + nol + angka2;
                }
            }
            return result;
        }

        public static bool CekNama(MMenuVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_menu.Where(x => x.name == model.name && x.is_active == true).ToList();
                if (data.Count > 0) result = true;
            }
                return result;
        }

        public static bool CekNama2(MMenuVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_menu.Where(x => x.name == model.name && x.is_active == true && x.id != model.id).ToList();
                if (data.Count > 0) result = true;
            }
            return result;
        }

        public static bool CekNama(string nama)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_menu.Where(x => x.name == nama && x.is_active == true).FirstOrDefault();
                if (data != null) result = true;

            }
                return result;
        }
    }
}

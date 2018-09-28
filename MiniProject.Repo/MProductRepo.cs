using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject.Model;
using MiniProject.ViewModel;

namespace MiniProject.Repo
{
    public class MProductRepo
    {
        public static List<MProductVM> GetAllData()
        {
            var data = new List<MProductVM>();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_product.Select(x => new MProductVM()
                {
                    id = x.id,
                    code = x.code,
                    name = x.name,
                    description = x.description,
                    is_active = x.is_active,
                    created_by = x.created_by,
                    created_date = x.created_date,
                    updated_by = x.updated_by,
                    updated_date = x.updated_date,
                    str_created_by = "Administrator"
                })
                .Where(x => x.is_active == true)
                .ToList();
            }
            return data;
        }

        public static MProductVM GetId(int id)
        {
            var data = new MProductVM();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_product.Select(x => new MProductVM()
                {
                    id = x.id,
                    code = x.code,
                    name = x.name,
                    description = x.description,
                    is_active = x.is_active,
                    created_by = x.created_by,
                    created_date = x.created_date,
                    updated_by = x.updated_by,
                    updated_date = x.updated_date
                })
                .Where(x => x.id == id)
                .FirstOrDefault();

            }
            return data;
        }

        public static bool CheckNama(string nama)
        {
            bool res = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_product.Where(x => x.name == nama).FirstOrDefault();
                if (data != null)
                {
                    res = true;
                }
            }
            return res;
        }

        public static bool CheckIfExists(MProductVM model)
        {
            bool res = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_product.Where(x => x.name == model.name && x.id != model.id).ToList();
                if (data.Count > 0)
                {
                    res = true;
                }
            }
            return res;
        }

        public static string AutoCode()
        {
            AppEntity db = new AppEntity();
            var auto = "";
            var jml = db.m_product.Select(x => new { kd = x.code })
                .OrderByDescending(x => x.kd).FirstOrDefault();
            if (jml == null)
            {
                auto = "PR" + "0001";
            }
            else
            {
                var kd = jml.kd;
                var idx = kd.Length;
                var angka = kd.Substring(idx - 4, 4);
                var inc = int.Parse(angka) + 1;
                auto = "PR" + inc.ToString("0000");
            }
            return auto;
        }

        public static bool Insert(MProductVM model, string userID)
        {
            bool res = false;
            using (AppEntity db = new AppEntity())
            {
                m_product data = new m_product()
                {
                    code = model.code,
                    name = model.name,
                    description = model.description,
                    created_by = 1,
                    created_date = DateTime.Now,
                    is_active = true
                };

                db.m_product.Add(data);
                try { db.SaveChanges(); res = true; } catch (Exception) { throw; }
            }
            return res;
        }

        public static bool Update(MProductVM model)
        {
            bool res = false;
            using (AppEntity db = new AppEntity())
            {
                m_product data = db.m_product.Find(model.id);
                data.code = model.code;
                data.name = model.name;
                data.description = model.description;
                data.updated_by = 1;
                data.updated_date = DateTime.Now;

                try { db.SaveChanges(); res = true; } catch (Exception) { throw; }
            }
            return res;
        }

        public static bool Delete(int id)
        {
            bool res = false;
            using (AppEntity db = new AppEntity())
            {
                m_product data = db.m_product.Find(id);
                data.id = data.id;
                data.code = data.code;
                data.name = data.name;
                data.is_active = false;
                data.updated_by = 1;
                data.updated_date = DateTime.Now;

                try { db.SaveChanges(); res = true; } catch (Exception) { throw; }
            }
            return res;
        }
    }
}

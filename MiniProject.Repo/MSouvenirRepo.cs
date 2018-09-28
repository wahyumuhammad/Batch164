using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject.Model;
using MiniProject.ViewModel;

namespace MiniProject.Repo
{
    public class MSouvenirRepo
    {
        public static List<MSouvenirVM> GetAllData()
        {
            var data = new List<MSouvenirVM>();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_souvenir.Select(x => new MSouvenirVM()
                {
                    id = x.id,
                    code = x.code,
                    name = x.name,
                    description = x.description,
                    m_unit_id = x.m_unit_id,
                    is_active = x.is_active,
                    created_by = x.created_by,
                    created_date = x.created_date,
                    updated_by = x.updated_by,
                    updated_date = x.updated_date,
                    str_unit = x.m_unit.name,
                    str_created_by = "Administrator"
                })
                .Where(x => x.is_active == true)
                .ToList();
            }
            return data;
        }

        public static MSouvenirVM GetId(int id)
        {
            var data = new MSouvenirVM();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_souvenir.Select(x => new MSouvenirVM()
                {
                    id = x.id,
                    code = x.code,
                    name = x.name,
                    description = x.description,
                    m_unit_id = x.m_unit_id,
                    is_active = x.is_active,
                    created_by = x.created_by,
                    created_date = x.created_date,
                    updated_by = x.updated_by,
                    updated_date = x.updated_date,
                    str_unit = x.m_unit.name
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
                var data = db.m_souvenir.Where(x => x.name == nama).FirstOrDefault();
                if (data != null)
                {
                    res = true;
                }
            }
            return res;
        }

        public static bool CheckIfExists(MSouvenirVM model)
        {
            bool res = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_souvenir.Where(x => x.name == model.name && x.id != model.id).ToList();
                if (data.Count > 0)
                {
                    res = true;
                }
            }
            return res;
        }

        public static List<MSouvenirVM> SearchData(MSouvenirVM model)
        {
            var data = new List<MSouvenirVM>();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_souvenir.Select(x => new MSouvenirVM()
                {
                    id = x.id,
                    code = x.code,
                    name = x.name,
                    description = x.description,
                    m_unit_id = x.m_unit_id,
                    is_active = x.is_active,
                    created_by = x.created_by,
                    created_date = x.created_date,
                    updated_by = x.updated_by,
                    updated_date = x.updated_date,
                    str_unit = x.m_unit.name
                })
                .Where(x => x.is_active == true && (x.code.Contains(model.code) || x.name.Contains(model.name) || x.m_unit_id == model.m_unit_id) || (x.created_date.Day == model.created_date.Day && x.created_date.Month == model.created_date.Month && x.created_date.Year == model.created_date.Year))
                .ToList();
            }
            return data;
        }

        public static string AutoCode()
        {
            AppEntity db = new AppEntity();
            var auto = "";
            var jml = db.m_souvenir.Select(x => new { kd = x.code })
                .OrderByDescending(x => x.kd).FirstOrDefault();
            if (jml == null)
            {
                auto = "SV" + "0001";
            }
            else
            {
                var kd = jml.kd;
                var idx = kd.Length;
                var angka = kd.Substring(idx - 4, 4);
                var inc = int.Parse(angka) + 1;
                auto = "SV" + inc.ToString("0000");
            }
            return auto;
        }

        public static bool Insert(MSouvenirVM model)
        {
            bool res = false;
            using (AppEntity db = new AppEntity())
            {
                m_souvenir data = new m_souvenir()
                {
                    code = model.code,
                    name = model.name,
                    description = model.description,
                    m_unit_id = model.m_unit_id,
                    created_by = 1,
                    created_date = DateTime.Now,
                    is_active = true
                };

                db.m_souvenir.Add(data);
                try { db.SaveChanges(); res = true; } catch (Exception) { throw; }
            }
            return res;
        }

        public static bool Update(MSouvenirVM model)
        {
            bool res = false;
            using (AppEntity db = new AppEntity())
            {
                m_souvenir data = db.m_souvenir.Find(model.id);
                data.code = model.code;
                data.name = model.name;
                data.description = model.description;
                data.m_unit_id = model.m_unit_id;
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
                m_souvenir data = db.m_souvenir.Find(id);
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

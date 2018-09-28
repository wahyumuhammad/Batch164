using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject.ViewModel;
using MiniProject.Model;

namespace MiniProject.Repo
{
    public class mUnitRepo
    {
        public static bool insert(mUnitVM model)
        {
            
            bool result = false;
            
            using (AppEntity db = new AppEntity())
            {
                m_unit item = new m_unit()
                {
                    code = model.code,
                    name = model.name,
                    description = model.description,
                    is_active= true,
                    created_by = 1,
                    created_date =  DateTime.Now.Date,
                    updated_by = 1,
                    updated_date =  DateTime.Now.Date,
                };
                db.m_unit.Add(item);
                try { db.SaveChanges(); result = true; } catch (Exception) { throw; }
            }
            return result;
        }

        public static String newCode()
        {
            String result = "UN" + "0001";
            using (AppEntity db = new AppEntity())
            {
                var item = db.m_unit.Select(x => new { code = x.code })
                    .Where(x => x.code.Contains("UN"))
                    .OrderByDescending(x => x.code)
                    .FirstOrDefault();
                if(item != null)
                {
                    int code = int.Parse(item.code.Substring(item.code.Length - 4, 4)) + 1;
                    result = "UN" + code.ToString("0000");
                }
            }
            return result;
        }

        public static bool cekNama(string nama)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_unit
                    .Where(x => x.name == nama).FirstOrDefault();
                if (data != null)
                { result = true; }
            }
            return result;
        }

        public static bool cekNama(mUnitVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_unit
                    .Where(x => x.name == model.name && x.id != model.id).ToList();
                if (data.Count > 0) result = true;
            }
            return result;
        }
        
        // update
        public static bool update(mUnitVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {               
                m_unit item = db.m_unit.Find(model.id);
                item.code = model.code;
                item.name = model.name;
                item.description = model.description;
                item.updated_by = 2;
                item.updated_date = DateTime.Now.Date;
                try { db.SaveChanges(); result = true; } catch (Exception) { throw; }
            }
            return result;
        }

        // delete

        public static bool HideUnit(int id)
        {
            var result = false;

            mUnitVM data = mUnitRepo.getById(id);

            using (AppEntity db = new AppEntity())
            {
                m_role item = db.m_role.Find(id);

                item.is_active = false;
                item.updated_by = 1;
                item.updated_date = DateTime.Now;

                try { db.SaveChanges(); result = true; } catch (Exception) { throw; }
            }

            return result;
        }

        
        // get all data
        public static List<mUnitVM> get()
        {
            List<mUnitVM> result = new List<mUnitVM>();
            using (AppEntity db = new AppEntity())
            {
                result = db.m_unit.Select(x => new mUnitVM()
                {
                    id = x.id,
                    code = x.code,
                    name = x.name,
                    description = x.description,
                    isActive= x.is_active,
                    createdBy = x.created_by,
                    createdDate = x.created_date,
                    updatedBy = x.updated_by,
                    updated_date = x.updated_date,
                }).Where(x => x.isActive== true).ToList();
            }
            return result;
        }
        // get data by id
        public static mUnitVM getById(int id)
        {
            mUnitVM result = new mUnitVM();
            using (AppEntity db = new AppEntity())
            {
                result = db.m_unit.Select(x => new mUnitVM()
                {
                    id = x.id,
                    code = x.code,
                    name = x.name,
                    description = x.description,
                    isActive= x.is_active,
                    createdBy = x.created_by,
                    createdDate = x.created_date,
                    updatedBy = x.updated_by,
                    updated_date = x.updated_date,
                })
                .Where(x => x.id == id)
                .FirstOrDefault();
            }
            return result;
        }        

        public static List<mUnitVM> Search(mUnitVM model)
        {
            var data = new List<mUnitVM>();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_unit.Select(x => new mUnitVM()
                {
                    id = x.id,
                    code = x.code,
                    name = x.name,
                    description = x.description,
                    isActive= x.is_active,
                    createdBy = x.created_by,
                    createdDate = x.created_date,
                }).Where(x => (x.isActive== true) && (x.code.Contains(model.code) || x.name.Contains(model.name)) || (x.createdDate.Day == model.createdDate.Day && x.createdDate.Month == model.createdDate.Month && x.createdDate.Year == model.createdDate.Year))
                .ToList();
            }
            return data;
        }
    }
}

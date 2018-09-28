using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject.Model;
using MiniProject.VM;

namespace MiniProject.Repo
{
    public class MCompanyRepo
    {
        public static List<MCompanyVM> get ()
        {
            List<MCompanyVM> result = new List<MCompanyVM>();
            using (AppEntity db = new AppEntity())
            {
                result = db.m_company.Select(x => new MCompanyVM()
                {
                    id=x.id,
                    code=x.code,
                    name=x.name,
                    address=x.address,
                    phone=x.phone,
                    email=x.email,
                    is_active =x.is_active,
                    created_by=x.created_by,
                    created_date=x.created_date,
                    updated_by=x.updated_by,
                    updated_date=x.updated_date,

                }).Where(x=>x.is_active==true)
                .ToList();
            }
                return result;
        }
        public static bool insert(MCompanyVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                m_company item = new m_company()
                {
                    code = model.code,
                    name = model.name,
                    address = model.address,
                    phone = model.phone,
                    email = model.email,
                    is_active = true,
                    created_by=1,
                    created_date=DateTime.Now
                   

                };
                db.m_company.Add(item);
                try { db.SaveChanges(); result = true; } catch (Exception) { throw; }
            }
                return result;
        }
        
        public static string KodeAuto()
        {
            string result = "";
            using (AppEntity db = new AppEntity())
            {
                var LastCode = db.m_company.Select(x => new { Code = x.code })
                                            .OrderByDescending(x => x.Code).FirstOrDefault();

                if(LastCode == null)
                {
                    result = "CP0001";
                }
                else
                {
                    var kd = LastCode.Code;
                    var idx = LastCode.Code.Length;
                    var angka = kd.Substring(idx - 4, 4);
                    var inc = int.Parse(angka) + 1;
                    result = "CP" + inc.ToString("0000");
                }

            }
                return result;
        }
        public static MCompanyVM getbyid(int id)
        {

            MCompanyVM hasil = new MCompanyVM();
            using (AppEntity db = new AppEntity())
            {
                hasil = db.m_company.Select(x => new MCompanyVM()
                {
                    id = x.id,
                    code = x.code,
                    name = x.name,
                    address = x.address,
                    phone = x.phone,
                    email = x.email,
                    created_by = x.created_by,
                    created_date = x.created_date


                }).Where(x => x.id == id).FirstOrDefault();
            }
            return hasil;
        }

        public static bool Delete(MCompanyVM model)
        {
            bool result = false;
            
            using (AppEntity db = new AppEntity())
            {
                m_company item = db.m_company.Find(model.id);
                item.is_active = false;
                item.updated_by = 1;
                item.updated_date = DateTime.Now;
                try { db.SaveChanges(); result = true; } catch (Exception) { throw; }

            }
            
                return result;
        }

        
        
        public static bool update(MCompanyVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                m_company item = db.m_company.Find(model.id);
                item.id = model.id;
                item.code = model.code;
                item.name = model.name;
                item.address = model.address;
                item.phone = model.phone;
                item.is_active = true;
                item.email = model.email;
                item.updated_by = 1;
                item.updated_date = DateTime.Now;
                try { db.SaveChanges(); result = true; } catch (Exception) { throw; }
            }
                return result;
        }

        public static MCompanyVM GetDetail(int id)
        {

            MCompanyVM hasil = new MCompanyVM();
            using (AppEntity db = new AppEntity())
            {
                hasil = db.m_company.Select(x => new MCompanyVM()
                {
                    id=x.id,
                    code = x.code,
                    name = x.name,
                    address = x.address,
                    phone = x.phone,
                    is_active=x.is_active,
                    email = x.email,
                    created_by = x.created_by,
                    created_date = x.created_date


                }).Where(x => x.id == id).FirstOrDefault();
            }
            return hasil;
        }

        //public static List<m_companyVM> SearchData(m_companyVM model)
        //{
            
        //    var data = new List<m_companyVM>();
        //    using (AppEntity db = new AppEntity())
        //    {
        //        data = db.m_company.Select(x => new m_companyVM()
        //        {
        //            id = x.id,
        //            code = x.code,
        //            name = x.name,
        //            address = x.address,
        //            phone = x.phone,
        //            email = x.email,
        //            is_active = x.is_active,
        //            created_by = x.created_by,
        //            created_date = x.created_date,
        //            updated_by = x.updated_by,
        //            updated_date = x.updated_date,

        //        })
        //        .Where(x => x.is_active == true && ( x.code.Contains(model.code)  || x.name.Contains(model.name)||(x.created_date.Day == model.created_date.Day && x.created_date.Month == model.created_date.Month && x.created_date.Year == model.created_date.Year)))
        //        .ToList();
        //    }
        //    return data;
        //}

        public static bool CheckCompany(MCompanyVM model)
        {
            bool result = false;

            using (AppEntity db = new AppEntity())
            {
                var data = db.m_company.Where(x=>x.name == model.name && x.id!= model.id).ToList();
                if (data.Count>0)
                {
                    result = true ;
                }
            }
                return result;

        }

        public static bool CheckNameCompany (string nama)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_company.Where(x => x.name == nama ).FirstOrDefault();
                if(data != null)
                {
                    result = true;
                }
            }
            return result;
        }


    }
}

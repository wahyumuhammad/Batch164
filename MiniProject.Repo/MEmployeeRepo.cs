using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject.Model;
using MiniProject.VM;

namespace MiniProject.Repo
{
    public class MEmployeeRepo
    {
        public static List<MEmployeeVM> get()
        {
            List<MEmployeeVM> result = new List<MEmployeeVM>();
            using (AppEntity db = new AppEntity())
            {
                result = db.m_employee
                    .Include("Company")
                    .Select(x => new MEmployeeVM()
                    {
                        id = x.id,
                        employee_number = x.employee_number,
                        first_name = x.first_name,
                        last_name = x.last_name,
                        m_company_id = x.m_company_id,
                        email=x.email,
                        is_active = x.is_active,
                        created_by = x.created_by,
                        created_date = x.created_date,
                        updated_by = x.updated_by,
                        updated_date = x.updated_date,
                        NmCompany=x.m_company.name,
                        FullName = x.first_name + " " + x.last_name

                    }).Where(x => x.is_active == true)
                .ToList();
            }
            return result;
        }

        public static bool insert(MEmployeeVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                m_employee item = new m_employee()
                {
                    employee_number = model.employee_number,
                    first_name = model.first_name,
                    last_name = model.last_name,
                    m_company_id = model.m_company_id,
                    email=model.email,
                    
                    is_active = true,
                    created_by = 1,
                    created_date = DateTime.Now


                };
                db.m_employee.Add(item);
                try { db.SaveChanges(); result = true; } catch (Exception) { throw; }
            }
            return result;
        }

        public static MEmployeeVM getbyid(int id)
        {

            MEmployeeVM hasil = new MEmployeeVM();
            using (AppEntity db = new AppEntity())
            {
                hasil = db.m_employee.Select(x => new MEmployeeVM()
                {
                    id = x.id,
                    employee_number = x.employee_number,
                    first_name = x.first_name,
                    last_name = x.last_name,
                    m_company_id = x.m_company_id,
                    NmCompany = x.m_company.name,
                    email = x.email,
                    created_by = x.created_by,
                    created_date = x.created_date,
                    FullName = x.first_name + " " + x.last_name


                }).Where(x => x.id == id).FirstOrDefault();
            }
            return hasil;
        }

        public static bool Delete(int id)
        {
            bool result = false;

            using (AppEntity db = new AppEntity())
            {
                m_employee item = db.m_employee.Find(id);
                item.is_active = false;
                item.updated_by = 1;
                item.updated_date = DateTime.Now;
                try { db.SaveChanges(); result = true; } catch (Exception) { throw; }

            }

            return result;
        }



        public static bool update(MEmployeeVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                m_employee item = db.m_employee.Find(model.id);
                item.employee_number = model.employee_number;
                item.first_name = model.first_name;
                item.last_name = model.last_name;
                item.m_company_id = model.m_company_id;
                
                item.email = model.email;
                item.is_active = true;
                item.created_by = 1;
                item.created_date = DateTime.Now;
                try { db.SaveChanges(); result = true; } catch (Exception) { throw; }
            }
            return result;
        }

        public static MEmployeeVM GetDetail(int id)
        {

            MEmployeeVM hasil = new MEmployeeVM();
            using (AppEntity db = new AppEntity())
            {
                hasil = db.m_employee.Select(x => new MEmployeeVM()
                {
                    id = x.id,
                    employee_number = x.employee_number,
                    first_name = x.first_name,
                    last_name = x.last_name,
                    m_company_id = x.m_company_id,
                    NmCompany = x.m_company.name,
                    email = x.email,
                    is_active = x.is_active,
                    created_by = x.created_by,
                    created_date = x.created_date,
                    updated_by = x.updated_by,
                    updated_date = x.updated_date


                }).Where(x => x.id == id).FirstOrDefault();
            }
            return hasil;
        }

        //public static List<m_employeeVM> SearchData(m_employeeVM model)
        //{


        //    var data = new List<m_employeeVM>();
        //    using (AppEntity db = new AppEntity())
        //    {
        //        data = db.m_employee.Select(x => new m_employeeVM()
        //        {
        //            id = x.id,
        //            employee_number = x.employee_number,
        //            first_name = x.first_name,
        //            last_name = x.last_name,
        //            m_company_id = x.m_company_id,
        //            email = x.email,
        //            is_active = x.is_active,
        //            created_by = x.created_by,
        //            created_date = x.created_date,
        //            updated_by = x.updated_by,
        //            updated_date = x.updated_date,
        //            NmCompany = x.m_company.name,
        //            FullName = x.first_name + " " + x.last_name


        //        })
        //        .Where(x => x.is_active == true && (x.employee_number.Contains(model.employee_number) || x.FullName.Contains(model.FullName)||x.NmCompany.Contains(model.NmCompany)|| (x.created_date.Day == model.created_date.Day && x.created_date.Month == model.created_date.Month && x.created_date.Year == model.created_date.Year)))
        //        .ToList();
        //    }
        //    return data;
        //}

        public static bool Hitung(int id)
        {
            bool result = false;
           
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_employee.Where(x => x.m_company_id == id).ToList();
                if(data.Count>0)
                {
                    result = true;
                }
                
            }
            return result;
        }

        public static bool HitungEmplo(MEmployeeVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_employee.Where(x => x.employee_number == model.employee_number && x.id!=model.id).ToList();
                {
                    if(data.Count>0)
                    {
                        result = true;
                    }
                }
            }
                return result;
        }

        public static bool CheckNama (string Nama)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_employee.Where(x => x.employee_number == Nama).FirstOrDefault();
                if(data!=null)
                {
                    result = true;
                }
            }
                return result;
        }

        public static List<MEmployeeVM> getDataNonUser()
        {
            List<MEmployeeVM> data = new List<MEmployeeVM>();
            using (AppEntity db = new AppEntity())
            {
                var query = from listEmployee in db.m_employee
                            join user in db.m_user on listEmployee.id equals user.m_employee_id into listKar
                            from t in listKar.DefaultIfEmpty()
                            where t == null
                            select new MEmployeeVM { id = listEmployee.id, FullName = listEmployee.first_name + " " + listEmployee.last_name };
                data = query.ToList();
            }
            return data;
        }
    }
}

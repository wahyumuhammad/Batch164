using MiniProject.Model;
using MiniProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Repo
{
    public class MUserRepo
    {
        public static List<MUserVM> get()
        {
            List<MUserVM> data = new List<MUserVM>();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_user
                    .Include("m_employee")
                    .Include("m_role")
                    .Select(x => new MUserVM()
                    {
                        id = x.id,
                        username = x.username,
                        createdDate = x.created_date,
                        createdBy = x.created_by,
                        firtsName = x.m_employee.first_name,
                        lastName = x.m_employee.last_name,
                        nameRole = x.m_role.name,
                        isActive = x.is_active,
                        company = x.m_employee.m_company.name,
                        fullName = x.m_employee.first_name + x.m_employee.last_name
                    })
                    .ToList();
            }
            return data;
        }

        public static bool delete(int id)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_user.Find(id);
                data.is_active = false;
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
        public static bool update(MUserVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_user.Find(model.id);
                data.id = model.id;
                data.m_employee_id = model.mEmployeeId;
                data.m_role_id = model.mRoleId;
                data.username = model.username;
                data.password = model.password;
                data.updated_by = model.updatedBy;
                data.updated_date = DateTime.Now;
                data.is_active = true;
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

        public static bool insert(MUserVM model)
        {
            bool result = false;
            using (AppEntity db = new AppEntity())
            {
                model.isActive = true;
                m_user data = new m_user()
                {
                    m_employee_id = model.mEmployeeId,
                    m_role_id = model.mRoleId,
                    username = model.username,
                    password = model.password,
                    created_by = model.createdBy,
                    created_date = DateTime.Now,
                    is_active = model.isActive
                };
                db.m_user.Add(data);
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

        public static MUserVM getById(int id)
        {
            var data = new MUserVM();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_user.Select(x => new MUserVM()
                {
                    id = x.id,
                    username = x.username,
                    password = x.password,
                    mRoleId = x.m_role_id,
                    mEmployeeId = x.m_employee_id,
                    nameRole = x.m_role.name,
                    firtsName = x.m_employee.first_name,
                    lastName = x.m_employee.last_name
                })
                .Where(x => x.id == id)
                .FirstOrDefault();
            }
            return data;
        }
        public static List<MUserVM> search(string user)
        {
            var data = new List<MUserVM>();
            using (AppEntity db = new AppEntity())
            {
                data = db.m_user.Select(x => new MUserVM()
                {
                    id = x.id,
                    username = x.username,
                    password = x.password,
                    mRoleId = x.m_role_id,
                    mEmployeeId = x.m_employee_id,
                    nameRole = x.m_role.name,
                    isActive = x.is_active,
                    createdBy = x.created_by,
                    createdDate = x.created_date,
                    firtsName = x.m_employee.first_name,
                    lastName = x.m_employee.last_name
                })
                .Where(x => x.username.Contains(user.ToLower()))
                .ToList();
            }
            return data;
        }

        public static bool CheckNama(string nama)
        {
            bool res = false;
            using (AppEntity db = new AppEntity())
            {
                var data = db.m_user.Where(x => x.username == nama).FirstOrDefault();
                if (data != null)
                {
                    res = true;
                }
            }
            return res;
        }
    }
}

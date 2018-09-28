using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MiniProject.Model;

namespace MiniProject.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() {}
        public CustomRole(string name) { Name = name; }
        //[Key]
        //public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int updated_by { get; set; }
        public DateTime updated_date { get; set; }
        public virtual ICollection<CustomMenuAcces> m_menu_access { get; set; }
        //public virtual ICollection<CustomUser> m_user { get; set; }

    }

    public class CustomMenu
    {

        [Key]
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string controller { get; set; }
        public Nullable<int> parent_id { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public virtual ICollection<CustomMenuAcces> m_menu_access { get; set; }
    }

    public class CustomMenuAcces
    {
        [Key]
        public int id { get; set; }
        public int? m_role_id { get; set; }
        public int m_menu_id { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }

        [ForeignKey("m_menu_id")]
        public virtual CustomMenu m_menu { get; set; }
        [ForeignKey("m_role_id")]
        public virtual CustomRole m_role { get; set; }
    }

    //public class CustomUser
    //{
    //    [Key]
    //    public int id { get; set; }
    //    public string username { get; set; }
    //    public string password { get; set; }
    //    public int m_role_id { get; set; }
    //    public int m_employee_id { get; set; }
    //    public bool is_active { get; set; }
    //    public int created_by { get; set; }
    //    public DateTime created_date { get; set; }
    //    public Nullable<int> updated_by { get; set; }
    //    public Nullable<DateTime> updated_date { get; set; }
    //    [ForeignKey("m_role_id")]
    //    public virtual CustomRole m_role { get; set; }
    //    [ForeignKey("m_employee_id")]
    //    public virtual m_employee m_employee { get; set; }
    //}

    public class CustomUserStrore : UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStrore(ApplicationDbContext context) : base(context)
        {

        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("m_application_user");
            modelBuilder.Entity<CustomRole>().ToTable("m_role");
            modelBuilder.Entity<CustomUserRole>().ToTable("m_user_role");
            modelBuilder.Entity<CustomUserClaim>().ToTable("m_user_claim");
            modelBuilder.Entity<CustomUserLogin>().ToTable("m_user_login");
            modelBuilder.Entity<CustomMenu>().ToTable("m_menu");
            modelBuilder.Entity<CustomMenuAcces>().ToTable("m_menu_access");


        }

        public ApplicationDbContext()
            : base("IdentityConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
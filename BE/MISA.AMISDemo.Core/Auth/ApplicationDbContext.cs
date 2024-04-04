using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMISDemo.Core.Auth
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("Account"); // Đặt tên bảng cho người dùng

                // Sửa đổi tên cột khóa chính từ Id thành AccountId
                b.Property(e => e.Id).HasColumnName("AccountId");

                b.Property(e => e.PasswordHash).HasColumnName("Password");

            });
        }
    }
}

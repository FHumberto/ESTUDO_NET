using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.DbContext;

//? herda do IdentityDbContext que permite acesso a tabelas exclusivas do identity
public class HrLeaveManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public HrLeaveManagementIdentityDbContext(DbContextOptions<HrLeaveManagementIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //? aplica qualquer configuração que esteja onde o assembly está (/Configurations)
        builder.ApplyConfigurationsFromAssembly(typeof(HrLeaveManagementIdentityDbContext).Assembly);
    }
}

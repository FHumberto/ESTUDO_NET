using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        // exemplo de seed
        builder.HasData(
            new LeaveType { Id = 1, Name = "Vacation", DefaultDays = 10, DateCreated = DateTime.Now, DateModified = DateTime.Now });

        // design
        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}

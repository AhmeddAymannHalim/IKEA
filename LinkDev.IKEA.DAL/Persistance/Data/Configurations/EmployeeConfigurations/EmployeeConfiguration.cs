using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Data.Configurations.EmployeeConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();

           

            builder.Property(E => E.Address).HasColumnType("varchar(20)").IsRequired(false);

            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            builder.Property(E => E.Gender)
                   .HasConversion(
                   (gender) => gender.ToString(),
                   (gender) => (Gender) Enum.Parse(typeof(Gender), gender)
                   );

            builder.Property(E => E.EmployeeType)
                   .HasConversion(
                   (Type) => Type.ToString(),
                   (Type) => (EmployeeType) Enum.Parse(typeof(EmployeeType), Type)
                   );

            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}

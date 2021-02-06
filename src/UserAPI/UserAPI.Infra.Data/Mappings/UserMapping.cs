using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAPI.Domain.Entities;

namespace UserAPI.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {            
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Gender).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.CPF).IsRequired();           
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAPI.Domain.Entities;

namespace UserAPI.Infra.Data.Mappings
{
    public class JobMapping : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Name).IsRequired();           
        }
    }
}

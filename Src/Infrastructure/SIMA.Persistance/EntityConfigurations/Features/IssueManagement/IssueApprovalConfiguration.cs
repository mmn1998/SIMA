using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pipelines.Sockets.Unofficial.Arenas;
using SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

public class IssueApprovalConfiguration : IEntityTypeConfiguration<IssueApproval>
{
    public void Configure(EntityTypeBuilder<IssueApproval> entity)
    {
        entity.ToTable("IssueApproval", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueApprovalId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.Description).IsUnicode();
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

    }
}

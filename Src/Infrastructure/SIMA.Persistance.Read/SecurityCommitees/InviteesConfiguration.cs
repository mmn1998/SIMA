using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.ValueObjects;

namespace SIMA.Persistance.Read.SecurityCommitees;

public class InviteesConfiguration : IEntityTypeConfiguration<Invitees>
{
    public void Configure(EntityTypeBuilder<Invitees> entity)
    {
        entity.ToTable("Invitees", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new InviteesId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.MeetingScheduleId)
            .HasConversion(x => x.Value, v => new MeetingScheduleId(v));
        entity.HasOne(x => x.MeetingSchedule)
            .WithMany(x => x.Inviteeses)
            .HasForeignKey(x => x.MeetingScheduleId);

        entity.Property(x => x.StaffId)
            .HasConversion(x => x.Value, v => new StaffId(v));
        entity.HasOne(x => x.Staff)
            .WithMany(x => x.Invitees)
            .HasForeignKey(x => x.StaffId);

        entity.Property(x => x.CompanyId)
            .HasConversion(x => x.Value, v => new CompanyId(v));
        entity.HasOne(x => x.Company)
            .WithMany(x => x.Invitees)
            .HasForeignKey(x => x.CompanyId);

        entity.Property(x => x.DepartmentId)
            .HasConversion(x => x.Value, v => new DepartmentId(v));
        entity.HasOne(x => x.Department)
            .WithMany(x => x.Invitees)
            .HasForeignKey(x => x.DepartmentId);
    }
}

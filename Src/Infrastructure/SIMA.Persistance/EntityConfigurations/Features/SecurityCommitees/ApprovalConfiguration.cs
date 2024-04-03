using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.SecurityCommitees;

public class ApprovalConfiguration : IEntityTypeConfiguration<Approval>
{
    public void Configure(EntityTypeBuilder<Approval> entity)
    {
        entity.ToTable("Approval", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new ApprovalId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.IssueId)
            .HasConversion(x => x.Value, v => new IssueId(v));
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.Approvals)
            .HasForeignKey(x => x.IssueId);

        entity.Property(x => x.MeetingId)
            .HasConversion(x => x.Value, v => new MeetingId(v));
        entity.HasOne(x => x.Meeting)
            .WithMany(x => x.Approvals)
            .HasForeignKey(x => x.MeetingId);

        entity.Property(x => x.ResponsibleCompanyId)
            .HasConversion(x => x.Value, v => new CompanyId(v));
        entity.HasOne(x => x.ResponsibleCompany)
            .WithMany(x => x.ResponsibleApprovals)
            .HasForeignKey(x => x.ResponsibleCompanyId);

        entity.Property(x => x.SupervisorCompanyId)
            .HasConversion(x => x.Value, v => new CompanyId(v));
        entity.HasOne(x => x.SupervisorCompany)
            .WithMany(x => x.SupervisorApprovals)
            .HasForeignKey(x => x.SupervisorCompanyId);

        entity.Property(x => x.ResponsibleDepartmentId)
            .HasConversion(x => x.Value, v => new DepartmentId(v));
        entity.HasOne(x => x.ResponsibleDepartment)
            .WithMany(x => x.ResponsibleApprovals)
            .HasForeignKey(x => x.ResponsibleDepartmentId);

        entity.Property(x => x.SupervisorDepartmentId)
            .HasConversion(x => x.Value, v => new DepartmentId(v));
        entity.HasOne(x => x.SupervisorDepartment)
            .WithMany(x => x.SupervisorApprovals)
            .HasForeignKey(x => x.SupervisorDepartmentId);

        entity.Property(x => x.ResponsibleStaffId)
            .HasConversion(x => x.Value, v => new StaffId(v));
        entity.HasOne(x => x.ResponsibleStaff)
            .WithMany(x => x.ResponsibleApprovals)
            .HasForeignKey(x => x.ResponsibleStaffId);

        entity.Property(x => x.SupervisorStaffId)
            .HasConversion(x => x.Value, v => new StaffId(v));
        entity.HasOne(x => x.SupervisorStaff)
            .WithMany(x => x.SupervisorApprovals)
            .HasForeignKey(x => x.SupervisorStaffId);

        //entity.HasIndex(x => x.Code).IsUnique();
        //entity.Property(x => x.Name).HasMaxLength(200).IsUnicode();
        //entity.Property(x => x.Code)
        //            .HasMaxLength(20).IsUnicode();
    }
}
public class ResponsibleAnswerTypeConfiguration : IEntityTypeConfiguration<ResponsibleAnswerType>
{
    public void Configure(EntityTypeBuilder<ResponsibleAnswerType> entity)
    {
        entity.ToTable("ResponsibleAnswerType", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new ResponsibleAnswerTypeId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Property(x => x.Name).HasMaxLength(200).IsUnicode();
        entity.Property(x => x.Code)
                    .HasMaxLength(20).IsUnicode();
    }
}
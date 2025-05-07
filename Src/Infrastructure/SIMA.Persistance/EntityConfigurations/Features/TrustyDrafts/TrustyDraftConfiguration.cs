using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class TrustyDraftConfiguration : IEntityTypeConfiguration<TrustyDraft>
{
    public void Configure(EntityTypeBuilder<TrustyDraft> entity)
    {
        entity.ToTable("TrustyDraft", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.DraftNumber).HasMaxLength(20);
        entity.HasIndex(x => x.DraftNumber).IsUnique();
        entity.Property(x => x.DraftNumberBasedOnOrder).HasMaxLength(20);
        entity.HasIndex(x => x.DraftNumberBasedOnOrder).IsUnique();
        entity.Property(x => x.CustomerAccountNumber).HasMaxLength(20);
        entity.Property(x => x.BlockingNumber).HasMaxLength(20);
        entity.Property(x => x.BranchLetterNumber).HasMaxLength(30);
        //entity.HasIndex(x => x.BlockingNumber).IsUnique();
        entity.Property(x => x.BlockNumber).HasMaxLength(30);
        entity.Property(x => x.IsFinished).HasMaxLength(1).IsFixedLength();
        entity.Property(x => x.IsFromAgentBank).HasMaxLength(1).IsFixedLength();
        entity.Property(x => x.PayingBankName).HasMaxLength(200);
        entity.Property(x => x.BeneficiaryBankName).HasMaxLength(200);
        entity.Property(x => x.BeneficiaryName).HasMaxLength(200);
        entity.Property(x => x.RegisterCode).HasMaxLength(200);
        entity.Property(x => x.AcceptorName).HasMaxLength(200);
        entity.Property(x => x.BeneficiaryAccountNumber).HasMaxLength(50);
        entity.Property(x => x.BeneficiaryPhoneNumber).HasMaxLength(20);
        entity.Property(x => x.BeneficiaryIban).HasMaxLength(50);
        entity.Property(x => x.BeneficiaryPassportNumber).HasMaxLength(20);
        entity.Property(x => x.IntermediaryBank).HasMaxLength(200);
        entity.Property(x => x.AgentBank).HasMaxLength(200);
        entity.Property(x => x.DraftOrderNumber).HasMaxLength(50);
        entity.Property(x => x.CustomerIban).HasMaxLength(50);
        entity.Property(x => x.CustomerPhoneNumber).HasMaxLength(20);
        entity.Property(x => x.CustomerPhoneNumber).HasMaxLength(12);
        entity.Property(x => x.CancellationReferenceNumber).HasMaxLength(50);
        entity.Property(x => x.CancellationValorNumber).HasMaxLength(50);

        entity.Property(e => e.StaffId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Staff)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftStatusId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftStatus)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.DraftStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.PaymentTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.PaymentType)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.PaymentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.IssueId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.IssueId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.ResponsibilityWageTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.ResponsibilityWageType)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.ResponsibilityWageTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.CancellationCurrencyTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.CancellationCurrencyType)
            .WithMany(x => x.CancellationTrustyDrafts)
            .HasForeignKey(x => x.CancellationCurrencyTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.CancellationResaonId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.CancellationResaon)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.CancellationResaonId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftDestinationId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftDestination)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.DraftDestinationId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftCurrencyOriginId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftCurrencyOrigin)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.DraftCurrencyOriginId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.RequestValorId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.RequestValor)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.RequestValorId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.LoanTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.LoanType)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.LoanTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftReviewResultId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftReviewResult)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.DraftReviewResultId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.AccountTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.AccountType)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.AccountTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.BranchId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Branch)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.BrokerId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Broker)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.BrokerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.BrokerTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.BrokerType)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.BrokerTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.CustomerId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Customer)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftCurrencyTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftCurrencyType)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.DraftCurrencyTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftNetCurrencyTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftNetCurrencyType)
            .WithMany(x => x.NetTrustyDrafts)
            .HasForeignKey(x => x.DraftNetCurrencyTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftOriginId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftOrigin)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.DraftOriginId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftType)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.DraftTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftValorStatusId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftValorStatus)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.DraftValorStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.BrokerSecondLevelAddressBookId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.BrokerSecondLevelAddressBook)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.BrokerSecondLevelAddressBookId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.WageDeductionMethodId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.WageDeductionMethod)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.WageDeductionMethodId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.InquiryRequestId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.InquiryRequest)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.InquiryRequestId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.AgentBankWageShareStatusId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.AgentBankWageShareStatus)
            .WithMany(x => x.TrustyDrafts)
            .HasForeignKey(x => x.AgentBankWageShareStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

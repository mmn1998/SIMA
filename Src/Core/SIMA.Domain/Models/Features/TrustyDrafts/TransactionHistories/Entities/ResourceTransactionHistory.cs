using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TransactionHistories.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.TrustyDrafts.TransactionHistories.Entities
{
    public class ResourceTransactionHistory : Entity
    {
        public ResourceTransactionHistoryId Id { get;  private set; }
        public FinancialSupplierId FinancialSupplierId { get;  private set; }
        public virtual FinancialSupplier FinancialSupplier { get;  private set; }
        public ResourceId ResourceId { get;  private set; }
        public virtual Resource Resource { get;  private set; }
        public FinancialActionTypeId FinancialActionTypeId { get;  private set; }
        public virtual FinancialActionType FinancialActionType { get;  private set; }
        public decimal Amount { get;  private set; }
        public DateTime EffectedDate { get;  private set; }
        public decimal? BalanceBeforeTransaction { get;  private set; }
        public decimal? CurrentBalance { get;  private set; }
        public string IsBlocked { get;  private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

    }
}

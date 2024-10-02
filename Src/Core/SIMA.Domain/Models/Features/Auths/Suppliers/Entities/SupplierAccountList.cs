using SIMA.Domain.Models.Features.Auths.Suppliers.Args;
using SIMA.Domain.Models.Features.Auths.Suppliers.Contracts;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Suppliers.Entities
{
    public class SupplierAccountList :Entity
    {
        private SupplierAccountList() { }

        private SupplierAccountList(CreateSupplierAccountListArg arg)
        {
            Id = new SupplierAccountListId(IdHelper.GenerateUniqueId());
            SupplierId = new SupplierId(arg.SupplierId);
            IBAN = arg.IBAN;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<SupplierAccountList> Create(CreateSupplierAccountListArg arg)
        {
            return new SupplierAccountList(arg);
        }

        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        public void ChangeStatus(ActiveStatusEnum activeStatus , long userId)
        {
            ActiveStatusId = (long)activeStatus;
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        }
        public SupplierAccountListId Id { get; private set; }
        public SupplierId SupplierId { get; private set; }
        public virtual Supplier Supplier { get; private set; }
        public string IBAN { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}

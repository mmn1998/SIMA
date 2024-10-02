using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Suppliers.Args;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Suppliers.Entities
{
    public class SupplierPhoneBook : Entity
    {
        private SupplierPhoneBook() { }

        private SupplierPhoneBook(CreateSupplierPhoneBookArg arg)
        {
            Id = new SupplierPhoneBookId(IdHelper.GenerateUniqueId());
            SupplierId = new SupplierId(arg.SupplierId);
            PhoneTypeId = new PhoneTypeId(arg.PhoneTypeId);
            PhoneNumber = arg.PhoneNumber;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<SupplierPhoneBook> Create(CreateSupplierPhoneBookArg arg)
        {
            return new SupplierPhoneBook(arg);
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
        public SupplierPhoneBookId Id { get; private set; }
        public SupplierId SupplierId { get; private set; }
        public virtual Supplier Supplier { get; private set; }
        public PhoneTypeId PhoneTypeId { get; private set; }
        public virtual PhoneType PhoneType { get; private set; }
        public string PhoneNumber { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}

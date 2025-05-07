using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Suppliers.Args;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Suppliers.Entities
{
    public class SupplierAddressBook : Entity
    {
        private SupplierAddressBook() { }

        private SupplierAddressBook(CreateSupplierAddressBookArg arg)
        {
            Id = new SupplierAddressBookId(IdHelper.GenerateUniqueId());
            SupplierId = new SupplierId(arg.SupplierId);
            AddressTypeId = new AddressTypeId(arg.AddressTypeId);
            PostalCode = arg.PostalCode;
            Address = arg.Address;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<SupplierAddressBook> Create(CreateSupplierAddressBookArg arg)
        {
            return new SupplierAddressBook(arg);
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
        public SupplierAddressBookId Id { get; private set; }
        public SupplierId SupplierId { get; private set; }
        public virtual Supplier Supplier { get; private set; }
        public AddressTypeId AddressTypeId { get; private set; }
        public virtual AddressType AddressType {  get; private set; }
        public string PostalCode {  get; private set; }
        public string Address { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}

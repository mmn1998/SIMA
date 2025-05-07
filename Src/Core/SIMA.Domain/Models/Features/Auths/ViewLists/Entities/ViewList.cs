using SIMA.Domain.Models.Features.Auths.ViewLists.Args;
using SIMA.Domain.Models.Features.Auths.ViewLists.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.ViewLists.Entities
{
    public class ViewList : Entity
    {
        private ViewList()
        {

        }

        private ViewList(CreateViewListArg arg)
        {
            Id = new ViewId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<ViewList> Create(CreateViewListArg arg)
        {
            return new ViewList(arg);
        }


        public async Task Modify(ModifyViewListArg arg)
        {
            Name = arg.Name;
            Code = arg.Code;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }

        public ViewId Id { get; private set; }
        public string? Name { get; private set; }
        public string? Code { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        private List<ViewField> _viewFields = new();
        public ICollection<ViewField> ViewFields => _viewFields;
    }
}

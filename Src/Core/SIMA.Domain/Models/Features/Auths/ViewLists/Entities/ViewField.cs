using SIMA.Domain.Models.Features.Auths.ViewLists.Args;
using SIMA.Domain.Models.Features.Auths.ViewLists.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.ViewLists.Entities
{
    public class ViewField : Entity
    {
        private ViewField()
        {

        }

        private ViewField(CreateViewFieldArg arg)
        {
            Id = new ViewFieldId(IdHelper.GenerateUniqueId());
            ViewId = new ViewId(arg.ViewId);
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public static async Task<ViewField> Create(CreateViewFieldArg arg)
        {
            return new ViewField(arg);
        }
        public async Task Modify(ModifyViewFieldArg arg)
        {
            ViewId = new ViewId(arg.ViewId);
            Name = arg.Name;
            Code = arg.Code;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public ViewFieldId Id { get; private set; }
        public ViewId ViewId { get; private set; }
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

        public virtual ViewList ViewList { get; set; }

    }
}

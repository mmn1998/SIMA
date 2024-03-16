using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.Entites
{
    public class ActionType : Entity
    {
        private ActionType()
        {

        }
        private ActionType(CreateActionTypeArg arg)
        {
            Id = new ActionTypeId(IdHelper.GenerateUniqueId());
            Code = arg.Code;
            Name = arg.Name;
            MainType = arg.MainType;
            EventInternalTag = arg.EventInternalTag;
            EventTag = arg.EventTag;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public static async Task<ActionType> New(CreateActionTypeArg arg)
        {
            return new ActionType(arg);
        }

        public async Task Modify(ModifyActionTypeArg arg)
        {
            Code = arg.Code;
            Name = arg.Name;
            MainType = arg.MainType;
            EventInternalTag = arg.EventInternalTag;
            EventTag = arg.EventTag;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
        }

        public void Delete()
        {
            ActiveStatusId =  (long)ActiveStatusEnum.Delete;
        }

       

        public ActionTypeId Id { get;  set; }
        public string MainType { get; set; }
        public string? Name { get;  set; }
        public string? Code { get;  set; }
        public string? EventTag { get; private set; }
        public string? EventInternalTag { get;  set; }
        public long? ActiveStatusId { get;  set; }
        public DateTime? CreatedAt { get;  set; }
        public long? CreatedBy { get;  set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
        public ICollection<Step> steps{ get; set; }

    }
}

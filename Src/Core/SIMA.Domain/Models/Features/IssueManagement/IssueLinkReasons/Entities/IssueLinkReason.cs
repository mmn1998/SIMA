using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Interfaces;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Entities
{
    public class IssueLinkReason : Entity
    {
        private IssueLinkReason()
        {
        }
        public IssueLinkReason(CreateIssueLinkReasonArg arg)
        {
            Id = new IssueLinkReasonId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<IssueLinkReason> Create(CreateIssueLinkReasonArg arg, IIssueLinkReasonDomainService service)
        {
            await CreateGuards(arg, service);
            return new IssueLinkReason(arg);
        }
        public async Task Modify(ModifyIssueLinkReasonArg arg, IIssueLinkReasonDomainService service)
        {
            await ModifyGuards(arg, service);
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
        }
        public IssueLinkReasonId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
        public ICollection<IssueLink> IssueLinks { get; private set; }

        public void Delete()
        {
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        public void Deactive()
        {
            ActiveStatusId = (long)ActiveStatusEnum.Deactive;
        }


        #region Gaurds

        private static async Task CreateGuards(CreateIssueLinkReasonArg arg, IIssueLinkReasonDomainService service)
        {
            arg.Name.NullCheck();
            arg.Code.NullCheck();
            arg.ActiveStatusId.NullCheck();
            if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
        }
        private async Task ModifyGuards(ModifyIssueLinkReasonArg arg, IIssueLinkReasonDomainService service)
        {
            arg.Name.NullCheck();
            arg.Code.NullCheck();
            arg.ActiveStatusId.NullCheck();
            if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
        }
        #endregion
    }
}

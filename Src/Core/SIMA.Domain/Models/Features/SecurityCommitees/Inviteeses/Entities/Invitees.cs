using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Entities;

internal class Invitees
{
    private Invitees() { }
    private Invitees(CreateInviteesArg arg) 
    {
        Id = new(IdHelper.GenerateUniqueId());
    }
    public static async Task<Invitees> Create(CreateInviteesArg arg, IInviteesDomaiService service)
    {
        await CreateGuards(arg, service);
        return new Invitees(arg);
    }
    public async Task Modify(ModifyInviteesArg arg, IInviteesDomaiService service)
    {
        await ModifyGuards(arg, service);
    }
    #region Guards
    private static async Task CreateGuards(CreateInviteesArg arg, IInviteesDomaiService service)
    {

    }
    private async Task ModifyGuards(ModifyInviteesArg arg, IInviteesDomaiService service)
    {

    }
    #endregion
    public InviteesId Id { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}

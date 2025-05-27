using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Args;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Entities;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.UseVulnerabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.UseVulnerabilities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Entities;

public class MatrixA : Entity, IAggregateRoot
{
    private MatrixA()
    {

    }
    private MatrixA(CreateMatrixAArg arg)
    {
        Id = new(arg.Id);
        Code = arg.Code;
        MatrixAValueId = new(arg.MatrixAValueId);
        UseVulnerabilityId = new(arg.UseVulnerabilityId);
        TriggerStatusId = new(arg.TriggerStatusId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<MatrixA> Create(CreateMatrixAArg arg, IMatrixADomainService service)
    {
        await CreateGuadrs(arg, service);
        return new MatrixA(arg);
    }
    public async Task Modify(ModifyMatrixAArg arg, IMatrixADomainService service)
    {
        await ModifyGuard(arg, service);
        Code = arg.Code;
        MatrixAValueId = new(arg.MatrixAValueId);
        UseVulnerabilityId = new(arg.UseVulnerabilityId);
        TriggerStatusId = new(arg.TriggerStatusId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public MatrixAId Id { get; private set; }
    public string? Code { get; private set; }
    public TriggerStatusId TriggerStatusId { get; private set; }
    public virtual TriggerStatus TriggerStatus { get; private set; }
    public UseVulnerabilityId UseVulnerabilityId { get; private set; }
    public virtual UseVulnerability UseVulnerability { get; private set; }
    public MatrixAValueId MatrixAValueId { get; private set; }
    public virtual MatrixAValue MatrixAValue { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateMatrixAArg arg, IMatrixADomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsRelationUnique(new(arg.UseVulnerabilityId), new(arg.TriggerStatusId))) throw new SimaResultException(CodeMessges._400Code, Messages.CombinationOfFieldsError);

    }
    private async Task ModifyGuard(ModifyMatrixAArg arg, IMatrixADomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsRelationUnique(new(arg.UseVulnerabilityId), new(arg.TriggerStatusId) , new(arg.Id))) throw new SimaResultException(CodeMessges._400Code, Messages.CombinationOfFieldsError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
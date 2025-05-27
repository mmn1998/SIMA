using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Args;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Entities;

public class MatrixAValue : Entity, IAggregateRoot
{
    private MatrixAValue()
    {

    }
    private MatrixAValue(CreateMatrixAValueArg arg)
    {
        Id = new(arg.Id);
        Color = arg.Color;
        Code = arg.Code;
        ValueTitle = arg.ValueTitle;
        NumericValue = arg.NumericValue;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<MatrixAValue> Create(CreateMatrixAValueArg arg, IMatrixAValueDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new MatrixAValue(arg);
    }
    public async Task Modify(ModifyMatrixAValueArg arg, IMatrixAValueDomainService service)
    {
        await ModifyGuard(arg, service);
        Color = arg.Color;
        Code = arg.Code;
        ValueTitle = arg.ValueTitle;
        NumericValue = arg.NumericValue;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public MatrixAValueId Id { get; private set; }
    public string? Color { get; private set; }
    public string? Code { get; private set; }
    public float NumericValue { get; private set; }
    public string? ValueTitle { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateMatrixAValueArg arg, IMatrixAValueDomainService service)
    {
        arg.NullCheck();
        arg.Color.NullCheck();
        arg.Code.NullCheck();
        arg.ValueTitle.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Color.Length > 10) throw new SimaResultException(CodeMessges._400Code, Messages.ColorMaxLengthError);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsNumericUnique(arg.NumericValue)) throw new SimaResultException(CodeMessges._400Code, Messages.NumericValueNotUniqueError);
    }
    private async Task ModifyGuard(ModifyMatrixAValueArg arg, IMatrixAValueDomainService service)
    {
        arg.NullCheck();
        arg.Color.NullCheck();
        arg.Code.NullCheck();
        arg.ValueTitle.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Color.Length > 10) throw new SimaResultException(CodeMessges._400Code, Messages.ColorMaxLengthError);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsNumericUnique(arg.NumericValue,Id)) throw new SimaResultException(CodeMessges._400Code, Messages.NumericValueNotUniqueError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<MatrixA> _matrixAs = new();
    public ICollection<MatrixA> MatrixAs => _matrixAs;
    private List<InherentOccurrenceProbability> _inherentOccurrenceProbabilities = new();
    public ICollection<InherentOccurrenceProbability> InherentOccurrenceProbabilities => _inherentOccurrenceProbabilities;
}
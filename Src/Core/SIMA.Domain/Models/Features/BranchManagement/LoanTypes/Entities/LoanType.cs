using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Entities;

public class LoanType : Entity
{
    private LoanType()
    {

    }
    private LoanType(CreateLoanTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<LoanType> Create(CreateLoanTypeArg arg, ILoanTypeDomainService domainService)
    {
        await CreateGuards(arg, domainService);
        return new LoanType(arg);
    }
    public async Task Modify(ModifyLoanTypeArg arg, ILoanTypeDomainService domainService)
    {
        await ModifyGuards(arg, domainService);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public LoanTypeId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long? ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuards(CreateLoanTypeArg arg, ILoanTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyLoanTypeArg arg, ILoanTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }

    #endregion
    private List<TrustyDraft> _trustyDrafts = new();
    public ICollection<TrustyDraft> TrustyDrafts => _trustyDrafts;
}

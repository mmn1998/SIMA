using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Domain.Models.Features.RiskManagement.Vulnerabilities.Args;
using SIMA.Domain.Models.Features.RiskManagement.Vulnerabilities.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

public class EffectedAsset : Entity
{
    private EffectedAsset()
    {
        
    }
    private EffectedAsset(CreateEffectedAssetArgs arg)
    {
        CreateGurd(arg);
        Id = new EffectedAssetId(IdHelper.GenerateUniqueId());
        AssetId = new(arg.AssetId);
        RiskId = new(arg.RiskId);
        AV = arg.AV;
        EF = arg.EF;
        SLE = arg.Sle;
        ALE = arg.Ale;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static EffectedAsset Create(CreateEffectedAssetArgs arg)
    {
        return new EffectedAsset(arg);
    }
    public void AddVulnerabilities(List<CreateVulnerabilityArg> args)
    {
        foreach (var arg in args)
        {
            var entity = Vulnerability.Create(arg);
            _vulnerabilities.Add(entity);
        }
    }
    public void Modify(ModifyEffectedAssetArg arg)
    {
        ModifyGurd(arg);
        AssetId = new(arg.AssetId);
        RiskId = new RiskId(arg.RiskId);
        AV = arg.AV;
        EF = arg.EF;
        SLE = arg.SLE;
        ALE = arg.ALE;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        DeleteVulnerablities(userId);
    }
    public void DeleteVulnerablities(long userId)
    {
        foreach (var item in _vulnerabilities)
        {
            item.Delete(userId);
        }
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }

    public void CreateGurd(CreateEffectedAssetArgs args)
    {
        if (args.EF > 100)
            throw new SimaResultException(CodeMessges._400Code , Messages.EFError);

        if(args.Ale > 100)
            throw new SimaResultException(CodeMessges._400Code, Messages.ALEError);
    }

    public void ModifyGurd(ModifyEffectedAssetArg args)
    {
        if (args.EF > 100)
            throw new SimaResultException(CodeMessges._400Code, Messages.EFError);

        if (args.ALE > 100)
            throw new SimaResultException(CodeMessges._400Code, Messages.ALEError);
    }
    public EffectedAssetId Id { get; private set; }
    public RiskId RiskId { get; private set; }
    public virtual Risk Risk { get; private set; }
    public AssetId AssetId { get; private set; }
    public virtual Asset Asset { get; private set; }
    public decimal AV { get; private set; }
    public float EF { get; private set; }
    public float SLE { get; private set; }
    public float ALE { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<Vulnerability> _vulnerabilities = new();
    public ICollection<Vulnerability> Vulnerabilities => _vulnerabilities;

}

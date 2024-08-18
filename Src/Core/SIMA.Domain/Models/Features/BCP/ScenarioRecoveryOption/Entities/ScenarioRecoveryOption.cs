using SIMA.Domain.Models.Features.BCP.NewFolder.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryOptions.Args;
using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryOptions.Contracts;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BCP.ScenarioRecoveryOptions.Entities
{
    public class ScenarioRecoveryOption : Entity
    {
        private ScenarioRecoveryOption()
        {

        }
        private ScenarioRecoveryOption(CreateScenarioRecoveryOptionArg arg)
        {
            Id = new ScenarioRecoveryOptionId(arg.Id);
            Title = arg.Title;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<ScenarioRecoveryOption> Create(CreateScenarioRecoveryOptionArg arg, IScenarioRecoveryOptionDomainService service)
        {
            await CreateGuards(arg, service);
            return new ScenarioRecoveryOption(arg);
        }
        public async Task Modify(ModifyScenarioRecoveryOptionArg arg, IScenarioRecoveryOptionDomainService service)
        {
            await ModifyGuards(arg, service);
            Title = arg.Title;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedBy = arg.ModifiedBy;
            ModifiedAt = arg.ModifiedAt;
        }
        #region Guards
        private static async Task CreateGuards(CreateScenarioRecoveryOptionArg arg, IScenarioRecoveryOptionDomainService service)
        {
            arg.NullCheck();
            arg.Title.NullCheck();
            arg.Code.NullCheck();

            if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        private async Task ModifyGuards(ModifyScenarioRecoveryOptionArg arg, IScenarioRecoveryOptionDomainService service)
        {
            arg.NullCheck();
            arg.Title.NullCheck();
            arg.Code.NullCheck();

            if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
            if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
            if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        }
        #endregion
        public ScenarioRecoveryOptionId Id { get; set; }
        public ScenarioId ScenarioId { get; private set; }
        public Scenario Scenario { get; private set; }
        public string Title { get; private set; }
        public string Code { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}

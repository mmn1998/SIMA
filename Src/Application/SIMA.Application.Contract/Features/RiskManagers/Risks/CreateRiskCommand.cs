using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.Risks
{
    public class CreateRiskCommand : ICommand<Result<long>>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long RiskType { get; set; }
        public List<CreateCorrectiveActionCommand> CorrectiveActions { get; set; }
        public List<CreatePreventiveActionCommand> PreventiveActions { get; set; }
        public int ARO { get; set; }
        public double AV { get; set; }
        public float EF { get; set; }
        public float SLE { get; set; }
        public float ALE { get; set; }
    }

    public class CreateCorrectiveActionCommand
    {
        public string ActionDescription { get; set; }
    }
    public class CreatePreventiveActionCommand
    {
        public string ActionDescription { get; set; }
    }
}

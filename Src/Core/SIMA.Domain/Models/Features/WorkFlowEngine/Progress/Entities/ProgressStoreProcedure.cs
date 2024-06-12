using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Framework.Core.Entities;
using System.ComponentModel;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities
{
    public class ProgressStoreProcedure : Entity
    {
        private ProgressStoreProcedure()
        {
        }
        private ProgressStoreProcedure(ProgressStoreProcedureArg arg)
        {
            Id = new(arg.Id);
            StoreProcedureName = arg.StoreProcedureName;
            ProgressId = new(arg.ProgressId);
            ActiveStatusId = arg.ActiveStatusId;
            ExecutionOrdering = arg.ExecutionOrdering;

            SetParams(arg.ProgressStoreProcedureParams);
        }
        public static ProgressStoreProcedure Create(ProgressStoreProcedureArg arg)
        {
            return new ProgressStoreProcedure(arg);
        }
        public ProgressStoreProcedureId Id { get; private set; }
        public string StoreProcedureName { get; private set; }
        public ProgressId ProgressId { get; private set; }
        public virtual Progress Progress { get; private set; }
        public float ExecutionOrdering { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<ProgressStoreProcedureParam> _progressStoreProcedureParams = new ();
        public ICollection<ProgressStoreProcedureParam> ProgressStoreProcedureParams => _progressStoreProcedureParams;
       
        public void SetParams(List<ProgressStoreProcedureParamArg> args)
        {
            var storeProcedureParams = args.Select(ProgressStoreProcedureParam.Create);
            _progressStoreProcedureParams.AddRange(storeProcedureParams);
        }
    }
}

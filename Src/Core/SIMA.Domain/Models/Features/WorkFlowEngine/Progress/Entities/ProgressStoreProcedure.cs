using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Framework.Common.Helper;
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

        private List<ProgressStoreProcedureParam> _progressStoreProcedureParams = new();
        public ICollection<ProgressStoreProcedureParam> ProgressStoreProcedureParams => _progressStoreProcedureParams;
        public void Activate(List<ProgressStoreProcedureParam> progressStoreProcedureParams)
        {
            foreach (var param in _progressStoreProcedureParams)
            {
                param.Delete();
            }
            ActiveStatusId = (long)ActiveStatusEnum.Active;
            var storeProcedureIds = progressStoreProcedureParams.Select(x => x.Id);
            var existSps = _progressStoreProcedureParams.Where(x => storeProcedureIds.Contains(x.Id));
            var existsIds = new List<long>();
            foreach (var item in existSps)
            {
                var existParam = progressStoreProcedureParams.FirstOrDefault(x => x.Id == item.Id);
                item.Modify(existParam);
                item.Activate();
                existsIds.Add(item.Id.Value);
            }
            var notExistsSps = progressStoreProcedureParams.Where(x => !existsIds.Contains(x.Id.Value));
            _progressStoreProcedureParams.AddRange(notExistsSps);

        }
        public void Delete()
        {
            ActiveStatusId = 3;
            foreach (var param in _progressStoreProcedureParams)
            {
                param.Delete();
            }
        }
        public void SetParams(List<ProgressStoreProcedureParamArg> args)
        {
            var storeProcedureParams = args.Select(ProgressStoreProcedureParam.Create);
            _progressStoreProcedureParams.AddRange(storeProcedureParams);
        }

        public void Modify(ProgressStoreProcedure addedSp)
        {
            StoreProcedureName = addedSp.StoreProcedureName;
            ExecutionOrdering = addedSp.ExecutionOrdering;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args
{
    public class ChangeStatusArg
    {
        public long Id { get; set; }
        public long StateId { get; set; }
        public string ConditionExpression { get; set; }
        public long? ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}

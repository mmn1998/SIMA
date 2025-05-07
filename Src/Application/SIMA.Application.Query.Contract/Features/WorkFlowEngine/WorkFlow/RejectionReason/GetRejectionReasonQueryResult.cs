using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.RejectionReason
{   
    public class GetRejectionReasonQueryResult
    {
        public int Id { get; set; }
        public int StepId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}

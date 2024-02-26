using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.Auths.Positions.Args
{
    public class ModifyPositionArg
    {
        public long Id { get; set; }
        public int? DepartmentId { get; set; }
        public long ActiveStatusId { get; set; }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.Auths.Staffs.Args
{
    public class ModifyStaffArg
    {
        public long Id { get; set; }

        public long? ProfileId { get; set; }
        public long? ManagerId { get; set; }
        public long? PositionId { get; set; }
        public long? SignatureId { get; set; }
        public long ActiveStatusId { get; set; }

        public string? StaffNumber { get; set; }

        public byte[]? ModifiedAt { get; set; }

        public long? ModifiedBy { get; set; }
    }
}

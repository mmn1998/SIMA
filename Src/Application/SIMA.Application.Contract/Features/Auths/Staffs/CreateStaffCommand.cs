using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Application.Contract.Features.Auths.Staffs
{
    public class CreateStaffCommand : ICommand<Result<long>>
    {
        public long ProfileId { get; set; }
        public long PositionId { get; set; }
        public long ManagerId { get; set; }
        public string? StaffNumber { get; set; }
    }
}

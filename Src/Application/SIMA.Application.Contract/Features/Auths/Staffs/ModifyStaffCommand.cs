using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMA.Application.Contract.Features.Auths.Staffs
{
    public class ModifyStaffCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }

        public long? ProfileId { get; set; }
        public long? ManagerId { get; set; }
        public long? PositionId { get; set; }
        public long ActiveStatusId { get; set; }

        public string? StaffNumber { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.Auths.Departments.Args
{
    public class ModifyDepartmentArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? ParentId { get; set; }
        public int? CompanyId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
        public long ActiveStatusId { get; set; }

    }
}

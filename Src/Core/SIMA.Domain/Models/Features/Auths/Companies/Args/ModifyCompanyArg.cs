using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.Auths.Companies.Args
{
    public class ModifyCompanyArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ParentId { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}

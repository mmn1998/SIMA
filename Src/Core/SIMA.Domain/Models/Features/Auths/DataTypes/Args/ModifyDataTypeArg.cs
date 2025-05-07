using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.Auths.DataTypes.Args
{
    public class ModifyDataTypeArg
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string IsList { get; private set; }
        public string IsMultiSelect { get; private set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
        public long ActiveStatusId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Args;

public class CreateServiceStatusArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}


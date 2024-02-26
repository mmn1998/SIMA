using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions
{
    public class GetAllWorkFlowDocumentExtensionQuery : IQuery<Result<List<GetWorkFlowDocumentExtensionQueryResult>>>
    {
        public BaseRequest Request { get; set; } = new();
    }
}

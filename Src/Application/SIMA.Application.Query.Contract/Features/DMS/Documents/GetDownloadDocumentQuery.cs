using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.DMS.Documents;

public class GetDownloadDocumentQuery : IQuery<GetDownloadDocumentQueryResult>
{
    public long DocumetId { get; set; }
}

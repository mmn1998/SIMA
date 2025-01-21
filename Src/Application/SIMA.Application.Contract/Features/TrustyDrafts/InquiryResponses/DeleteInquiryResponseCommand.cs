using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.InquiryResponses
{
    public class DeleteInquiryResponseCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}

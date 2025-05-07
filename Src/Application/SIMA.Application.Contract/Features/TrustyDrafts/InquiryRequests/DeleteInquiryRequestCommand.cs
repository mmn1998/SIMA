using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.InquiryRequests
{
    public class DeleteInquiryRequestCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}

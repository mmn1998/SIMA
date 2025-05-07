using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Companies
{
    public class DeleteCompanyCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}

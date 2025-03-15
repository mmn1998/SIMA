using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.BiaValues;

public class DeleteBiaValueCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
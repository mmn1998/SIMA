using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.MatrixAValues;

public class DeleteMatrixAValuesCommand :  ICommand<Result<long>>
{
    public long Id { get; set; }
}
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.WebApi.Extensions;

namespace SIMA.WebApi.Controllers.Features.Logistics.LogisticsSupplies.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "LogisticsSupplies")]
[Authorize]
public class LogisticsSuppliesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogisticsSuppliesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.LogisticsSupplyGetAll)]
    public async Task<Result> Get([FromBody] GetAllLogisticsSuppliesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpPost("GetAllMy")]
    [SimaAuthorize(Permissions.LogisticsSupplyMyGetAll)]
    public async Task<Result> Get([FromBody] GetAllMyLogisticsSuppliesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.LogisticsSupplyGet)]
    public async Task<Result> Post([FromRoute] long id)
    {
        var query = new GetLogisticsSupplyQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result.Data.DocumentList is not null)
        {
            foreach (var document in result.Data.DocumentList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        if (result.Data.CandidateSupplierList is not null)
        {
            foreach (var document in result.Data.CandidateSupplierList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        if (result.Data.OrderingList is not null)
        {
            foreach (var document in result.Data.OrderingList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        if (result.Data.ReceiveList is not null)
        {
            foreach (var document in result.Data.ReceiveList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        if (result.Data.ReceiveList is not null)
        {
            foreach (var document in result.Data.ReceiveList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        if (result.Data.DeliveryList is not null)
        {
            foreach (var document in result.Data.DeliveryList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        if (result.Data.ReturnList is not null)
        {
            foreach (var document in result.Data.ReturnList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        if (result.Data.SupplierContractList is not null)
        {
            foreach (var document in result.Data.SupplierContractList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        if (result.Data.PaymentHistoryList is not null)
        {
            foreach (var document in result.Data.PaymentHistoryList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        if (result.Data.PrepaymentHistoryList is not null)
        {
            foreach (var document in result.Data.PrepaymentHistoryList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        return result;
    }

    [HttpPost("GetLogisticsSupplyGoodsCategory/{LogisticsSupplyId}")]
    public async Task<Result> GetLogisticsSupplyGoodsCategory([FromRoute] long LogisticsSupplyId)
    {
        var query = new GetLogisticsSupplyGoodsCategoryQuery { LogisticsSupplyId = LogisticsSupplyId };
        return await _mediator.Send(query);
    }
    [HttpPost("GetOrderingByLogisticsSupplyId/{LogisticsSupplyId}")]
    public async Task<Result> Get([FromRoute] long LogisticsSupplyId)
    {
        var query = new GetAllOrderingByLogisticsSupplyIdQuery { LogisticsSupplyId = LogisticsSupplyId };
        return await _mediator.Send(query);
    }
    [HttpPost("GetPaymentCommandByLogisticsSupplyId/{LogisticsSupplyId}")]
    public async Task<Result> GetPaymentCommand([FromRoute] long LogisticsSupplyId)
    {
        var query = new GetAllPaymentCommandByLogisticsSupplyIdQuery { LogisticsSupplyId = LogisticsSupplyId };
        return await _mediator.Send(query);
    }

    [HttpPost("GetPrePaymentCommandByLogisticsSupplyId/{LogisticsSupplyId}")]
    public async Task<Result> GetPrePaymentCommand([FromRoute] long LogisticsSupplyId)
    {
        var query = new GetPrePaymentCommandByLogisticsSupplyQuery { LogisticsSupplyId = LogisticsSupplyId };
        return await _mediator.Send(query);
    }
}

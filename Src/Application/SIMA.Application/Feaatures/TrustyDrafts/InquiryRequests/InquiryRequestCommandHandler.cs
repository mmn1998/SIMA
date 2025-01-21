using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;
using System.Globalization;

namespace SIMA.Application.Feaatures.TrustyDrafts.InquiryRequests;

public class InquiryRequestCommandHandler : ICommandHandler<CreateInquiryRequestCommand, Result<long>>,
ICommandHandler<ModifyInquiryRequestCommand, Result<long>>, ICommandHandler<DeleteInquiryRequestCommand, Result<long>>
{
    private readonly IInquiryRequestRepository _repository;
    private readonly IInquiryRequestDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public InquiryRequestCommandHandler(IInquiryRequestRepository repository, IInquiryRequestDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateInquiryRequestCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateInquiryRequestArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        arg.ReferenceNumber = await CalculateRefrenceNumber(request.CustomerId);
        var entity = await InquiryRequest.Create(arg, _service);
        if (request.InquiryRequestDocuments is not null && request.InquiryRequestDocuments.Count > 0)
        {
            var docArgs = _mapper.Map<List<CreateInquiryRequestDocumentArg>>(request.InquiryRequestDocuments);
            foreach (var doc in docArgs)
            {
                doc.InquiryRequestId = entity.Id.Value;
                doc.CreatedBy = _simaIdentity.UserId;
            }
            entity.AddDocuments(docArgs);
        }
        if (request.InquiryRequestCurrencies is not null && request.InquiryRequestCurrencies.Count > 3)
            throw new SimaResultException(CodeMessges._100110Code, Messages.CurrencyTypeOverCountError);
        if (request.InquiryRequestCurrencies is not null && request.InquiryRequestCurrencies.Count > 0)
        {
            var currencyArgs = _mapper.Map<List<CreateInquiryRequestCurrencyArg>>(request.InquiryRequestCurrencies);
            foreach (var currencyArg in currencyArgs)
            {
                currencyArg.InquiryRequestId = entity.Id.Value;
                currencyArg.CreatedBy = _simaIdentity.UserId;
            }
            entity.AddCurrencies(currencyArgs);
        }
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyInquiryRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyInquiryRequestArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteInquiryRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    private async Task<string> CalculateRefrenceNumber(long customerId)
    {
        var value = string.Empty;
        // 5 digits
        var branchCode = await _service.GetBranchCodeByUserId(_simaIdentity.UserId);
        if (branchCode == null || branchCode.Length >= 6) throw new SimaResultException(CodeMessges._100108Code, Messages.BranchCodeError);
        // 12 digits

        var customerNumber = await _service.GetCustomerNumber(customerId);
        if (customerNumber.Length > 12) throw new SimaResultException(CodeMessges._100109Code, Messages.CustomerNumberNotValidError);

        // 24 digits
        var lastRefrenceNumber = await _service.GetLastRefrenceNumber();

        // 4 digits
        var year = DateTime.Now.Year.ToString();
        // 3 digits
        var counter = "001";
        if (!string.IsNullOrEmpty(lastRefrenceNumber))
        {
            var lastCounter = lastRefrenceNumber.Substring(21, 3);
            counter = (Convert.ToInt64(lastCounter) + 1).ToString("000");
        }
        value = $@"{year}{branchCode}{customerNumber}{counter}";
        return value;
    }
}

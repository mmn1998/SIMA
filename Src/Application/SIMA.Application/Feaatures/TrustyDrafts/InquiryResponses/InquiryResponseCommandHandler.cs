using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.InquiryResponses;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.InquiryResponses;

public class InquiryResponseCommandHandler : ICommandHandler<CreateInquiryResponseCommand, Result<long>>,
ICommandHandler<ModifyInquiryResponseCommand, Result<long>>, ICommandHandler<DeleteInquiryResponseCommand, Result<long>>
{
    private readonly IInquiryResponseRepository _repository;
    private readonly IInquiryResponseDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public InquiryResponseCommandHandler(IInquiryResponseRepository repository, IInquiryResponseDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateInquiryResponseCommand request, CancellationToken cancellationToken)
    {
        try 
        {
            var arg = _mapper.Map<CreateInquiryResponseArg>(request);
            if(request.BrokerInquiryStatusId != 3)
            {
                if (string.IsNullOrEmpty(request.ValidityPeriod))
                    arg.ValidityPeriod = DateTime.Now.AddMonths(1);
                else
                {
                    var georgianDate = request.ValidityPeriod.ToMiladiDate();
                    arg.ValidityPeriod = georgianDate.Value;
                }
            }
           
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await InquiryResponse.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }
        catch(Exception ex)
        {
            throw;
        }
        
    }

    public async Task<Result<long>> Handle(ModifyInquiryResponseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyInquiryResponseArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteInquiryResponseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}

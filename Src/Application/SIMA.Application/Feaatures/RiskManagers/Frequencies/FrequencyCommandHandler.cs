using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.Frequencies;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Args;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.RiskManagers.Frequencies;

public class FrequencyCommandHandler: ICommandHandler<CreateFrequencyCommand, Result<long>>,
    ICommandHandler<ModifyFrequencyCommand, Result<long>>, ICommandHandler<DeleteFrequencyCommand, Result<long>>
{
    private readonly IFrequencyRepository _repository;
    private readonly IFrequencyDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public  FrequencyCommandHandler(IFrequencyRepository repository, IFrequencyDomainService service, IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    
    
    public async Task<Result<long>> Handle(CreateFrequencyCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateFrequencyArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Frequency.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyFrequencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyFrequencyArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteFrequencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
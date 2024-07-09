using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Staffs;
using SIMA.Domain.Models.Features.Auths.Staffs.Args;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Staffs;

public class StaffCommandHandler : ICommandHandler<DeleteStaffCommand, Result<long>>,
    ICommandHandler<CreateStaffCommand, Result<long>>, ICommandHandler<ModifyStaffCommand, Result<long>>
{
    private readonly IStaffRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStaffService _staffService;
    private readonly ISimaIdentity _simaIdentity;

    public StaffCommandHandler(IStaffRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IStaffService staffService, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _staffService = staffService;
        _simaIdentity = simaIdentity;
    }

    public async Task<Result<long>> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateStaffArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Staff.Create(arg, _staffService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyStaffCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyStaffArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _staffService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}

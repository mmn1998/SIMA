using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Staffs;
using SIMA.Domain.Models.Features.Auths.Staffs.Args;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Staffs;

public class StaffCommandHandler : ICommandHandler<DeleteStaffCommand, Result<long>>,
    ICommandHandler<CreateStaffCommand, Result<long>>, ICommandHandler<ModifyStaffCommand, Result<long>>
{
    private readonly IStaffRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStaffService _staffService;

    public StaffCommandHandler(IStaffRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IStaffService staffService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _staffService = staffService;
    }

    public async Task<Result<long>> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateStaffArg>(request);
        var entity = await Staff.Create(arg, _staffService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyStaffCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyStaffArg>(request);
        await entity.Modify(arg, _staffService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}

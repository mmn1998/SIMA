using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Departments;
using SIMA.Domain.Models.Features.Auths.Departments.Args;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Departments;

public class DepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, Result<long>>,
    ICommandHandler<DeleteDepartmentCommand, Result<long>>, ICommandHandler<ModifyDepartmentCommand, Result<long>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDepartmentRepository _repository;
    private readonly IMapper _mapper;
    private readonly IDepartmentService _service;
    private readonly ISimaIdentity _simaIdentity;

    public DepartmentCommandHandler(IUnitOfWork unitOfWork, IDepartmentRepository repository, IMapper mapper,
        IDepartmentService service, ISimaIdentity simaIdentity)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById((int)request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyDepartmentArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDepartmentArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Department.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}

using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Companies;
using SIMA.Domain.Models.Features.Auths.Companies;
using SIMA.Domain.Models.Features.Auths.Companies.Args;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Companies;

public class CompanyCommandHandler : ICommandHandler<CreateCompanyCommand, Result<long>>, ICommandHandler<DeleteCompanyCommand, Result<long>>,
    ICommandHandler<ModifyCompanyCommands, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyService _service;

    public CompanyCommandHandler(IMapper mapper, ICompanyRepository repository,
        IUnitOfWork unitOfWork, ICompanyService service)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
    }
    public async Task<Result<long>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateCompanyArg>(request);
            var entity = await Company.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }


    }

    public async Task<Result<long>> Handle(ModifyCompanyCommands request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyCompanyArg>(request);
            entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<Result<long>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}

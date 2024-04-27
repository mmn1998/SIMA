using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Forms;
using SIMA.Domain.Models.Features.Auths.Forms.Args;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Forms;

public class FormCommandHandler : ICommandHandler<CreateFormCommand, Result<long>>, ICommandHandler<ModifyFormCommand, Result<long>>
{
    private readonly IFormRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public FormCommandHandler(IFormRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<long>> Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateFormArg>(request);
            var entity = await Form.Create(arg);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }
       
    }

    public async Task<Result<long>> Handle(ModifyFormCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);

        var arg = _mapper.Map<ModifyFormArg>(request);
        await entity.Modify(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}

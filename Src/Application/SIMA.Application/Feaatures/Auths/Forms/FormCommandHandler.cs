using AutoMapper;
using Newtonsoft.Json;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Forms;
using SIMA.Application.Feaatures.Auths.Forms.Mappers;
using SIMA.Domain.Models.Features.Auths.Forms.Args;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Helper.FormMaker;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using System.Net.Http.Json;

namespace SIMA.Application.Feaatures.Auths.Forms;

public class FormCommandHandler : ICommandHandler<CreateFormCommand, Result<long>>, ICommandHandler<ModifyFormCommand, Result<long>>
{
    private readonly IFormRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;

    public FormCommandHandler(IFormRepository repository, IMapper mapper, IUnitOfWork unitOfWork, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateFormArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await Form.Create(arg);
            if (request.PermissoinIdList is not null && request.PermissoinIdList.Count > 0)
            {
                var args = _mapper.Map<List<CreateFormPermissionArg>>(request.PermissoinIdList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.FormId = entity.Id.Value;
                }
                entity.AddFormPermissions(args);
            }
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
        try
        {
            var entity = await _repository.GetById(request.Id);

            var arg = _mapper.Map<ModifyFormArg>(request);
            await entity.Modify(arg);
            #region FormFields
            if (!string.IsNullOrEmpty(arg.JsonContent))
            {
                var formComponents = JsonConvert.DeserializeObject<FormWrapper>(arg.JsonContent)?.components;
                if (formComponents != null)
                {
                    foreach (var formComponent in formComponents)
                    {
                        if (!string.Equals(formComponent.type, FormInputType.button.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            var formFieldArg = new CreateFormFieldArg
                            {
                                ActiveStatusId = (long)ActiveStatusEnum.Active,
                                CreatedAt = DateTime.Now,
                                CreatedBy = _simaIdentity.UserId,
                                FormId = entity.Id.Value,
                                Type = formComponent.type,
                                Name = formComponent.label ?? ""
                            };
                            entity.AddFromField(formFieldArg);
                        }
                    }
                }
            }
            #endregion
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        catch (Exception e)
        {

            throw;
        }
    }
}



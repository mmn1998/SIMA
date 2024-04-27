using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.WorkFlowEngine.Progress
{
    public class ProgressCommandHandler : ICommandHandler<ChangeStatusCommand, Result<long>>
    {
        private readonly IProgressRepository _progressRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProgressCommandHandler( IUnitOfWork unitOfWork, IMapper mapper, IProgressRepository progressRepository)
        {
            _progressRepository = progressRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _progressRepository.GetById(request.Id);
                var arg = _mapper.Map<ChangeStatusArg>(request);
                entity.ChangeStatus(arg);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(entity.Id.Value);
            }
            catch (Exception ex)
            {
                throw;
            }

           

        }
    }
}

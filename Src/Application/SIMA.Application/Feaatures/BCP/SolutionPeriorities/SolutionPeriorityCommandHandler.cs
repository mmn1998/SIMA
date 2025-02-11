namespace SIMA.Application.Feaatures.BCP.SolutionPeriorities;

//public class SolutionPeriorityCommandHandler : ICommandHandler<CreateSolutionPeriorityCommand, Result<long>>,
//    ICommandHandler<ModifySolutionPeriorityCommand, Result<long>>, ICommandHandler<DeleteSolutionPeriorityCommand, Result<long>>
//{
//    private readonly ISolutionPeriorityRepository _repository;
//    private readonly ISolutionPeriorityDomainService _service;
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly ISimaIdentity _simaIdentity;
//    private readonly IMapper _mapper;

//    public SolutionPeriorityCommandHandler(ISolutionPeriorityRepository repository, ISolutionPeriorityDomainService service,
//        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
//    {
//        _repository = repository;
//        _service = service;
//        _unitOfWork = unitOfWork;
//        _simaIdentity = simaIdentity;
//        _mapper = mapper;
//    }
//    public async Task<Result<long>> Handle(CreateSolutionPeriorityCommand request, CancellationToken cancellationToken)
//    {
//        var arg = _mapper.Map<CreateSolutionPeriorityArg>(request);
//        arg.CreatedBy = _simaIdentity.UserId;
//        var entity = await SolutionPeriority.Create(arg, _service);
//        await _repository.Add(entity);
//        await _unitOfWork.SaveChangesAsync();
//        return Result.Ok(arg.Id);
//    }

//    public async Task<Result<long>> Handle(ModifySolutionPeriorityCommand request, CancellationToken cancellationToken)
//    {
//        var entity = await _repository.GetById(new(request.Id));
//        var arg = _mapper.Map<ModifySolutionPeriorityArg>(request);
//        arg.ModifiedBy = _simaIdentity.UserId;
//        await entity.Modify(arg, _service);
//        await _unitOfWork.SaveChangesAsync();
//        return Result.Ok(request.Id);
//    }

//    public async Task<Result<long>> Handle(DeleteSolutionPeriorityCommand request, CancellationToken cancellationToken)
//    {
//        var entity = await _repository.GetById(new(request.Id));
//        long userId = _simaIdentity.UserId; entity.Delete(userId);
//        await _unitOfWork.SaveChangesAsync();
//        return Result.Ok(request.Id);
//    }
//}
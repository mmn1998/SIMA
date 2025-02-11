namespace SIMA.Application.Feaatures.BCP.SolutionPeriorities.Mappers;

//public class SolutionPeriorityMapper : Profile
//{
//    public SolutionPeriorityMapper()
//    {
//        CreateMap<CreateSolutionPeriorityCommand, CreateSolutionPeriorityArg>()
//            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
//            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
//            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
//            ;
//        CreateMap<ModifySolutionPeriorityCommand, ModifySolutionPeriorityArg>()
//            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
//            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
//            ;
//    }
//}
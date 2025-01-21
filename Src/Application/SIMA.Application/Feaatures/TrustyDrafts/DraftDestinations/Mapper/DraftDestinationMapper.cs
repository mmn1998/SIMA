using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftDestinations;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftDestinations.Mapper
{
    public class DraftDestinationMapper : Profile
    {
        public DraftDestinationMapper()
        {
            CreateMap<CreateDraftDestinationCommand, CreateDraftDestinationArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
                .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
                ;
            CreateMap<ModifyDraftDestinationCommand, ModifyDraftDestinationArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
                ;
        }
    }
}

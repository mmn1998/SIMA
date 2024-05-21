using AutoMapper;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowCompany;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.WorkFlowEngine.WorkFlowCompany.Mapper
{
    public class WorkFlowCompanyMapper : Profile
    {
        public WorkFlowCompanyMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateWorkFlowCompanyCommand, CreateWorkFlowCompanyArg>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));

            CreateMap<ModifyWorkFlowCompanyCommand, ModifyWorkFlowCompanyArg>()
         .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())));
         //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId));
        }
    }
}

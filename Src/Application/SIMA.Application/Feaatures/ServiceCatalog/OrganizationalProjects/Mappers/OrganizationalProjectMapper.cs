using AutoMapper;
using SIMA.Application.Contract.Features.ServiceCatalog.OrganizationalProjects;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Args;
using SIMA.Framework.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Application.Feaatures.ServiceCatalog.OrganizationalProjects.Mappers
{
    public class OrganizationalProjectMapper : Profile
    {
        public OrganizationalProjectMapper()
        {
            CreateMap<CreateOrganizationalProjectCommand, CreateOrganizationalProjectArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
                .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
                ;
            CreateMap<ModifyOrganizationalProjectCommand, ModifyOrganizationalProjectArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
                ;
        }
    }
}

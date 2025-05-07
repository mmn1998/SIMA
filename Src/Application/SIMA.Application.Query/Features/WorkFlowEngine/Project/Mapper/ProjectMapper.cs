using AutoMapper;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;
using SIMA.Domain.Models.Features.Auths.Domains.Entities;


namespace SIMA.Application.Query.Features.WorkFlowEngine.Project.Mapper
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<Domain.Models.Features.WorkFlowEngine.Project.Entites.Project, GetProjectQueryResult>();
        }
    }
}

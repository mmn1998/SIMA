
using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.Companies;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;

namespace SIMA.Application.Query.Features.Auths.Companies.Mappers
{
    public class CompanyQueryMapper : Profile
    {
        public CompanyQueryMapper()
        {
            CreateMap<Company, GetCompanyQueryResult>();
        }
    }
}

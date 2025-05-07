using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.SysConfigs;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;

namespace SIMA.Application.Query.Features.Auths.SysConfigs.Mappers;

public class SysConfigQueryMapper : Profile
{
    public SysConfigQueryMapper()
    {
        CreateMap<SysConfig, GetSysConfigQueryResult>();
    }
}

using AutoMapper;
using Microsoft.Build.Framework.Profiler;
using VpnetworkAPI.Dto;
using VpnetworkAPI.Models;

namespace VpnetworkAPI.Mapper
{
    public class Mapper :Profile
    {
        public Mapper()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<ProgramData, ProgramDataDto>().ReverseMap();
            CreateMap<ThresholdSettings, ThresholdSettingsDto>().ReverseMap();
            CreateMap<Analysis,AnalysesDto>().ReverseMap();
            CreateMap<Analysis, AnalysisUserDto>().ReverseMap();
        }
    }
}

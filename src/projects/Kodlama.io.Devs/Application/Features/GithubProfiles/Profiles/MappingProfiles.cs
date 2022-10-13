using Application.Features.GithubProfiles.Commands;
using Application.Features.GithubProfiles.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<GithubProfile, CreateGithubProfileCommand>().ReverseMap();
            CreateMap<GithubProfile, CreateGithubProfileDto>().ReverseMap();



            /* CreateMap<Technology, TechnologyListDto>().ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name)).ReverseMap();
             CreateMap<IPaginate<Technology>, TechnologyModel>().ReverseMap();
             CreateMap<Technology, GetByIdTechnologyDto>().ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name)).ReverseMap();
            */


        }
    }
}

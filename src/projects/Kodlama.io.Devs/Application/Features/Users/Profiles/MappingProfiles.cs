using Application.Features.Users.Dtos;
using Application.Features.Users.Queries;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<User, GetByEmailUserDto>().ReverseMap();
            CreateMap<User, GetByEmailUserQuery>().ReverseMap();



        }
    }
}

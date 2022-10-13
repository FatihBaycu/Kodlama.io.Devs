using Application.Features.UserOperationClaims.Command;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Queries;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, CreateUserOperationClaimDto>().ReverseMap();
                      
            CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateUserOperationClaimDto>().ReverseMap();
                                                
            CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, DeleteUserOperationClaimDto>().ReverseMap();
                      
            CreateMap<UserOperationClaim, UserOperationClaimListDto>()
                .ForMember(c=>c.UserEmail,opt=>opt.MapFrom(c=>c.User.Email))
                .ForMember(c=>c.UserName,opt=>opt.MapFrom(c=>c.User.FirstName))
                .ForMember(c=>c.UserSurname,opt=>opt.MapFrom(c=>c.User.LastName))
                .ForMember(c=>c.OperationClaimName,opt=>opt.MapFrom(c=>c.OperationClaim.Name))
                .ReverseMap();


            CreateMap<IPaginate<UserOperationClaim>, UserOperationClaimListModel>().ReverseMap();
            CreateMap<UserOperationClaim, GetListUserOperationClaimsQuery>().ReverseMap();





        }
    }
}

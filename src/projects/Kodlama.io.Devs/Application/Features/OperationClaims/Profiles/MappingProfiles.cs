
using Application.Features.OperationClaims.Command;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries;

using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.OperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, CreateOperationClaimDto>().ReverseMap();

            CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, UpdateOperationClaimDto>().ReverseMap();

            CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, DeleteOperationClaimDto>().ReverseMap();

            CreateMap<OperationClaim, OperationClaimListDto>().ReverseMap();
            CreateMap<IPaginate<OperationClaim>, OperationClaimListModel>().ReverseMap();
            CreateMap<OperationClaim, GetListOperationClaimsQuery>().ReverseMap();



        }
    }
}

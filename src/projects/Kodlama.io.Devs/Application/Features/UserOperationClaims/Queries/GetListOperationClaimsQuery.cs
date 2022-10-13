using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries
{
    public class GetListUserOperationClaimsQuery:IRequest<UserOperationClaimListModel>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListUserOperationClaimsQueryHandler : IRequestHandler<GetListUserOperationClaimsQuery, UserOperationClaimListModel>
        {

            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public GetListUserOperationClaimsQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UserOperationClaimListModel> Handle(GetListUserOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync
                    (
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    include:a=>a.Include(m=>m.User).Include(c=>c.OperationClaim)
                   );

                UserOperationClaimListModel mappedModel=_mapper.Map<UserOperationClaimListModel>(userOperationClaims);
                return mappedModel;
            }
        }
    }
}

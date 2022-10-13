using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries
{
    public class GetListOperationClaimsQuery:IRequest<OperationClaimListModel>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListOperationClaimsQueryHandler : IRequestHandler<GetListOperationClaimsQuery, OperationClaimListModel>
        {

            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;

            public GetListOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync
                    (index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                OperationClaimListModel mappedModel=_mapper.Map<OperationClaimListModel>(operationClaims);
                return mappedModel;
            }
        }
    }
}

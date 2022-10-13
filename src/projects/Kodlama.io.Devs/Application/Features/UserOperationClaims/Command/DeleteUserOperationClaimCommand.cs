using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Command
{
    public class DeleteUserOperationClaimCommand:IRequest<DeleteUserOperationClaimDto>
    {
        public int Id { get; set; }

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeleteUserOperationClaimDto>
        {

            private readonly UserOperationClaimBusinessRules _rule;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;


            public DeleteUserOperationClaimCommandHandler(IMapper mapper,UserOperationClaimBusinessRules rule, IUserOperationClaimRepository userOperationClaimRepository)
            {
                _rule = rule;
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<DeleteUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _rule.UserOperationClaimIsExistControl(request.Id);


                UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(c => c.Id == request.Id);
                UserOperationClaim mappedUserClaim = _mapper.Map<UserOperationClaim>(userOperationClaim);
                await _userOperationClaimRepository.DeleteAsync(mappedUserClaim);
                DeleteUserOperationClaimDto deleteOp=_mapper.Map<DeleteUserOperationClaimDto>(mappedUserClaim);
                return deleteOp;
            }
        }

    }
}

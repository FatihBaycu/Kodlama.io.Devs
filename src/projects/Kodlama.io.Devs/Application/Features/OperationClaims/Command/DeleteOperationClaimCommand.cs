using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Command
{
    public class DeleteOperationClaimCommand : IRequest<DeleteOperationClaimDto>
    {
        public int Id { get; set; }

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeleteOperationClaimDto>
        {
            private readonly IOperationClaimRepository _repo;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _rules;

            public DeleteOperationClaimCommandHandler(IOperationClaimRepository repo, IMapper mapper, OperationClaimBusinessRules rules)
            {
                _repo = repo;
                _mapper = mapper;
                _rules = rules;
            }

            public async Task<DeleteOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _rules.OperationClaimIdIsExistControl(request.Id);


                OperationClaim? operationClaim = await _repo.GetAsync(c => c.Id == request.Id);
                OperationClaim mappedClaim = _mapper.Map(request, operationClaim);
                await _repo.DeleteAsync(mappedClaim);
                DeleteOperationClaimDto deleteOperationClaimDto = _mapper.Map<DeleteOperationClaimDto>(mappedClaim);
                return deleteOperationClaimDto;
            }
        }
    }
}

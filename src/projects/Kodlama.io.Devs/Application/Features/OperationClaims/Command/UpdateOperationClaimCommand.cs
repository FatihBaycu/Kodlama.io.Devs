using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Command
{
    public class UpdateOperationClaimCommand : IRequest<UpdateOperationClaimDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateOperationClaimCommandHandler:IRequestHandler<UpdateOperationClaimCommand, UpdateOperationClaimDto>
        {
            private readonly IOperationClaimRepository _repo;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _rules;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepository repo, IMapper mapper, OperationClaimBusinessRules rules)
            {
                _repo = repo;
                _mapper = mapper;
                _rules = rules;
            }

            public async Task<UpdateOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _rules.OperationClaimIdIsExistControl(request.Id);
                OperationClaim operationClaim=await _repo.GetAsync(c=>c.Id==request.Id);
                OperationClaim operationClaimUpdated = await _repo.UpdateAsync(operationClaim);
                UpdateOperationClaimDto updateOperationClaimDto = _mapper.Map<UpdateOperationClaimDto>(operationClaimUpdated);
                return updateOperationClaimDto;
            }
        }
    }

    public class UpdateOperationClaimCommandValidator : AbstractValidator<UpdateOperationClaimCommand>
    {
        public UpdateOperationClaimCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();

        }
    }
}

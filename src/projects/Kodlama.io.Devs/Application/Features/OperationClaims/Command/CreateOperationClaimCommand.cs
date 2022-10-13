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

    public class CreateOperationClaimCommand : IRequest<CreateOperationClaimDto>
    {
        //public int Id { get; set; }
        public string Name { get; set; }

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreateOperationClaimDto>
        {

            private readonly IOperationClaimRepository _repo;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRule;

            public CreateOperationClaimCommandHandler(IOperationClaimRepository repo, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRule)
            {
                _repo = repo;
                _mapper = mapper;
                _operationClaimBusinessRule = operationClaimBusinessRule;
            }

            public async Task<CreateOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
               await _operationClaimBusinessRule.OperationClaimNameIsExistControl(request.Name);
                
                OperationClaim mappedClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim createdClaim = await _repo.AddAsync(mappedClaim);
                CreateOperationClaimDto createOperationClaimDto = _mapper.Map<CreateOperationClaimDto>(createdClaim);
                return createOperationClaimDto;


            }
        }
    }

    
    public class CreateOperationClaimCommandValidator : AbstractValidator<CreateOperationClaimCommand>
    {
        public CreateOperationClaimCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();

        }
    }
    

}
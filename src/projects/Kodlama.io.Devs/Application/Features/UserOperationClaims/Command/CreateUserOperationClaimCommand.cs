using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Command
{
    public class CreateUserOperationClaimCommand:IRequest<CreateUserOperationClaimDto>,ISecuredRequest
    {
        public int OperationClaimId { get; set; }
        public int UserId { get; set; }

        public string[] Roles => new[] {"admin"};

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreateUserOperationClaimDto>
        {

            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
            private readonly IMapper _mapper;

            public CreateUserOperationClaimCommandHandler(IMapper mapper,IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
                _mapper= mapper;
            }

            public async Task<CreateUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {

                await _userOperationClaimBusinessRules.OperationClaimIsExistControl(request.OperationClaimId);
                await _userOperationClaimBusinessRules.UserIsExistControl(request.UserId);

                UserOperationClaim userOperationClaim=_mapper.Map<UserOperationClaim>(request);
                UserOperationClaim createdOp = await _userOperationClaimRepository.AddAsync(userOperationClaim);
                CreateUserOperationClaimDto createUserOperationClaimDto = _mapper.Map<CreateUserOperationClaimDto>(createdOp);
                return createUserOperationClaimDto;
            }
        }
    }


    public class CreateUserOperationClaimCommandValidator : AbstractValidator<CreateUserOperationClaimCommand>
    {
        public CreateUserOperationClaimCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.OperationClaimId).NotEmpty();

        }
    }
}

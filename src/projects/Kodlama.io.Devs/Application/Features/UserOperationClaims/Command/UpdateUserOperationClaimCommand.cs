using Application.Features.OperationClaims.Command;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
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

namespace Application.Features.UserOperationClaims.Command
{
    public class UpdateUserOperationClaimCommand:IRequest<UpdateUserOperationClaimDto>
    {
        public int Id { get; set; }
        public int OperationClaimId { get; set; }
        public int UserId { get; set; }


        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdateUserOperationClaimDto>
        {

            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<UpdateUserOperationClaimDto> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.OperationClaimIsExistControl(request.OperationClaimId);
                await _userOperationClaimBusinessRules.UserIsExistControl(request.UserId);

                //UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(request);
                UserOperationClaim? userOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
                UpdateUserOperationClaimDto updateUserOperationClaimDto=_mapper.Map<UpdateUserOperationClaimDto>(updatedUserOperationClaim);
                return updateUserOperationClaimDto;

               // UserOperationClaim updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
                //UpdateUserOperationClaimDto updateUserOperationClaimDto=_mapper.Map<UpdateUserOperationClaimDto>(updatedUserOperationClaim);
                //return updateUserOperationClaimDto;               
            }
        }

    }

    public class UpdateUserOperationClaimCommandValidator : AbstractValidator<UpdateUserOperationClaimCommand>
    {
        public UpdateUserOperationClaimCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.OperationClaimId).NotEmpty();

        }
    }

}

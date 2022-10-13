using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
      

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;

        }


        public async Task OperationClaimNameIsExistControl(string name)
        {
            OperationClaim? operation= await _operationClaimRepository.GetAsync(c => c.Name == name);
            if (operation == null) throw new BusinessException("Operation Claim is exist.");

        }
        public async Task OperationClaimIdIsExistControl(int id)
        {
            OperationClaim? operation= await _operationClaimRepository.GetAsync(c => c.Id == id);
            if (operation == null) throw new BusinessException("Operation Claim is not exist.");

        } 
        public async Task OperationClaimIdIsNotExistControl(int id)
        {
            OperationClaim? operation= await _operationClaimRepository.GetAsync(c => c.Id == id);
            if (operation != null) throw new BusinessException("Operation Claim is not exist.");

        }
    }
}

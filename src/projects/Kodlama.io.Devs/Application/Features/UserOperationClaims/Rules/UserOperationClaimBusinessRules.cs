using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
     
       private readonly IUserRepository _userRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;


        public UserOperationClaimBusinessRules(IUserRepository userRepository, IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task UserIsExistControl(int id)
       {
           User user= await _userRepository.GetAsync(c => c.Id == id);
           if (user == null) throw new BusinessException("User not exist.");
       }
        public async Task OperationClaimIsExistControl(int id)
       {
           OperationClaim operationClaim= await _operationClaimRepository.GetAsync(c => c.Id == id);
           if (operationClaim == null) throw new BusinessException("Operation claim not exist.");
       }

        public async Task UserOperationClaimIsExistControl(int id)
       {
           UserOperationClaim userOperationClaim= await _userOperationClaimRepository.GetAsync(c => c.Id == id);
           if (userOperationClaim == null) throw new BusinessException("User Operation claim not exist.");
       }
        



    }
}

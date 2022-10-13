using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Rules
{
    public class GithubProfileRules
    {

        private readonly IUserRepository _userRepository;
        private readonly IGithubProfileRepository _gitRepo;

        public GithubProfileRules(IUserRepository userRepository, IGithubProfileRepository gitRepo)
        {
            _userRepository = userRepository;
            _gitRepo = gitRepo;
        }

        public async Task UserCheck(int id)
        {
         User? user=await _userRepository.GetAsync(c => c.Id == id);
            if(user==null) throw new BusinessException("User is not exist.");
        }


        public async Task UserIsExistCheck(int id)
        {
            GithubProfile? githubProfile = await _gitRepo.GetAsync(c => c.UserId == id);
            if (githubProfile != null) throw new BusinessException("User is already exist.");
        }  
        
        public async Task GithubProfileIsExistCheck(int id)
        {
            GithubProfile? githubProfile = await _gitRepo.GetAsync(c => c.Id == id);
            if (githubProfile == null) throw new BusinessException("Github Profile not exist.");
        }

    }
}

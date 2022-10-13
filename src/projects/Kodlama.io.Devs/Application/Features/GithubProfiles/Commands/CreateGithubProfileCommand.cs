using Application.Features.GithubProfiles.Dtos;
using Application.Features.GithubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands
{
    public class CreateGithubProfileCommand : IRequest<CreateGithubProfileDto>
    {
        public int UserId { get; set; }
        public string GithubAddress { get; set; }


        public class CreateGithubProfileCommandHandler : IRequestHandler<CreateGithubProfileCommand, CreateGithubProfileDto>
        {


            private readonly IGithubProfileRepository _githubProfileRepository;
            private readonly GithubProfileRules _githubProfileRules;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public CreateGithubProfileCommandHandler()
            {
            }

            public CreateGithubProfileCommandHandler(IGithubProfileRepository githubProfileRepository, IMapper mapper, IUserRepository userRepository,GithubProfileRules githubProfileRules)
            {
                _githubProfileRepository = githubProfileRepository;
                _mapper = mapper;
                _userRepository = userRepository;
                _githubProfileRules = githubProfileRules;   
            }

            public async Task<CreateGithubProfileDto> Handle(CreateGithubProfileCommand request, CancellationToken cancellationToken)
            {
               await _githubProfileRules.UserCheck(request.UserId);
               await _githubProfileRules.UserIsExistCheck(request.UserId);


                GithubProfile mappedGit=_mapper.Map<GithubProfile>(request);
                GithubProfile createdGit = await _githubProfileRepository.AddAsync(mappedGit);
                //GithubProfile? getByIdGit = await _githubProfileRepository.GetAsync(
                //    c => c.Id == createdGit.Id,
                //    include: i => i.Include(t => t.User)
                    
                //    );
                CreateGithubProfileDto createGithubProfileDto = _mapper.Map<CreateGithubProfileDto>(createdGit);
                return createGithubProfileDto;

                
            }
        }
    }
}

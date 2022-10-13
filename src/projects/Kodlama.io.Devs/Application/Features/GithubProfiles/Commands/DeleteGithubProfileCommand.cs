using Application.Features.GithubProfiles.Dtos;
using Application.Features.GithubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands
{
    public class DeleteGithubProfileCommand:IRequest<DeleteGithubProfileDto>
    {
        public int Id { get; set; }

        

        public class DeleteGithubProfileCommandHandler : IRequestHandler<DeleteGithubProfileCommand, DeleteGithubProfileDto>
        {
            private readonly IGithubProfileRepository _repo;
            private readonly GithubProfileRules _rules;
            private readonly IMapper _mapper;

            public DeleteGithubProfileCommandHandler(IGithubProfileRepository repo, GithubProfileRules rules,IMapper mapper)
            {
                _repo = repo;
                _rules = rules;
                _mapper = mapper;   
            }

            public async Task<DeleteGithubProfileDto> Handle(DeleteGithubProfileCommand request, CancellationToken cancellationToken)
            {
                await _rules.GithubProfileIsExistCheck(request.Id);
                GithubProfile? githubProfile = await _repo.GetAsync(c=>c.Id==request.Id);
                GithubProfile? mappedGit =  _mapper.Map(request, githubProfile);
                await _repo.DeleteAsync(mappedGit);
                DeleteGithubProfileDto deleteGithubProfileDto=  _mapper.Map<DeleteGithubProfileDto>(mappedGit);
                return deleteGithubProfileDto;
            }
        }

    }
}

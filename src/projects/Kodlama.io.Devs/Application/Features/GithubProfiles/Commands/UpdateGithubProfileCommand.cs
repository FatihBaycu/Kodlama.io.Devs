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
    public class UpdateGithubProfileCommand : IRequest<UpdateGithubProfileDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GithubAddress { get; set; }


        public class UpdateGithubProfileCommandHandler : IRequestHandler<UpdateGithubProfileCommand, UpdateGithubProfileDto>
        {

            private readonly IGithubProfileRepository _repo;
            private readonly GithubProfileRules _rules;
            private readonly IMapper _mapper;

            public UpdateGithubProfileCommandHandler(IGithubProfileRepository repo, GithubProfileRules rules, IMapper mapper)
            {
                _repo = repo;
                _rules = rules;
                _mapper = mapper;
            }

            /*
             id yi kontrol et varmı yok mu diye+
             id ile gelen github nesnesini oluşturduğun nesneye bağla+
             güncellenecek kısımları kontrol et(User varmı yokmu,githubAddress in boş olup olmamaması+
             güncellenecek nesneyi repo ya gönder.+
             */

            public async Task<UpdateGithubProfileDto> Handle(UpdateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                await _rules.GithubProfileIsExistCheck(request.Id);
                await _rules.UserCheck(request.UserId);

                GithubProfile? githubProfileToUpdate = await _repo.GetAsync(c => c.Id == request.Id);
                GithubProfile mappedTech = _mapper.Map(request, githubProfileToUpdate);
                GithubProfile githubBeUpdated = await _repo.UpdateAsync(mappedTech);
                UpdateGithubProfileDto updateGithubProfileDto = _mapper.Map<UpdateGithubProfileDto>(githubBeUpdated);
                return updateGithubProfileDto;
            }
        }

    }
}

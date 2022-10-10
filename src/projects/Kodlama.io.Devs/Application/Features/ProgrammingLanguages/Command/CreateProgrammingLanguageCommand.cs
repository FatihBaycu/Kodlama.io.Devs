using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Command
{
    public class CreateProgrammingLanguageCommand:IRequest<CreatedProgrammingLanguageDto>
    {

        public string Name { get; set; }

        public class CreateBrandCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
        {

            private readonly IProgrammingLanguageRepository _repo;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _progLangBusinessRules;


            public CreateBrandCommandHandler(IProgrammingLanguageRepository repo, IMapper mapper, ProgrammingLanguageBusinessRules progLangBusinessRules)
            {
                _repo = repo;
                _mapper = mapper;
                _progLangBusinessRules = progLangBusinessRules;
            }

            public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _progLangBusinessRules.ProgLangNameCanNotBeDuplicatedWhenInserted(request.Name);

                ProgrammingLanguage mappedBrand = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage createdBrand = await _repo.AddAsync(mappedBrand);
                CreatedProgrammingLanguageDto createdProgLangDto = _mapper.Map<CreatedProgrammingLanguageDto>(createdBrand);
                return createdProgLangDto;
            }
        }

    }
}

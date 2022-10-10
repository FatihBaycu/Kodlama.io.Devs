using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Command
{
    public class UpdateProgrammingLanguageCommand : IRequest<UpdateProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdateProgrammingLanguageDto>
        {

            private readonly IProgrammingLanguageRepository _repo;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _progLangBusinessRules;

            public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repo, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _repo = repo;
                _mapper = mapper;
                _progLangBusinessRules = programmingLanguageBusinessRules;
            }



            public async Task<UpdateProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _progLangBusinessRules.ProgLangNameCanNotBeDuplicatedWhenInserted(request.Name);
               
                ProgrammingLanguage progLangToUpdate = await _repo.GetAsync(b => b.Id == request.Id);

                ProgrammingLanguage mappedProgLang = _mapper.Map(request, progLangToUpdate);
                await _progLangBusinessRules.ProgLangCannotBeNull(mappedProgLang.Name);
                ProgrammingLanguage updatedProgLang = await _repo.UpdateAsync(mappedProgLang);

             

                UpdateProgrammingLanguageDto updateProgrammingLanguageDto = _mapper.Map<UpdateProgrammingLanguageDto>(updatedProgLang);
                return updateProgrammingLanguageDto;
            }
        }

    }
}

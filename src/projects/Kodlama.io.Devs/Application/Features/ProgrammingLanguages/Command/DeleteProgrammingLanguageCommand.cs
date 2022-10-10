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
    public class DeleteProgrammingLanguageCommand : IRequest<DeleteProgrammingLanguageDto>
    {
        public int Id { get; set; }


        public class DeleteProgrammingLanguageHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeleteProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _repo;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _progLangBusinessRules;

            public DeleteProgrammingLanguageHandler(IProgrammingLanguageRepository repo, IMapper mapper, ProgrammingLanguageBusinessRules progLangBusinessRules)
            {
                _repo = repo;
                _mapper = mapper;
                _progLangBusinessRules = progLangBusinessRules;
            }

            public async Task<DeleteProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage programmingLanguage = await _repo.GetAsync(p => p.Id == request.Id);
                await _progLangBusinessRules.ForDelete(programmingLanguage);
                ProgrammingLanguage mappedProgLang = _mapper.Map(request, programmingLanguage);
                await _repo.DeleteAsync(mappedProgLang);
                DeleteProgrammingLanguageDto deleteProgrammingLanguageDto = _mapper.Map<DeleteProgrammingLanguageDto>(mappedProgLang);
                return deleteProgrammingLanguageDto;
            }
        }

    }
}

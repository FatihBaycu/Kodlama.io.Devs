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

namespace Application.Features.ProgramingLanguages.Queries
{
    public class GetByIdProgrammingLanguageQuery:IRequest<ProgrammingLanguageGetByIdDto>
    {
        public GetByIdProgrammingLanguageQuery()
        {
        }

        public int Id { get; set; }

        public class GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, ProgrammingLanguageGetByIdDto>
        {

            private readonly IProgrammingLanguageRepository _repo;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _progLangBusinessRules;

            public GetByIdProgrammingLanguageQueryHandler(IProgrammingLanguageRepository progLangRepository, IMapper mapper)
            {
                _repo = progLangRepository;
                _mapper = mapper;
               // _brandBusinessRules = brandBusinessRules;
            }

            public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage? programmingLanguage = await _repo.GetAsync(b => b.Id == request.Id);

                _progLangBusinessRules.ProgLangShouldExistWhenRequested(programmingLanguage);

                ProgrammingLanguageGetByIdDto programmingLanguageGetByIdDto= _mapper.Map<ProgrammingLanguageGetByIdDto>(programmingLanguage);
                return programmingLanguageGetByIdDto;
            }

        }


    }
}

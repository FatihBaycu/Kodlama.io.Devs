using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
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

namespace Application.Features.Technologies.Command
{
    public class CreateTechnologyCommand:IRequest<CreatedTechnologyDto>
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _rules;

            public CreateTechnologyCommandHandler()
            {
            }

            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules rules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _rules = rules;
            }
    
            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _rules.ProgLangControl(request.ProgrammingLanguageId);
                await _rules.TechIsExist(request.Name);

                Technology mappedTech = _mapper.Map<Technology>(request);
                Technology createdTech = await _technologyRepository.AddAsync(mappedTech);
                Technology? getByIdTech=await _technologyRepository.GetAsync(c=>c.Id==createdTech.Id,include:i=>i.Include(t=>t.ProgrammingLanguage));
                CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(getByIdTech);
                return createdTechnologyDto;
            }
        }
    }
}

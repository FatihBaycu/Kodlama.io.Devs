using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Command
{
    public class UpdateTechnologyCommand:IRequest<UpdateTechnologyDto>
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdateTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<UpdateTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechIsNotExist(request.Name);
                await _technologyBusinessRules.ProgLangControl(request.ProgrammingLanguageId);

                Technology? technologyToUpdate = await _technologyRepository.GetAsync(c=>c.Id==request.Id);
                Technology mappedTech = _mapper.Map(request, technologyToUpdate);
                await _technologyBusinessRules.TechNameCannotBeNull(mappedTech.Name);
                Technology technologyBeUpdated = await _technologyRepository.UpdateAsync(mappedTech);

                UpdateTechnologyDto updateTechnologyDto = _mapper.Map<UpdateTechnologyDto>(technologyBeUpdated);
                return updateTechnologyDto;
                
                   


            }
        }

    }
}

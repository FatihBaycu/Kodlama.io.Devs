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
    public class DeleteTechnologyCommand:IRequest<DeleteTechnologyDto>
    {
        public int Id { get; set; }

        public class DeleteTechnolgyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeleteTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public DeleteTechnolgyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<DeleteTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechControl(request.Id);

                Technology? technology = await _technologyRepository.GetAsync(c=>c.Id==request.Id);
                Technology mappedTech=_mapper.Map(request,technology);
                await _technologyRepository.DeleteAsync(mappedTech);
                DeleteTechnologyDto deleteTechnologyDto = _mapper.Map<DeleteTechnologyDto>(mappedTech);
                return deleteTechnologyDto;





            }
        }
    }
}

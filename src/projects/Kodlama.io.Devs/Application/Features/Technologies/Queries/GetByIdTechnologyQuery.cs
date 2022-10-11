using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Features.Technologies.Rules;

namespace Application.Features.Technologies.Queries
{
    public class GetByIdTechnologyQuery:IRequest<GetByIdTechnologyDto>
    {
        public int Id { get; set; }

        public GetByIdTechnologyQuery()
        {

        }

        public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery,GetByIdTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public GetByIdTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<GetByIdTechnologyDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
            {

                Technology? technology=await _technologyRepository.GetAsync(c=>c.Id==request.Id,include: a=>a.Include(m=>m.ProgrammingLanguage));
               await _technologyBusinessRules.TechControl(request.Id);
                GetByIdTechnologyDto getByIdTechnologyDto = _mapper.Map<GetByIdTechnologyDto>(technology);
                return getByIdTechnologyDto;
            }
        }
        

        }

    }


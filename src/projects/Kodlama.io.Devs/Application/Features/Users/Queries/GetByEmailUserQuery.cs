using Application.Features.Auths.Rules;
using Application.Features.Users.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetByEmailUserQuery:IRequest<GetByEmailUserDto>
    {
        public string Email { get; set; }

        public GetByEmailUserQuery()
        {
        }

        public class GetByEmailUserQueryHandler : IRequestHandler<GetByEmailUserQuery, GetByEmailUserDto>
        {

            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly AuthBusinessRules _authBusinessRules;

            public GetByEmailUserQueryHandler(IUserRepository userRepository, IMapper mapper, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<GetByEmailUserDto> Handle(GetByEmailUserQuery request, CancellationToken cancellationToken)
            {

                await _authBusinessRules.EmailIsExistWhenLogin(request.Email);
                User? user = await _userRepository.GetAsync(c => c.Email == request.Email);
                GetByEmailUserDto getByEmailUserDto = _mapper.Map<GetByEmailUserDto>(request);
                return getByEmailUserDto;
            }
        }


    }
}

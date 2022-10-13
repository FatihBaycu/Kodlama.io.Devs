using Application.Features.Users.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Command
{
    public class CreateUserCommand:IRequest<CreateUserDto>
    {
        public string Email { get; set; }
        public UserForRegisterDto UserForRegisterDto { get; set; }  

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly ITokenHelper _tokenHelper;
            //private readonly AuthorizationsBusinessRules _authorizationsBusiness;
            //private readonly IAuthService _authService;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, IMapper mapper)
            {
                _userRepository = userRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _tokenHelper = tokenHelper;
                _mapper = mapper;
            }

            public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                var user = new User
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };

                User createdUser=await _userRepository.AddAsync(user);


                var result = await _userOperationClaimRepository.AddAsync(new UserOperationClaim()
                {
                    UserId = createdUser.Id,
                    OperationClaimId = 1
                }); ;

                //AccessToken createdAccessToken=await 


                throw new NotImplementedException();
            }
        }


    }
}

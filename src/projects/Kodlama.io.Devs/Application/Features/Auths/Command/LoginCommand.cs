using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
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

namespace Application.Features.Auths.Command
{
    public class LoginCommand:IRequest<LoggedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        public string IpAddress { get; set; }


        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
        { 
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserRepository _userRepository;

            private readonly IAuthService _authService;

            public LoginCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
            {
                _authBusinessRules = authBusinessRules;
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.EmailIsExistWhenLogin(request.UserForLoginDto.Email);
                User? user= await _userRepository.GetAsync(c => c.Email == request.UserForLoginDto.Email);

                _authBusinessRules.VerifyUserPasswordHash(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                LoggedDto loggedDto = new()
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = addedRefreshToken,
                };

                return loggedDto;
            }
        }

    }
}

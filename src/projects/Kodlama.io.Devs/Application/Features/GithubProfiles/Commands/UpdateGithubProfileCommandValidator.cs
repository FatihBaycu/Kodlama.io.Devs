using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands
{
    public class UpdateGithubProfileCommandValidator : AbstractValidator<UpdateGithubProfileCommand>
    {
        public UpdateGithubProfileCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.UserId).NotNull();
            RuleFor(c => c.GithubAddress).NotEmpty();
            RuleFor(c => c.GithubAddress).NotNull();
        }
    }
}

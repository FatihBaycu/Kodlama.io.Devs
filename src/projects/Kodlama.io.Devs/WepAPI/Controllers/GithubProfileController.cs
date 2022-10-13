using Application.Features.GithubProfiles.Commands;
using Application.Features.GithubProfiles.Dtos;
using Application.Features.Technologies.Command;
using Application.Features.Technologies.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubProfileController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGithubProfileCommand createGithubProfileCommand)
        {
            CreateGithubProfileDto result = await Mediator.Send(createGithubProfileCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGithubProfileCommand updateGithubProfileCommand)
        {
            UpdateGithubProfileDto result = await Mediator.Send(updateGithubProfileCommand);
            return Created("", result);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGithubProfileCommand deleteGithubProfileCommand)
        {
            DeleteGithubProfileDto result = await Mediator.Send(deleteGithubProfileCommand);
            return Created("", result);
        }
    }
}

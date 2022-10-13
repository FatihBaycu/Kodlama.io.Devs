using Application.Features.GithubProfiles.Commands;
using Application.Features.GithubProfiles.Dtos;
using Application.Features.OperationClaims.Command;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimsQuery getListOperationClaimsQuery = new() { PageRequest=pageRequest};
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimsQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreateOperationClaimDto result = await Mediator.Send(createOperationClaimCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            UpdateOperationClaimDto result = await Mediator.Send(updateOperationClaimCommand);
            return Created("", result);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            DeleteOperationClaimDto result = await Mediator.Send(deleteOperationClaimCommand);
            return Created("", result);
        }
    }
}

using Application.Features.ProgrammingLanguages.Command;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.Technologies.Command;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecnologiesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery getListTechQuery = new() { PageRequest = pageRequest };
            TechnologyModel result = await Mediator.Send(getListTechQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            CreatedTechnologyDto result = await Mediator.Send(createTechnologyCommand);
            return Created("", result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdTechnologyQuery getByIdTechnologyQuery)
        {
            GetByIdTechnologyDto getByIdTechnologyDto = await Mediator.Send(getByIdTechnologyQuery);
            return Ok(getByIdTechnologyDto);
        }



        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyCommand updateTechnologyCommand)
        {
            UpdateTechnologyDto result = await Mediator.Send(updateTechnologyCommand);
            return Created("", result);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            DeleteTechnologyDto result = await Mediator.Send(deleteTechnologyCommand);
            return Created("", result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.Features.Engineers;

namespace API.Controllers
{
    public class EngineerController : BaseAPIController
    {
        [HttpGet]
        public async Task<IActionResult> GetEngineers()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEngineer(int id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
            ;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEngineer(Engineer engineer)
        {
            return Ok(await Mediator.Send(new Create.Command
            {
                Engineer = engineer
            }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEngineer(int id, Engineer engineer)
        {
            engineer.Id = id;
            return Ok(await Mediator.Send(new Edit.Command
            {
                Engineer = engineer
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngineer(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}

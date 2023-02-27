using Application.Features.Locations;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LocationController : BaseAPIController
    {
        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation(int id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
            ;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(Location location)
        {
            return Ok(await Mediator.Send(new Create.Command
            {
                Location = location
            }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditLocation(int id, Location location)
        {
            location.Id = id;
            return Ok(await Mediator.Send(new Edit.Command
            {
                Location = location
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivty(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
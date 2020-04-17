using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.DTOs;

namespace BeerOverflowAPI.ApiControllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerService _service;

        public BeersController(IBeerService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }
        // GET: api/Beers
        [HttpGet]
        public IActionResult Get()
        {
            var beers = _service.GetAll();
            return Ok(beers);
        }

        // GET: api/Beers/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var beers = _service.GetBeer(id);
            return Ok(beers);
        }

        // POST: api/Beers
        [HttpPost]
        public IActionResult Post([FromBody] BeerDTO beer)
        {
            if (beer == null)
            {
                return BadRequest();
            }
            var theNewBeer = this._service.Create(beer);
            return Created("Post", theNewBeer);
        }

        // PUT: api/Beers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BeerDTO beer)
        {
            if (beer == null)
            {
                return BadRequest();
            }
            var theNewBeer = this._service.Update(id,beer);
            return Ok(theNewBeer);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}

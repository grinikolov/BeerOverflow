using BeerOverflow.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowAPI.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : ControllerBase
    {
        private readonly IBreweryService _service;

        public BreweryController(IBreweryService service)
        {
            this._service = service;
        }

        // GET: api/Brewery
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var breweries =  await this._service.GetAll();
            return Ok(breweries);
        }

        // GET: api/Brewery/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var brewery = await this._service.GetBrewery(id);
                return Ok(brewery);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: api/Brewery
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BreweryDTO brewery)
        {
            if (brewery == null)
            {
                return  BadRequest();
            }

            var theNewBrewery = await this._service.Create(brewery);
            if (theNewBrewery.ID == default)
            {
                return BadRequest();
            }
            return Created("Post", theNewBrewery);

        }


        // PUT: api/Brewery/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BreweryDTO brewery)
        {
            if (id == 0 || brewery == null)
            {
                return BadRequest();
            }

            var model = await this._service.Update(id, brewery);

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await this._service.Delete(id);
        }
    }
}

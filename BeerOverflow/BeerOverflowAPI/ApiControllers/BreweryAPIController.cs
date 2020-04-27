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
    public class BreweryAPIController : ControllerBase
    {
        private readonly IBreweryService _service;

        public BreweryAPIController(IBreweryService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/Brewery
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var breweries =  await this._service.GetAllAsync();
            return Ok(breweries);
        }

        // GET: api/Brewery/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await this._service.GetAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // PUT: api/Brewery/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BreweryDTO model)
        {
            if (id <= 0 || model == null)
            {
                return BadRequest();
            }

            var returnModel = await this._service.UpdateAsync(id, model);
            if (returnModel == null)
            {
                return NotFound();
            }
            return Ok(returnModel);
        }

        // POST: api/Brewery
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BreweryDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var theNewBrewery = await this._service.CreateAsync(model);
            if (theNewBrewery.ID == default)
            {
                return BadRequest();
            }

            return Created("Post", theNewBrewery);

        }
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await this._service.DeleteAsync(id);
        }


    }
}

﻿using System;
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
    public class BeersAPIController : ControllerBase
    {
        private readonly IBeerService _service;

        public BeersAPIController(IBeerService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }
        // GET: api/Beers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var beers = await _service.GetAllAsync();
            return Ok(beers);
        }

        // GET: api/Beers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var beers = await _service.GetAsync(id);
            return Ok(beers);
        }

        // POST: api/Beers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BeerDTO beer)
        {
            if (beer == null)
            {
                return BadRequest();
            }
            var theNewBeer =await this._service.CreateAsync(beer);
            return Created("Post", theNewBeer);
        }

        // PUT: api/Beers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BeerDTO beer)
        {
            if (beer == null)
            {
                return BadRequest();
            }
            var theNewBeer = await this._service.UpdateAsync(id,beer);
            return Ok(theNewBeer);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _service.DeleteAsync(id);
        }

        // GET: api/Beers/search?
        [HttpGet("search/{searchString}")]  //TODO: Routing: to search [FromQuery] 
        public async Task<IActionResult>Search(string searchString) //name, string brewery, string country)
        {
            var beers = await _service.Search(searchString);
            return Ok(beers);
        }
        // GET: api/Beers/RandomBeer
        [HttpGet("RandomBeer")]
        public async Task<IActionResult> GetRandom()
        {
            var beers = await _service.GetRandom();
            return Ok(beers);
        }
    }
}

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
        public IActionResult Get()
        {
            var breweries = this._service.GetAll();
                //.Select(br => new BreweryDTO
                //{
                //    ID = br.ID,
                //    Name = br.Name,
                //    Country = br.Country,
                //    //Beers = br.Beers.Select(b => new BeerDTO { }).ToList(),
                //})
                //.ToList();
            return Ok(breweries);
        }

        // GET: api/Brewery/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var brewery = this._service.GetBrewery(id);
                //var result = new BreweryDTO
                //{
                //    ID = brewery.ID,
                //    Name = brewery.Name,
                //    Country = brewery.Country,
                //    Beers = brewery.Beers,
                //};
                return Ok(brewery);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: api/Brewery
        [HttpPost]
        public IActionResult Post([FromBody] BreweryDTO brewery)
        {
            if (brewery == null)
            {
                return BadRequest();
            }
            //var beerStyleDTO = new BeerStyleDTO
            //{
            //    ID = style.ID,
            //    Name = style.Name,
            //    Description = style.Description,
            //};

            var theNewBrewery = this._service.Create(brewery);
            return Created("Post", theNewBrewery);

        }


        // PUT: api/Brewery/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BreweryDTO brewery)
        {
            if (id == 0 || brewery == null)
            {
                return BadRequest();
            }

            //var beerStyleDTO = new BeerStyleDTO
            //{
            //    ID = style.ID,
            //    Name = style.Name,
            //    Description = style.Description,
            //};

            var model = this._service.Update(id, brewery);

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return this._service.Delete(id);

            //return didDelete/*;*/
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await this._service.DeleteAsync(id);

            //return didDelete;
        }
    }
}

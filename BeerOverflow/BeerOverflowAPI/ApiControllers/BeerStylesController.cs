using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTOs;

namespace BeerOverflowAPI.ApiControllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BeerStylesController : ControllerBase
    {
        private readonly IBeerStylesService _service;
        public BeerStylesController(IBeerStylesService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/BeerStyles
        [HttpGet]
        public IActionResult Get()
        {
            var styles = this._service.GetAll()
                .ToList();

            return Ok(styles);
        }

        // GET: api/BeerStyles/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var beerStyleDTO = this._service.Get(id);
            if (beerStyleDTO == null)
            {
                return NotFound();
            }
            return Ok(beerStyleDTO);

        }

        // POST: api/BeerStyles
        [HttpPost]
        public IActionResult Post([FromBody] BeerStyleDTO style)
        {
            if (style.Name == null || style.Description == null)
            {
                return BadRequest();
            }
            var model = new BeerStyleDTO
            {
                //ID = style.ID,
                Name = style.Name,
                Description = style.Description,
            };

            var theNewBeer = this._service.Create(model);
            return Created("Post", theNewBeer);

        }

        // PUT: api/BeerStyles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, BeerStyleDTO style)
        {
            if (id <= 0 || style == null)
            {
                return BadRequest();
            }

            var model = new BeerStyleDTO
            {
                //ID = style.ID,
                Name = style.Name,
                Description = style.Description,
            };

            var returnModel = this._service.Update(id, model);

            return NoContent();
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            bool didDelete = await this._service.DeleteAsync(id);

            return didDelete;
        }
    }
}

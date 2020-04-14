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
    [Route("api/[controller]")]
    [ApiController]
    public class BeerStylesController : ControllerBase
    {
        private readonly IBeerStylesService _service;
        public BeerStylesController(IBeerStylesService service)
        {
            this._service = service;
        }

        // GET: api/BeerStyles
        [HttpGet]
        public IActionResult Get()
        {
            var styles = this._service.GetAll()
                .Select(bs => new BeerStyleViewModel
                {
                    ID = bs.ID,
                    Name = bs.Name,
                    Description = bs.Description,
                })
                .ToList();

            return Ok(styles);
        }

        // GET: api/BeerStyles/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var beerStyleDTO = this._service.GetBeerStyle(id);
                var style = new BeerStyleViewModel
                {
                    ID = beerStyleDTO.ID,
                    Name = beerStyleDTO.Name,
                    Description = beerStyleDTO.Description,
                };
                return Ok(style);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: api/BeerStyles
        [HttpPost]
        public IActionResult Post([FromBody] BeerStyleViewModel style)
        {
            if (style == null)
            {
                return BadRequest();
            }
            var beerStyleDTO = new BeerStyleDTO
            {
                ID = style.ID,
                Name = style.Name,
                Description = style.Description,
            };

            var theNewBeer = this._service.Create(beerStyleDTO);
            return Created("Post", beerStyleDTO);

        }

        // PUT: api/BeerStyles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BeerStyleViewModel style)
        {
            if (id == 0 || style == null)
            {
                return BadRequest();
            }

            var beerStyleDTO = new BeerStyleDTO
            {
                ID = style.ID,
                Name = style.Name,
                Description = style.Description,
            };

            var model = this._service.Update(id,beerStyleDTO);

            return Ok();
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            bool didDelete = this._service.Delete(id);

            return didDelete;
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

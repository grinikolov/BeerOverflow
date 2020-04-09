using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

using Services.Contracts;

namespace BeerOverflowAPI.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerStylesController : ControllerBase
    {
        private readonly IBeerStyleService beerStyleService;

        public BeerStylesController(IBeerStyleService beerStyleService)
        {
            this.beerStyleService = beerStyleService ?? throw new ArgumentNullException(nameof(beerStyleService));
        }

        // GET: api/BeerStyles
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/BeerStyles/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/BeerStyles
        //[HttpPost]
        [Route("api/BeerStyles")]
        public IActionResult Post([FromBody] BeerStyleViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            BeerStyleDTO theBeerStyle = new BeerStyleDTO
            {
                ID = model.ID,
                Name = model.Name,
                Description = model.Description
            };

            var theNewBeerDTO = this.beerStyleService.CreateStyle(theBeerStyle);
            return Created("Post", theNewBeerDTO);
        }

        // PUT: api/BeerStyles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

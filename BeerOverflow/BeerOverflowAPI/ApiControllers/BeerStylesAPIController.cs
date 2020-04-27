using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTOs;

namespace BeerOverflowAPI.ApiControllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BeerStylesAPIController : ControllerBase
    {
        private readonly IBeerStylesService _service;
        public BeerStylesAPIController(IBeerStylesService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/BeerStyles
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var styles = await this._service.GetAllAsync();

            return Ok(styles);
        }

        // GET: api/BeerStyles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var model = await this._service.GetAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // PUT: api/BeerStyles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, BeerStyleDTO model)
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

        // POST: api/BeerStyles
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BeerStyleDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var theNewStyle = await this._service.CreateAsync(model);
            if (theNewStyle.ID == default)
            {
                return BadRequest();
            }
            return Created("Post", theNewStyle);

        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await this._service.DeleteAsync(id);
        }
    }
}

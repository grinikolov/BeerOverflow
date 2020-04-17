using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Models;
using Database;
using Services;
using Services.Contracts;
using Services.DTOs;

namespace BeerOverflowAPI.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _service;

        public CountriesController(ICountriesService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var countries = await this._service.GetAll();

            return Ok(countries);
        }

        // GET: api/Countries/5
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

        // PUT: api/Countries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CountryDTO model)
        {
            if (id <= 0 || model == null)
            {
                return BadRequest();
            }

            //var model = new CountryDTO
            //{
            //    Name = countryDTO.Name,
            //};

            var returnModel =await this._service.UpdateAsync(id, model);
            if (returnModel == null)
            {
                return NotFound();
            }
            return Ok(returnModel);
        }

        // POST: api/Countries
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CountryDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            //var model = new CountryDTO
            //{
            //    Name = countryDTO.Name,
            //};
            var theNewCountry = await this._service.CreateAsync(model);
            if (theNewCountry.ID == default)
            {
                return BadRequest();
            }

            return Created("Post", theNewCountry);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            bool didDelete = await this._service.DeleteAsync(id);

            return didDelete;
        }
        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public async Task<bool> DeleteAsync(int id)
        //{
        //    bool didDelete = await this._service.DeleteAsync(id);

        //    return didDelete;
        //}


    }
}

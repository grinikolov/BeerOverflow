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
        public IActionResult Get()
        {
            var countries = this._service.GetAll()
                .ToList();

            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var model = this._service.Get(id);

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
        public IActionResult Put(int id, CountryDTO countryDTO)
        {
            if (id <= 0 || countryDTO == null)
            {
                return BadRequest();
            }

            var model = new CountryDTO
            {
                Name = countryDTO.Name,
            };

            var returnModel = this._service.Update(id, model);

            return NoContent();
        }

        // POST: api/Countries
        [HttpPost]
        public IActionResult Post(CountryDTO countryDTO)
        {
            if (countryDTO.Name == null )
            {
                return BadRequest();
            }
            var model = new CountryDTO
            {
                Name = countryDTO.Name,
            };
            var theNewCountry = this._service.Create(model);
            return Created("Post", theNewCountry);
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.DTOs;

namespace BeerOverflowAPI.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        private readonly IUsersService _service;

        public UsersAPIController(IUsersService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/Users
        [HttpGet]
        //TODO: return UserViewModel-s
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            try
            {
                var users = await this._service.GetAllUsers();
                //TODO map to UserViewModel-s here, not in Service
                return Ok(users);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await this._service.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO user)
        {
            if (id <= 0 || user == null)
            {
                return BadRequest();
            }

            var model = await this._service.UpdateUserAsync(id, user);
            return Ok(model);

            //else: return NoContent();
        }

        //TODO: enpoints routing
        [HttpPut("user/{id}/Drinkbeer/{beerID}")]
        public async Task<IActionResult> Drink(int id, int beerID) // was [FromBody] BeerDTO beerDTO)
        {
            if (id <= 0 || beerID == default)
            {
                return BadRequest();
            }

            try
            {
                var model = await this._service.Drink(id, beerID);
                return Ok(model);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("user/{id}/DrankList")]
        public async Task<ActionResult<IEnumerable<BeerDTO>>> GetDrankBeers(int id)
        {
            try
            {
                var theBeers = await this._service.GetDrankBeers(id);
                return Ok(theBeers);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



        //TODO: enpoints routing
        [HttpPut("user/{id}/Wish/{beerID}")]
        public async Task<IActionResult> Wish(int id, int beerID) // was [FromBody] BeerDTO beerDTO)
        {
            //var beerID = beerDTO.ID;
            if (id <= 0 || beerID == default)
            {
                return BadRequest();
            }

            try
            {
                var model = await this._service.Wish(id, beerID);
                return Ok(model);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("user/{id}/WishList")]
        public async Task<ActionResult<IEnumerable<BeerDTO>>> GetWishBeers(int id)
        {
            try
            {
                var theBeers = await this._service.GetWishBeers(id);
                return Ok(theBeers);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        //TODO: enpoints routing
        [HttpPut("user/{id}/Rate")]
        public async Task<IActionResult> Rate(int id, [FromBody] BeerDTO beerDTO)
        {
            var beerID = beerDTO.ID;
            if (id <= 0 || beerID == default)
            {
                return BadRequest();
            }

            try
            {
                var model = await this._service.Rate(id, beerDTO.ID, (int)beerDTO.Rating);
                return Ok(model);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var theNewUser = await this._service.CreateUser(user);

            return CreatedAtAction("GetUser", theNewUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            return await this._service.DeleteUser(id);
        }

    }
}

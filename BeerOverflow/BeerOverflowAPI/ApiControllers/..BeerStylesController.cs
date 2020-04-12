//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using BeerOverflow.Models;
////using Database;
//using Services.DTOs;
//using Services.Contracts;

//namespace BeerOverflowAPI.ApiControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BeerStylesController : ControllerBase
//    {
//        //private readonly BOContext _context;
//        private readonly IBeerStyleService _beerStyleService;

//        public BeerStylesController(IBeerStyleService beerStyleService)
//        {
//            this._beerStyleService = beerStyleService ?? throw new ArgumentNullException(nameof(beerStyleService));
//        }

//        // GET: api/BeerStyles
//        [HttpGet]
//        public ICollection<BeerStyleDTO> GetAllBeerStyles()
//        {
//            List<BeerStyleDTO> beerStyles = _context.BeerStyles
//                .Where(beer => beer.IsDeleted == false)
//                .Select(style => new BeerStyleDTO
//                {
//                    ID = style.ID,
//                    Name = style.Name,
//                    Description = style.Description
//                })
//               .ToList();
//            return beerStyles;
//        }

//        // GET: api/BeerStyles/5
//        [HttpGet("{id}")]
//        public IActionResult GetBeerStyle(int id)
//        {
//            var beerStyleDTO = this._beerStyleService.GetStyle(id);

//            if (beerStyleDTO == null)
//            {
//                return NotFound();
//            }

//            return Ok();
//        }

//        // PUT: api/BeerStyles/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for
//        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
//        [HttpPut("{id}")]
//        // was: public async Task<IActionResult> PutBeerStyle(int? id, BeerStyle beerStyle)
//        public BeerStyleDTO PutBeerStyle(int? id, BeerStyleDTO beerStyleDTO)
//        {

//            var theNewBeerStyleDTO = new BeerStyleDTO
//            {
//                Name = beerStyleDTO.Name,
//                Description = beerStyleDTO.Description
//            };


//            return theNewBeerStyleDTO;
//        }

//        // POST: api/BeerStyles
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for
//        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
//        [HttpPost]
//        public IActionResult PostBeerStyle(BeerStyleViewModel model)
//        {
//            if (model == null)
//            {
//                return BadRequest();
//            }
//            var theNewBeerStyleDTO = new BeerStyleDTO
//            {
//                Name = beerStyleDTO.Name,
//                Description = beerStyleDTO.Description
//            };
//            var newBeerStyle = this.Service

//            //_context.BeerStyles.Add(beerStyle);
//            //await _context.SaveChangesAsync();

//            return CreatedAtAction("GetBeerStyle", new { id = beerStyle.ID }, beerStyle);
//        }

//        // DELETE: api/BeerStyles/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<BeerStyle>> DeleteBeerStyle(int? id)
//        {
//            var beerStyle = await _context.BeerStyles.FindAsync(id);
//            if (beerStyle == null)
//            {
//                return NotFound();
//            }

//            _context.BeerStyles.Remove(beerStyle);
//            await _context.SaveChangesAsync();

//            return beerStyle;
//        }

//        private bool BeerStyleExists(int? id)
//        {
//            return _context.BeerStyles.Any(e => e.ID == id);
//        }
//    }
//}

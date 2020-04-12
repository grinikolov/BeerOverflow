//using BeerOverflow.Models;
//using Database;
//using Services.Contracts;
//using Services.DTOs;
//using System;
//using System.Collections.Generic;

//namespace Services
//{
//    public class BeerStyleService : IBeerStyleService
//    {
//        private readonly BOContext _context;
//        public BeerStyleService(BOContext context)
//        {
//            this._context = context;
//        }
//        public BeerStyleDTO CreateStyle(BeerStyleDTO style)
//        {
//            BeerStyle theNewStyle = new BeerStyle
//            {
//                ID = style.ID,
//                Name = style.Name,
//                Description = style.Description,
//                CreatedOn = DateTime.UtcNow,

//            };
//            // TODO: Database add: 
//            _context.BeerStyles.Add(theNewStyle);

//            return style;
//        }

//        public bool DeleteStyle()
//        {
//            throw new NotImplementedException();
//        }

//        public ICollection<BeerStyleDTO> GetAllStyles()
//        {
//            //TODO:
//            throw new NotImplementedException();
//        }

//        public BeerStyleDTO GetStyle(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public BeerStyleDTO UpdateStyle(int id, string newName, string newDescription)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

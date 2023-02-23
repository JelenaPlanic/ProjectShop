using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopService.Interfaces;
using ShopService.Models;
using ShopService.Models.DTO;

namespace ShopService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IMapper _mapper;

        public SellersController(ISellerRepository sellerRepository, IMapper mapper)
        {
            _sellerRepository = sellerRepository;
            _mapper = mapper;
        }

        // GET: api/Sellers
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetSellers()
        {
            return Ok(_sellerRepository.GetAll().ProjectTo<SellerDTO>(_mapper.ConfigurationProvider).ToList()); 
        }


        // GET: api/Seller/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IActionResult GetSeller(int id)
        {
            var found = _sellerRepository.GetById(id);
            if(found == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SellerDTO>(found));
        }

        // PUT: api/Sellers/5
        [HttpPut("{id}")]
        public IActionResult PutSeller(int id, Seller seller)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != seller.Id)
            {
                return BadRequest();
            }

            try
            {
                _sellerRepository.Update(seller);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(seller);
        }

        // POST: api/Sellers
        [HttpPost]
        public IActionResult PostSeller(Seller seller)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _sellerRepository.Add(seller);

            return CreatedAtAction("GetSeller", new { id = seller.Id }, seller);
        }

        // DELETE: api/Sellers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSeller(int id)
        {
            var found = _sellerRepository.GetById(id);
            if (found == null)
            {
                return NotFound();
            }

            _sellerRepository.Delete(found);
            return NoContent();
        }
    }
}

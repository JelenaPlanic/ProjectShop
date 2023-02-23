using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopService.Interfaces;
using ShopService.Models;

namespace ShopService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;

        public ShopsController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        // GET: api/Shops
        [HttpGet]
        public IActionResult GetShops()
        {
            return Ok(_shopRepository.GetAll().ToList());
        }

        //GET: api/Shops/5
        [HttpGet("{id}")]
        public IActionResult GetShop(int id)
        {
            var found = _shopRepository.GetById(id);
            if(found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }
        //PUT: api/Shops/5
        [HttpPut("{id}")]
        public IActionResult PutShop(int id, Shop shop)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != shop.Id)
            {
                return BadRequest();
            }

            try
            {
                _shopRepository.Update(shop);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(shop);
        }

        //POST: api/Shops
        [HttpPost]
        public IActionResult PostShop(Shop shop)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _shopRepository.Add(shop);

            return CreatedAtAction("GetShop", new { id = shop.Id }, shop);
        }

        //DELETE: api/Shops/5
        [HttpDelete("{id}")]
        public IActionResult DeleteShop(int id)
        {
            var found = _shopRepository.GetById(id);
            if(found == null)
            {
                return NotFound();
            }

            _shopRepository.Delete(found);
            return NoContent();
        }



    }
}

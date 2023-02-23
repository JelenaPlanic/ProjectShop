using Microsoft.EntityFrameworkCore;
using ShopService.Interfaces;
using ShopService.Models;

namespace ShopService.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly AppDbContext _context;

        public ShopRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Shop> GetAll()
        {
            return _context.Shops;
        }

        public Shop GetById(int id)
        {
            return _context.Shops.FirstOrDefault(shop => shop.Id == id);
        }

        public void Update(Shop shop)
        {
           _context.Entry(shop).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Add(Shop shop)
        {
            _context.Shops.Add(shop);
            _context.SaveChanges();
        }


        public void Delete(Shop shop)
        {
            _context.Shops.Remove(shop);
            _context.SaveChanges();
        }




















    }
}

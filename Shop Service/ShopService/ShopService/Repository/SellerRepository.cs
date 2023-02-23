using Microsoft.EntityFrameworkCore;
using ShopService.Interfaces;
using ShopService.Models;

namespace ShopService.Repository
{
    public class SellerRepository : ISellerRepository
    {
        private readonly AppDbContext _context;

        public SellerRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Seller> GetAll()
        {
            return _context.Sellers.OrderBy(seller => seller.Year);
        }

        public Seller GetById(int id)
        {
            return _context.Sellers.FirstOrDefault(seller => seller.Id == id);
        }


        public void Update(Seller seller)
        {
            _context.Entry(seller).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Add(Seller seller)
        {
           _context.Sellers.Add(seller);
            _context.SaveChanges();
        }

        public void Delete(Seller seler)
        {
            _context.Sellers.Remove(seler);
            _context.SaveChanges();
        }

       

       

    }
}

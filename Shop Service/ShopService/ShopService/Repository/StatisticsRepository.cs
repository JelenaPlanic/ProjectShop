using Microsoft.EntityFrameworkCore;
using ShopService.Interfaces;
using ShopService.Models;
using ShopService.Models.DTO;

namespace ShopService.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly AppDbContext _context;

        public StatisticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public StatisticsDTO GetStatistics()
        {
            return new StatisticsDTO()
            { 
                SellerAges = this.GetAgeForSellers(),
                AverageShopAge = this.GetAverageAgeForSellersInShops(), 
                TotalShopAge = this.GetTotalAgeForSellersInShops() 
            };
        }


        private List<SellerAgeDTO> GetAgeForSellers()
        {
           return _context.Sellers.Select(s =>
           new SellerAgeDTO()
           {
               Id = s.Id,
               Seller = s.Name + " " + s.Surname,
               Age = DateTime.Now.Year - s.Year
           }
           ).ToList();
        }

        private List<ShopAgeDTO> GetAverageAgeForSellersInShops()
        {
            return _context.Sellers.GroupBy(s => s.ShopId).Select(s => new ShopAgeDTO()
            {
                Name = _context.Shops.Where(shop => shop.Id == s.Key).Select(shop => shop.Name).Single(),
                AverageAge = s.Average(s => DateTime.Now.Year - s.Year)
            }
            ).ToList();
        }

        private List<ShopTotalAgeDTO> GetTotalAgeForSellersInShops()
        {
            return _context.Sellers.Include(s => s.Shop).GroupBy(s => s.ShopId)
                .Select(s => new ShopTotalAgeDTO()
                {
                    Name = _context.Shops.Where(shop => shop.Id == s.Key).Select(shop => shop.Name).Single(),
                    TotalAge = s.Sum( s => DateTime.Now.Year - s.Year)
                    
                }).ToList();
        }
    }
}

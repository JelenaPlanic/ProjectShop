using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IShopRepository
    {
        IQueryable<Shop> GetAll();
        Shop GetById(int id);
        void Add(Shop shop);
        void Update(Shop shop);
        void Delete(Shop shop);
    }
}

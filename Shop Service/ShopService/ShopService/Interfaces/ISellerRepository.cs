using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface ISellerRepository
    {
        IQueryable<Seller> GetAll();
        Seller GetById(int id);
        void Add(Seller seller);
        void Update(Seller seller);
        void Delete(Seller seler);
    }
}

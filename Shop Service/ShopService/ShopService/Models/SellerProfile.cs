using AutoMapper;
using ShopService.Models.DTO;

namespace ShopService.Models
{
    public class SellerProfile : Profile
    {
        public SellerProfile()
        {
            CreateMap<Seller, SellerDTO>();
        }
    }
}

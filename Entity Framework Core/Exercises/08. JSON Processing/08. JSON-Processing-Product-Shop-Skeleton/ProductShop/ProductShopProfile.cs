using AutoMapper;

using ProductShop.Data.DTO.ProductsDTO;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<Product, ProductsInRange>()
                .ForMember(x => x.Seller, y => y.MapFrom(s => s.Seller.FirstName + " " + s.Seller.LastName));
        }
    }
}

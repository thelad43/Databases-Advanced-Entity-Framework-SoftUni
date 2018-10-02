namespace ProductShop.App.Infrastructure.Mapping
{
    using AutoMapper;
    using Data.Models;
    using Models;
    using System.Linq;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Seller, opt => opt.MapFrom(src => $"{src.Seller.FirstName} {src.Seller.LastName}"));

            CreateMap<User, ShortUserProductsModel>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.SoldProducts));

            CreateMap<User, UserProductsModel>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.SoldProducts))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age));

            CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Count))
                .ForMember(dest => dest.AveragePrice, opt => opt.MapFrom(src => src.Products.Average(p => p.Product.Price)))
                .ForMember(dest => dest.TotalPriceSum, opt => opt.MapFrom(src => src.Products.Sum(p => p.Product.Price)));
        }
    }
}
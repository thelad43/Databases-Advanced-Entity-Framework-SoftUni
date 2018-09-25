namespace ProductShop.App.Infrastructure.Mappings
{
    using AutoMapper;
    using Data.Models;
    using Models;
    using System.Linq;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserModel, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName ?? string.Empty));

            CreateMap<UserModel, User>()
               .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age != null ? int.Parse(src.Age) : (int?)null));

            CreateMap<CategoryNameModel, Category>();

            CreateMap<ShortProductModel, Product>();

            CreateMap<Product, ProductBuyerModel>()
                .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => $"{src.Buyer.FirstName} {src.Buyer.LastName}"));

            CreateMap<User, UserProductsModel>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.SoldProducts));

            CreateMap<Category, CategoryInfoModel>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Count))
                .ForMember(dest => dest.AveragePrice, opt => opt.MapFrom(src => src.Products.Average(p => p.Product.Price)))
                .ForMember(dest => dest.TotalPriceSum, opt => opt.MapFrom(src => src.Products.Sum(p => p.Product.Price)));
        }
    }
}
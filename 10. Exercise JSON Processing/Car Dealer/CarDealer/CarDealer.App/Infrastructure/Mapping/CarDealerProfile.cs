namespace CarDealer.App.Infrastructure.Mapping
{
    using AutoMapper;
    using Data.Models;
    using Models;
    using System.Linq;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<Supplier, SupplierModel>()
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src.Parts.Count));

            CreateMap<Sale, ShortSaleModel>()
                .ForMember(dest => dest.CarMake, opt => opt.MapFrom(src => src.Car.Make))
                .ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.Car.Model));

            CreateMap<Car, CarPartsModel>()
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src
                    .Parts
                    .Select(p => new PartModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })));

            CreateMap<Customer, CustomerSalesModel>()
                .ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src
                    .Sales
                    .Select(s => new ShortSaleModel()
                    {
                        CarMake = s.Car.Make,
                        CarModel = s.Car.Model,
                        Discount = s.Discount
                    })));

            CreateMap<Customer, ShortCustomerSalesModel>();
        }
    }
}
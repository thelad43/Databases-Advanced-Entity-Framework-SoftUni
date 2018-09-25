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
            CreateMap<ShortSupplierModel, Supplier>();

            CreateMap<Supplier, LongSupplierModel>()
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src.Parts.Count));

            CreateMap<Car, CarPartsModel>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src.Parts.Select(p => p.Part)));

            CreateMap<Customer, CustomerCarsModel>()
                .ForMember(dest => dest.Cars, opt => opt.MapFrom(src => src.Sales.Count));

            CreateMap<Sale, SaleModel>();
        }
    }
}
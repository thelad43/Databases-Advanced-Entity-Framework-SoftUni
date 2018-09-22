namespace BusTicketsSystem.App.Infrastructure.Mapping
{
    using AutoMapper;
    using Models;
    using Services.Models;

    public class BusTicketsProfile : Profile
    {
        public BusTicketsProfile()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Trip, TripInfoModel>();

                cfg.CreateMap<Trip, Trip>();

                cfg.CreateMap<BusStation, BusStationInfoModel>()
                    .ForMember(s => s.Town, opt => opt.MapFrom(bs => bs.Town.Name));

                cfg.CreateMap<Review, ReviewCompanyModel>()
                   .ForMember(r => r.CustomerName, opt => opt.MapFrom(r => $"{r.Customer.FirstName} {r.Customer.LastName}"))
                   .ForMember(r => r.BusCompanyId, opt => opt.MapFrom(r => r.CompanyId));
            });
        }
    }
}
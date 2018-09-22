namespace PhotoShare.App.Infrastructure.Mappings
{
    using AutoMapper;
    using Core.Models;
    using Models;

    public class PhotoShareProfile : Profile
    {
        public PhotoShareProfile()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Town, Town>();

                cfg.CreateMap<Album, Album>();

                cfg.CreateMap<AlbumRole, AlbumRole>();

                cfg.CreateMap<AlbumTag, AlbumTag>();

                cfg.CreateMap<Friendship, Friendship>();

                cfg.CreateMap<Picture, Picture>();

                cfg.CreateMap<Tag, Tag>();

                cfg.CreateMap<User, User>();

                cfg.CreateMap<Town, TownModel>().ReverseMap();

                cfg.CreateMap<Album, AlbumModel>().ReverseMap();

                cfg.CreateMap<Tag, TagModel>().ReverseMap();

                cfg.CreateMap<AlbumRole, AlbumRoleModel>()
                        .ForMember(dest => dest.AlbumName, from => from.MapFrom(p => p.Album.Name))
                        .ForMember(dest => dest.Username, from => from.MapFrom(p => p.User.Username))
                        .ReverseMap();

                cfg.CreateMap<User, UserFriendsModel>()
                    .ForMember(dto => dto.Friends,
                        opt => opt.MapFrom(u => u.FriendsAdded));

                cfg.CreateMap<Friendship, FriendModel>()
                    .ForMember(dto => dto.Username,
                        opt => opt.MapFrom(f => f.Friend.Username));
            });
        }
    }
}
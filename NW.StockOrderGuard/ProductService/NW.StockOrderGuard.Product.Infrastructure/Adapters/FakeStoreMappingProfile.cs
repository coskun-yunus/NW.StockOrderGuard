using AutoMapper;
using NW.StockOrderGuard.Product.Domain.Entities;
using NW.StockOrderGuard.Product.Domain.ValueObjects;

namespace NW.StockOrderGuard.Product.Infrastructure.Adapters
{
    public class FakeStoreMappingProfile : Profile
    {
        public FakeStoreMappingProfile()
        {
            CreateMap<FakeStoreProductDto, Domain.Entities.Product>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.rating))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.category))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.image))
                .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<RatingDto, Rating>()
                .ConstructUsing(dto => new Rating(dto.rate??0, dto.count ?? 0));

            CreateMap<string, Category>()
                .ConstructUsing(name => new Category(0, name, "", true));

            CreateMap<string, Image>()
                .ConstructUsing(url => new Image(url));
        }
    }
} 
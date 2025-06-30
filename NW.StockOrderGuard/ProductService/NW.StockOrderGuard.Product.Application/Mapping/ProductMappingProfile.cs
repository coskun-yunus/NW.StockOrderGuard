using AutoMapper;
using NW.StockOrderGuard.Product.Domain.Entities;
using NW.StockOrderGuard.Product.Application.Dto;
using System.Linq;

namespace NW.StockOrderGuard.Product.Application.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Domain.Entities.Product, ProductDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image != null ? src.Image.Url : null))
                .ForMember(dest => dest.RatingRate, opt => opt.MapFrom(src => src.Rating != null ? src.Rating.Rate : 0))
                .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => src.Rating != null ? src.Rating.Count : 0))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category != null ? src.Category.Id : 0))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Category != null ? src.Category.Description : null))
                .ForMember(dest => dest.CategoryIsActive, opt => opt.MapFrom(src => src.Category != null && src.Category.IsActive))
                .ForMember(dest => dest.BestPrice, opt => opt.MapFrom(src =>
                    src.Offers != null && src.Offers.Any() ? src.Offers.Min(o => o.Price) : src.Price
                ));
        }
    }
} 
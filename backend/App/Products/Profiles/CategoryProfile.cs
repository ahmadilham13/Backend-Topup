using AutoMapper;
using backend.BaseModule.Models.Base;
using backend.Entities;
using backend.Products.Models.Category;

namespace backend.Products.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(dest =>
                dest.Created,
                opt => opt.MapFrom( src => DateTime.UtcNow ));

        CreateMap<Category, CategoryResponse>();

        CreateMap<Category, SelectDataResponse>()
            .ForMember(dest =>
                dest.Label,
                opt => opt.MapFrom( src => src.Name ))
            .ForMember(dest =>
                dest.Value,
                opt => opt.MapFrom( src => src.Id ));
    }
}
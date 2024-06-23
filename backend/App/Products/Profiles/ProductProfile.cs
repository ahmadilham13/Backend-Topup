using AutoMapper;
using backend.BaseModule.Models.Base;
using backend.Entities;
using backend.Products.Models.Category;
using backend.Products.Models.Product;

namespace backend.Products.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, Product>()
            .ForMember(dest =>
                dest.Created,
                opt => opt.MapFrom( src => DateTime.UtcNow ));

        CreateMap<Product, ProductResponse>();
        CreateMap<Product, ProductSingleResponse>()
            .ForMember(dest =>
                dest.Category,
                opt => opt.MapFrom( src => src.Category));
    }
}
using AutoMapper;
using backend.BaseModule.Models.Base;
using backend.BaseModule.Models.Media;
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
                opt => opt.MapFrom( src => DateTime.UtcNow ))
            .ForMember(dest =>
                dest.ProductItems,
                opt => opt.Ignore());

        CreateMap<Product, ProductResponse>();
        CreateMap<Product, ProductSingleResponse>()
            .AfterMap((src, dest) =>
            {
                dest.Thumbnail ??= new ImageUploadResponse()
                {
                    ImageBig = "template/img/product/product-bg.jpg",
                    ImageSmall = "template/img/product/product-sm.jpg",
                    ImageDefault = "template/img/product/product.jpg"
                };
            });
        CreateMap<Product, CategoryProductResponse>()
            .AfterMap((src, dest) =>
            {
                dest.Thumbnail ??= new ImageUploadResponse()
                {
                    ImageBig = "template/img/product/product-bg.jpg",
                    ImageSmall = "template/img/product/product-sm.jpg",
                    ImageDefault = "template/img/product/product.jpg"
                };
            });
    }
}
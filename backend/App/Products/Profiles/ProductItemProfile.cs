using AutoMapper;
using backend.Entities;
using backend.Products.Models.ProductItem;

namespace backend.Products.Profiles;

public class ProductItemProfile : Profile
{
    public ProductItemProfile()
    {
        CreateMap<ProductItem, ProductItemResponse>();
    }
}
using AutoMapper;
using backend.BaseModule.Models.Base;
using backend.Entities;
using backend.Products.Models.Category;
using backend.Vouchers.Models.Request;
using backend.Vouchers.Models.Response;

namespace backend.Vouchers.Profiles;

public class VoucherProfile : Profile
{
    public VoucherProfile()
    {
        CreateMap<CreateVoucherRequest, Voucher>()
            .ForMember(dest =>
                dest.Created,
                opt => opt.MapFrom( src => DateTime.UtcNow ));

        CreateMap<Voucher, VoucherResponse>();
        CreateMap<Voucher, VoucherSingleResponse>();
    }
}
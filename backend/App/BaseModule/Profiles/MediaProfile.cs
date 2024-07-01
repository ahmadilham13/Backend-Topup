using AutoMapper;
using backend.BaseModule.Models.Media;
using backend.Entities;

namespace backend.BaseModule.Profiles;

public class MediaProfile : Profile
{
    // mappings between model and entity objects
    public MediaProfile()
    {
        CreateMap<Media, ImageUploadResponse>();

        CreateMap<Media, ImageFullUploadResponse>();

        CreateMap<Task<Media>, Media>();
    }
}
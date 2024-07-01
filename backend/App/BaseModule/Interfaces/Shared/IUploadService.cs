using backend.Entities;

namespace backend.BaseModule.Interfaces.Shared;

public interface IUploadService
{
    Task<Media> UploadImage(IFormFile file, string imageType);
}
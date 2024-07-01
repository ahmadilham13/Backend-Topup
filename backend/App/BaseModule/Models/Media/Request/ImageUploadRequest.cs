using System.ComponentModel.DataAnnotations;
using backend.CustomValidations;

namespace backend.BaseModule.Models.Media;

public class ImageUploadRequest
{
    [Required]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions([".jpg", ".jpeg", ".png"])]
    public IFormFile image { get; set; }
}
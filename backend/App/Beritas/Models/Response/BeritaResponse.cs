using backend.BaseModule.Models.Media;
using backend.Entities;

namespace backend.Beritas.Models.Response;

public class BeritaResponse
{
    public Guid Id { get; set; }
    public ImageUploadResponse Path { get; set; }
    public Beritatype Type { get; set; }
    public string Description { get; set; }
}
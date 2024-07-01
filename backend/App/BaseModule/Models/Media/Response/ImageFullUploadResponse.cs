namespace backend.BaseModule.Models.Media;

public class ImageFullUploadResponse
{
    public Guid Id { get; set; }
    public string ImageDefault { get; set; }
    public string ImageBig { get; set; }
    public string ImageSmall { get; set; }
}
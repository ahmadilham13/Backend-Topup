using backend.BaseModule.Interfaces.Repositories;
using backend.BaseModule.Interfaces.Shared;
using backend.Constants;
using backend.Entities;
using backend.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace backend.BaseModule.Services;

public class UploadService : IUploadService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediaRepo _mediaRepo;

    public UploadService(
        IHttpContextAccessor httpContextAccessor,
        IMediaRepo mediaRepo,
        HttpClient httpClient = null
        )
    {
        _httpClient = httpClient ?? new HttpClient();
        _httpContextAccessor = httpContextAccessor;
        _mediaRepo = mediaRepo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="imageType"></param>
    /// <returns></returns>
    public async Task<Media> UploadImage(IFormFile file, string imageType)
    {
        if (checkIfImageFile(file))
        {
            var account = (Account)_httpContextAccessor.HttpContext.Items["Account"];

            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1].ToLower();
            var guid = Guid.NewGuid().ToString();
            var imagePath = await writeTempFile(file, guid, extension);

            Media media = new Media();

            (media.ImageDefault, media.ImageBig, media.ImageSmall) = checkImageTypeAndResize(imageType, imagePath, guid, extension);

            media.Id = Guid.NewGuid();
            media.ImageType = imageType;
            media.AuthorId = account != null ? account.Id : new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97");
            media.Created = DateTime.UtcNow;

            // save uploaded image
            await _mediaRepo.CreateMedia(media);

            DeleteImage(imagePath);

            return media;
        }

        throw new AppException("Invalid image file");
    }

    public void DeleteImage(string imageUri)
    {
        var imagePath = convertUriToPath(imageUri);
        var path = Path.Combine(Directory.GetCurrentDirectory(), imagePath);
        var NumberOfRetries = 3;
        var DelayOnRetry = 500;

        for (int i = 1; i <= NumberOfRetries; ++i)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                break; // When done we can break loop
            }
            catch (IOException e) when (i <= NumberOfRetries)
            {
                Console.WriteLine("Delete Image : {0}", e.Message);
                // You may check error code to filter some exceptions, not every error
                // can be recovered.
                Thread.Sleep(DelayOnRetry);
            }
        }
    }

    public void DeleteFile(string filePath)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);
        var NumberOfRetries = 3;
        var DelayOnRetry = 1000;

        for (int i = 1; i <= NumberOfRetries; ++i)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                break; // When done we can break loop
            }
            catch (IOException e) when (i <= NumberOfRetries)
            {
                Console.WriteLine("Delete Image : {0}", e.Message);
                // You may check error code to filter some exceptions, not every error
                // can be recovered.
                Thread.Sleep(DelayOnRetry);
            }
        }
    }

    private string convertUriToPath(string uriPath)
    {
        // var path = uriPath.Replace("/", "\\");
        var path = uriPath.Contains("wwwroot") ? uriPath : "wwwroot/" + uriPath;

        return path;
    }

    /// <summary>
    /// Method to check if file is image file
    /// </summary>
    private bool checkIfImageFile(IFormFile file)
    {
        byte[] fileBytes;
        using (var ms = new MemoryStream())
        {
            file.CopyTo(ms);
            fileBytes = ms.ToArray();
        }

        return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
    }

    /// <summary>
    /// Method to write file onto the disk
    /// </summary>
    private async Task<string> writeTempFile(IFormFile file, string guid, string extension)
    {
        string fileName;

        try
        {
            if (!Directory.Exists(SiteConstant.TempUploadPath))
            {
                Directory.CreateDirectory(SiteConstant.TempUploadPath);
            }

            fileName = "temp-" + guid + extension; //Create a new Name for the file due to security reasons.
            var path = Path.Combine(Directory.GetCurrentDirectory(), SiteConstant.TempUploadPath, fileName);

            using (var bits = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(bits);

                if (!WriterHelper.IsValidFileExtensionAndSignature(fileName, bits))
                {
                    DeleteFile(path);

                    throw new AppException("The file type isn't permitted or the file's " +
                        "signature doesn't match the file's extension.");
                }
            }

            return path;
        }
        catch (Exception e)
        {
            throw new AppException(e.Message);
        }
    }

    private (string, string, string) checkImageTypeAndResize(string imageType, string imagePath, string guid, string extension)
    {
        var sizeNames = new[] { "original", "bg", "sm" };
        string imageDefault, imageBig, imageSmall;

        switch (imageType)
        {
            case "avatar":
                var avatarSizes = new[] { new Size(1200), new Size(600), new Size(180) };

                imageDefault = resizeImage(imagePath, avatarSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = cropImage(imagePath, avatarSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = cropImage(imagePath, avatarSizes[2], imageType, sizeNames[2], guid, extension);
                break;

            case "news":
                var newsSizes = new[] { new Size(1200), new Size(900, 600), new Size(500, 240) };

                imageDefault = resizeImage(imagePath, newsSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = resizeImage(imagePath, newsSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = cropImage(imagePath, newsSizes[2], imageType, sizeNames[2], guid, extension);
                break;

            case "event":
                var eventSizes = new[] { new Size(1200), new Size(600), new Size(180) };

                imageDefault = resizeImage(imagePath, eventSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = cropImage(imagePath, eventSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = cropImage(imagePath, eventSizes[2], imageType, sizeNames[2], guid, extension);
                break;

            case "carousel":
                var landingSizes = new[] { new Size(1200), new Size(600), new Size(180) };

                imageDefault = resizeImage(imagePath, landingSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = cropImage(imagePath, landingSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = cropImage(imagePath, landingSizes[2], imageType, sizeNames[2], guid, extension);
                break;

            case "education":
                var educationSizes = new[] { new Size(1200), new Size(1200, 311), new Size(180) };

                imageDefault = resizeImage(imagePath, educationSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = cropImage(imagePath, educationSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = cropImage(imagePath, educationSizes[2], imageType, sizeNames[2], guid, extension);
                break;

            case "partner":
                var partnerSizes = new[] { new Size(1200), new Size(600), new Size(300, 180) };

                imageDefault = resizeImage(imagePath, partnerSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = resizeImage(imagePath, partnerSizes[1], imageType, sizeNames[1], guid, extension, "all-pad");
                imageSmall = resizeImage(imagePath, partnerSizes[2], imageType, sizeNames[2], guid, extension, "all-pad");
                break;

            case "media":
                var mediaSizes = new[] { new Size(1200), new Size(800), new Size(400) };

                imageDefault = resizeImage(imagePath, mediaSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = resizeImage(imagePath, mediaSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = resizeImage(imagePath, mediaSizes[2], imageType, sizeNames[2], guid, extension);
                break;

            case "icon":
                var iconSizes = new[] { new Size(100), new Size(48), new Size(24) };

                imageDefault = resizeImage(imagePath, iconSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = cropImage(imagePath, iconSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = cropImage(imagePath, iconSizes[2], imageType, sizeNames[2], guid, extension);
                break;

            case "featureinfo":
                var featureinfoSizes = new[] { new Size(200), new Size(100), new Size(50) };

                imageDefault = resizeImage(imagePath, featureinfoSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = cropImage(imagePath, featureinfoSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = cropImage(imagePath, featureinfoSizes[2], imageType, sizeNames[2], guid, extension);
                break;

            case "banner":
                var bannerSizes = new[] { new Size(1200), new Size(1130, 200), new Size(600, 200) };

                imageDefault = resizeImage(imagePath, bannerSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = cropImage(imagePath, bannerSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = cropImage(imagePath, bannerSizes[2], imageType, sizeNames[2], guid, extension);
                break;

            default:
                var defaultSizes = new[] { new Size(1200), new Size(600), new Size(180) };

                imageDefault = resizeImage(imagePath, defaultSizes[0], imageType, sizeNames[0], guid, extension);
                imageBig = cropImage(imagePath, defaultSizes[1], imageType, sizeNames[1], guid, extension);
                imageSmall = cropImage(imagePath, defaultSizes[2], imageType, sizeNames[2], guid, extension);
                break;
        }

        return (imageDefault, imageBig, imageSmall);
    }

    private string cropImage(string imagePath, Size size, string imageType, string sizeName, string guid, string extension)
    {
        string tempImagePath;
        string filePath;
        string newImageName;
        string newImagePath = $"{SiteConstant.UploadPath}/{imageType}";

        if (!Directory.Exists(newImagePath))
        {
            Directory.CreateDirectory(newImagePath);
        }

        using (Image image = Image.Load(imagePath))
        {
            var ratioX = (double)size.Width / image.Width;
            var ratioY = (double)size.Height / image.Height;
            var ratio = Math.Max(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            // if new width and height less than intended width and height then use the intended one
            newWidth = (newWidth < size.Width) ? newWidth + (size.Width - newWidth) : newWidth;
            newHeight = (newHeight < size.Height) ? newHeight + (size.Height - newHeight) : newHeight;

            var startX = (newWidth - size.Width) / 2;
            var startY = (newHeight - size.Height) / 2;

            var clone = image.Clone(
                i => i.Resize(newWidth, newHeight) //Resize image to crop size while retaining image ratio
                    .Crop(new Rectangle(startX, startY, size.Width, size.Height))
                );

            if (sizeName == "original")
            {
                newImageName = $"{guid}{extension}";
            }
            else
            {
                newImageName = $"{guid}-{sizeName}{extension}";
            }

            tempImagePath = Path.Combine(Directory.GetCurrentDirectory(), SiteConstant.TempUploadPath, newImageName);

            // Get image encoder based on image extension
            IImageEncoder encoder = getImageEncoder(extension);

            filePath = $"uploads/{imageType}/{newImageName}";

            // Save image to temporary path and then upload image to bucket
            clone.Save(tempImagePath, encoder);

            // // Upload image data to bucket
            // uploadBucket(tempImagePath, filePath, getMimeType(tempImagePath)).Wait();

            return $"{SiteConstant.UriPath}/{imageType}/{newImageName}";
        }
    }

    private string resizeImage(string imagePath, Size size, string imageType, string sizeName, string guid, string extension, string reference = "all, location")
    {
        string tempImagePath;
        string filePath;
        string newImageName;
        string newImagePath;

        newImagePath = $"{SiteConstant.UploadPath}/{imageType}";

        if (!Directory.Exists(newImagePath))
        {
            Directory.CreateDirectory(newImagePath);
        }

        using Image image = Image.Load(imagePath);
        Image clone;

        // Only resize image if original image width and height more than new width and height
        if (image.Width >= size.Width || image.Height >= size.Height)
        {
            var newWidth = size.Width;
            var newHeight = size.Height;
            var mode = ResizeMode.Crop;

            if (reference == "all")
            {
                var ratioX = (double)size.Width / image.Width;
                var ratioY = (double)size.Height / image.Height;
                var ratio = Math.Max(ratioX, ratioY);

                newWidth = (int)(image.Width * ratio);
                newHeight = (int)(image.Height * ratio);
            }
            else if (reference == "all-pad")
            {
                newWidth = size.Width;
                newHeight = size.Height;
                mode = ResizeMode.Pad;
            }
            else if (reference == "height")
            {
                newWidth = 0;
                newHeight = size.Height;
            }
            else
            {
                newWidth = size.Width;
                newHeight = 0;
            }

            clone = image.Clone(
                i => i.Resize(new ResizeOptions
                {
                    Size = new Size(newWidth, newHeight),
                    Mode = mode
                }).BackgroundColor(Color.Transparent)
                );
        }
        else
        {
            clone = image;
        }

        if (sizeName == "original")
        {
            newImageName = $"{guid}{extension}";
        }
        else
        {
            newImageName = $"{guid}-{sizeName}{extension}";
        }

        tempImagePath = Path.Combine(Directory.GetCurrentDirectory(), SiteConstant.TempUploadPath, newImageName);

        // Get image encoder based on image extension
        IImageEncoder encoder = getImageEncoder(extension);

        filePath = $"uploads/{imageType}/{newImageName}";

        // Save image to temporary path and then upload image to bucket
        clone.Save(tempImagePath, encoder);

        // // Upload image data to bucket
        // uploadBucket(tempImagePath, filePath, getMimeType(tempImagePath)).Wait();

        return $"{SiteConstant.UriPath}/{imageType}/{newImageName}";
    }

    private IImageEncoder getImageEncoder(string extension)
    {
        IImageEncoder encoder;

        // Check the image extension and encode image
        if (extension == ".png")
        {
            encoder = new PngEncoder()
            {
                // BitDepth = PngBitDepth.Bit8,
                // ColorType = PngColorType.RgbWithAlpha,
                CompressionLevel = PngCompressionLevel.Level9,
                // Quantizer = new WuQuantizer()
            };
        }
        else
        {
            encoder = new JpegEncoder()
            {
                Quality = 80 //Use variable to set between 0-100 based on your requirements
            };
        }

        return encoder;
    }
}
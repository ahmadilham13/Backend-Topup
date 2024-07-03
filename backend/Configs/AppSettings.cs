namespace backend.Configs;

public class AppSettings
{
    public string ServiceName { get; set; }
    public string Secret { get; set; }
    public int RefreshTokenTTL { get; set; }
    public string MainUrl { get; set; }
    public string AssetUrl { get; set; }
    public bool UseSwagger { get; set; }
    public bool UseStrictCors { get; set; }
    public string MinioUrl { get; set; }
    public string MinioBucket { get; set; }
    public string MinioAccessKey { get; set; }
    public string MinioSecretKey { get; set; }
    public string InternalSecret { get; set; }
}
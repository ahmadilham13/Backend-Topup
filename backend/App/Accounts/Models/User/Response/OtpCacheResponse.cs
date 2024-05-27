namespace backend.Accounts.Models.User;

public class OtpCacheResponse
{
    public string IpAddress { get; set; }
    public string Username { get; set; }

    public int WrongCounter { get; set; }
}
namespace backend.Accounts.Models.User;

public class CaptchaResponse
{
    public bool success { get; set; }
    public string challege_ts { get; set; }
    public string hostname { get; set; }
}
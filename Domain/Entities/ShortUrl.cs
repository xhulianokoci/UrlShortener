namespace Domain.Entities;

public  class ShortUrl
{
    public int Id { get; set; }
    public string Link { get; set; } = string.Empty;
    public string ShortLink { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
}

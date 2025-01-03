using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public  class ShortUrl
{
    [Key]
    public int Id { get; set; }
    [Required]
    [Url]
    [MaxLength(2048)]
    public string Link { get; set; } = string.Empty;
    [Required]
    [MaxLength(2048)]
    public string ShortLink { get; set; } = string.Empty;
    [Required]
    public DateTime DateCreated { get; set; }
}

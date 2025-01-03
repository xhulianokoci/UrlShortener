using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class LongUrlRequestDTO
{
    [Required]
    [Url]
    public string LongUrl { get; set; } = string.Empty;
}

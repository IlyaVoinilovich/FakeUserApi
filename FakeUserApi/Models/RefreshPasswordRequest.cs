using System.ComponentModel.DataAnnotations;

namespace FakeUserApi.Models
{
    public class RefreshPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}

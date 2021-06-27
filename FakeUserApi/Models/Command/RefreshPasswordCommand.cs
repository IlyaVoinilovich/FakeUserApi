using System.ComponentModel.DataAnnotations;

namespace FakeUserApi.Models
{
    public class RefreshPasswordCommand
    {
        [Required]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FakeUserApi.Models
{
    public class AuthenticateCommand
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Pass  { get; set; }
    }
}

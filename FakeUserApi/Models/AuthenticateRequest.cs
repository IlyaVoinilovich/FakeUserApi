using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FakeUserApi.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Pass  { get; set; }
    }
}

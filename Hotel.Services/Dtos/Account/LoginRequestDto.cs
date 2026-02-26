using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Dtos.Account
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

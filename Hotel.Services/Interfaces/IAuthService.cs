using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IAuthService
    {

        public Task<string> GenerateTokenAsync(string userName, string password);
        public Task<bool> ValidateTokenAsync(string token);
        public Task RevokeTokenAsync(string token);
        
    }
}

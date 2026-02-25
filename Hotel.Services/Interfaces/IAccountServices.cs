using Hotel.Services.Dtos.Account;
using Hotel.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IAccountServices
    {
        Task<ResultT<UserResponseDto?>> LoginAsync(LoginRequestDto requestDto);
        Task<ResultT<UserResponseDto?>> RegisterAsync(RegisterRequestDto requestDto);
    }
}

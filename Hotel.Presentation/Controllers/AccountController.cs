using AutoMapper;
using Hotel.Presentation.Validations.Account;
using Hotel.Presentation.Validations.Rooms;
using Hotel.Presentation.ViewModels.Account;
using Hotel.Presentation.ViewModels.Response;
using Hotel.Services.Dtos.Account;
using Hotel.Services.Dtos.Rooms;
using Hotel.Services.Interfaces;
using Hotel.Services.ResultPattern;
using Hotel.Services.Rooms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController(IAccountServices _accountServices, IMapper _mapper) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ResponseViewModel> Login(LoginRequestViewModel loginRequest)
        {

            var validator = new LoginRequestViewModelValidator().Validate(loginRequest);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidUserData, "Invalid User Data From Request !!");
            var userDto = _mapper.Map<LoginRequestDto>(loginRequest);
            var result = await _accountServices.LoginAsync(userDto);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.UserNotFound, "User Is Not Found !!");
            var userViewModel = _mapper.Map<UserResponseViewModel>(result.Data);
            return new SuccessResponseViewModelT<UserResponseViewModel>(userViewModel);
        }

        [HttpPost("Register")]
        public async Task<ResponseViewModel> Register(RegisterRequestViewModel registerRequest)
        {
            var validator = new RegisterRequestViewModelValidator().Validate(registerRequest);
            if (!validator.IsValid) return new FailedResponseViewModel(ErrorType.InvalidUserData, "Invalid User Data From Request !!");
            var userDto = _mapper.Map<RegisterRequestDto>(registerRequest);
            var result = await _accountServices.RegisterAsync(userDto);
            if (!result.IsSuccess) return new FailedResponseViewModel(ErrorType.RegistrationFailed, "Registration Failed !!");
            var userViewModel = _mapper.Map<UserResponseViewModel>(result.Data);
            return new SuccessResponseViewModelT<UserResponseViewModel>(userViewModel);
        }
    }
}

using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Mapper.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserInfoDTO>();
        }
    }
}

using Hotel.Services.Dtos.User;
using Hotel.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IUserService 
    {
       Task<ResultT<string>> GetUserNameByUserId(Guid id);
    }
}

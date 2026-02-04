using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Services.Dtos.User;
using Hotel.Services.Interfaces;
using Hotel.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Services
{
    public class UserService(IGenericRepository<User> repository, IAsyncQueryExecutor _executor,IMapper _mapper) : IUserService
    {
        private readonly IGenericRepository<User> _repository = repository;

        public async Task<ResultT<string>> GetUserNameByUserId(Guid id)
        {
            var query = _repository.GetById(id);
            var map = query.ProjectTo<GetUserInfoDTO>(_mapper.ConfigurationProvider);
            var user = await _executor.FirstOrDefaultAsync(map);
            if (user is null) return ResultT<string>.Failure(new Error(ErrorCode.NotFound, "User not found !"));
            return ResultT<string>.Success(user.FullName);
        }
    }
}

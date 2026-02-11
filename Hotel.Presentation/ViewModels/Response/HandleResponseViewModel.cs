using AutoMapper;
using Hotel.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Hotel.Presentation.ViewModels.Response
//{
//    public class HandleResponseViewModel(IMapper mapper) : ResponseViewModel
//    {
//          protected ResponseViewModel HandleResult<TSourse, TDistination>(ResultT<TSourse> result)
//        {
//            if (!result.IsSuccess)
//            {
//                return new FailedResponseViewModel(
//                    result.Error,
//                    result.Message
//                );
//            }

//            var data = _mapper.Map<TDistination>(result.Data);
//            return new SuccessResponseViewModel<TDistination>(data);
//        }
//    }
//}

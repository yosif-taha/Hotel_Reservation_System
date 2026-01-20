using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Shared.ViewModels.Response
{
    public class SuccessResponseViewModel<T> : ResponseViewModel
    {
        public T Data { get; set; }

        public SuccessResponseViewModel(T data) 
        {
            Data = data;
            IsSuccess = true;
        }
    }
}

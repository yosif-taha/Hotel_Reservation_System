using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Shared.ViewModels.Response
{
    public class FailedResponseViewModel : ResponseViewModel
    {
        public string Message { get; set; }
        public ErrorType ErrorType { get; set; }

        public FailedResponseViewModel(ErrorType errorType, string message) 
        {
            Message = message;
            IsSuccess = false;
            ErrorType = errorType;
        }
    }
}

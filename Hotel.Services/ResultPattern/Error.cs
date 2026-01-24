using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.ResultPattern
{
    public sealed record Error(ErrorCode Code, string Message)
    {
        public static readonly Error None =
            new(ErrorCode.NoError, string.Empty);
    }
}

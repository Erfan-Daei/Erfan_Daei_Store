using Practice_Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Store.Application.Services.Users.Commands.RefreshToken
{
    public interface IRefreshToken
    {
        public ResultDto<(string, string)> Execute(string RefreshToken);
    }
}

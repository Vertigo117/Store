using MediatR;
using Store.Core.Models;

namespace Store.Core.Queries
{
    public class LoginQuery : IRequest<AuthenticateResponse>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

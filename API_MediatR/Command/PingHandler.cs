using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace API_MediatR.Command
{
    public class PingHandler: IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }
}